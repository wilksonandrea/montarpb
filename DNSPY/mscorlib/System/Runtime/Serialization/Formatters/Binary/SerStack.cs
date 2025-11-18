using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000798 RID: 1944
	internal sealed class SerStack
	{
		// Token: 0x06005442 RID: 21570 RVA: 0x00128B56 File Offset: 0x00126D56
		internal SerStack()
		{
			this.stackId = "System";
		}

		// Token: 0x06005443 RID: 21571 RVA: 0x00128B7C File Offset: 0x00126D7C
		internal SerStack(string stackId)
		{
			this.stackId = stackId;
		}

		// Token: 0x06005444 RID: 21572 RVA: 0x00128BA0 File Offset: 0x00126DA0
		internal void Push(object obj)
		{
			if (this.top == this.objects.Length - 1)
			{
				this.IncreaseCapacity();
			}
			object[] array = this.objects;
			int num = this.top + 1;
			this.top = num;
			array[num] = obj;
		}

		// Token: 0x06005445 RID: 21573 RVA: 0x00128BE0 File Offset: 0x00126DE0
		internal object Pop()
		{
			if (this.top < 0)
			{
				return null;
			}
			object obj = this.objects[this.top];
			object[] array = this.objects;
			int num = this.top;
			this.top = num - 1;
			array[num] = null;
			return obj;
		}

		// Token: 0x06005446 RID: 21574 RVA: 0x00128C20 File Offset: 0x00126E20
		internal void IncreaseCapacity()
		{
			int num = this.objects.Length * 2;
			object[] array = new object[num];
			Array.Copy(this.objects, 0, array, 0, this.objects.Length);
			this.objects = array;
		}

		// Token: 0x06005447 RID: 21575 RVA: 0x00128C5C File Offset: 0x00126E5C
		internal object Peek()
		{
			if (this.top < 0)
			{
				return null;
			}
			return this.objects[this.top];
		}

		// Token: 0x06005448 RID: 21576 RVA: 0x00128C76 File Offset: 0x00126E76
		internal object PeekPeek()
		{
			if (this.top < 1)
			{
				return null;
			}
			return this.objects[this.top - 1];
		}

		// Token: 0x06005449 RID: 21577 RVA: 0x00128C92 File Offset: 0x00126E92
		internal int Count()
		{
			return this.top + 1;
		}

		// Token: 0x0600544A RID: 21578 RVA: 0x00128C9C File Offset: 0x00126E9C
		internal bool IsEmpty()
		{
			return this.top <= 0;
		}

		// Token: 0x0600544B RID: 21579 RVA: 0x00128CAC File Offset: 0x00126EAC
		[Conditional("SER_LOGGING")]
		internal void Dump()
		{
			for (int i = 0; i < this.Count(); i++)
			{
				object obj = this.objects[i];
			}
		}

		// Token: 0x04002656 RID: 9814
		internal object[] objects = new object[5];

		// Token: 0x04002657 RID: 9815
		internal string stackId;

		// Token: 0x04002658 RID: 9816
		internal int top = -1;

		// Token: 0x04002659 RID: 9817
		internal int next;
	}
}
