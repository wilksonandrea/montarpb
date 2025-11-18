using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

// Token: 0x02000028 RID: 40
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Class11
{
	// Token: 0x060000B7 RID: 183 RVA: 0x00002133 File Offset: 0x00000333
	internal Class11()
	{
	}

	// Token: 0x17000019 RID: 25
	// (get) Token: 0x060000B8 RID: 184 RVA: 0x000056D8 File Offset: 0x000038D8
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager_0
	{
		get
		{
			if (Class11.resourceManager_0 == null)
			{
				ResourceManager resourceManager = new ResourceManager("Class11", typeof(Class11).Assembly);
				Class11.resourceManager_0 = resourceManager;
			}
			return Class11.resourceManager_0;
		}
	}

	// Token: 0x1700001A RID: 26
	// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000247C File Offset: 0x0000067C
	// (set) Token: 0x060000BA RID: 186 RVA: 0x00002483 File Offset: 0x00000683
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

	// Token: 0x1700001B RID: 27
	// (get) Token: 0x060000BB RID: 187 RVA: 0x00005714 File Offset: 0x00003914
	internal static Bitmap Bitmap_0
	{
		get
		{
			object @object = Class11.ResourceManager_0.GetObject("PBLOGO 256", Class11.cultureInfo_0);
			return (Bitmap)@object;
		}
	}

	// Token: 0x1700001C RID: 28
	// (get) Token: 0x060000BC RID: 188 RVA: 0x0000573C File Offset: 0x0000393C
	internal static Bitmap Bitmap_1
	{
		get
		{
			object @object = Class11.ResourceManager_0.GetObject("Setting", Class11.cultureInfo_0);
			return (Bitmap)@object;
		}
	}

	// Token: 0x040000A8 RID: 168
	private static ResourceManager resourceManager_0;

	// Token: 0x040000A9 RID: 169
	private static CultureInfo cultureInfo_0;
}
