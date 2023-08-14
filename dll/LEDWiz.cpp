// Copyright (c) 2015, Ben Baker
// All rights reserved.

#include "stdafx.h"
#include "LEDWiz.h"
extern "C" {
#include "Include\hidsdi.h"
#include "Include\hid.h"
}
#include <uuids.h>
#include <tchar.h>
#include <stdio.h>
#include <wtypes.h>

HINSTANCE m_hInstance = NULL;
HANDLE m_hThread = NULL;
HWND m_hWnd = NULL;
HANDLE m_hStopEvent = NULL;
CRITICAL_SECTION m_crSection;
HID_DEVICE_DATA m_hidDeviceData[LWZ_MAX_DEVICES] = { NULL };
INT m_deviceCount = 0;
USBDEVICE_ATTACHED_CALLBACK m_usbDeviceAttachedCallback = NULL;
USBDEVICE_REMOVED_CALLBACK m_usbDeviceRemovedCallback = NULL;

BOOL APIENTRY DllMain( HANDLE hModule, 
					   DWORD  fdwReason, 
					   LPVOID lpReserved
					 )
{
	DWORD dwThreadId = NULL, dwThrdParam = 1;

	switch (fdwReason)
	{
		case DLL_PROCESS_ATTACH:
			m_hInstance = (HINSTANCE) hModule;
			DisableThreadLibraryCalls(m_hInstance);

			InitializeCriticalSection(&m_crSection);
			m_hStopEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

			m_hThread = CreateThread(NULL, 0, EventWindowThread, &dwThrdParam, 0, &dwThreadId);
			break;
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
			break;
		case DLL_PROCESS_DETACH:
			SendMessage(m_hWnd, WM_CLOSE, 0, 0);
			break;
	}

	return TRUE;
}

LEDWIZ_API VOID __stdcall LWZ_SetCallbacks(USBDEVICE_ATTACHED_CALLBACK usbDeviceAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbDeviceRemovedCallback)
{
	m_usbDeviceAttachedCallback = usbDeviceAttachedCallback;
	m_usbDeviceRemovedCallback = usbDeviceRemovedCallback;
}

// Sort the LEDWiz by Product Id

void SortDevices()
{
	for(INT i = 0; i < m_deviceCount; i++)
	{
		for(INT j = 0; j < i; j++)
		{
			if(m_hidDeviceData[i].ProductID < m_hidDeviceData[j].ProductID)
			{
				HID_DEVICE_DATA temp = m_hidDeviceData[i];
				m_hidDeviceData[i] = m_hidDeviceData[j];
				m_hidDeviceData[j] = temp;
			}
		}
	}
}

void OutputDevice(INT id, PHID_DEVICE_DATA pHidDeviceData)
{
	DEBUGLOG(L"Id: %d", id);
	DEBUGLOG(L"HID Handle: %08x", pHidDeviceData->hDevice);
	DEBUGLOG(L"VendorID: %04x", pHidDeviceData->VendorID);
	DEBUGLOG(L"ProductID: %04x", pHidDeviceData->ProductID);
	DEBUGLOG(L"VersionNumber: %04x", pHidDeviceData->VersionNumber);
	DEBUGLOG(L"VendorName: %s", pHidDeviceData->VendorName);
	DEBUGLOG(L"ProductName: %s", pHidDeviceData->ProductName);
	DEBUGLOG(L"SerialNumber: %s", pHidDeviceData->SerialNumber);
	DEBUGLOG(L"DevicePath: %s", pHidDeviceData->DevicePath);
	DEBUGLOG(L"InputReportByteLength: %d", pHidDeviceData->InputReportLen);
	DEBUGLOG(L"OutputReportByteLength: %d", pHidDeviceData->OutputReportLen);
	DEBUGLOG(L"UsagePage: %d", pHidDeviceData->UsagePage);
	DEBUGLOG(L"Usage: %d", pHidDeviceData->Usage);
}

