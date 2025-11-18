using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C7 RID: 2247
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	[Serializable]
	public sealed class DependencyAttribute : Attribute
	{
		// Token: 0x06005DC5 RID: 24005 RVA: 0x001496D2 File Offset: 0x001478D2
		public DependencyAttribute(string dependentAssemblyArgument, LoadHint loadHintArgument)
		{
			this.dependentAssembly = dependentAssemblyArgument;
			this.loadHint = loadHintArgument;
		}

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06005DC6 RID: 24006 RVA: 0x001496E8 File Offset: 0x001478E8
		public string DependentAssembly
		{
			get
			{
				return this.dependentAssembly;
			}
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x06005DC7 RID: 24007 RVA: 0x001496F0 File Offset: 0x001478F0
		public LoadHint LoadHint
		{
			get
			{
				return this.loadHint;
			}
		}

		// Token: 0x04002A39 RID: 10809
		private string dependentAssembly;

		// Token: 0x04002A3A RID: 10810
		private LoadHint loadHint;
	}
}
