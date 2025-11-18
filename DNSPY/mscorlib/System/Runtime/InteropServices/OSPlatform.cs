using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AC RID: 2476
	public struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x06006301 RID: 25345 RVA: 0x001515A8 File Offset: 0x0014F7A8
		public static OSPlatform Linux
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<Linux>k__BackingField;
			}
		} = new OSPlatform("LINUX");

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06006302 RID: 25346 RVA: 0x001515AF File Offset: 0x0014F7AF
		public static OSPlatform OSX
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<OSX>k__BackingField;
			}
		} = new OSPlatform("OSX");

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06006303 RID: 25347 RVA: 0x001515B6 File Offset: 0x0014F7B6
		public static OSPlatform Windows
		{
			[CompilerGenerated]
			get
			{
				return OSPlatform.<Windows>k__BackingField;
			}
		} = new OSPlatform("WINDOWS");

		// Token: 0x06006304 RID: 25348 RVA: 0x001515BD File Offset: 0x0014F7BD
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyValue"), "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x06006305 RID: 25349 RVA: 0x001515F1 File Offset: 0x0014F7F1
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x06006306 RID: 25350 RVA: 0x001515F9 File Offset: 0x0014F7F9
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06006307 RID: 25351 RVA: 0x00151607 File Offset: 0x0014F807
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x00151616 File Offset: 0x0014F816
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x0015162E File Offset: 0x0014F82E
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x00151645 File Offset: 0x0014F845
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x0600630B RID: 25355 RVA: 0x00151656 File Offset: 0x0014F856
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600630C RID: 25356 RVA: 0x00151660 File Offset: 0x0014F860
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x0600630D RID: 25357 RVA: 0x0015166C File Offset: 0x0014F86C
		// Note: this type is marked as 'beforefieldinit'.
		static OSPlatform()
		{
		}

		// Token: 0x04002CB2 RID: 11442
		private readonly string _osPlatform;

		// Token: 0x04002CB3 RID: 11443
		[CompilerGenerated]
		private static readonly OSPlatform <Linux>k__BackingField;

		// Token: 0x04002CB4 RID: 11444
		[CompilerGenerated]
		private static readonly OSPlatform <OSX>k__BackingField;

		// Token: 0x04002CB5 RID: 11445
		[CompilerGenerated]
		private static readonly OSPlatform <Windows>k__BackingField;
	}
}
