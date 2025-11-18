using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Class11
{
	private static ResourceManager resourceManager_0;

	private static CultureInfo cultureInfo_0;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager_0
	{
		get
		{
			if (resourceManager_0 == null)
			{
				ResourceManager resourceManager = new ResourceManager("Class11", typeof(Class11).Assembly);
				resourceManager_0 = resourceManager;
			}
			return resourceManager_0;
		}
	}

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo CultureInfo_0
	{
		get
		{
			return cultureInfo_0;
		}
		set
		{
			cultureInfo_0 = value;
		}
	}

	internal static Bitmap Bitmap_0
	{
		get
		{
			object @object = ResourceManager_0.GetObject("PBLOGO 256", cultureInfo_0);
			return (Bitmap)@object;
		}
	}

	internal static Bitmap Bitmap_1
	{
		get
		{
			object @object = ResourceManager_0.GetObject("Setting", cultureInfo_0);
			return (Bitmap)@object;
		}
	}

	internal Class11()
	{
	}
}