void OutputDevices()
{
	DEBUGLOG(L"===============================");
	DEBUGLOG(L"DEVICE LIST");

	for(INT i = 0; i < m_deviceCount; i++)
	{
		DEBUGLOG(L"===============================");

		OutputDevice(i, &m_hidDeviceData[i]);
	}

	DEBUGLOG(L"===============================");
}

// int LWZ_Initialize()
// Initialize all LEDWiz Devices
// Returns the number of LEDWiz's on the PC

LEDWIZ_API INT __stdcall LWZ_Initialize()
{
	struct _GUID hidGuid;
	SP_DEVICE_INTERFACE_DATA deviceInterfaceData;
	struct { DWORD cbSize; WCHAR DevicePath[256]; } FunctionClassDeviceData;
	INT success;
	DWORD hidDevice;
	HANDLE pnPHandle;
	ULONG bytesReturned;

	m_deviceCount = 0;

	HidD_GetHidGuid(&hidGuid);

	pnPHandle = SetupDiGetClassDevs(&hidGuid, 0, 0, 0x12);

	if ((INT) pnPHandle == -1)
		return 0;

	for (hidDevice = 0; hidDevice < 127; hidDevice++)
	{
		deviceInterfaceData.cbSize = sizeof(SP_DEVICE_INTERFACE_DATA);

		success = SetupDiEnumDeviceInterfaces(pnPHandle, 0, &hidGuid, hidDevice, &deviceInterfaceData);

		if (success == 1)
		{
			FunctionClassDeviceData.cbSize = sizeof(SP_DEVICE_INTERFACE_DETAIL_DATA);
			success = SetupDiGetDeviceInterfaceDetail(pnPHandle, &deviceInterfaceData, (PSP_DEVICE_INTERFACE_DETAIL_DATA) &FunctionClassDeviceData, sizeof(FunctionClassDeviceData), &bytesReturned, 0);

			if (success == 0)
				continue;

			if(UsbOpen(FunctionClassDeviceData.DevicePath, &m_hidDeviceData[m_deviceCount]))
				m_deviceCount++;
		}
	}

	SetupDiDestroyDeviceInfoList(pnPHandle);

	SortDevices();

#ifdef DEBUG_OUTPUT
	OutputDevices();
#endif

	DEBUGLOG(L"%d LEDWiz Devices Found\n", m_deviceCount);

	return m_deviceCount;
}

// void LWZ_Shutdown()
// Shutdown all LEDWiz Devices
// No return value

LEDWIZ_API VOID __stdcall LWZ_Shutdown()
{
	INT i;

	for(INT i = 0; i < m_deviceCount; i++)
	{
		DEBUGLOG(L"Closing HID Handle: %08x\n", m_hidDeviceData[i].hDevice);

		CloseHandle(m_hidDeviceData[i].hDevice);
	}
}

LEDWIZ_API BOOL __stdcall LWZ_SBA(INT id, BYTE bank0, BYTE bank1, BYTE bank2, BYTE bank3, BYTE globalPulseSpeed, BYTE unused0, BYTE unused1)
{
	if (id >= m_deviceCount)
		return FALSE;

	REPORT_BUF outputReport;
	BOOL retVal = FALSE;

	outputReport.ReportID = 0;

	outputReport.ReportBuffer[0] = 64;
	outputReport.ReportBuffer[1] = bank0;
	outputReport.ReportBuffer[2] = bank1;
	outputReport.ReportBuffer[3] = bank2;
	outputReport.ReportBuffer[4] = bank3;
	outputReport.ReportBuffer[5] = globalPulseSpeed;
	outputReport.ReportBuffer[6] = unused0;
	outputReport.ReportBuffer[7] = unused1;

	retVal = UsbWrite(&m_hidDeviceData[id], &outputReport);

	FlushFileBuffers(m_hidDeviceData[id].hDevice);

	return retVal;
}

