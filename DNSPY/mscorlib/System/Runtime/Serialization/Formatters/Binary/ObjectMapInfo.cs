using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200077E RID: 1918
	internal sealed class ObjectMapInfo
	{
		// Token: 0x060053B9 RID: 21433 RVA: 0x00126725 File Offset: 0x00124925
		internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
		{
			this.objectId = objectId;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.memberTypes = memberTypes;
		}

		// Token: 0x060053BA RID: 21434 RVA: 0x0012674C File Offset: 0x0012494C
		internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
		{
			bool flag = true;
			if (this.numMembers == numMembers)
			{
				for (int i = 0; i < numMembers; i++)
				{
					if (!this.memberNames[i].Equals(memberNames[i]))
					{
						flag = false;
						break;
					}
					if (memberTypes != null && this.memberTypes[i] != memberTypes[i])
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x040025BC RID: 9660
		internal int objectId;

		// Token: 0x040025BD RID: 9661
		private int numMembers;

		// Token: 0x040025BE RID: 9662
		private string[] memberNames;

		// Token: 0x040025BF RID: 9663
		private Type[] memberTypes;
	}
}
