using System;

namespace System.Collections
{
	// Token: 0x020004A8 RID: 1192
	[Serializable]
	internal class StructuralEqualityComparer : IEqualityComparer
	{
		// Token: 0x06003902 RID: 14594 RVA: 0x000DA2EC File Offset: 0x000D84EC
		public bool Equals(object x, object y)
		{
			if (x == null)
			{
				return y == null;
			}
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}
			return y != null && x.Equals(y);
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000DA324 File Offset: 0x000D8524
		public int GetHashCode(object obj)
		{
			if (obj == null)
			{
				return 0;
			}
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000DA34E File Offset: 0x000D854E
		public StructuralEqualityComparer()
		{
		}
	}
}