LEDWIZ_API BOOL __stdcall LWZ_PBA(INT id, PBYTE brightness)
{
	if (id >= m_deviceCount)
		return FALSE;

	REPORT_BUF outputReport;
	BOOL retVal = FALSE;
	INT i = 0;
	
	outputReport.ReportID = 0;

	for (int chunks = 0; chunks < 4; chunks++)
	{
		for (int bytes = 0; bytes < 8; bytes++)
			outputReport.ReportBuffer[bytes] = brightness[i++];

		retVal = UsbWrite(&m_hidDeviceData[id], &outputReport);

		FlushFileBuffers(m_hidDeviceData[id].hDevice);

		Sleep(2);
	}

	return retVal;
}

// Returns the device type

LEDWIZ_API INT __stdcall LWZ_GetDeviceType(INT id)
{
	if (id >= m_deviceCount)
		return 0;

	return m_hidDeviceData[id].Type;
}

// Returns the Vendor Id of the device specified by id

LEDWIZ_API INT __stdcall LWZ_GetVendorId(INT id)
{
	if (id >= m_deviceCount)
		return 0;

	return m_hidDeviceData[id].VendorID;
}

// Returns the Product Id of the device specified by id

LEDWIZ_API INT __stdcall LWZ_GetProductId(INT id)
{
	if (id >= m_deviceCount)
		return 0;

	return m_hidDeviceData[id].ProductID;
}

// Returns the Version Number of the device specified by id

LEDWIZ_API INT __stdcall LWZ_GetVersionNumber(INT id)
{
	if (id >= m_deviceCount)
		return 0;

	return m_hidDeviceData[id].VersionNumber;
}

// Copies the Vendor Name string into sVendorName
// No Return Value

LEDWIZ_API VOID __stdcall LWZ_GetVendorName(INT id, PWCHAR sVendorName)
{
	if (id >= m_deviceCount)
		return;

	wcscpy(sVendorName, m_hidDeviceData[id].VendorName);
}

// Copies the Product Name string into sVendorName
// No Return Value

LEDWIZ_API VOID __stdcall LWZ_GetProductName(INT id, PWCHAR sProductName)
{
	if (id >= m_deviceCount)
		return;

	wcscpy(sProductName, m_hidDeviceData[id].ProductName);
}

// Copies the Serial Number string into sVendorName
// No Return Value

LEDWIZ_API VOID __stdcall LWZ_GetSerialNumber(INT id, PWCHAR sSerialNumber)
{
	if (id >= m_deviceCount)
		return;

	wcscpy(sSerialNumber, m_hidDeviceData[id].SerialNumber);
}

// Copies the Device Path string into sVendorName
// No Return Value

LEDWIZ_API VOID __stdcall LWZ_GetDevicePath(INT id, PWCHAR sDevicePath)
{
	if (id >= m_deviceCount)
		return;

	wcscpy(sDevicePath, m_hidDeviceData[id].DevicePath);
}

BOOL UsbOpen(LPCWSTR devicePath, PHID_DEVICE_DATA pDeviceData)
{
	HANDLE hidHandle;
	USHORT vendorID, productID, versionNumber;
	USHORT inputReportLen, outputReportLen;
	USHORT usagePage, usage;
	WCHAR tempPath[256];
	INT success;

	wcscpy_s(tempPath, devicePath);

	strlow(tempPath);

	hidHandle = CreateFile(tempPath, GENERIC_WRITE | GENERIC_READ, FILE_SHARE_WRITE | FILE_SHARE_READ, NULL, OPEN_EXISTING, FILE_FLAG_OVERLAPPED, NULL);

	// Open device as non-overlapped so we can get data
	//hidHandle = CreateFile(tempPath, GENERIC_WRITE | GENERIC_READ, FILE_SHARE_WRITE | FILE_SHARE_READ, NULL, OPEN_EXISTING, 0, NULL);

	if (hidHandle == INVALID_HANDLE_VALUE)
		return FALSE;

	if(GetDeviceInfo(hidHandle, vendorID, productID, versionNumber, usage, usagePage, inputReportLen, outputReportLen))
	{
		if(IS_GGG(vendorID) && IS_LWZ(productID))
		{
			pDeviceData->Type = DEVICETYPE_LEDWIZ;

			wcscpy_s(pDeviceData->DevicePath, tempPath);

			pDeviceData->hDevice = hidHandle;
			pDeviceData->VendorID = vendorID;
			pDeviceData->ProductID = productID;
			pDeviceData->VersionNumber = versionNumber;
			pDeviceData->InputReportLen = inputReportLen;
			pDeviceData->OutputReportLen = outputReportLen;
			pDeviceData->UsagePage = usagePage;
			pDeviceData->Usage = usage;

			HidD_GetManufacturerString(hidHandle, pDeviceData->VendorName, sizeof(pDeviceData->VendorName));
			HidD_GetProductString(hidHandle, pDeviceData->ProductName, sizeof(pDeviceData->ProductName));
			HidD_GetSerialNumberString(hidHandle, pDeviceData->SerialNumber, sizeof(pDeviceData->SerialNumber));

			//DEBUGLOG(L"Error: %x\n", GetLastError());
			
			//OutputDevice(pDeviceData);

			return TRUE;
		}
	}

	CloseHandle(hidHandle);

	return FALSE;
}

BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport)
{
	return UsbRead(pHidDeviceData, pInputReport, INFINITE);
}

BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport, DWORD timeOut)
{
	OVERLAPPED ol;
	DWORD cbRet = 0;
	BOOL bRet = FALSE;

	memset(&ol, 0, sizeof(ol));
	ol.hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

	EnterCriticalSection(&m_crSection);
	ResetEvent(ol.hEvent);

	bRet = ReadFile(pHidDeviceData->hDevice, pInputReport, pHidDeviceData->InputReportLen, &cbRet, &ol);
	LeaveCriticalSection(&m_crSection);

	if (!bRet)
	{
		if (GetLastError() == ERROR_IO_PENDING)
		{
			HANDLE handles[2] = { ol.hEvent, m_hStopEvent };
			DWORD waitRet = WaitForMultipleObjects(2, handles, FALSE, timeOut);

			if (waitRet == WAIT_OBJECT_0)
			{
				// Data came in
				bRet = GetOverlappedResult(pHidDeviceData->hDevice, &ol, &cbRet, TRUE);
			}
			else if (waitRet == (WAIT_OBJECT_0 + 1))
			{
				// Stop event was set
				ResetEvent(m_hStopEvent);

				bRet = FALSE;
			}
			else if (waitRet == WAIT_TIMEOUT)
			{
				bRet = FALSE;
			}
		}
	}

	CloseHandle(ol.hEvent);

	return bRet;
}

BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pOutputReport)
{
	return UsbWrite(pHidDeviceData, pOutputReport, INFINITE);
}

BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pOutputReport, DWORD timeOut)
{
	OVERLAPPED ol;
	DWORD cbRet = 0;
	BOOL bRet = FALSE;

	memset(&ol, 0, sizeof(ol));
	ol.hEvent = CreateEvent(NULL, TRUE, FALSE, NULL);

	EnterCriticalSection(&m_crSection);
	ResetEvent(ol.hEvent);

	bRet = WriteFile(pHidDeviceData->hDevice, pOutputReport, pHidDeviceData->OutputReportLen, &cbRet, &ol);
	LeaveCriticalSection(&m_crSection);

	if (!bRet)
	{
		if (GetLastError() == ERROR_IO_PENDING)
		{
			HANDLE handles[2] = { ol.hEvent, m_hStopEvent };
			DWORD waitRet = WaitForMultipleObjects(2, handles, FALSE, timeOut);

			if (waitRet == WAIT_OBJECT_0)
			{
				// Data came in
				bRet = GetOverlappedResult(pHidDeviceData->hDevice, &ol, &cbRet, TRUE);
			}
			else if (waitRet == (WAIT_OBJECT_0 + 1))
			{
				// Stop event was set
				ResetEvent(m_hStopEvent);

				bRet = FALSE;
			}
			else if (waitRet == WAIT_TIMEOUT)
			{
				bRet = FALSE;
			}
		}
	}

	CloseHandle(ol.hEvent);

	return bRet;
}

