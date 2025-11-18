using System;
using System.Diagnostics;

internal class Class2
{
    public Process method_0(string string_0, string string_1, Class3 class3_0)
    {
        Class3 class2 = class3_0 ?? Class3.Class3_0;
        ProcessStartInfo info1 = new ProcessStartInfo();
        info1.FileName = string_0;
        info1.Arguments = string_1;
        info1.CreateNoWindow = class2.Boolean_1;
        info1.WindowStyle = class2.ProcessWindowStyle_0;
        info1.RedirectStandardOutput = class2.Boolean_2;
        info1.UseShellExecute = class2.Boolean_0;
        info1.WorkingDirectory = class2.String_0;
        Process process1 = new Process();
        process1.StartInfo = info1;
        return process1;
    }

    public Process method_1(string string_0, string string_1) => 
        this.method_0(string_0, string_1, Class3.Class3_0);

    public Class4 method_2(string string_0, string string_1, Class3 class3_0, bool bool_0)
    {
        Process process = this.method_0(string_0, string_1, class3_0);
        Class4 class1 = new Class4();
        class1.String_0 = string_0;
        class1.String_1 = string_1;
        Class4 class2 = class1;
        try
        {
            if (!bool_0)
            {
                process.Start();
                class2.String_3 = process.StandardOutput.ReadToEnd();
            }
            else
            {
                process.Start();
                class2.String_3 = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                class2.Enum1_0 = Enum1.Ok;
                class2.Int32_0 = process.ExitCode;
                if (!process.HasExited)
                {
                    process.Kill();
                }
            }
        }
        catch (Exception exception1)
        {
            class2.Int32_0 = 0x7fffffff;
            class2.Enum1_0 = Enum1.ExceptionBeforeRun;
            class2.String_2 = exception1.ToString();
        }
        return class2;
    }

    public Class4 method_3(string string_0, string string_1, bool bool_0) => 
        this.method_2(string_0, string_1, Class3.Class3_0, bool_0);
}

