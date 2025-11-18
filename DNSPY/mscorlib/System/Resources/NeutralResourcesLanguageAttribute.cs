using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x02000393 RID: 915
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class NeutralResourcesLanguageAttribute : Attribute
	{
		// Token: 0x06002D11 RID: 11537 RVA: 0x000AA05E File Offset: 0x000A825E
		[__DynamicallyInvokable]
		public NeutralResourcesLanguageAttribute(string cultureName)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			this._culture = cultureName;
			this._fallbackLoc = UltimateResourceFallbackLocation.MainAssembly;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x000AA084 File Offset: 0x000A8284
		public NeutralResourcesLanguageAttribute(string cultureName, UltimateResourceFallbackLocation location)
		{
			if (cultureName == null)
			{
				throw new ArgumentNullException("cultureName");
			}
			if (!Enum.IsDefined(typeof(UltimateResourceFallbackLocation), location))
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_InvalidNeutralResourcesLanguage_FallbackLoc", new object[] { location }));
			}
			this._culture = cultureName;
			this._fallbackLoc = location;
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002D13 RID: 11539 RVA: 0x000AA0E9 File Offset: 0x000A82E9
		[__DynamicallyInvokable]
		public string CultureName
		{
			[__DynamicallyInvokable]
			get
			{
				return this._culture;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000AA0F1 File Offset: 0x000A82F1
		public UltimateResourceFallbackLocation Location
		{
			get
			{
				return this._fallbackLoc;
			}
		}

		// Token: 0x0400122F RID: 4655
		private string _culture;

		// Token: 0x04001230 RID: 4656
		private UltimateResourceFallbackLocation _fallbackLoc;
	}
}
