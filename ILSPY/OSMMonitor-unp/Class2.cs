using System;
using System.Diagnostics;

internal class Class2
{
	public Process method_0(string string_0, string string_1, global::Class3 class3_0)
	{
		global::Class3 @class = class3_0 ?? global::Class3.Class3_0;
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

	public Process method_1(string string_0, string string_1)
	{
		return method_0(string_0, string_1, global::Class3.Class3_0);
	}

	public global::Class4 method_2(string string_0, string string_1, global::Class3 class3_0, bool bool_0)
	{
		Process process = method_0(string_0, string_1, class3_0);
		global::Class4 @class = new global::Class4
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
					return @class;
				}
				return @class;
			}
			process.Start();
			@class.String_3 = process.StandardOutput.ReadToEnd();
			return @class;
		}
		catch (Exception ex)
		{
			@class.Int32_0 = int.MaxValue;
			@class.Enum1_0 = Enum1.ExceptionBeforeRun;
			@class.String_2 = ex.ToString();
			return @class;
		}
	}

	public global::Class4 method_3(string string_0, string string_1, bool bool_0)
	{
		return method_2(string_0, string_1, global::Class3.Class3_0, bool_0);
	}
}
