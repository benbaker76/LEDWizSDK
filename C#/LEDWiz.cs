// Copyright (c) 2015, Ben Baker
// All rights reserved.

using System;
using System.Collections;
using System.Windows;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;
using System.Text;

public class LEDWiz : IDisposable
{
	public const int MAX_DEVICES = 16;
	public const int MAX_LEDCOUNT = 32;
	public const int MAX_INTENSITY = 49;

	public enum DeviceType
	{
		Unknown,
		LEDWiz
	};

	public enum AutoPulseMode : int
	{
		RampUp_RampDown = 129,				// /\/\
		On_Off = 130,						// _|-|_|-|
		On_RampDown = 131,					// -\|-\
		RampUp_On = 132						// /-|/-
	}

	// ================== 32-bit ====================

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_SetCallbacks", CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_SetCallbacks32(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_Initialize", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_Initialize32();

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_Shutdown", CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_Shutdown32();

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_SBA", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private extern static bool LWZ_SBA32(int id, byte bank0, byte bank1, byte bank2, byte bank3, byte globalPulseSpeed, byte unused0, byte unused1);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_PBA", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private extern static bool LWZ_PBA32(int id, byte[] brightness);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetDeviceType", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetDeviceType32(int id);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetVendorId", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetVendorId32(int id);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetProductId", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetProductId32(int id);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetVersionNumber", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetVersionNumber32(int id);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetVendorName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetVendorName32(int id, StringBuilder vendorName);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetProductName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetProductName32(int id, StringBuilder productName);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetSerialNumber", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetSerialNumber32(int id, StringBuilder serialNumber);

