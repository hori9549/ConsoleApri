Imports System

Module lisener
    Sub Main(args As String())
        'Listen����IP�A�h���X
        Dim ipString As String = "192.168.0.24"
        Dim ipAdd As System.Net.IPAddress = System.Net.IPAddress.Parse(ipString)

        '�z�X�g������IP�A�h���X���擾���鎞�́A���̂悤�ɂ���
        'Dim host As String = "localhost"
        'Dim ipAdd As System.Net.IPAddress = _
        '    System.Net.Dns.GetHostEntry(host).AddressList(0)
        '.NET Framework 1.1�ȑO�ł́A�ȉ��̂悤�ɂ���
        'Dim ipAdd As System.Net.IPAddress = _
        '    System.Net.Dns.Resolve(host).AddressList(0)

        'Listen����|�[�g�ԍ�
        Dim port As Integer = 2001

        'TcpListener�I�u�W�F�N�g���쐬����
        Dim listener As New System.Net.Sockets.TcpListener(ipAdd, port)

        'Listen���J�n����
        listener.Start()
        Console.WriteLine("Listen���J�n���܂���({0}:{1})�B",
        DirectCast(listener.LocalEndpoint, System.Net.IPEndPoint).Address,
        DirectCast(listener.LocalEndpoint, System.Net.IPEndPoint).Port)

        '�ڑ��v������������󂯓����
        Dim client As System.Net.Sockets.TcpClient = listener.AcceptTcpClient()
        Console.WriteLine("�N���C�A���g({0}:{1})�Ɛڑ����܂����B",
        DirectCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint).Address,
        DirectCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint).Port)

        'NetworkStream���擾
        Dim ns As System.Net.Sockets.NetworkStream = client.GetStream()

        '�ǂݎ��A�������݂̃^�C���A�E�g��10�b�ɂ���
        '�f�t�H���g��Infinite�ŁA�^�C���A�E�g���Ȃ�
        '(.NET Framework 2.0�ȏオ�K�v)
        ns.ReadTimeout = 10000
        ns.WriteTimeout = 10000

        '�N���C�A���g���瑗��ꂽ�f�[�^����M����
        Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8
        Dim disconnected As Boolean = False
        Dim ms As New System.IO.MemoryStream()
        Dim resBytes As Byte() = New Byte(255) {}
        Dim resSize As Integer = 0
        Do
            '�f�[�^�̈ꕔ����M����
            resSize = ns.Read(resBytes, 0, resBytes.Length)
            'Read��0��Ԃ������̓N���C�A���g���ؒf�����Ɣ��f
            If resSize = 0 Then
                disconnected = True
                Console.WriteLine("�N���C�A���g���ؒf���܂����B")
                Exit Do
            End If
            '��M�����f�[�^��~�ς���
            ms.Write(resBytes, 0, resSize)
            '�܂��ǂݎ���f�[�^�����邩�A�f�[�^�̍Ōオ\n�łȂ����́A
            ' ��M�𑱂���
        Loop While ns.DataAvailable OrElse
        resBytes(resSize - 1) <> AscW(ControlChars.Lf)
        '��M�����f�[�^�𕶎���ɕϊ�
        Dim resMsg As String = enc.GetString(ms.GetBuffer(), 0, CInt(ms.Length))
        ms.Close()
        '������\n���폜
        resMsg = resMsg.TrimEnd(ControlChars.Lf)
        Console.WriteLine(resMsg)

        If Not disconnected Then
            '�N���C�A���g�Ƀf�[�^�𑗐M����
            '�N���C�A���g�ɑ��M���镶������쐬
            Dim sendMsg As String = resMsg.Length.ToString()
            '�������Byte�^�z��ɕϊ�
            Dim sendBytes As Byte() = enc.GetBytes(sendMsg & ControlChars.Lf)
            '�f�[�^�𑗐M����
            ns.Write(sendBytes, 0, sendBytes.Length)
            Console.WriteLine(sendMsg)
        End If

        '����
        ns.Close()
        client.Close()
        Console.WriteLine("�N���C�A���g�Ƃ̐ڑ�����܂����B")

        '���X�i�����
        listener.Stop()
        Console.WriteLine("Listener����܂����B")

        Console.ReadLine()
    End Sub
End Module
