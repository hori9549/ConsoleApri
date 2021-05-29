Imports System
Imports System.Net.Mail

Class Sample2
    Shared Sub Main2()
        Using client As New SmtpClient("smtp.gmail.com", 587)
            ' MailMessageクラスを使って送信するメールを作成する
            Dim message As New MailMessage()

            ' 差出人アドレス
            message.From = New MailAddress("hori9549@gmail.com")

            ' 送信先に3つのメールアドレスを設定する
            message.To.Add(New MailAddress("hori9549@gmail.com"))
            'message.To.Add(New MailAddress("bob@example.net"))
            'message.To.Add(New MailAddress("charlie@example.net"))

            ' メールの優先度を設定する
            message.Priority = MailPriority.High

            ' メールの送信日時(Dateヘッダ)を設定する
            message.Headers("Date") = (New DateTime(2001, 2, 3, 4, 56, 7)).ToString("r")

            ' メールの件名
            message.Subject = "Hello!"

            ' メールの本文
            message.Body = "This is a test mail."

            ' 作成したメールを送信する
            client.Send(message)
        End Using
    End Sub
End Class