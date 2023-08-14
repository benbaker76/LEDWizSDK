// LEDWiz.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "windows.h"
#include "LEDWiz.h"

int main(int argc, char* argv[])
{
    BYTE brightness[32] = { 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49, 49 };
	INT deviceCount = 0;
    
    deviceCount = LWZ_Initialize();

    LWZ_PBA(0, brightness);
    LWZ_SBA(0, 0xFF, 0xFF, 0xFF, 0xFF, 1, 0, 0);

	LWZ_Shutdown();

	return 0;
}
