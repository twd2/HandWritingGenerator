Public Class TiltCorrection

    Public Const physicsAcreage = 0.0144 '0.12inch * 0.12inch, @see: HandWritingGenerator.docx

    Public Shared Function [Do](img As Bitmap) As Bitmap
        Dim theta As Double
        Return [Do](img, theta)
    End Function

    Public Shared Function [Do](bmp As Bitmap, ByRef outTheta As Double) As Bitmap
        Dim gd = GrayData.FromBitmap(bmp)
        Dim T = ImageProcessor.FindBinarizationThreshold(gd)
        Dim newimg = internalDo1(bmp, outTheta, T)
        Dim outTheta2 As Double
        newimg = internalDo2(newimg, outTheta2, T)
        outTheta -= outTheta2
        Return newimg
    End Function

    Private Shared Function internalDo1(img As Bitmap, ByRef outTheta As Double, T As Double) As Bitmap
        '旋转矫正
        Dim bd = BinaryData.FromBitmap(img, T)

        Dim physicsAcreageModulus = 1 / (bd.xDPI * bd.yDPI)

        Dim Acc = 0.25
        Dim Acreage As Integer = (physicsAcreage / physicsAcreageModulus)

        Dim lstblk = ImageProcessor.FindBlocks(bd, True, Acreage * (1 + Acc))
        'lstblk.Sort()
        Dim lstsq = ImageProcessor.FindSquares(lstblk, Acreage, Acc)

        Dim sumTheta = 0.0
        Dim countTheta = 0

        For i = 0 To lstsq.Count - 1
            Dim sqA = lstsq(i)
            Dim minDis = Integer.MaxValue, minSq As Block = Nothing
            '找一个最近的方块
            For j = 0 To lstsq.Count - 1
                If i = j Then
                    Continue For
                End If
                Dim sqB = lstsq(j)
                Dim dx = sqA.Centre.X - sqB.Centre.X
                Dim dy = sqA.Centre.Y - sqB.Centre.Y
                Dim dis = dx * dx + dy * dy
                If dis < minDis Then
                    minDis = dis
                    minSq = sqB
                End If
            Next
            If minSq Is Nothing Then
                Throw New Exception("No such square")
            End If
            Dim deltaX = minSq.Centre.X - sqA.Centre.X
            Dim deltaY = minSq.Centre.Y - sqA.Centre.Y
            sumTheta += Math.Atan(deltaX / deltaY)
            countTheta += 1
        Next
        '测量多次取平均值减小相对误差
        outTheta = sumTheta / countTheta
        If outTheta < 0 Then '图像发生了顺时针旋转，需要逆时针转回去
            Return ImageProcessor.RotateCounterclockwise(img, -outTheta)
        Else '图像发生了逆时针旋转，需要顺时针转回去
            Return ImageProcessor.RotateClockwise(img, outTheta)
        End If

    End Function

    Private Shared Function internalDo2(img As Bitmap, ByRef outTheta As Double, T As Double) As Bitmap
        '进一步旋转矫正
        Dim leftBlock As New List(Of Block),
           rightBlock As New List(Of Block)
        Dim bd = BinaryData.FromBitmap(img, T)
        Dim physicsAcreageModulus = 1 / (bd.xDPI * bd.yDPI)

        Dim Acc = 0.25
        Dim Acreage As Integer = (physicsAcreage / physicsAcreageModulus)

        Dim blks = ImageProcessor.FindBlocks(bd, Acreage * (1 + Acc))
        Dim sqs = ImageProcessor.FindSquares(blks, Acreage, Acc)
        Dim halfWidth = img.Width / 2
        For Each sq In sqs
            If sq.Centre.X > halfWidth Then
                rightBlock.Add(sq)
            Else
                leftBlock.Add(sq)
            End If
        Next
        If leftBlock.Count <> rightBlock.Count Then
            Throw New Exception("leftBlock.Count <> rightBlock.Count")
        End If
        Dim sumTheta = 0.0
        For i = 0 To leftBlock.Count - 1
            Dim lblk = leftBlock(i),
                rblk = rightBlock(i)
            Dim deltaY = rblk.Centre.Y - lblk.Centre.Y
            Dim deltaX = rblk.Centre.X - lblk.Centre.X
            sumTheta += Math.Atan(deltaY / deltaX)
        Next
        outTheta = sumTheta / leftBlock.Count
        If outTheta > 0 Then '图像发生了顺时针旋转，需要逆时针转回去
            Return ImageProcessor.RotateCounterclockwise(img, outTheta)
        Else '图像发生了逆时针旋转，需要顺时针转回去
            Return ImageProcessor.RotateClockwise(img, -outTheta)
        End If
    End Function

End Class
