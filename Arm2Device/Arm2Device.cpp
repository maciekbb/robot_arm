// This is the main DLL file.

#include "stdafx.h"
#include "Arm2Device.h"
#include "stdio.h"

static const int VID = 0x16C0;
static const int PID = 0x1A0A;
static const int SIG_MOVE = 2;

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

	bool Arm2Device::IsOpen()
	{
		return this->handle != NULL;
	}

	void Arm2Device::Close()
	{
		if(this->handle != NULL)
		{
			usb_close(this->handle);
			this->handle = NULL;
		}
	}

	bool Arm2Device::Ping()
	{
		char buffer[256];
		int bytes = usb_control_msg(this->handle,
			USB_TYPE_VENDOR | USB_RECIP_DEVICE | USB_ENDPOINT_IN,
			SIG_MOVE,
			0,
			0,
			buffer,
			sizeof(buffer),
			1000);

		if(bytes > 0)
			return true;
		else
			return false;
	}

	void Arm2Device::MoveServo(byte servoId, double position)
	{
		char buffer[256];
		char pos = Convert::ToByte(position * 200);
		int bytes = usb_control_msg(this->handle,
			USB_TYPE_VENDOR | USB_RECIP_DEVICE | USB_ENDPOINT_IN,
			SIG_MOVE,
			pos,
			servoId,
			buffer,
			sizeof(buffer),
			1000);

		if(bytes > 0)
		{
			printf("Message: %s \n", buffer);
		}
		else
		{
			printf("Device don't respond\n");
		}
	}
}