using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079E RID: 1950
	internal sealed class NameInfo
	{
		// Token: 0x06005461 RID: 21601 RVA: 0x00129235 File Offset: 0x00127435
		internal NameInfo()
		{
		}

		// Token: 0x06005462 RID: 21602 RVA: 0x00129240 File Offset: 0x00127440
		internal void Init()
		{
			this.NIFullName = null;
			this.NIobjectId = 0L;
			this.NIassemId = 0L;
			this.NIprimitiveTypeEnum = InternalPrimitiveTypeE.Invalid;
			this.NItype = null;
			this.NIisSealed = false;
			this.NItransmitTypeOnObject = false;
			this.NItransmitTypeOnMember = false;
			this.NIisParentTypeOnObject = false;
			this.NIisArray = false;
			this.NIisArrayItem = false;
			this.NIarrayEnum = InternalArrayTypeE.Empty;
			this.NIsealedStatusChecked = false;
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06005463 RID: 21603 RVA: 0x001292AA File Offset: 0x001274AA
		public bool IsSealed
		{
			get
			{
				if (!this.NIsealedStatusChecked)
				{
					this.NIisSealed = this.NItype.IsSealed;
					this.NIsealedStatusChecked = true;
				}
				return this.NIisSealed;
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06005464 RID: 21604 RVA: 0x001292D2 File Offset: 0x001274D2
		// (set) Token: 0x06005465 RID: 21605 RVA: 0x001292F3 File Offset: 0x001274F3
		public string NIname
		{
			get
			{
				if (this.NIFullName == null)
				{
					this.NIFullName = this.NItype.FullName;
				}
				return this.NIFullName;
			}
			set
			{
				this.NIFullName = value;
			}
		}

		// Token: 0x0400266C RID: 9836
		internal string NIFullName;

		// Token: 0x0400266D RID: 9837
		internal long NIobjectId;

		// Token: 0x0400266E RID: 9838
		internal long NIassemId;

		// Token: 0x0400266F RID: 9839
		internal InternalPrimitiveTypeE NIprimitiveTypeEnum;

		// Token: 0x04002670 RID: 9840
		internal Type NItype;

		// Token: 0x04002671 RID: 9841
		internal bool NIisSealed;

		// Token: 0x04002672 RID: 9842
		internal bool NIisArray;

		// Token: 0x04002673 RID: 9843
		internal bool NIisArrayItem;

		// Token: 0x04002674 RID: 9844
		internal bool NItransmitTypeOnObject;

		// Token: 0x04002675 RID: 9845
		internal bool NItransmitTypeOnMember;

		// Token: 0x04002676 RID: 9846
		internal bool NIisParentTypeOnObject;

		// Token: 0x04002677 RID: 9847
		internal InternalArrayTypeE NIarrayEnum;

		// Token: 0x04002678 RID: 9848
		private bool NIsealedStatusChecked;
	}
}
