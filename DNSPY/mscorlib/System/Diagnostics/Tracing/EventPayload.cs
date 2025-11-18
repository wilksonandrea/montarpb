using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000446 RID: 1094
	internal class EventPayload : IDictionary<string, object>, ICollection<KeyValuePair<string, object>>, IEnumerable<KeyValuePair<string, object>>, IEnumerable
	{
		// Token: 0x06003611 RID: 13841 RVA: 0x000D2235 File Offset: 0x000D0435
		internal EventPayload(List<string> payloadNames, List<object> payloadValues)
		{
			this.m_names = payloadNames;
			this.m_values = payloadValues;
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x06003612 RID: 13842 RVA: 0x000D224B File Offset: 0x000D044B
		public ICollection<string> Keys
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06003613 RID: 13843 RVA: 0x000D2253 File Offset: 0x000D0453
		public ICollection<object> Values
		{
			get
			{
				return this.m_values;
			}
		}

		// Token: 0x17000800 RID: 2048
		public object this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				int num = 0;
				foreach (string text in this.m_names)
				{
					if (text == key)
					{
						return this.m_values[num];
					}
					num++;
				}
				throw new KeyNotFoundException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000D22E3 File Offset: 0x000D04E3
		public void Add(string key, object value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000D22EA File Offset: 0x000D04EA
		public void Add(KeyValuePair<string, object> payloadEntry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000D22F1 File Offset: 0x000D04F1
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000D22F8 File Offset: 0x000D04F8
		public bool Contains(KeyValuePair<string, object> entry)
		{
			return this.ContainsKey(entry.Key);
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000D2308 File Offset: 0x000D0508
		public bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			foreach (string text in this.m_names)
			{
				if (text == key)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x0600361B RID: 13851 RVA: 0x000D2374 File Offset: 0x000D0574
		public int Count
		{
			get
			{
				return this.m_names.Count;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x0600361C RID: 13852 RVA: 0x000D2381 File Offset: 0x000D0581
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000D2384 File Offset: 0x000D0584
		public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
		{
			int num;
			for (int i = 0; i < this.Keys.Count; i = num + 1)
			{
				yield return new KeyValuePair<string, object>(this.m_names[i], this.m_values[i]);
				num = i;
			}
			yield break;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000D2394 File Offset: 0x000D0594
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<string, object>>)this).GetEnumerator();
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000D23A9 File Offset: 0x000D05A9
		public void CopyTo(KeyValuePair<string, object>[] payloadEntries, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000D23B0 File Offset: 0x000D05B0
		public bool Remove(string key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000D23B7 File Offset: 0x000D05B7
		public bool Remove(KeyValuePair<string, object> entry)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000D23C0 File Offset: 0x000D05C0
		public bool TryGetValue(string key, out object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			int num = 0;
			foreach (string text in this.m_names)
			{
				if (text == key)
				{
					value = this.m_values[num];
					return true;
				}
				num++;
			}
			value = null;
			return false;
		}

		// Token: 0x04001830 RID: 6192
		private List<string> m_names;

		// Token: 0x04001831 RID: 6193
		private List<object> m_values;

		// Token: 0x02000B9E RID: 2974
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__17 : IEnumerator<KeyValuePair<string, object>>, IDisposable, IEnumerator
		{
			// Token: 0x06006CA0 RID: 27808 RVA: 0x00177D2A File Offset: 0x00175F2A
			[DebuggerHidden]
			public <GetEnumerator>d__17(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06006CA1 RID: 27809 RVA: 0x00177D39 File Offset: 0x00175F39
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006CA2 RID: 27810 RVA: 0x00177D3C File Offset: 0x00175F3C
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				EventPayload eventPayload = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					int num2 = i;
					i = num2 + 1;
				}
				else
				{
					this.<>1__state = -1;
					i = 0;
				}
				if (i >= eventPayload.Keys.Count)
				{
					return false;
				}
				this.<>2__current = new KeyValuePair<string, object>(eventPayload.m_names[i], eventPayload.m_values[i]);
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17001260 RID: 4704
			// (get) Token: 0x06006CA3 RID: 27811 RVA: 0x00177DD1 File Offset: 0x00175FD1
			KeyValuePair<string, object> IEnumerator<KeyValuePair<string, object>>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006CA4 RID: 27812 RVA: 0x00177DD9 File Offset: 0x00175FD9
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001261 RID: 4705
			// (get) Token: 0x06006CA5 RID: 27813 RVA: 0x00177DE0 File Offset: 0x00175FE0
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x04003536 RID: 13622
			private int <>1__state;

			// Token: 0x04003537 RID: 13623
			private KeyValuePair<string, object> <>2__current;

			// Token: 0x04003538 RID: 13624
			public EventPayload <>4__this;

			// Token: 0x04003539 RID: 13625
			private int <i>5__2;
		}
	}
}
