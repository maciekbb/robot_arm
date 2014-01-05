// Arm2Device.h

#pragma once

#include "usb.h"

using namespace System;

namespace Arm2 
{

	public ref class Arm2Device
	{
	private:
		struct usb_dev_handle* handle;
	public:
		Arm2Device();
		bool Open();
		void Close();

		//temp
		void LedOn();
		void LedOff();
	};
}
