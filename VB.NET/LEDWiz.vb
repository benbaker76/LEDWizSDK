' Copyright (c) 2015, Ben Baker
' All rights reserved.

Imports System.Text
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports System.Windows
Imports System.Collections
Imports System

Public Class LEDWiz
	Implements IDisposable
	Public Const MAX_DEVICES As Integer = 16
	Public Const MAX_LEDCOUNT As Integer = 32
	Public Const MAX_INTENSITY As Integer = 49

	Public Enum DeviceType
		Unknown
		LEDWiz
	End Enum

	Public Enum AutoPulseMode As Integer
		RampUp_RampDown = 129	' /\/\
		On_Off = 130			' _|-|_|-|
		On_RampDown = 131		' -\|-\
		RampUp_On = 132			' /-|/-
	End Enum

	' ================== 32-bit ====================

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_SetCallbacks", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_SetCallbacks32(ByVal usbDeviceAttachedCallback As USBDEVICE_ATTACHED_CALLBACK, ByVal usbDeviceRemovedCallback As USBDEVICE_REMOVED_CALLBACK)
	End Sub

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_Initialize", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_Initialize32() As Integer
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_Shutdown", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_Shutdown32()
	End Sub

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_SBA", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_SBA32(ByVal id As Integer, ByVal bank0 As Byte, ByVal bank1 As Byte, ByVal bank2 As Byte, ByVal bank3 As Byte, ByVal globalPulseSpeed As Byte, _
	 ByVal unused0 As Byte, ByVal unused1 As Byte) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_PBA", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_PBA32(ByVal id As Integer, ByVal brightness As Byte()) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetDeviceType", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetDeviceType32(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetVendorId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetVendorId32(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetProductId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetProductId32(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetVersionNumber", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetVersionNumber32(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetVendorName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetVendorName32(ByVal id As Integer, ByVal vendorName As StringBuilder)
	End Sub

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetProductName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetProductName32(ByVal id As Integer, ByVal productName As StringBuilder)
	End Sub

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetSerialNumber", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetSerialNumber32(ByVal id As Integer, ByVal serialNumber As StringBuilder)
	End Sub

	<DllImport("LEDWiz32.dll", EntryPoint:="LWZ_GetDevicePath", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetDevicePath32(ByVal id As Integer, ByVal devicePath As StringBuilder)
	End Sub

	' ================== 64-bit ====================

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_SetCallbacks", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_SetCallbacks64(ByVal usbDeviceAttachedCallback As USBDEVICE_ATTACHED_CALLBACK, ByVal usbDeviceRemovedCallback As USBDEVICE_REMOVED_CALLBACK)
	End Sub

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_Initialize", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_Initialize64() As Integer
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_Shutdown", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_Shutdown64()
	End Sub

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_SBA", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_SBA64(ByVal id As Integer, ByVal bank0 As Byte, ByVal bank1 As Byte, ByVal bank2 As Byte, ByVal bank3 As Byte, ByVal globalPulseSpeed As Byte, _
	 ByVal unused0 As Byte, ByVal unused1 As Byte) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_PBA", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_PBA64(ByVal id As Integer, ByVal brightness As Byte()) As <MarshalAs(UnmanagedType.Bool)> Boolean
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetDeviceType", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetDeviceType64(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetVendorId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetVendorId64(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetProductId", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetProductId64(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetVersionNumber", CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Function LWZ_GetVersionNumber64(ByVal id As Integer) As Integer
	End Function

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetVendorName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetVendorName64(ByVal id As Integer, ByVal vendorName As StringBuilder)
	End Sub

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetProductName", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetProductName64(ByVal id As Integer, ByVal productName As StringBuilder)
	End Sub

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetSerialNumber", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetSerialNumber64(ByVal id As Integer, ByVal serialNumber As StringBuilder)
	End Sub

	<DllImport("LEDWiz64.dll", EntryPoint:="LWZ_GetDevicePath", CharSet:=CharSet.Unicode, CallingConvention:=CallingConvention.StdCall)> _
	Private Shared Sub LWZ_GetDevicePath64(ByVal id As Integer, ByVal devicePath As StringBuilder)
	End Sub

	Private Delegate Sub USBDEVICE_ATTACHED_CALLBACK(ByVal id As Integer)
	Private Delegate Sub USBDEVICE_REMOVED_CALLBACK(ByVal id As Integer)

	Public Delegate Sub UsbDeviceAttachedDelegate(ByVal id As Integer)
	Public Delegate Sub UsbDeviceRemovedDelegate(ByVal id As Integer)

	Public Event OnUsbDeviceAttached As UsbDeviceAttachedDelegate
	Public Event OnUsbDeviceRemoved As UsbDeviceRemovedDelegate

	<MarshalAs(UnmanagedType.FunctionPtr)> _
	Private UsbDeviceAttachedCallbackPtr As USBDEVICE_ATTACHED_CALLBACK = Nothing

	<MarshalAs(UnmanagedType.FunctionPtr)> _
	Private UsbDeviceRemovedCallbackPtr As USBDEVICE_REMOVED_CALLBACK = Nothing

	Private m_ctrl As Control

	Private m_is64Bit As Boolean = False

	Private m_LEDState As Boolean()()
	Private m_LEDIntensity As Byte()()

	Private m_pulseSpeed As Integer = 1

	Private m_deviceCount As Integer = 0

	Private m_disposed As Boolean = False

	Public Sub New(ByVal ctrl As Control)
		m_ctrl = ctrl
		m_is64Bit = Is64Bit()

		UsbDeviceAttachedCallbackPtr = New USBDEVICE_ATTACHED_CALLBACK(AddressOf UsbDeviceAttachedCallback)
		UsbDeviceRemovedCallbackPtr = New USBDEVICE_REMOVED_CALLBACK(AddressOf UsbDeviceRemovedCallback)

		If m_is64Bit Then
			LWZ_SetCallbacks64(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr)
		Else
			LWZ_SetCallbacks32(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr)
		End If

		m_LEDState = New Boolean(MAX_DEVICES)() {}
		m_LEDIntensity = New Byte(MAX_DEVICES)() {}

		For i As Integer = 0 To MAX_DEVICES - 1
			m_LEDState(i) = New Boolean(MAX_LEDCOUNT) {}
			m_LEDIntensity(i) = New Byte(MAX_LEDCOUNT) {}

			For j As Integer = 0 To MAX_LEDCOUNT - 1
				m_LEDState(i)(j) = False
				m_LEDIntensity(i)(j) = 0
			Next
		Next
	End Sub

	Private Sub UsbDeviceAttachedCallback(ByVal id As Integer)
		m_deviceCount += 1

		RaiseEvent OnUsbDeviceAttached(id)
	End Sub

	Private Sub UsbDeviceRemovedCallback(ByVal id As Integer)
		m_deviceCount -= 1

		RaiseEvent OnUsbDeviceRemoved(id)
	End Sub

	Public Function Initialize() As Integer
		m_deviceCount = (If(m_is64Bit, LWZ_Initialize64(), LWZ_Initialize32()))

		Return m_deviceCount
	End Function

	Public Sub Shutdown()
		If m_is64Bit Then
			LWZ_Shutdown64()
		Else
			LWZ_Shutdown32()
		End If
	End Sub

	Private Function SBA(ByVal id As Integer, ByVal bank0 As Byte, ByVal bank1 As Byte, ByVal bank2 As Byte, ByVal bank3 As Byte, ByVal globalPulseSpeed As Byte) As Boolean
		If id >= m_deviceCount Then
			Return False
		End If

		If m_is64Bit Then
			Return LWZ_SBA64(id, bank0, bank1, bank2, bank3, globalPulseSpeed, 0, 0)
		Else
			Return LWZ_SBA32(id, bank0, bank1, bank2, bank3, globalPulseSpeed, 0, 0)
		End If
	End Function

	Private Function PBA(ByVal id As Integer, ByVal brightnessArray As Byte()) As Boolean
		If id >= m_deviceCount Then
			Return False
		End If

		If m_is64Bit Then
			Return LWZ_PBA64(id, brightnessArray)
		Else
			Return LWZ_PBA32(id, brightnessArray)
		End If
	End Function

	Public Sub SetSingleLEDState(ByVal id As Integer, ByVal port As Integer, ByVal state As Boolean)
		m_LEDState(id)(port) = state

		SetLEDState(id, m_LEDState(id))
	End Sub

	Public Sub SetSingleLEDIntensity(ByVal id As Integer, ByVal port As Integer, ByVal intensity As Byte)
		m_LEDIntensity(id)(port) = intensity

		SetLEDIntensity(id, m_LEDIntensity(id))
	End Sub

	Public Sub SetRGBLEDIntensity(ByVal id As Integer, ByVal portArray As Integer(), ByVal intensityArray As Byte())
		For i As Integer = 0 To portArray.Length - 1
			m_LEDIntensity(id)(portArray(i)) = intensityArray(i)
		Next

		SetLEDIntensity(id, m_LEDIntensity(id))
	End Sub

	Public Sub SetRGBLEDState(ByVal id As Integer, ByVal portArray As Integer(), ByVal stateArray As Boolean())
		For i As Integer = 0 To portArray.Length - 1
			m_LEDState(id)(portArray(i)) = stateArray(i)
		Next

		SetLEDState(id, m_LEDState(id))
	End Sub

	Public Sub SetLEDState(ByVal id As Integer, ByVal bank0 As Byte, ByVal bank1 As Byte, ByVal bank2 As Byte, ByVal bank3 As Byte)
		SetLEDState(id, bank0, bank1, bank2, bank3, m_pulseSpeed)
	End Sub

	Public Sub SetLEDState(ByVal id As Integer, ByVal bank0 As Byte, ByVal bank1 As Byte, ByVal bank2 As Byte, ByVal bank3 As Byte, ByVal pulseSpeed As Integer)
		m_pulseSpeed = pulseSpeed

		For i As Integer = 0 To 7
			m_LEDState(id)(i) = (((bank0 >> i) And 1) <> 0)
			m_LEDState(id)(i + 8) = (((bank1 >> i) And 1) <> 0)
			m_LEDState(id)(i + 16) = (((bank2 >> i) And 1) <> 0)
			m_LEDState(id)(i + 24) = (((bank3 >> i) And 1) <> 0)
		Next

		SBA(id, bank0, bank1, bank2, bank3, CType(pulseSpeed, Byte))
	End Sub

	Public Sub SetLEDState(ByVal id As Integer, ByVal stateArray As Boolean())
		Array.Copy(stateArray, m_LEDState(id), Math.Min(stateArray.Length, m_LEDState(id).Length))

		SetLEDState(id)
	End Sub

	Public Sub SetLEDState(ByVal id As Integer)
		Dim bank0 As Byte = 0, bank1 As Byte = 0, bank2 As Byte = 0, bank3 As Byte = 0

		For i As Integer = 0 To 7
			bank0 = bank0 Or If(m_LEDState(id)(i), (1 << i), 0)
			bank1 = bank1 Or If(m_LEDState(id)(i + 8), (1 << i), 0)
			bank2 = bank2 Or If(m_LEDState(id)(i + 16), (1 << i), 0)
			bank3 = bank3 Or If(m_LEDState(id)(i + 24), (1 << i), 0)
		Next

		SBA(id, bank0, bank1, bank2, bank3, CType(m_pulseSpeed, Byte))
	End Sub

	Public Sub SetLEDStateAll(ByVal state As Boolean)
		For i As Integer = 0 To m_deviceCount - 1
			For j As Integer = 0 To 31
				m_LEDState(i)(j) = state
			Next
			SetLEDState(i)
		Next
	End Sub

	Public Sub SetLEDIntensity(ByVal id As Integer, ByVal intensityArray As Byte())
		Array.Copy(intensityArray, m_LEDIntensity(id), Math.Min(intensityArray.Length, m_LEDIntensity(id).Length))

		SetLEDIntensity(id)
	End Sub

	Public Sub SetLEDIntensity(ByVal id As Integer)
		PBA(id, m_LEDIntensity(id))
	End Sub

	Public Sub SetLEDPulseAll(ByVal intensity As LEDWiz.AutoPulseMode)
		For i As Integer = 0 To m_deviceCount - 1
			SetLEDIntensityAll(i, CType(intensity, Byte))
		Next
	End Sub

	Public Sub SetLEDIntensityAll(ByVal intensity As Byte)
		For i As Integer = 0 To m_deviceCount - 1
			For j As Integer = 0 To 31
				m_LEDIntensity(i)(j) = intensity
			Next
			SetLEDIntensity(i)
		Next
	End Sub

	Public Sub SetLEDPulseAll(ByVal id As Integer, ByVal intensity As LEDWiz.AutoPulseMode)
		SetLEDIntensityAll(id, CType(intensity, Byte))
	End Sub

	Public Sub SetLEDIntensityAll(ByVal id As Integer, ByVal intensity As Byte)
		For i As Integer = 0 To m_deviceCount - 1
			For j As Integer = 0 To 31
				m_LEDIntensity(i)(j) = intensity
			Next
			SetLEDIntensity(i)
		Next
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
		' remove this from gc finalizer list
	End Sub

	Private Sub Dispose(ByVal disposing As Boolean)
		If Not Me.m_disposed Then
			' dispose once only
			If disposing Then
				' Dispose managed resources.
				' called from Dispose
			End If

			' Clean up unmanaged resources here.
			Shutdown()
		End If

		m_disposed = True
	End Sub

	Public ReadOnly Property DeviceCount() As Integer
		Get
			Return m_deviceCount
		End Get
	End Property

	Public Property LEDState() As Boolean()()
		Get
			Return m_LEDState
		End Get
		Set(ByVal value As Boolean()())
			m_LEDState = value
		End Set
	End Property

	Public Property LEDIntensity() As Byte()()
		Get
			Return m_LEDIntensity
		End Get
		Set(ByVal value As Byte()())
			m_LEDIntensity = value
		End Set
	End Property

	Public Property PulseSpeed() As Integer
		Get
			Return m_pulseSpeed
		End Get
		Set(ByVal value As Integer)
			m_pulseSpeed = value
		End Set
	End Property

	Public Function GetDeviceType(ByVal id As Integer) As DeviceType
		Return CType(If(m_is64Bit, LWZ_GetDeviceType64(id), LWZ_GetDeviceType32(id)), DeviceType)
	End Function

	Public Function GetVendorId(ByVal id As Integer) As Integer
		If id >= m_deviceCount Then
			Return 0
		End If

		Return If(m_is64Bit, LWZ_GetVendorId64(id), LWZ_GetVendorId32(id))
	End Function

	Public Function GetProductId(ByVal id As Integer) As Integer
		If id >= m_deviceCount Then
			Return 0
		End If

		Return If(m_is64Bit, LWZ_GetProductId64(id), LWZ_GetProductId32(id))
	End Function

	Public Function GetVersionNumber(ByVal id As Integer) As Integer
		If id >= m_deviceCount Then
			Return 0
		End If

		Return If(m_is64Bit, LWZ_GetVersionNumber64(id), LWZ_GetVersionNumber32(id))
	End Function

	Public Function GetVendorName(ByVal id As Integer) As String
		If id >= m_deviceCount Then
			Return [String].Empty
		End If

		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			LWZ_GetVendorName64(id, sb)
		Else
			LWZ_GetVendorName32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Function GetProductName(ByVal id As Integer) As String
		If id >= m_deviceCount Then
			Return [String].Empty
		End If

		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			LWZ_GetProductName64(id, sb)
		Else
			LWZ_GetProductName32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Function GetSerialNumber(ByVal id As Integer) As String
		If id >= m_deviceCount Then
			Return [String].Empty
		End If

		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			LWZ_GetSerialNumber64(id, sb)
		Else
			LWZ_GetSerialNumber32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Public Function GetDevicePath(ByVal id As Integer) As String
		If id >= m_deviceCount Then
			Return [String].Empty
		End If

		Dim sb As New StringBuilder(256)

		If m_is64Bit Then
			LWZ_GetDevicePath64(id, sb)
		Else
			LWZ_GetDevicePath32(id, sb)
		End If

		Return sb.ToString()
	End Function

	Private Function Is64Bit() As Boolean
		Return Marshal.SizeOf(GetType(IntPtr)) = 8
	End Function
End Class