DWORD WINAPI EventWindowThread(LPVOID lpParam)
{
	INT exitcode = 1;
	WNDCLASS wc = { 0 };

	wc.lpszClassName 	= WINDOW_CLASS;
	wc.hInstance 		= m_hInstance;
	wc.lpfnWndProc		= WndProc;

	if (!RegisterClass(&wc))
		return 0;

	m_hWnd = CreateWindowEx(0, WINDOW_CLASS, WINDOW_NAME, WS_OVERLAPPEDWINDOW, 0, 0, 1, 1, NULL, NULL, m_hInstance, NULL);

	if (m_hWnd == NULL)
	{
		UnregisterClass(WINDOW_CLASS, m_hInstance);

		return 0;
	}

	RegisterDeviceInterface(m_hWnd);

	MSG msg;

	while(GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	UnregisterClass(WINDOW_CLASS, m_hInstance);

	return msg.wParam;
}

BOOL RegisterDeviceInterface(HWND hWnd)
{
	DEV_BROADCAST_DEVICEINTERFACE NotificationFilter;

	ZeroMemory(&NotificationFilter, sizeof(NotificationFilter));
	NotificationFilter.dbcc_size =
	sizeof(DEV_BROADCAST_DEVICEINTERFACE);
	NotificationFilter.dbcc_devicetype = DBT_DEVTYP_DEVICEINTERFACE;
	NotificationFilter.dbcc_classguid = GUID_DEVINTERFACE_HID;

	HDEVNOTIFY hDevNotify = RegisterDeviceNotification(m_hWnd, &NotificationFilter, DEVICE_NOTIFY_WINDOW_HANDLE);

	if(!hDevNotify)
		return FALSE;

	return TRUE;
}

LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	PDEV_BROADCAST_HDR pHdr;
	PDEV_BROADCAST_HANDLE pHandle;
	PDEV_BROADCAST_DEVICEINTERFACE pInterface;

	switch(message)
	{
	case WM_CLOSE:
		DestroyWindow(hWnd);
		break;
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	case WM_DEVICECHANGE:
		switch(wParam)
		{
			case DBT_DEVICEARRIVAL:
				pHdr = (PDEV_BROADCAST_HDR) lParam;

				switch (pHdr->dbch_devicetype)
				{
					case DBT_DEVTYP_DEVICEINTERFACE:
						pInterface = (PDEV_BROADCAST_DEVICEINTERFACE) lParam;

						if(UsbOpen(pInterface->dbcc_name, &m_hidDeviceData[m_deviceCount]))
						{
							m_deviceCount++;

							SortDevices();

#ifdef DEBUG_OUTPUT
							DEBUGLOG(L"DBT_DEVICEARRIVAL");
							OutputDevices();
#endif
							
							for(INT id = 0; id < m_deviceCount; id++)
							{
								if(_wcsicmp(m_hidDeviceData[id].DevicePath, pInterface->dbcc_name) != 0)
									continue;

								if(m_usbDeviceAttachedCallback != NULL)
									m_usbDeviceAttachedCallback(id);

								break;
							}
						}
						break;
				}
				break;
			case DBT_DEVICEREMOVECOMPLETE:
				pHdr = (PDEV_BROADCAST_HDR) lParam;

				switch (pHdr->dbch_devicetype)
				{
					case DBT_DEVTYP_HANDLE:
						pHandle = (PDEV_BROADCAST_HANDLE) pHdr;

						UnregisterDeviceNotification(pHandle->dbch_hdevnotify);
						break;
					case DBT_DEVTYP_DEVICEINTERFACE:
						pInterface = (PDEV_BROADCAST_DEVICEINTERFACE) lParam;

						for(INT id = 0; id < m_deviceCount; id++)
						{
							if(_wcsicmp(m_hidDeviceData[id].DevicePath, pInterface->dbcc_name) != 0)
								continue;

							for(INT i = id; i < m_deviceCount - 1; i++)
								m_hidDeviceData[i] = m_hidDeviceData[i + 1];

							m_deviceCount--;

#ifdef DEBUG_OUTPUT
							DEBUGLOG(L"DBT_DEVICEREMOVECOMPLETE");
							OutputDevices();
#endif

							if(m_usbDeviceRemovedCallback != NULL)
								m_usbDeviceRemovedCallback(id);

							break;
						}
						break;
				}
				break;
		}
		break;
	default:
		break;
	}

	return DefWindowProc(hWnd, message, wParam, lParam);
}

