using System;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;

namespace System.Runtime.Versioning
{
	// Token: 0x02000728 RID: 1832
	internal static class MultitargetingHelpers
	{
		// Token: 0x0600516C RID: 20844 RVA: 0x0011EE50 File Offset: 0x0011D050
		internal static string GetAssemblyQualifiedName(Type type, Func<Type, string> converter)
		{
			string text = null;
			if (type != null)
			{
				if (converter != null)
				{
					try
					{
						text = converter(type);
					}
					catch (Exception ex)
					{
						if (MultitargetingHelpers.IsSecurityOrCriticalException(ex))
						{
							throw;
						}
					}
				}
				if (text == null)
				{
					text = MultitargetingHelpers.defaultConverter(type);
				}
			}
			return text;
		}

		// Token: 0x0600516D RID: 20845 RVA: 0x0011EEA4 File Offset: 0x0011D0A4
		private static bool IsCriticalException(Exception ex)
		{
			return ex is NullReferenceException || ex is StackOverflowException || ex is OutOfMemoryException || ex is ThreadAbortException || ex is IndexOutOfRangeException || ex is AccessViolationException;
		}

		// Token: 0x0600516E RID: 20846 RVA: 0x0011EED9 File Offset: 0x0011D0D9
		private static bool IsSecurityOrCriticalException(Exception ex)
		{
			return ex is SecurityException || MultitargetingHelpers.IsCriticalException(ex);
		}

		// Token: 0x0600516F RID: 20847 RVA: 0x0011EEEB File Offset: 0x0011D0EB
		// Note: this type is marked as 'beforefieldinit'.
		static MultitargetingHelpers()
		{
		}

		// Token: 0x04002436 RID: 9270
		private static Func<Type, string> defaultConverter = (Type t) => t.AssemblyQualifiedName;

		// Token: 0x02000C67 RID: 3175
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06007084 RID: 28804 RVA: 0x0018343C File Offset: 0x0018163C
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06007085 RID: 28805 RVA: 0x00183448 File Offset: 0x00181648
			public <>c()
			{
			}

			// Token: 0x06007086 RID: 28806 RVA: 0x00183450 File Offset: 0x00181650
			internal string <.cctor>b__4_0(Type t)
			{
				return t.AssemblyQualifiedName;
			}

			// Token: 0x040037D5 RID: 14293
			public static readonly MultitargetingHelpers.<>c <>9 = new MultitargetingHelpers.<>c();
		}
	}
}
