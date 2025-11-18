using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

[CompilerGenerated]
[DebuggerNonUserCode]
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
internal class Class11
{
	private static ResourceManager resourceManager_0;

	private static CultureInfo cultureInfo_0;

	internal static Bitmap Bitmap_0
	{
		get
		{
			return (Bitmap)Class11.ResourceManager_0.GetObject("PBLOGO 256", Class11.cultureInfo_0);
		}
	}

	internal static Bitmap Bitmap_1
	{
		get
		{
			return (Bitmap)Class11.ResourceManager_0.GetObject("Setting", Class11.cultureInfo_0);
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo CultureInfo_0
	{
		get
		{
			return Class11.cultureInfo_0;
		}
		set
		{
			Class11.cultureInfo_0 = value;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager_0
	{
		get
		{
			if (Class11.resourceManager_0 == null)
			{
				Class11.resourceManager_0 = new ResourceManager("Class11", typeof(Class11).Assembly);
			}
			return Class11.resourceManager_0;
		}
	}

	internal Class11()
	{
	}
}