using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization
{
	// Token: 0x0200074C RID: 1868
	[ComVisible(true)]
	[Serializable]
	public class ObjectIDGenerator
	{
		// Token: 0x06005264 RID: 21092 RVA: 0x0012108C File Offset: 0x0011F28C
		public ObjectIDGenerator()
		{
			this.m_currentCount = 1;
			this.m_currentSize = ObjectIDGenerator.sizes[0];
			this.m_ids = new long[this.m_currentSize * 4];
			this.m_objs = new object[this.m_currentSize * 4];
		}

		// Token: 0x06005265 RID: 21093 RVA: 0x001210DC File Offset: 0x0011F2DC
		private int FindElement(object obj, out bool found)
		{
			int num = RuntimeHelpers.GetHashCode(obj);
			int num2 = 1 + (num & int.MaxValue) % (this.m_currentSize - 2);
			int i;
			for (;;)
			{
				int num3 = (num & int.MaxValue) % this.m_currentSize * 4;
				for (i = num3; i < num3 + 4; i++)
				{
					if (this.m_objs[i] == null)
					{
						goto Block_1;
					}
					if (this.m_objs[i] == obj)
					{
						goto Block_2;
					}
				}
				num += num2;
			}
			Block_1:
			found = false;
			return i;
			Block_2:
			found = true;
			return i;
		}

		// Token: 0x06005266 RID: 21094 RVA: 0x00121148 File Offset: 0x0011F348
		public virtual long GetId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			bool flag;
			int num = this.FindElement(obj, out flag);
			long num3;
			if (!flag)
			{
				this.m_objs[num] = obj;
				long[] ids = this.m_ids;
				int num2 = num;
				int currentCount = this.m_currentCount;
				this.m_currentCount = currentCount + 1;
				ids[num2] = (long)currentCount;
				num3 = this.m_ids[num];
				if (this.m_currentCount > this.m_currentSize * 4 / 2)
				{
					this.Rehash();
				}
			}
			else
			{
				num3 = this.m_ids[num];
			}
			firstTime = !flag;
			return num3;
		}

		// Token: 0x06005267 RID: 21095 RVA: 0x001211D0 File Offset: 0x0011F3D0
		public virtual long HasId(object obj, out bool firstTime)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj", Environment.GetResourceString("ArgumentNull_Obj"));
			}
			bool flag;
			int num = this.FindElement(obj, out flag);
			if (flag)
			{
				firstTime = false;
				return this.m_ids[num];
			}
			firstTime = true;
			return 0L;
		}

		// Token: 0x06005268 RID: 21096 RVA: 0x00121214 File Offset: 0x0011F414
		private void Rehash()
		{
			int[] array = (AppContextSwitches.UseNewMaxArraySize ? ObjectIDGenerator.sizesWithMaxArraySwitch : ObjectIDGenerator.sizes);
			int num = 0;
			int currentSize = this.m_currentSize;
			while (num < array.Length && array[num] <= currentSize)
			{
				num++;
			}
			if (num == array.Length)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_TooManyElements"));
			}
			this.m_currentSize = array[num];
			long[] array2 = new long[this.m_currentSize * 4];
			object[] array3 = new object[this.m_currentSize * 4];
			long[] ids = this.m_ids;
			object[] objs = this.m_objs;
			this.m_ids = array2;
			this.m_objs = array3;
			for (int i = 0; i < objs.Length; i++)
			{
				if (objs[i] != null)
				{
					bool flag;
					int num2 = this.FindElement(objs[i], out flag);
					this.m_objs[num2] = objs[i];
					this.m_ids[num2] = ids[i];
				}
			}
		}

		// Token: 0x06005269 RID: 21097 RVA: 0x001212F2 File Offset: 0x0011F4F2
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectIDGenerator()
		{
		}

		// Token: 0x0400247B RID: 9339
		private const int numbins = 4;

		// Token: 0x0400247C RID: 9340
		internal int m_currentCount;

		// Token: 0x0400247D RID: 9341
		internal int m_currentSize;

		// Token: 0x0400247E RID: 9342
		internal long[] m_ids;

		// Token: 0x0400247F RID: 9343
		internal object[] m_objs;

		// Token: 0x04002480 RID: 9344
		private static readonly int[] sizes = new int[]
		{
			5, 11, 29, 47, 97, 197, 397, 797, 1597, 3203,
			6421, 12853, 25717, 51437, 102877, 205759, 411527, 823117, 1646237, 3292489,
			6584983
		};

		// Token: 0x04002481 RID: 9345
		private static readonly int[] sizesWithMaxArraySwitch = new int[]
		{
			5, 11, 29, 47, 97, 197, 397, 797, 1597, 3203,
			6421, 12853, 25717, 51437, 102877, 205759, 411527, 823117, 1646237, 3292489,
			6584983, 13169977, 26339969, 52679969, 105359939, 210719881, 421439783
		};
	}
}
