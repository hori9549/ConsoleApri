Imports System

Module ConsoleOutput

    Sub Main(args As String())
        Console.WriteLine("Hello World!")
        Dim s1 As String = "foo"
        Dim s2 As String = "bar"
        Dim s3 As String = "baz"

        Console.WriteLine("Compare({0}, {1}) : {2}", s1, s2, String.Compare(s1, s2))
        Console.WriteLine("Compare({0}, {1}) : {2}", s1, s3, String.Compare(s1, s3))
        Console.WriteLine("Compare({0}, {1}) : {2}", s2, s3, String.Compare(s2, s3))
        Console.WriteLine("Compare({0}, {1}) : {2}", s2, s2, String.Compare(s2, s2))

        Console.WriteLine("Equals({0}, {1}) : {2}", s1, s2, String.Equals(s1, s2))
        Console.WriteLine("Equals({0}, {1}) : {2}", s1, s3, String.Equals(s1, s3))
        Console.WriteLine("Equals({0}, {1}) : {2}", s2, s3, String.Equals(s2, s3))
        Console.WriteLine("Equals({0}, {1}) : {2}", s2, s2, String.Equals(s2, s2))

        'NULL条件演算子(?.)
        Dim numbers As Integer() = Nothing
        ' NULL条件演算子(?.)でメンバ、インデックス操作
        Console.WriteLine(" NULL条件演算子(?.)でメンバ、インデックス操作] のTEST")
        Console.WriteLine(numbers?.Length)  ' nothingなので空
        Console.WriteLine(numbers?(0))      ' nothingなので空

        Console.WriteLine("HasValue]のTEST ")
        Dim val1 As Integer? = Nothing
        Dim val2 As Integer? = 1
        If val1.HasValue Then
            Console.WriteLine(val1.Value)
        Else
            Console.WriteLine("val1はNothing(Null)です")
        End If
        If val2.HasValue Then
            Console.WriteLine(val2.Value)
        Else
            Console.WriteLine("val3はNothing(Null)です")


        End If

        '文字列補間を使ったコード
        Dim unitPrice = 700
        Dim count = 3
        Dim tax = Math.Floor(unitPrice * count * 8.0 / 108.0)

        ' 従来の記法:
        Dim msg1 = String.Format("単価{0:#,#}×{1}個＝{2:#,#}円 (内消費税{3:#,#}円)",
                         unitPrice, count, unitPrice * count, tax)
        ' Console.WriteLine("従来の記法: + msg1 + ")
        Console.WriteLine(msg1)

        ' VB 14の記法:
        Console.WriteLine($"単価{unitPrice:#,#}×{count}個" &
           $"＝{unitPrice * count:#,#}円 (内消費税{tax:#,#}円)")

        ' msg1／msg2とも、次の結果になる
        ' "単価700×3個＝2,100円 (内消費税155円)"

    End Sub
End Module