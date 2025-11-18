using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Runtime.Serialization.Formatters
{
	// Token: 0x02000765 RID: 1893
	[SecurityCritical]
	[ComVisible(true)]
	public sealed class InternalST
	{
		// Token: 0x06005312 RID: 21266 RVA: 0x001239DF File Offset: 0x00121BDF
		private InternalST()
		{
		}

		// Token: 0x06005313 RID: 21267 RVA: 0x001239E7 File Offset: 0x00121BE7
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
		}

		// Token: 0x06005314 RID: 21268 RVA: 0x001239E9 File Offset: 0x00121BE9
		public static bool SoapCheckEnabled()
		{
			return BCLDebug.CheckEnabled("Soap");
		}

		// Token: 0x06005315 RID: 21269 RVA: 0x001239F8 File Offset: 0x00121BF8
		[Conditional("SER_LOGGING")]
		public static void Soap(params object[] messages)
		{
			if (!(messages[0] is string))
			{
				messages[0] = messages[0].GetType().Name + " ";
				return;
			}
			int num = 0;
			object obj = messages[0];
			messages[num] = ((obj != null) ? obj.ToString() : null) + " ";
		}

		// Token: 0x06005316 RID: 21270 RVA: 0x00123A46 File Offset: 0x00121C46
		[Conditional("_DEBUG")]
		public static void SoapAssert(bool condition, string message)
		{
		}

		// Token: 0x06005317 RID: 21271 RVA: 0x00123A48 File Offset: 0x00121C48
		public static void SerializationSetValue(FieldInfo fi, object target, object value)
		{
			if (fi == null)
			{
				throw new ArgumentNullException("fi");
			}
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			FormatterServices.SerializationSetValue(fi, target, value);
		}

		// Token: 0x06005318 RID: 21272 RVA: 0x00123A82 File Offset: 0x00121C82
		public static Assembly LoadAssemblyFromString(string assemblyString)
		{
			return FormatterServices.LoadAssemblyFromString(assemblyString);
		}
	}
}
