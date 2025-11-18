using System;

namespace System.Security.Policy
{
	// Token: 0x0200034C RID: 844
	internal class CodeGroupPositionMarker
	{
		// Token: 0x060029FE RID: 10750 RVA: 0x0009B35C File Offset: 0x0009955C
		internal CodeGroupPositionMarker(int elementIndex, int groupIndex, SecurityElement element)
		{
			this.elementIndex = elementIndex;
			this.groupIndex = groupIndex;
			this.element = element;
		}

		// Token: 0x0400112F RID: 4399
		internal int elementIndex;

		// Token: 0x04001130 RID: 4400
		internal int groupIndex;

		// Token: 0x04001131 RID: 4401
		internal SecurityElement element;
	}
}
