![](/images/GGG.gif)

# LEDWiz SDK

![](/images/LEDWiz.png)

## Description

The LEDWiz SDK is a collection of source code projects for using LEDWiz32.dll / LEDWiz64.dll.

It contains source code projects for the following languages:

- C#
- C++
- Delphi
- VB6
- VB.NET

## API

### Initialize LEDWiz

`INT LWZ_Initialize();`

Returns: The number of LEDWiz devices detected.

Call this to initialize the device.

### Shutdown LEDWiz

`VOID LWZ_Shutdown();`

Call this to shutdown the device.

### Set notification callbacks for attached/removed events

```
typedef VOID (__stdcall *USBDEVICE_ATTACHED_CALLBACK)(INT id);
typedef VOID (__stdcall *USBDEVICE_REMOVED_CALLBACK)(INT id);
```

`VOID LWZ_SetCallbacks(USBDEVICE_ATTACHED_CALLBACK usbAttachedCallback, USBDEVICE_REMOVED_CALLBACK usbRemovedCallback);`

- `id` is an identifier for a specific LED-WIZ device

Set a notification callback for attached/removed events. The function you provide will be called whenever a new LED-WIZ is plugged into a USB port or when an existing device is removed.

### All Outputs State plus Global Pulse Speed (Most Efficient)

`VOID LWZ_SBA(INT id, BYTE bank0, BYTE bank1, BYTE bank2, BYTE bank3, BYTE globalPulseSpeed, BYTE unused0, BYTE unused1);`

- `id` is an identifier for a specific LED-WIZ device
- `bank0`, `bank1`, `bank2`, and `bank3` equal 8-bit representations of on/off states for banks 1-4
- `globalPulseSpeed` equals Global Pulse Speed setting (1 through 7)

### Examples

`LWZ_SBA(id, 255, 255, 255, 255, 2, 0, 0);`

All Outputs ON, and Global Pulse Speed set to 2

`LWZ_SBA(id, 0, 0, 0, 0, 1, 0, 0);`

All Outputs OFF, and Global Pulse Speed set to 1

### All Outputs Profile Settings (Most Efficient):

`void LWZ_PBA(int id,  CHAR *brightness)`

- `id` is an identifier for a specific LED-WIZ device
- `brightness` is a char array of 32 values. array elements are validated to make sure they are legal values.

Each of the 32 parameters coincide with outputs 1-32. A value of 1 to 49 sets the brightness of each LED using PWM (49 is a solid signal). A value of 129-132 indicates an automated pulse mode as follows:

- `129` = Ramp Up / Ramp Down
- `130` = On / Off
- `131` = On / Ramp Down
- `132` = Ramp Up / On

The speed is controlled by the Global Pulse Speed parameter.

### Example

```
char brightness[32] = {49,49,49,129,49,49,49,49,49,49,49,49,49,20,49,49,49,49,49,49,49,49,49,49,49,49,49,49,49,49,49,49};

LWZ-PBA(id, brightness);
```

Output 4 set to a `sawtooth` pulse, Output 14 set to a `brightness` of 20 and the rest of the outputs are at full brightness settings.

**NOTE:** This command should not be used as an ON/OFF control. It is for setting up the behavior profile of each output. Use the more efficient `LWZ_SBA` command for switching inputs ON/OFF. This command should only be used when the profile of an input changes.

## Release Dates

- 2-4-2018 - 1.4 - Device re-plug fix
- 2-2-2018 - 1.3 - Added flush and delay
- 21-1-2018 - 1.2 - Re-wrote drivers from scratch
- 14-10-2016 - 1.1 - Added support for 64-bit, changed from cdecl to stdcall (for VB6 support), removed limit checks, added support for last two bytes in SBA call
- 16-3-2007 - 1.0 - First Release

## Credits

- LEDWiz Hardware [GroovyGameGear]
- LEDWiz32.dll / LEDWiz64.dll [Ben Baker]
- C# [Ben Baker]
- C++ [Ben Baker]
- Delphi [Ben Baker]
- VB6 [Ben Baker]
- VB.NET [Ben Baker]

## Thanks

MikeQ for his work on the original driver.

## Contacts

- Randy T (GroovyGameGear.com): randy@idvtinc.com
- Ben Baker (LEDWiz SDK Developer): headkaze@gmail.com
