// Copyright (c) 2015, Ben Baker
// All rights reserved.

#ifdef LEDWIZ_EXPORTS
#define LEDWIZ_API __declspec(dllexport)
#else
#define LEDWIZ_API __declspec(dllimport)
#endif

#define LWZ_MAX_DEVICES				16
#define LWZ_VENDORID				0xFAFA
#define LWZ_PRODUCTID_LO			0xF0
#define LWZ_PRODUCTID_HI			(LWZ_PRODUCTID_LO + LWZ_MAX_DEVICES)

#define IS_GGG(vid)					(vid == LWZ_VENDORID)
#define IS_LWZ(pid)					(pid >= LWZ_PRODUCTID_LO && pid < LWZ_PRODUCTID_HI)

#define WINDOW_NAME					L"LEDWiz Event Window"
#define WINDOW_CLASS				L"LEDWiz Event Class"

static GUID GUID_DEVINTERFACE_HID =
{ 0x4D1E55B2L, 0xF16F, 0x11CF, { 0x88, 0xCB, 0x00, 0x11, 0x11, 0x00, 0x00, 0x30 } };

enum DeviceType
{
	DEVICETYPE_UNKNOWN,
	DEVICETYPE_LEDWIZ
};

typedef struct
{
	UCHAR ReportID;
	UCHAR ReportBuffer[8];
} REPORT_BUF, *PREPORT_BUF;

typedef struct HID_DEVICE_DATA
{
	INT Type;
	HANDLE hDevice;
	USHORT VendorID;
	USHORT ProductID;
	USHORT VersionNumber;
	WCHAR VendorName[256];
	WCHAR ProductName[256];
	WCHAR SerialNumber[256];
	WCHAR DevicePath[256];
	USHORT InputReportLen;
	USHORT OutputReportLen;
	USHORT UsagePage;
	USHORT Usage;
} *PHID_DEVICE_DATA;

typedef VOID (__stdcall *USBDEVICE_ATTACHED_CALLBACK)(INT id);
typedef VOID (__stdcall *USBDEVICE_REMOVED_CALLBACK)(INT id);

LEDWIZ_API VOID __stdcall LWZ_SetCallbacks(USBDEVICE_ATTACHED_CALLBACK usbAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbRemovedCallback);

LEDWIZ_API INT __stdcall LWZ_Initialize();
LEDWIZ_API VOID __stdcall LWZ_Shutdown();

LEDWIZ_API BOOL __stdcall LWZ_SBA(INT id, BYTE bank0, BYTE bank1, BYTE bank2, BYTE bank3, BYTE globalPulseSpeed, BYTE unused0, BYTE unused1);
LEDWIZ_API BOOL __stdcall LWZ_PBA(INT id, PBYTE brightness);

LEDWIZ_API INT __stdcall LWZ_GetDeviceType(INT id);
LEDWIZ_API INT __stdcall LWZ_GetVendorId(INT id);
LEDWIZ_API INT __stdcall LWZ_GetProductId(INT id);
LEDWIZ_API INT __stdcall LWZ_GetVersionNumber(INT id);
LEDWIZ_API VOID __stdcall LWZ_GetVendorName(INT id, PWCHAR sVendorName);
LEDWIZ_API VOID __stdcall LWZ_GetProductName(INT id, PWCHAR sProductName);
LEDWIZ_API VOID __stdcall LWZ_GetSerialNumber(INT id, PWCHAR sSerialNumber);
LEDWIZ_API VOID __stdcall LWZ_GetDevicePath(INT id, PWCHAR sDevicePath);

BOOL UsbOpen(LPCWSTR devicePath, HID_DEVICE_DATA *pDeviceData);
BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport);
BOOL UsbRead(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport, DWORD timeOut);
BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pOutputReport);
BOOL UsbWrite(PHID_DEVICE_DATA pHidDeviceData, PREPORT_BUF pInputReport, DWORD timeOut);

DWORD WINAPI EventWindowThread(LPVOID lpParam);
BOOL RegisterDeviceInterface(HWND hWnd);
LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);

BOOL GetDeviceInfo(HANDLE hDevice, USHORT& vendorID, USHORT& productID, USHORT& versionNumber, USHORT& usage, USHORT& usagePage, USHORT& inputReportLen, USHORT& outputReportLen);

void strlow(wchar_t *src);
void strlow(char *s);
