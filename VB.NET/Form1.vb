Public Class Form1

    Private ledwiz As LEDWiz

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ledwiz = New LEDWiz(Me)
        ledwiz.Initialize()

        ledwiz.SetLEDStateAll(True)
        ledwiz.SetLEDIntensityAll(49)
    End Sub

    Private Sub Form1_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        ledwiz.Dispose()
    End Sub
End Class
