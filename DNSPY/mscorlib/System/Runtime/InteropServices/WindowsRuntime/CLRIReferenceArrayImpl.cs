using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A04 RID: 2564
	internal sealed class CLRIReferenceArrayImpl<T> : CLRIPropertyValueImpl, IReferenceArray<T>, IPropertyValue, ICustomPropertyProvider, IList, ICollection, IEnumerable
	{
		// Token: 0x0600653E RID: 25918 RVA: 0x00158AFB File Offset: 0x00156CFB
		public CLRIReferenceArrayImpl(PropertyType type, T[] obj)
			: base(type, obj)
		{
			this._value = obj;
			this._list = this._value;
		}

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x0600653F RID: 25919 RVA: 0x00158B18 File Offset: 0x00156D18
		public T[] Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06006540 RID: 25920 RVA: 0x00158B20 File Offset: 0x00156D20
		public override string ToString()
		{
			if (this._value != null)
			{
				return this._value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x06006541 RID: 25921 RVA: 0x00158B3C File Offset: 0x00156D3C
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._value, name);
		}

		// Token: 0x06006542 RID: 25922 RVA: 0x00158B4A File Offset: 0x00156D4A
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._value, name, indexParameterType);
		}

		// Token: 0x06006543 RID: 25923 RVA: 0x00158B59 File Offset: 0x00156D59
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return this._value.ToString();
		}

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06006544 RID: 25924 RVA: 0x00158B66 File Offset: 0x00156D66
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._value.GetType();
			}
		}

		// Token: 0x06006545 RID: 25925 RVA: 0x00158B73 File Offset: 0x00156D73
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._value.GetEnumerator();
		}

		// Token: 0x17001165 RID: 4453
		object IList.this[int index]
		{
			get
			{
				return this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		// Token: 0x06006548 RID: 25928 RVA: 0x00158B9D File Offset: 0x00156D9D
		int IList.Add(object value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06006549 RID: 25929 RVA: 0x00158BAB File Offset: 0x00156DAB
		bool IList.Contains(object value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x0600654A RID: 25930 RVA: 0x00158BB9 File Offset: 0x00156DB9
		void IList.Clear()
		{
			this._list.Clear();
		}

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x0600654B RID: 25931 RVA: 0x00158BC6 File Offset: 0x00156DC6
		bool IList.IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x0600654C RID: 25932 RVA: 0x00158BD3 File Offset: 0x00156DD3
		bool IList.IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x0600654D RID: 25933 RVA: 0x00158BE0 File Offset: 0x00156DE0
		int IList.IndexOf(object value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x0600654E RID: 25934 RVA: 0x00158BEE File Offset: 0x00156DEE
		void IList.Insert(int index, object value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x0600654F RID: 25935 RVA: 0x00158BFD File Offset: 0x00156DFD
		void IList.Remove(object value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06006550 RID: 25936 RVA: 0x00158C0B File Offset: 0x00156E0B
		void IList.RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x06006551 RID: 25937 RVA: 0x00158C19 File Offset: 0x00156E19
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06006552 RID: 25938 RVA: 0x00158C28 File Offset: 0x00156E28
		int ICollection.Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06006553 RID: 25939 RVA: 0x00158C35 File Offset: 0x00156E35
		object ICollection.SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06006554 RID: 25940 RVA: 0x00158C42 File Offset: 0x00156E42
		bool ICollection.IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x00158C50 File Offset: 0x00156E50
		[FriendAccessAllowed]
		internal static object UnboxHelper(object wrapper)
		{
			IReferenceArray<T> referenceArray = (IReferenceArray<T>)wrapper;
			return referenceArray.Value;
		}

		// Token: 0x04002D3D RID: 11581
		private T[] _value;

		// Token: 0x04002D3E RID: 11582
		private IList _list;
	}
}
