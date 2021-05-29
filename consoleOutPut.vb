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

        'NULL�������Z�q(?.)
        Dim numbers As Integer() = Nothing
        ' NULL�������Z�q(?.)�Ń����o�A�C���f�b�N�X����
        Console.WriteLine(" NULL�������Z�q(?.)�Ń����o�A�C���f�b�N�X����] ��TEST")
        Console.WriteLine(numbers?.Length)  ' nothing�Ȃ̂ŋ�
        Console.WriteLine(numbers?(0))      ' nothing�Ȃ̂ŋ�

        Console.WriteLine("HasValue]��TEST ")
        Dim val1 As Integer? = Nothing
        Dim val2 As Integer? = 1
        If val1.HasValue Then
            Console.WriteLine(val1.Value)
        Else
            Console.WriteLine("val1��Nothing(Null)�ł�")
        End If
        If val2.HasValue Then
            Console.WriteLine(val2.Value)
        Else
            Console.WriteLine("val3��Nothing(Null)�ł�")


        End If

        '�������Ԃ��g�����R�[�h
        Dim unitPrice = 700
        Dim count = 3
        Dim tax = Math.Floor(unitPrice * count * 8.0 / 108.0)

        ' �]���̋L�@:
        Dim msg1 = String.Format("�P��{0:#,#}�~{1}��{2:#,#}�~ (�������{3:#,#}�~)",
                         unitPrice, count, unitPrice * count, tax)
        ' Console.WriteLine("�]���̋L�@: + msg1 + ")
        Console.WriteLine(msg1)

        ' VB 14�̋L�@:
        Console.WriteLine($"�P��{unitPrice:#,#}�~{count}��" &
           $"��{unitPrice * count:#,#}�~ (�������{tax:#,#}�~)")

        ' msg1�^msg2�Ƃ��A���̌��ʂɂȂ�
        ' "�P��700�~3��2,100�~ (�������155�~)"

    End Sub
End Module