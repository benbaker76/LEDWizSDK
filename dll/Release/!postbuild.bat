@ECHO OFF
CD %~dp0
copy ..\LEDWiz.h "..\..\dlls\"
copy LEDWiz32.dll "..\..\dlls\"
copy LEDWiz32.dll "..\..\C#\bin\Debug\"
copy LEDWiz32.dll "..\..\VB.NET\bin\Debug\"
copy LEDWiz32.dll "..\..\VB6\"
copy LEDWiz32.dll "..\..\Delphi\"
copy LEDWiz32.dll "..\..\C++\Release"
copy LEDWiz32.lib "..\..\C++\"
copy LEDWiz32.lib "..\..\dlls\"
copy LEDWiz64.dll "..\..\dlls\"
copy LEDWiz64.dll "..\..\C#\bin\Debug\"
copy LEDWiz64.dll "..\..\VB.NET\bin\Debug\"
copy LEDWiz64.dll "..\..\VB6\"
copy LEDWiz64.dll "..\..\Delphi\"
copy LEDWiz64.dll "..\..\C++\Release"
copy LEDWiz64.lib "..\..\C++\"
copy LEDWiz64.lib "..\..\dlls\"
copy ..\LEDWiz.h "..\..\C++"
