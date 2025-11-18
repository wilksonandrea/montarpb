using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200061A RID: 1562
	[ComVisible(true)]
	[Serializable]
	public struct ParameterModifier
	{
		// Token: 0x0600484F RID: 18511 RVA: 0x00106A2A File Offset: 0x00104C2A
		public ParameterModifier(int parameterCount)
		{
			if (parameterCount <= 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ParmArraySize"));
			}
			this._byRef = new bool[parameterCount];
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x00106A4C File Offset: 0x00104C4C
		internal bool[] IsByRefArray
		{
			get
			{
				return this._byRef;
			}
		}

		// Token: 0x17000B3D RID: 2877
		public bool this[int index]
		{
			get
			{
				return this._byRef[index];
			}
			set
			{
				this._byRef[index] = value;
			}
		}

		// Token: 0x04001E07 RID: 7687
		private bool[] _byRef;
	}
}
