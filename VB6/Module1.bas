Attribute VB_Name = "Module1"
Option Explicit

Public Declare Function LWZ_Initialize Lib "LEDWiz32.dll" () As Long
Public Declare Sub LWZ_Shutdown Lib "LEDWiz32.dll" ()
Public Declare Sub LWZ_SBA Lib "LEDWiz32.dll" (ByVal id As Long, ByVal bank0 As Long, ByVal bank1 As Long, ByVal bank2 As Long, ByVal bank3 As Long, globalPulseSpeed As Long, ByVal unused0 As Long, ByVal unused1 As Long)
Public Declare Sub LWZ_PBA Lib "LEDWiz32.dll" (ByVal id As Long, ByRef brightness As Byte)

Public hWndForm As Long

Public Const LWZ_MAX_DEVICES As Integer = 16
