using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
internal class Class11
{
    private static ResourceManager resourceManager_0;
    private static CultureInfo cultureInfo_0;

    internal Class11()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager_0
    {
        get
        {
            resourceManager_0 ??= new ResourceManager("Class11", typeof(Class11).Assembly);
            return resourceManager_0;
        }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo CultureInfo_0
    {
        get => 
            cultureInfo_0;
        set => 
            cultureInfo_0 = value;
    }

    internal static Bitmap Bitmap_0 =>
        (Bitmap) ResourceManager_0.GetObject("PBLOGO 256", cultureInfo_0);

    internal static Bitmap Bitmap_1 =>
        (Bitmap) ResourceManager_0.GetObject("Setting", cultureInfo_0);
}

