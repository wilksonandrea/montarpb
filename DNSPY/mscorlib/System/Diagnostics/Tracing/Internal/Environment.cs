using System;
using System.Resources;
using Microsoft.Reflection;

namespace System.Diagnostics.Tracing.Internal
{
	// Token: 0x02000489 RID: 1161
	internal static class Environment
	{
		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x0600376D RID: 14189 RVA: 0x000D5260 File Offset: 0x000D3460
		public static int TickCount
		{
			get
			{
				return Environment.TickCount;
			}
		}

		// Token: 0x0600376E RID: 14190 RVA: 0x000D5268 File Offset: 0x000D3468
		public static string GetResourceString(string key, params object[] args)
		{
			string @string = Environment.rm.GetString(key);
			if (@string != null)
			{
				return string.Format(@string, args);
			}
			string text = string.Empty;
			foreach (object obj in args)
			{
				if (text != string.Empty)
				{
					text += ", ";
				}
				text += obj.ToString();
			}
			return key + " (" + text + ")";
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000D52DF File Offset: 0x000D34DF
		public static string GetRuntimeResourceString(string key, params object[] args)
		{
			return Environment.GetResourceString(key, args);
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000D52E8 File Offset: 0x000D34E8
		// Note: this type is marked as 'beforefieldinit'.
		static Environment()
		{
		}

		// Token: 0x040018B3 RID: 6323
		public static readonly string NewLine = Environment.NewLine;

		// Token: 0x040018B4 RID: 6324
		private static ResourceManager rm = new ResourceManager("Microsoft.Diagnostics.Tracing.Messages", typeof(Environment).Assembly());
	}
}
