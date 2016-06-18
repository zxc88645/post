Imports System.Net
Imports System.IO

Public Class Form1

    Dim generator As New Random
    Dim randomValue As String
    Dim web As New WebClient()
    Dim d As Byte()

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click

        Timer1.Enabled = Not (Timer1.Enabled)

        If Timer1.Enabled = True Then
            Button3.Text = "停止"
        Else
            Button3.Text = "開始"
        End If

    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        randomValue = generator.Next(1, 255) & "." & generator.Next(1, 255) & "." & generator.Next(1, 255) & "." & generator.Next(1, 255)

        Dim web As New WebClient()
        web.Headers.Add("Content-Type", "application/x-www-form-urlencoded")
        web.Headers.Add("X-Forwarded-For", randomValue)
        web.Headers.Add("Client-IP", randomValue)

        Try
            web.UploadDataAsync(New Uri(TextBox1.Text), d)
            Label1.Text += 1
        Catch ex As Exception

        End Try

    End Sub
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        d = System.Text.Encoding.ASCII.GetBytes("")
    End Sub
    Private Sub TrackBar1_Scroll(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TrackBar1.Scroll

        Label4.Text = TrackBar1.Value
        Timer1.Interval = TrackBar1.Value

    End Sub

    Private Sub NotifyIcon1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles NotifyIcon1.DoubleClick

        Me.Visible = True
        Me.Show()

    End Sub
    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Resize

        If Me.WindowState = FormWindowState.Minimized Then
            NotifyIcon1.Visible = True
            Me.Visible = False
        End If
    End Sub

    Private Sub Button1_Click() Handles Button1.Click


        Dim url1 = New Uri("http://www.mi.com/tw/miwifi/")

        WebBrowser1.Url = url1

    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        TextBox2.Text = WebBrowser1.DocumentText

        Dim s = Split(TextBox2.Text, "<a class=""btn btn-dakeLight btn-small js-wifi-color"">loading...</a>")

        If s.Length = 2 Then
            'MsgBox("無貨")
            NotifyIcon1.BalloonTipText = "無貨"
            NotifyIcon1.ShowBalloonTip(1000)
        ElseIf s.Length = 1 Then
            'MsgBox("有貨")
            NotifyIcon1.BalloonTipText = "有貨"
            NotifyIcon1.ShowBalloonTip(1000)

        End If

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick

        Button1_Click()


    End Sub
End Class