BOOL GetDeviceInfo(HANDLE hidHandle, USHORT& vendorID, USHORT& productID, USHORT& versionNumber, USHORT& usage, USHORT& usagePage, USHORT& inputReportLen, USHORT& outputReportLen)
{
	HIDD_ATTRIBUTES hidAttributes;

	if (!HidD_GetAttributes(hidHandle, &hidAttributes))
		return FALSE;

	vendorID = hidAttributes.VendorID;
	productID = hidAttributes.ProductID;
	versionNumber = hidAttributes.VersionNumber;

	//DEBUGLOG(L"VendorID: %04x\n", hidAttributes.VendorID);
	//DEBUGLOG(L"ProductID: %04x\n", hidAttributes.ProductID);
	//DEBUGLOG(L"VersionNumber: %04x\n", hidAttributes.VersionNumber);

	PHIDP_PREPARSED_DATA hidPreparsedData;

	if (!HidD_GetPreparsedData(hidHandle, &hidPreparsedData))
		return FALSE;

	HIDP_CAPS hidCaps;

	if(HidP_GetCaps(hidPreparsedData, &hidCaps) != HIDP_STATUS_SUCCESS)
		return FALSE;

	usage = hidCaps.Usage;
	usagePage = hidCaps.UsagePage;
	inputReportLen = hidCaps.InputReportByteLength;
	outputReportLen = hidCaps.OutputReportByteLength;

	/* DEBUGLOG(L"UsagePage: %d\n", hidCaps.UsagePage);
	DEBUGLOG(L"Usage: %d\n", hidCaps.Usage);
	DEBUGLOG(L"InputReportByteLength: %d\n", hidCaps.InputReportByteLength);
	DEBUGLOG(L"OutputReportByteLength: %d\n", hidCaps.OutputReportByteLength);
	DEBUGLOG(L"FeatureReportByteLength: %d\n", hidCaps.FeatureReportByteLength);
	DEBUGLOG(L"NumberLinkCollectionNodes: %d\n", hidCaps.NumberLinkCollectionNodes);
	DEBUGLOG(L"NumberInputButtonCaps: %d\n", hidCaps.NumberInputButtonCaps);
	DEBUGLOG(L"NumberInputValueCaps: %d\n", hidCaps.NumberInputValueCaps);
	DEBUGLOG(L"NumberOutputButtonCaps: %d\n", hidCaps.NumberOutputButtonCaps);
	DEBUGLOG(L"NumberOutputValueCaps: %d\n", hidCaps.NumberOutputValueCaps);
	DEBUGLOG(L"NumberFeatureButtonCaps: %d\n", hidCaps.NumberFeatureButtonCaps);
	DEBUGLOG(L"NumberFeatureValueCaps: %d\n", hidCaps.NumberFeatureValueCaps); */

	// For more info use FillDeviceInfo
	// https://xp-dev.com/sc/36636/44/%2Ftrunk%2FProjects%2Fusb-device-hid-transfer-project-at91sam7x-ek%2Fusb-device-hid-transfer-project%2FHIDTest%2Fpnp.c

	return TRUE;
}

void strlow(wchar_t *src)
{
	for (unsigned int i = 0; i < wcslen(src); i++)
		src[i] = towlower(src[i]);
}

void strlow(char *s)
{
	for (;*s != '\0';s++)
		*s = tolower(*s);
}
