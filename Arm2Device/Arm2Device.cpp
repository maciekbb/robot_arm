// This is the main DLL file.

#include "stdafx.h"

#include "Arm2Device.h"

static const int VID = 0x16C0;
static const int PID = 0x1A0A;

namespace Arm2
{
	Arm2Device::Arm2Device()
	{
		this->handle = NULL;
	}

	bool Arm2Device::Open()
	{
		struct usb_bus* bus;
		struct usb_device* dev;

		usb_init();
		usb_find_busses();
		usb_find_devices();

		for(bus = usb_get_busses(); bus; bus = bus->next)
		{
			for(dev = bus->devices; dev; dev = dev->next)
			{
				if(dev->descriptor.idVendor == VID && dev->descriptor.idProduct == PID)
				{
					this->handle = usb_open(dev);
					if(this->handle != NULL) return true;
				}
			}
		}
		return false;
	}

	void Arm2Device::Close()
	{
		if(this->handle != NULL)
		{
			usb_close(this->handle);
			this->handle = NULL;
		}
	}
}