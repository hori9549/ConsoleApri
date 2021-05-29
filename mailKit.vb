Imports System
Imports System.Net
Imports System.Net.Mail

Class Sample
    Shared Sub Main3()
        '' GMail SMTPサーバーを使用するSmtpClientを作成
        Using client As New SmtpClient("smtp.gmail.com", 587)
            ' SSL接続を有効にする
            client.EnableSsl = True

            ' SMTPサーバーの認証に使用するユーザー名(GMailアカウント名)とパスワードを指定
            client.Credentials = New NetworkCredential("hori9549@gmail.com", "g9459irohe")

            client.Send("hori9549@gmail.com",
                        "hori9549@gmail.com",
                        "Hello!",
                        "This is a test mail.")
        End Using
    End Sub
End Class