VERSION 5.00
Begin VB.Form Form1 
   Caption         =   "Form1"
   ClientHeight    =   3090
   ClientLeft      =   60
   ClientTop       =   450
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   ScaleHeight     =   3090
   ScaleWidth      =   4680
   StartUpPosition =   3  'Windows Default
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private brightness(0 To 31) As Byte
Private numDevices As Integer

Private Sub Form_Load()
    hWndForm = Me.hWnd

    numDevices = LWZ_Initialize
	
    Dim i As Integer
    For i = 0 To 31
        brightness(i) = 49
    Next
	
    For i = 0 To numDevices - 1
        Call LWZ_PBA(i, brightness(0))
        Call LWZ_SBA(i, 255, 255, 255, 255, 2, 0, 0)
    Next
End Sub

Private Sub Form_Unload(Cancel As Integer)
    Dim i As Integer
    For i = 0 To numDevices - 1
        Call LWZ_SBA(i, 0, 0, 0, 0, 2, 0, 0)
    Next
	LWZ_Shutdown
End Sub
