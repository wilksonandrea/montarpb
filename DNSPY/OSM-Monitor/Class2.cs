using System;
using System.Diagnostics;

// Token: 0x02000009 RID: 9
internal class Class2
{
	// Token: 0x0600002E RID: 46 RVA: 0x00003C44 File Offset: 0x00001E44
	public Process method_0(string string_0, string string_1, Class3 class3_0)
	{
		Class3 @class = class3_0 ?? Class3.Class3_0;
		return new Process
		{
			StartInfo = new ProcessStartInfo
			{
				FileName = string_0,
				Arguments = string_1,
				CreateNoWindow = @class.Boolean_1,
				WindowStyle = @class.ProcessWindowStyle_0,
				RedirectStandardOutput = @class.Boolean_2,
				UseShellExecute = @class.Boolean_0,
				WorkingDirectory = @class.String_0
			}
		};
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000021BB File Offset: 0x000003BB
	public Process method_1(string string_0, string string_1)
	{
		return this.method_0(string_0, string_1, Class3.Class3_0);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00003CB8 File Offset: 0x00001EB8
	public Class4 method_2(string string_0, string string_1, Class3 class3_0, bool bool_0)
	{
		Process process = this.method_0(string_0, string_1, class3_0);
		Class4 @class = new Class4
		{
			String_0 = string_0,
			String_1 = string_1
		};
		try
		{
			if (bool_0)
			{
				process.Start();
				@class.String_3 = process.StandardOutput.ReadToEnd();
				process.WaitForExit();
				@class.Enum1_0 = Enum1.Ok;
				@class.Int32_0 = process.ExitCode;
				if (!process.HasExited)
				{
					process.Kill();
				}
			}
			else
			{
				process.Start();
				@class.String_3 = process.StandardOutput.ReadToEnd();
			}
		}
		catch (Exception ex)
		{
			@class.Int32_0 = int.MaxValue;
			@class.Enum1_0 = Enum1.ExceptionBeforeRun;
			@class.String_2 = ex.ToString();
		}
		return @class;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x000021CA File Offset: 0x000003CA
	public Class4 method_3(string string_0, string string_1, bool bool_0)
	{
		return this.method_2(string_0, string_1, Class3.Class3_0, bool_0);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002133 File Offset: 0x00000333
	public Class2()
	{
	}
}
