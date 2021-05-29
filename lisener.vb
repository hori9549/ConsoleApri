Imports System

Module lisener
    Sub Main(args As String())
        'ListenするIPアドレス
        Dim ipString As String = "192.168.0.24"
        Dim ipAdd As System.Net.IPAddress = System.Net.IPAddress.Parse(ipString)

        'ホスト名からIPアドレスを取得する時は、次のようにする
        'Dim host As String = "localhost"
        'Dim ipAdd As System.Net.IPAddress = _
        '    System.Net.Dns.GetHostEntry(host).AddressList(0)
        '.NET Framework 1.1以前では、以下のようにする
        'Dim ipAdd As System.Net.IPAddress = _
        '    System.Net.Dns.Resolve(host).AddressList(0)

        'Listenするポート番号
        Dim port As Integer = 2001

        'TcpListenerオブジェクトを作成する
        Dim listener As New System.Net.Sockets.TcpListener(ipAdd, port)

        'Listenを開始する
        listener.Start()
        Console.WriteLine("Listenを開始しました({0}:{1})。",
        DirectCast(listener.LocalEndpoint, System.Net.IPEndPoint).Address,
        DirectCast(listener.LocalEndpoint, System.Net.IPEndPoint).Port)

        '接続要求があったら受け入れる
        Dim client As System.Net.Sockets.TcpClient = listener.AcceptTcpClient()
        Console.WriteLine("クライアント({0}:{1})と接続しました。",
        DirectCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint).Address,
        DirectCast(client.Client.RemoteEndPoint, System.Net.IPEndPoint).Port)

        'NetworkStreamを取得
        Dim ns As System.Net.Sockets.NetworkStream = client.GetStream()

        '読み取り、書き込みのタイムアウトを10秒にする
        'デフォルトはInfiniteで、タイムアウトしない
        '(.NET Framework 2.0以上が必要)
        ns.ReadTimeout = 10000
        ns.WriteTimeout = 10000

        'クライアントから送られたデータを受信する
        Dim enc As System.Text.Encoding = System.Text.Encoding.UTF8
        Dim disconnected As Boolean = False
        Dim ms As New System.IO.MemoryStream()
        Dim resBytes As Byte() = New Byte(255) {}
        Dim resSize As Integer = 0
        Do
            'データの一部を受信する
            resSize = ns.Read(resBytes, 0, resBytes.Length)
            'Readが0を返した時はクライアントが切断したと判断
            If resSize = 0 Then
                disconnected = True
                Console.WriteLine("クライアントが切断しました。")
                Exit Do
            End If
            '受信したデータを蓄積する
            ms.Write(resBytes, 0, resSize)
            'まだ読み取れるデータがあるか、データの最後が\nでない時は、
            ' 受信を続ける
        Loop While ns.DataAvailable OrElse
        resBytes(resSize - 1) <> AscW(ControlChars.Lf)
        '受信したデータを文字列に変換
        Dim resMsg As String = enc.GetString(ms.GetBuffer(), 0, CInt(ms.Length))
        ms.Close()
        '末尾の\nを削除
        resMsg = resMsg.TrimEnd(ControlChars.Lf)
        Console.WriteLine(resMsg)

        If Not disconnected Then
            'クライアントにデータを送信する
            'クライアントに送信する文字列を作成
            Dim sendMsg As String = resMsg.Length.ToString()
            '文字列をByte型配列に変換
            Dim sendBytes As Byte() = enc.GetBytes(sendMsg & ControlChars.Lf)
            'データを送信する
            ns.Write(sendBytes, 0, sendBytes.Length)
            Console.WriteLine(sendMsg)
        End If

        '閉じる
        ns.Close()
        client.Close()
        Console.WriteLine("クライアントとの接続を閉じました。")

        'リスナを閉じる
        listener.Stop()
        Console.WriteLine("Listenerを閉じました。")

        Console.ReadLine()
    End Sub
End Module
