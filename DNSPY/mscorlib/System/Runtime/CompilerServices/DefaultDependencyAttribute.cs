using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C6 RID: 2246
	[AttributeUsage(AttributeTargets.Assembly)]
	[Serializable]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		// Token: 0x06005DC3 RID: 24003 RVA: 0x001496BB File Offset: 0x001478BB
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.loadHint = loadHintArgument;
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06005DC4 RID: 24004 RVA: 0x001496CA File Offset: 0x001478CA
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002A38 RID: 10808
		private LoadHint loadHint;
	}
}
