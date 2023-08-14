unit SDIMAIN;

interface

uses Windows, Classes, Graphics, Forms, Controls, Menus,
  Dialogs, StdCtrls, Buttons, ExtCtrls, ComCtrls, ImgList, StdActns,
  ActnList, ToolWin, SysUtils;

const
  LWZ_MAX_DEVICES : Integer = 16;

type
  TSDIAppForm = class(TForm)
    procedure FormShow(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

 Function LWZ_Initialize() : Integer; stdcall; external 'LEDWiz32.dll';
Procedure LWZ_Shutdown() stdcall; external 'LEDWiz32.dll';
Procedure LWZ_SBA(device: LWZHANDLE; bank0, bank1, bank2, bank3: Byte; globalPulseSpeed: Byte, unused0: Byte, unused1: Byte) stdcall; external 'LEDWiz32.dll';
Procedure LWZ_PBA(device: LWZHANDLE; brightness: Pointer) stdcall; external 'LEDWiz32.dll';

var
  SDIAppForm: TSDIAppForm;
  hWndForm: HWND;
  numDevices: Integer;
  brightness: array[0..31] of byte;

implementation

{$R *.dfm}

procedure TSDIAppForm.FormClose(Sender: TObject; var Action: TCloseAction);
var
  I: Integer;
begin
  for I := 0 to deviceList.numdevices - 1 do
  begin
    LWZ_SBA(deviceList.handles[I], 0, 0, 0, 0, 2);
  end;
  LWZ_Shutdown();
end;

procedure TSDIAppForm.FormShow(Sender: TObject);
var
  I: Integer;
begin
  hWndForm := self.Handle;

  numDevices = LWZ_Initialize();

  for I := 0 to 31 do
  begin
     brightness[I] := 49;
  end;

  for I := 0 to deviceList.numdevices - 1 do
  begin
    LWZ_PBA(I, Addr(brightness[0]));
    LWZ_SBA(I, 255, 255, 255, 255, 2);
  end;
end;

end.

