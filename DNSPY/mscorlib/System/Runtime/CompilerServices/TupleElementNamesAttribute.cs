using System;
using System.Collections.Generic;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000900 RID: 2304
	[CLSCompliant(false)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public sealed class TupleElementNamesAttribute : Attribute
	{
		// Token: 0x06005E5B RID: 24155 RVA: 0x0014B35F File Offset: 0x0014955F
		public TupleElementNamesAttribute(string[] transformNames)
		{
			if (transformNames == null)
			{
				throw new ArgumentNullException("transformNames");
			}
			this._transformNames = transformNames;
		}

		// Token: 0x17001032 RID: 4146
		// (get) Token: 0x06005E5C RID: 24156 RVA: 0x0014B37C File Offset: 0x0014957C
		public IList<string> TransformNames
		{
			get
			{
				return this._transformNames;
			}
		}

		// Token: 0x04002A61 RID: 10849
		private readonly string[] _transformNames;
	}
}
