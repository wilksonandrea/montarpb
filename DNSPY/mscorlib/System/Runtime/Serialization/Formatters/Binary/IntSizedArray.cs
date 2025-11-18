using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079A RID: 1946
	[Serializable]
	internal sealed class IntSizedArray : ICloneable
	{
		// Token: 0x06005453 RID: 21587 RVA: 0x00128EB0 File Offset: 0x001270B0
		public IntSizedArray()
		{
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x00128ED4 File Offset: 0x001270D4
		private IntSizedArray(IntSizedArray sizedArray)
		{
			this.objects = new int[sizedArray.objects.Length];
			sizedArray.objects.CopyTo(this.objects, 0);
			this.negObjects = new int[sizedArray.negObjects.Length];
			sizedArray.negObjects.CopyTo(this.negObjects, 0);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x00128F4A File Offset: 0x0012714A
		public object Clone()
		{
			return new IntSizedArray(this);
		}

		// Token: 0x17000DD9 RID: 3545
		internal int this[int index]
		{
			get
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						return 0;
					}
					return this.negObjects[-index];
				}
				else
				{
					if (index > this.objects.Length - 1)
					{
						return 0;
					}
					return this.objects[index];
				}
			}
			set
			{
				if (index < 0)
				{
					if (-index > this.negObjects.Length - 1)
					{
						this.IncreaseCapacity(index);
					}
					this.negObjects[-index] = value;
					return;
				}
				if (index > this.objects.Length - 1)
				{
					this.IncreaseCapacity(index);
				}
				this.objects[index] = value;
			}
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x00128FDC File Offset: 0x001271DC
		internal void IncreaseCapacity(int index)
		{
			try
			{
				if (index < 0)
				{
					int num = Math.Max(this.negObjects.Length * 2, -index + 1);
					int[] array = new int[num];
					Array.Copy(this.negObjects, 0, array, 0, this.negObjects.Length);
					this.negObjects = array;
				}
				else
				{
					int num2 = Math.Max(this.objects.Length * 2, index + 1);
					int[] array2 = new int[num2];
					Array.Copy(this.objects, 0, array2, 0, this.objects.Length);
					this.objects = array2;
				}
			}
			catch (Exception)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
			}
		}

		// Token: 0x0400265C RID: 9820
		internal int[] objects = new int[16];

		// Token: 0x0400265D RID: 9821
		internal int[] negObjects = new int[4];
	}
}
