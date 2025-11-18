using System;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x0200089A RID: 2202
	internal class ActivationAttributeStack
	{
		// Token: 0x06005D2B RID: 23851 RVA: 0x0014681B File Offset: 0x00144A1B
		internal ActivationAttributeStack()
		{
			this.activationTypes = new object[4];
			this.activationAttributes = new object[4];
			this.freeIndex = 0;
		}

		// Token: 0x06005D2C RID: 23852 RVA: 0x00146844 File Offset: 0x00144A44
		internal void Push(Type typ, object[] attr)
		{
			if (this.freeIndex == this.activationTypes.Length)
			{
				object[] array = new object[this.activationTypes.Length * 2];
				object[] array2 = new object[this.activationAttributes.Length * 2];
				Array.Copy(this.activationTypes, array, this.activationTypes.Length);
				Array.Copy(this.activationAttributes, array2, this.activationAttributes.Length);
				this.activationTypes = array;
				this.activationAttributes = array2;
			}
			this.activationTypes[this.freeIndex] = typ;
			this.activationAttributes[this.freeIndex] = attr;
			this.freeIndex++;
		}

		// Token: 0x06005D2D RID: 23853 RVA: 0x001468E1 File Offset: 0x00144AE1
		internal object[] Peek(Type typ)
		{
			if (this.freeIndex == 0 || this.activationTypes[this.freeIndex - 1] != typ)
			{
				return null;
			}
			return (object[])this.activationAttributes[this.freeIndex - 1];
		}

		// Token: 0x06005D2E RID: 23854 RVA: 0x00146914 File Offset: 0x00144B14
		internal void Pop(Type typ)
		{
			if (this.freeIndex != 0 && this.activationTypes[this.freeIndex - 1] == typ)
			{
				this.freeIndex--;
				this.activationTypes[this.freeIndex] = null;
				this.activationAttributes[this.freeIndex] = null;
			}
		}

		// Token: 0x040029FA RID: 10746
		private object[] activationTypes;

		// Token: 0x040029FB RID: 10747
		private object[] activationAttributes;

		// Token: 0x040029FC RID: 10748
		private int freeIndex;
	}
}
