using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal class Class3
{
    public Class3()
    {
        this.bool_1 = true;
        this.processWindowStyle_0 = ProcessWindowStyle.Hidden;
        this.bool_2 = true;
    }

    public Class3(bool bool_3 = false, bool bool_4 = true, ProcessWindowStyle processWindowStyle_1 = 1, bool bool_5 = true) : this()
    {
        this.Boolean_0 = bool_3;
        this.Boolean_1 = bool_4;
        this.ProcessWindowStyle_0 = processWindowStyle_1;
        this.Boolean_2 = bool_5;
    }

    public static Class3 Class3_0 { get; }

    public bool Boolean_0 { get; set; }

    public bool Boolean_1 { get; set; }

    public ProcessWindowStyle ProcessWindowStyle_0 { get; set; }

    public bool Boolean_2 { get; set; }

    public string String_0 { get; set; }
}