	[DllImport("LEDWiz32.dll", EntryPoint = "LWZ_GetDevicePath", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetDevicePath32(int id, StringBuilder devicePath);

	// ================== 64-bit ====================

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_SetCallbacks", CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_SetCallbacks64(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_Initialize", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_Initialize64();

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_Shutdown", CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_Shutdown64();

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_SBA", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private extern static bool LWZ_SBA64(int id, byte bank0, byte bank1, byte bank2, byte bank3, byte globalPulseSpeed, byte unused0, byte unused1);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_PBA", CallingConvention = CallingConvention.StdCall)]
	[return: MarshalAs(UnmanagedType.Bool)]
	private extern static bool LWZ_PBA64(int id, byte[] brightness);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetDeviceType", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetDeviceType64(int id);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetVendorId", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetVendorId64(int id);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetProductId", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetProductId64(int id);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetVersionNumber", CallingConvention = CallingConvention.StdCall)]
	private static extern int LWZ_GetVersionNumber64(int id);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetVendorName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetVendorName64(int id, StringBuilder vendorName);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetProductName", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetProductName64(int id, StringBuilder productName);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetSerialNumber", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetSerialNumber64(int id, StringBuilder serialNumber);

	[DllImport("LEDWiz64.dll", EntryPoint = "LWZ_GetDevicePath", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.StdCall)]
	private static extern void LWZ_GetDevicePath64(int id, StringBuilder devicePath);

	private delegate void USBDEVICE_ATTACHED_CALLBACK(int id);
	private delegate void USBDEVICE_REMOVED_CALLBACK(int id);

	public delegate void UsbDeviceAttachedDelegate(int id);
	public delegate void UsbDeviceRemovedDelegate(int id);

	public event UsbDeviceAttachedDelegate OnUsbDeviceAttached = null;
	public event UsbDeviceRemovedDelegate OnUsbDeviceRemoved = null;

	[MarshalAs(UnmanagedType.FunctionPtr)]
	USBDEVICE_ATTACHED_CALLBACK UsbDeviceAttachedCallbackPtr = null;

	[MarshalAs(UnmanagedType.FunctionPtr)]
	USBDEVICE_REMOVED_CALLBACK UsbDeviceRemovedCallbackPtr = null;

	private Control m_ctrl;

	private bool m_is64Bit = false;

	private bool[][] m_LEDState;
	private byte[][] m_LEDIntensity;

	private int m_pulseSpeed = 1;

	private int m_deviceCount = 0;

	private bool m_disposed = false;

	public LEDWiz(Control ctrl)
	{
		m_ctrl = ctrl;
		m_is64Bit = Is64Bit();

		UsbDeviceAttachedCallbackPtr = new USBDEVICE_ATTACHED_CALLBACK(UsbDeviceAttachedCallback);
		UsbDeviceRemovedCallbackPtr = new USBDEVICE_REMOVED_CALLBACK(UsbDeviceRemovedCallback);

		if (m_is64Bit)
			LWZ_SetCallbacks64(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr);
		else
			LWZ_SetCallbacks32(UsbDeviceAttachedCallbackPtr, UsbDeviceRemovedCallbackPtr);

		m_LEDState = new bool[MAX_DEVICES][];
		m_LEDIntensity = new byte[MAX_DEVICES][];

		for (int i = 0; i < MAX_DEVICES; i++)
		{
			m_LEDState[i] = new bool[MAX_LEDCOUNT];
			m_LEDIntensity[i] = new byte[MAX_LEDCOUNT];

			for (int j = 0; j < MAX_LEDCOUNT; j++)
			{
				m_LEDState[i][j] = false;
				m_LEDIntensity[i][j] = 0;
			}
		}
	}

	private void UsbDeviceAttachedCallback(int id)
	{
		m_deviceCount++;

		if (OnUsbDeviceAttached != null)
			m_ctrl.BeginInvoke(OnUsbDeviceAttached, id);
	}

	private void UsbDeviceRemovedCallback(int id)
	{
		m_deviceCount--;

		if (OnUsbDeviceRemoved != null)
			m_ctrl.BeginInvoke(OnUsbDeviceRemoved, id);
	}

	public int Initialize()
	{
		m_deviceCount = (m_is64Bit ? LWZ_Initialize64() : LWZ_Initialize32());

		return m_deviceCount;
	}

	public void Shutdown()
	{
		if (m_is64Bit)
			LWZ_Shutdown64();
		else
			LWZ_Shutdown32();
	}

	private bool SBA(int id, byte bank0, byte bank1, byte bank2, byte bank3, byte globalPulseSpeed)
	{
		if (id >= m_deviceCount)
			return false;

		if (m_is64Bit)
			return LWZ_SBA64(id, bank0, bank1, bank2, bank3, globalPulseSpeed, 0, 0);
		else
			return LWZ_SBA32(id, bank0, bank1, bank2, bank3, globalPulseSpeed, 0, 0);
	}

	private bool PBA(int id, byte[] brightnessArray)
	{
		if (id >= m_deviceCount)
			return false;

		if (m_is64Bit)
			return LWZ_PBA64(id, brightnessArray);
		else
			return LWZ_PBA32(id, brightnessArray);
	}

	public void SetSingleLEDState(int id, int port, bool state)
	{
		m_LEDState[id][port] = state;

		SetLEDState(id, m_LEDState[id]);
	}

	public void SetSingleLEDIntensity(int id, int port, byte intensity)
	{
		m_LEDIntensity[id][port] = intensity;

		SetLEDIntensity(id, m_LEDIntensity[id]);
	}

	public void SetRGBLEDIntensity(int id, int[] portArray, byte[] intensityArray)
	{
		for (int i = 0; i < portArray.Length; i++)
			m_LEDIntensity[id][portArray[i]] = intensityArray[i];

		SetLEDIntensity(id, m_LEDIntensity[id]);
	}

	public void SetRGBLEDState(int id, int[] portArray, bool[] stateArray)
	{
		for (int i = 0; i < portArray.Length; i++)
			m_LEDState[id][portArray[i]] = stateArray[i];

		SetLEDState(id, m_LEDState[id]);
	}

	public void SetLEDState(int id, byte bank0, byte bank1, byte bank2, byte bank3)
	{
		SetLEDState(id, bank0, bank1, bank2, bank3, m_pulseSpeed);
	}

	public void SetLEDState(int id, byte bank0, byte bank1, byte bank2, byte bank3, int pulseSpeed)
	{
		int i = 0;

		m_pulseSpeed = pulseSpeed;

		for (i = 0; i < 8; i++)
		{
			m_LEDState[id][i] = (((bank0 >> i) & 1) != 0);
			m_LEDState[id][i + 8] = (((bank1 >> i) & 1) != 0);
			m_LEDState[id][i + 16] = (((bank2 >> i) & 1) != 0);
			m_LEDState[id][i + 24] = (((bank3 >> i) & 1) != 0);
		}

		SBA(id, bank0, bank1, bank2, bank3, (byte)pulseSpeed);
	}

	public void SetLEDState(int id, bool[] stateArray)
	{
		Array.Copy(stateArray, m_LEDState[id], Math.Min(stateArray.Length, m_LEDState[id].Length));

		SetLEDState(id);
	}

	public void SetLEDState(int id)
	{
		byte bank0 = 0, bank1 = 0, bank2 = 0, bank3 = 0;

		for (int i = 0; i < 8; i++)
		{
			bank0 |= (byte)(m_LEDState[id][i] ? (1 << i) : 0);
			bank1 |= (byte)(m_LEDState[id][i + 8] ? (1 << i) : 0);
			bank2 |= (byte)(m_LEDState[id][i + 16] ? (1 << i) : 0);
			bank3 |= (byte)(m_LEDState[id][i + 24] ? (1 << i) : 0);
		}

		SBA(id, bank0, bank1, bank2, bank3, (byte)m_pulseSpeed);
	}

	public void SetLEDStateAll(bool state)
	{
		for (int i = 0; i < m_deviceCount; i++)
		{
			for (int j = 0; j < MAX_LEDCOUNT; j++)
				m_LEDState[i][j] = state;

			SetLEDState(i);
		}
	}

	public void SetLEDIntensity(int id, byte[] intensityArray)
	{
		Array.Copy(intensityArray, m_LEDIntensity[id], Math.Min(intensityArray.Length, m_LEDIntensity[id].Length));

		SetLEDIntensity(id);
	}

	public void SetLEDIntensity(int id)
	{
		PBA(id, m_LEDIntensity[id]);
	}

	public void SetLEDPulseAll(LEDWiz.AutoPulseMode intensity)
	{
		for (int i = 0; i < m_deviceCount; i++)
			SetLEDIntensityAll(i, (byte)intensity);
	}

	public void SetLEDIntensityAll(byte intensity)
	{
		for (int i = 0; i < m_deviceCount; i++)
			SetLEDIntensityAll(i, intensity);
	}

	public void SetLEDPulseAll(int id, LEDWiz.AutoPulseMode intensity)
	{
		SetLEDIntensityAll(id, (byte)intensity);
	}

	public void SetLEDIntensityAll(int id, byte intensity)
	{
		for (int j = 0; j < MAX_LEDCOUNT; j++)
			m_LEDIntensity[id][j] = intensity;

		SetLEDIntensity(id);
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this); // remove this from gc finalizer list
	}

	private void Dispose(bool disposing)
	{
		if (!this.m_disposed) // dispose once only
		{
			if (disposing) // called from Dispose
			{
				// Dispose managed resources.
			}

			// Clean up unmanaged resources here.
			Shutdown();
		}

		m_disposed = true;
	}

	public int DeviceCount
	{
		get { return m_deviceCount; }
	}

	public bool[][] LEDState
	{
		get { return m_LEDState; }
		set { m_LEDState = value; }
	}

	public byte[][] LEDIntensity
	{
		get { return m_LEDIntensity; }
		set { m_LEDIntensity = value; }
	}

	public int PulseSpeed
	{
		get { return m_pulseSpeed; }
		set { m_pulseSpeed = value; }
	}

	public DeviceType GetDeviceType(int id)
	{
		return (DeviceType)(m_is64Bit ? LWZ_GetDeviceType64(id) : LWZ_GetDeviceType32(id));
	}

	public int GetVendorId(int id)
	{
		if (id >= m_deviceCount)
			return 0;

		return (m_is64Bit ? LWZ_GetVendorId64(id) : LWZ_GetVendorId32(id));
	}

	public int GetProductId(int id)
	{
		if (id >= m_deviceCount)
			return 0;

		return (m_is64Bit ? LWZ_GetProductId64(id) : LWZ_GetProductId32(id));
	}

	public int GetVersionNumber(int id)
	{
		if (id >= m_deviceCount)
			return 0;

		return (m_is64Bit ? LWZ_GetVersionNumber64(id) : LWZ_GetVersionNumber32(id));
	}

	public string GetVendorName(int id)
	{
		if (id >= m_deviceCount)
			return String.Empty;

		StringBuilder sb = new StringBuilder(256);

		if (m_is64Bit)
			LWZ_GetVendorName64(id, sb);
		else
			LWZ_GetVendorName32(id, sb);

		return sb.ToString();
	}

	public string GetProductName(int id)
	{
		if (id >= m_deviceCount)
			return String.Empty;

		StringBuilder sb = new StringBuilder(256);

		if (m_is64Bit)
			LWZ_GetProductName64(id, sb);
		else
			LWZ_GetProductName32(id, sb);

		return sb.ToString();
	}

	public string GetSerialNumber(int id)
	{
		if (id >= m_deviceCount)
			return String.Empty;

		StringBuilder sb = new StringBuilder(256);

		if (m_is64Bit)
			LWZ_GetSerialNumber64(id, sb);
		else
			LWZ_GetSerialNumber32(id, sb);

		return sb.ToString();
	}

	public string GetDevicePath(int id)
	{
		if (id >= m_deviceCount)
			return String.Empty;

		StringBuilder sb = new StringBuilder(256);

		if (m_is64Bit)
			LWZ_GetDevicePath64(id, sb);
		else
			LWZ_GetDevicePath32(id, sb);

		return sb.ToString();
	}

	private bool Is64Bit()
	{
		return Marshal.SizeOf(typeof(IntPtr)) == 8;
	}
}

