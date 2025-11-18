using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000540 RID: 1344
	[DebuggerTypeProxy(typeof(SystemThreading_ThreadLocalDebugView<>))]
	[DebuggerDisplay("IsValueCreated={IsValueCreated}, Value={ValueForDebugDisplay}, Count={ValuesCountForDebugDisplay}")]
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ThreadLocal<T> : IDisposable
	{
		// Token: 0x06003EEC RID: 16108 RVA: 0x000E9E0F File Offset: 0x000E800F
		[__DynamicallyInvokable]
		public ThreadLocal()
		{
			this.Initialize(null, false);
		}

		// Token: 0x06003EED RID: 16109 RVA: 0x000E9E2B File Offset: 0x000E802B
		[__DynamicallyInvokable]
		public ThreadLocal(bool trackAllValues)
		{
			this.Initialize(null, trackAllValues);
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000E9E47 File Offset: 0x000E8047
		[__DynamicallyInvokable]
		public ThreadLocal(Func<T> valueFactory)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, false);
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000E9E71 File Offset: 0x000E8071
		[__DynamicallyInvokable]
		public ThreadLocal(Func<T> valueFactory, bool trackAllValues)
		{
			if (valueFactory == null)
			{
				throw new ArgumentNullException("valueFactory");
			}
			this.Initialize(valueFactory, trackAllValues);
		}

		// Token: 0x06003EF0 RID: 16112 RVA: 0x000E9E9C File Offset: 0x000E809C
		private void Initialize(Func<T> valueFactory, bool trackAllValues)
		{
			this.m_valueFactory = valueFactory;
			this.m_trackAllValues = trackAllValues;
			try
			{
			}
			finally
			{
				this.m_idComplement = ~ThreadLocal<T>.s_idManager.GetId();
				this.m_initialized = true;
			}
		}

		// Token: 0x06003EF1 RID: 16113 RVA: 0x000E9EE4 File Offset: 0x000E80E4
		[__DynamicallyInvokable]
		~ThreadLocal()
		{
			this.Dispose(false);
		}

		// Token: 0x06003EF2 RID: 16114 RVA: 0x000E9F14 File Offset: 0x000E8114
		[__DynamicallyInvokable]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003EF3 RID: 16115 RVA: 0x000E9F24 File Offset: 0x000E8124
		[__DynamicallyInvokable]
		protected virtual void Dispose(bool disposing)
		{
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			int num;
			lock (idManager)
			{
				num = ~this.m_idComplement;
				this.m_idComplement = 0;
				if (num < 0 || !this.m_initialized)
				{
					return;
				}
				this.m_initialized = false;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = linkedSlot.SlotArray;
					if (slotArray != null)
					{
						linkedSlot.SlotArray = null;
						slotArray[num].Value.Value = default(T);
						slotArray[num].Value = null;
					}
				}
			}
			this.m_linkedSlot = null;
			ThreadLocal<T>.s_idManager.ReturnId(num);
		}

		// Token: 0x06003EF4 RID: 16116 RVA: 0x000E9FF8 File Offset: 0x000E81F8
		[__DynamicallyInvokable]
		public override string ToString()
		{
			T value = this.Value;
			return value.ToString();
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000EA01C File Offset: 0x000E821C
		// (set) Token: 0x06003EF6 RID: 16118 RVA: 0x000EA070 File Offset: 0x000E8270
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[__DynamicallyInvokable]
		public T Value
		{
			[__DynamicallyInvokable]
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array != null && num >= 0 && num < array.Length && (value = array[num].Value) != null && this.m_initialized)
				{
					return value.Value;
				}
				return this.GetValueSlow();
			}
			[__DynamicallyInvokable]
			set
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value2;
				if (array != null && num >= 0 && num < array.Length && (value2 = array[num].Value) != null && this.m_initialized)
				{
					value2.Value = value;
					return;
				}
				this.SetValueSlow(value, array);
			}
		}

		// Token: 0x06003EF7 RID: 16119 RVA: 0x000EA0C4 File Offset: 0x000E82C4
		private T GetValueSlow()
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
			}
			Debugger.NotifyOfCrossThreadDependency();
			T t;
			if (this.m_valueFactory == null)
			{
				t = default(T);
			}
			else
			{
				t = this.m_valueFactory();
				if (this.IsValueCreated)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_Value_RecursiveCallsToValue"));
				}
			}
			this.Value = t;
			return t;
		}

		// Token: 0x06003EF8 RID: 16120 RVA: 0x000EA130 File Offset: 0x000E8330
		private void SetValueSlow(T value, ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
		{
			int num = ~this.m_idComplement;
			if (num < 0)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
			}
			if (slotArray == null)
			{
				slotArray = new ThreadLocal<T>.LinkedSlotVolatile[ThreadLocal<T>.GetNewTableSize(num + 1)];
				ThreadLocal<T>.ts_finalizationHelper = new ThreadLocal<T>.FinalizationHelper(slotArray, this.m_trackAllValues);
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (num >= slotArray.Length)
			{
				this.GrowTable(ref slotArray, num + 1);
				ThreadLocal<T>.ts_finalizationHelper.SlotArray = slotArray;
				ThreadLocal<T>.ts_slotArray = slotArray;
			}
			if (slotArray[num].Value == null)
			{
				this.CreateLinkedSlot(slotArray, num, value);
				return;
			}
			ThreadLocal<T>.LinkedSlot value2 = slotArray[num].Value;
			if (!this.m_initialized)
			{
				throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
			}
			value2.Value = value;
		}

		// Token: 0x06003EF9 RID: 16121 RVA: 0x000EA1F0 File Offset: 0x000E83F0
		private void CreateLinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, int id, T value)
		{
			ThreadLocal<T>.LinkedSlot linkedSlot = new ThreadLocal<T>.LinkedSlot(slotArray);
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				if (!this.m_initialized)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
				}
				ThreadLocal<T>.LinkedSlot next = this.m_linkedSlot.Next;
				linkedSlot.Next = next;
				linkedSlot.Previous = this.m_linkedSlot;
				linkedSlot.Value = value;
				if (next != null)
				{
					next.Previous = linkedSlot;
				}
				this.m_linkedSlot.Next = linkedSlot;
				slotArray[id].Value = linkedSlot;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000EA2A0 File Offset: 0x000E84A0
		[__DynamicallyInvokable]
		public IList<T> Values
		{
			[__DynamicallyInvokable]
			get
			{
				if (!this.m_trackAllValues)
				{
					throw new InvalidOperationException(Environment.GetResourceString("ThreadLocal_ValuesNotAvailable"));
				}
				List<T> valuesAsList = this.GetValuesAsList();
				if (valuesAsList == null)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
				}
				return valuesAsList;
			}
		}

		// Token: 0x06003EFB RID: 16123 RVA: 0x000EA2E0 File Offset: 0x000E84E0
		private List<T> GetValuesAsList()
		{
			List<T> list = new List<T>();
			int num = ~this.m_idComplement;
			if (num == -1)
			{
				return null;
			}
			for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
			{
				list.Add(linkedSlot.Value);
			}
			return list;
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000EA32C File Offset: 0x000E852C
		private int ValuesCountForDebugDisplay
		{
			get
			{
				int num = 0;
				for (ThreadLocal<T>.LinkedSlot linkedSlot = this.m_linkedSlot.Next; linkedSlot != null; linkedSlot = linkedSlot.Next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06003EFD RID: 16125 RVA: 0x000EA35C File Offset: 0x000E855C
		[__DynamicallyInvokable]
		public bool IsValueCreated
		{
			[__DynamicallyInvokable]
			get
			{
				int num = ~this.m_idComplement;
				if (num < 0)
				{
					throw new ObjectDisposedException(Environment.GetResourceString("ThreadLocal_Disposed"));
				}
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				return array != null && num < array.Length && array[num].Value != null;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x000EA3A8 File Offset: 0x000E85A8
		internal T ValueForDebugDisplay
		{
			get
			{
				ThreadLocal<T>.LinkedSlotVolatile[] array = ThreadLocal<T>.ts_slotArray;
				int num = ~this.m_idComplement;
				ThreadLocal<T>.LinkedSlot value;
				if (array == null || num >= array.Length || (value = array[num].Value) == null || !this.m_initialized)
				{
					return default(T);
				}
				return value.Value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003EFF RID: 16127 RVA: 0x000EA3F8 File Offset: 0x000E85F8
		internal List<T> ValuesForDebugDisplay
		{
			get
			{
				return this.GetValuesAsList();
			}
		}

		// Token: 0x06003F00 RID: 16128 RVA: 0x000EA400 File Offset: 0x000E8600
		private void GrowTable(ref ThreadLocal<T>.LinkedSlotVolatile[] table, int minLength)
		{
			int newTableSize = ThreadLocal<T>.GetNewTableSize(minLength);
			ThreadLocal<T>.LinkedSlotVolatile[] array = new ThreadLocal<T>.LinkedSlotVolatile[newTableSize];
			ThreadLocal<T>.IdManager idManager = ThreadLocal<T>.s_idManager;
			lock (idManager)
			{
				for (int i = 0; i < table.Length; i++)
				{
					ThreadLocal<T>.LinkedSlot value = table[i].Value;
					if (value != null && value.SlotArray != null)
					{
						value.SlotArray = array;
						array[i] = table[i];
					}
				}
			}
			table = array;
		}

		// Token: 0x06003F01 RID: 16129 RVA: 0x000EA49C File Offset: 0x000E869C
		private static int GetNewTableSize(int minSize)
		{
			if (minSize > 2146435071)
			{
				return int.MaxValue;
			}
			int num = minSize - 1;
			num |= num >> 1;
			num |= num >> 2;
			num |= num >> 4;
			num |= num >> 8;
			num |= num >> 16;
			num++;
			if (num > 2146435071)
			{
				num = 2146435071;
			}
			return num;
		}

		// Token: 0x06003F02 RID: 16130 RVA: 0x000EA4EF File Offset: 0x000E86EF
		// Note: this type is marked as 'beforefieldinit'.
		static ThreadLocal()
		{
		}

		// Token: 0x04001A78 RID: 6776
		private Func<T> m_valueFactory;

		// Token: 0x04001A79 RID: 6777
		[ThreadStatic]
		private static ThreadLocal<T>.LinkedSlotVolatile[] ts_slotArray;

		// Token: 0x04001A7A RID: 6778
		[ThreadStatic]
		private static ThreadLocal<T>.FinalizationHelper ts_finalizationHelper;

		// Token: 0x04001A7B RID: 6779
		private int m_idComplement;

		// Token: 0x04001A7C RID: 6780
		private volatile bool m_initialized;

		// Token: 0x04001A7D RID: 6781
		private static ThreadLocal<T>.IdManager s_idManager = new ThreadLocal<T>.IdManager();

		// Token: 0x04001A7E RID: 6782
		private ThreadLocal<T>.LinkedSlot m_linkedSlot = new ThreadLocal<T>.LinkedSlot(null);

		// Token: 0x04001A7F RID: 6783
		private bool m_trackAllValues;

		// Token: 0x02000BFE RID: 3070
		private struct LinkedSlotVolatile
		{
			// Token: 0x0400364C RID: 13900
			internal volatile ThreadLocal<T>.LinkedSlot Value;
		}

		// Token: 0x02000BFF RID: 3071
		private sealed class LinkedSlot
		{
			// Token: 0x06006F90 RID: 28560 RVA: 0x0018059F File Offset: 0x0017E79F
			internal LinkedSlot(ThreadLocal<T>.LinkedSlotVolatile[] slotArray)
			{
				this.SlotArray = slotArray;
			}

			// Token: 0x0400364D RID: 13901
			internal volatile ThreadLocal<T>.LinkedSlot Next;

			// Token: 0x0400364E RID: 13902
			internal volatile ThreadLocal<T>.LinkedSlot Previous;

			// Token: 0x0400364F RID: 13903
			internal volatile ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04003650 RID: 13904
			internal T Value;
		}

		// Token: 0x02000C00 RID: 3072
		private class IdManager
		{
			// Token: 0x06006F91 RID: 28561 RVA: 0x001805B0 File Offset: 0x0017E7B0
			internal int GetId()
			{
				List<bool> freeIds = this.m_freeIds;
				int num2;
				lock (freeIds)
				{
					int num = this.m_nextIdToTry;
					while (num < this.m_freeIds.Count && !this.m_freeIds[num])
					{
						num++;
					}
					if (num == this.m_freeIds.Count)
					{
						this.m_freeIds.Add(false);
					}
					else
					{
						this.m_freeIds[num] = false;
					}
					this.m_nextIdToTry = num + 1;
					num2 = num;
				}
				return num2;
			}

			// Token: 0x06006F92 RID: 28562 RVA: 0x00180648 File Offset: 0x0017E848
			internal void ReturnId(int id)
			{
				List<bool> freeIds = this.m_freeIds;
				lock (freeIds)
				{
					this.m_freeIds[id] = true;
					if (id < this.m_nextIdToTry)
					{
						this.m_nextIdToTry = id;
					}
				}
			}

			// Token: 0x06006F93 RID: 28563 RVA: 0x001806A0 File Offset: 0x0017E8A0
			public IdManager()
			{
			}

			// Token: 0x04003651 RID: 13905
			private int m_nextIdToTry;

			// Token: 0x04003652 RID: 13906
			private List<bool> m_freeIds = new List<bool>();
		}

		// Token: 0x02000C01 RID: 3073
		private class FinalizationHelper
		{
			// Token: 0x06006F94 RID: 28564 RVA: 0x001806B3 File Offset: 0x0017E8B3
			internal FinalizationHelper(ThreadLocal<T>.LinkedSlotVolatile[] slotArray, bool trackAllValues)
			{
				this.SlotArray = slotArray;
				this.m_trackAllValues = trackAllValues;
			}

			// Token: 0x06006F95 RID: 28565 RVA: 0x001806CC File Offset: 0x0017E8CC
			protected override void Finalize()
			{
				try
				{
					ThreadLocal<T>.LinkedSlotVolatile[] slotArray = this.SlotArray;
					for (int i = 0; i < slotArray.Length; i++)
					{
						ThreadLocal<T>.LinkedSlot value = slotArray[i].Value;
						if (value != null)
						{
							if (this.m_trackAllValues)
							{
								value.SlotArray = null;
							}
							else
							{
								ThreadLocal<T>.IdManager s_idManager = ThreadLocal<T>.s_idManager;
								lock (s_idManager)
								{
									if (value.Next != null)
									{
										value.Next.Previous = value.Previous;
									}
									value.Previous.Next = value.Next;
								}
							}
						}
					}
				}
				finally
				{
					base.Finalize();
				}
			}

			// Token: 0x04003653 RID: 13907
			internal ThreadLocal<T>.LinkedSlotVolatile[] SlotArray;

			// Token: 0x04003654 RID: 13908
			private bool m_trackAllValues;
		}
	}
}
