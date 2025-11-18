using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200055C RID: 1372
	internal class Shared<T>
	{
		// Token: 0x06004074 RID: 16500 RVA: 0x000F03C9 File Offset: 0x000EE5C9
		internal Shared(T value)
		{
			this.Value = value;
		}

		// Token: 0x04001AEE RID: 6894
		internal T Value;
	}
}
