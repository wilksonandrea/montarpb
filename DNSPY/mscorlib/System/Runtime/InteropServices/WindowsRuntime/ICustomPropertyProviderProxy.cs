using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A11 RID: 2577
	internal class ICustomPropertyProviderProxy<T1, T2> : IGetProxyTarget, ICustomPropertyProvider, ICustomQueryInterface, IEnumerable, IBindableVector, IBindableIterable, IBindableVectorView
	{
		// Token: 0x0600658F RID: 25999 RVA: 0x00159394 File Offset: 0x00157594
		internal ICustomPropertyProviderProxy(object target, InterfaceForwardingSupport flags)
		{
			this._target = target;
			this._flags = flags;
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x001593AC File Offset: 0x001575AC
		internal static object CreateInstance(object target)
		{
			InterfaceForwardingSupport interfaceForwardingSupport = InterfaceForwardingSupport.None;
			if (target is IList)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableVector;
			}
			if (target is IList<T1>)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IVector;
			}
			if (target is IBindableVectorView)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableVectorView;
			}
			if (target is IReadOnlyList<T2>)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IVectorView;
			}
			if (target is IEnumerable)
			{
				interfaceForwardingSupport |= InterfaceForwardingSupport.IBindableIterableOrIIterable;
			}
			return new ICustomPropertyProviderProxy<T1, T2>(target, interfaceForwardingSupport);
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x001593FF File Offset: 0x001575FF
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._target, name);
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x0015940D File Offset: 0x0015760D
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._target, name, indexParameterType);
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x0015941C File Offset: 0x0015761C
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return IStringableHelper.ToString(this._target);
		}

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06006594 RID: 26004 RVA: 0x00159429 File Offset: 0x00157629
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._target.GetType();
			}
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x00159436 File Offset: 0x00157636
		public override string ToString()
		{
			return IStringableHelper.ToString(this._target);
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x00159443 File Offset: 0x00157643
		object IGetProxyTarget.GetTarget()
		{
			return this._target;
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0015944C File Offset: 0x0015764C
		[SecurityCritical]
		public CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv)
		{
			ppv = IntPtr.Zero;
			if (iid == typeof(IBindableIterable).GUID && (this._flags & InterfaceForwardingSupport.IBindableIterableOrIIterable) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			if (iid == typeof(IBindableVector).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVector | InterfaceForwardingSupport.IVector)) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			if (iid == typeof(IBindableVectorView).GUID && (this._flags & (InterfaceForwardingSupport.IBindableVectorView | InterfaceForwardingSupport.IVectorView)) == InterfaceForwardingSupport.None)
			{
				return CustomQueryInterfaceResult.Failed;
			}
			return CustomQueryInterfaceResult.NotHandled;
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x001594DB File Offset: 0x001576DB
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)this._target).GetEnumerator();
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x001594F0 File Offset: 0x001576F0
		object IBindableVector.GetAt(uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.GetAt(index);
			}
			return this.GetVectorOfT().GetAt(index);
		}

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x0600659A RID: 26010 RVA: 0x00159520 File Offset: 0x00157720
		uint IBindableVector.Size
		{
			get
			{
				IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
				if (ibindableVectorNoThrow != null)
				{
					return ibindableVectorNoThrow.Size;
				}
				return this.GetVectorOfT().Size;
			}
		}

		// Token: 0x0600659B RID: 26011 RVA: 0x0015954C File Offset: 0x0015774C
		IBindableVectorView IBindableVector.GetView()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.GetView();
			}
			return new ICustomPropertyProviderProxy<T1, T2>.IVectorViewToIBindableVectorViewAdapter<T1>(this.GetVectorOfT().GetView());
		}

		// Token: 0x0600659C RID: 26012 RVA: 0x0015957C File Offset: 0x0015777C
		bool IBindableVector.IndexOf(object value, out uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				return ibindableVectorNoThrow.IndexOf(value, out index);
			}
			return this.GetVectorOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value), out index);
		}

		// Token: 0x0600659D RID: 26013 RVA: 0x001595B0 File Offset: 0x001577B0
		void IBindableVector.SetAt(uint index, object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.SetAt(index, value);
				return;
			}
			this.GetVectorOfT().SetAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x0600659E RID: 26014 RVA: 0x001595E4 File Offset: 0x001577E4
		void IBindableVector.InsertAt(uint index, object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.InsertAt(index, value);
				return;
			}
			this.GetVectorOfT().InsertAt(index, ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x0600659F RID: 26015 RVA: 0x00159618 File Offset: 0x00157818
		void IBindableVector.RemoveAt(uint index)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.RemoveAt(index);
				return;
			}
			this.GetVectorOfT().RemoveAt(index);
		}

		// Token: 0x060065A0 RID: 26016 RVA: 0x00159644 File Offset: 0x00157844
		void IBindableVector.Append(object value)
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.Append(value);
				return;
			}
			this.GetVectorOfT().Append(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T1>(value));
		}

		// Token: 0x060065A1 RID: 26017 RVA: 0x00159674 File Offset: 0x00157874
		void IBindableVector.RemoveAtEnd()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.RemoveAtEnd();
				return;
			}
			this.GetVectorOfT().RemoveAtEnd();
		}

		// Token: 0x060065A2 RID: 26018 RVA: 0x001596A0 File Offset: 0x001578A0
		void IBindableVector.Clear()
		{
			IBindableVector ibindableVectorNoThrow = this.GetIBindableVectorNoThrow();
			if (ibindableVectorNoThrow != null)
			{
				ibindableVectorNoThrow.Clear();
				return;
			}
			this.GetVectorOfT().Clear();
		}

		// Token: 0x060065A3 RID: 26019 RVA: 0x001596C9 File Offset: 0x001578C9
		[SecuritySafeCritical]
		private IBindableVector GetIBindableVectorNoThrow()
		{
			if ((this._flags & InterfaceForwardingSupport.IBindableVector) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IBindableVector>(this._target);
			}
			return null;
		}

		// Token: 0x060065A4 RID: 26020 RVA: 0x001596E2 File Offset: 0x001578E2
		[SecuritySafeCritical]
		private IVector_Raw<T1> GetVectorOfT()
		{
			if ((this._flags & InterfaceForwardingSupport.IVector) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IVector_Raw<T1>>(this._target);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060065A5 RID: 26021 RVA: 0x00159700 File Offset: 0x00157900
		object IBindableVectorView.GetAt(uint index)
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.GetAt(index);
			}
			return this.GetVectorViewOfT().GetAt(index);
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x060065A6 RID: 26022 RVA: 0x00159730 File Offset: 0x00157930
		uint IBindableVectorView.Size
		{
			get
			{
				IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
				if (ibindableVectorViewNoThrow != null)
				{
					return ibindableVectorViewNoThrow.Size;
				}
				return this.GetVectorViewOfT().Size;
			}
		}

		// Token: 0x060065A7 RID: 26023 RVA: 0x0015975C File Offset: 0x0015795C
		bool IBindableVectorView.IndexOf(object value, out uint index)
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.IndexOf(value, out index);
			}
			return this.GetVectorViewOfT().IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T2>(value), out index);
		}

		// Token: 0x060065A8 RID: 26024 RVA: 0x00159790 File Offset: 0x00157990
		IBindableIterator IBindableIterable.First()
		{
			IBindableVectorView ibindableVectorViewNoThrow = this.GetIBindableVectorViewNoThrow();
			if (ibindableVectorViewNoThrow != null)
			{
				return ibindableVectorViewNoThrow.First();
			}
			return new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T2>(this.GetVectorViewOfT().First());
		}

		// Token: 0x060065A9 RID: 26025 RVA: 0x001597BE File Offset: 0x001579BE
		[SecuritySafeCritical]
		private IBindableVectorView GetIBindableVectorViewNoThrow()
		{
			if ((this._flags & InterfaceForwardingSupport.IBindableVectorView) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IBindableVectorView>(this._target);
			}
			return null;
		}

		// Token: 0x060065AA RID: 26026 RVA: 0x001597D7 File Offset: 0x001579D7
		[SecuritySafeCritical]
		private IVectorView<T2> GetVectorViewOfT()
		{
			if ((this._flags & InterfaceForwardingSupport.IVectorView) != InterfaceForwardingSupport.None)
			{
				return JitHelpers.UnsafeCast<IVectorView<T2>>(this._target);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060065AB RID: 26027 RVA: 0x001597F4 File Offset: 0x001579F4
		private static T ConvertTo<T>(object value)
		{
			ThrowHelper.IfNullAndNullsAreIllegalThenThrow<T>(value, ExceptionArgument.value);
			return (T)((object)value);
		}

		// Token: 0x04002D49 RID: 11593
		private object _target;

		// Token: 0x04002D4A RID: 11594
		private InterfaceForwardingSupport _flags;

		// Token: 0x02000CAA RID: 3242
		private sealed class IVectorViewToIBindableVectorViewAdapter<T> : IBindableVectorView, IBindableIterable
		{
			// Token: 0x06007154 RID: 29012 RVA: 0x00185D1F File Offset: 0x00183F1F
			public IVectorViewToIBindableVectorViewAdapter(IVectorView<T> vectorView)
			{
				this._vectorView = vectorView;
			}

			// Token: 0x06007155 RID: 29013 RVA: 0x00185D2E File Offset: 0x00183F2E
			object IBindableVectorView.GetAt(uint index)
			{
				return this._vectorView.GetAt(index);
			}

			// Token: 0x1700136C RID: 4972
			// (get) Token: 0x06007156 RID: 29014 RVA: 0x00185D41 File Offset: 0x00183F41
			uint IBindableVectorView.Size
			{
				get
				{
					return this._vectorView.Size;
				}
			}

			// Token: 0x06007157 RID: 29015 RVA: 0x00185D4E File Offset: 0x00183F4E
			bool IBindableVectorView.IndexOf(object value, out uint index)
			{
				return this._vectorView.IndexOf(ICustomPropertyProviderProxy<T1, T2>.ConvertTo<T>(value), out index);
			}

			// Token: 0x06007158 RID: 29016 RVA: 0x00185D62 File Offset: 0x00183F62
			IBindableIterator IBindableIterable.First()
			{
				return new ICustomPropertyProviderProxy<T1, T2>.IteratorOfTToIteratorAdapter<T>(this._vectorView.First());
			}

			// Token: 0x04003892 RID: 14482
			private IVectorView<T> _vectorView;
		}

		// Token: 0x02000CAB RID: 3243
		private sealed class IteratorOfTToIteratorAdapter<T> : IBindableIterator
		{
			// Token: 0x06007159 RID: 29017 RVA: 0x00185D74 File Offset: 0x00183F74
			public IteratorOfTToIteratorAdapter(IIterator<T> iterator)
			{
				this._iterator = iterator;
			}

			// Token: 0x1700136D RID: 4973
			// (get) Token: 0x0600715A RID: 29018 RVA: 0x00185D83 File Offset: 0x00183F83
			public bool HasCurrent
			{
				get
				{
					return this._iterator.HasCurrent;
				}
			}

			// Token: 0x1700136E RID: 4974
			// (get) Token: 0x0600715B RID: 29019 RVA: 0x00185D90 File Offset: 0x00183F90
			public object Current
			{
				get
				{
					return this._iterator.Current;
				}
			}

			// Token: 0x0600715C RID: 29020 RVA: 0x00185DA2 File Offset: 0x00183FA2
			public bool MoveNext()
			{
				return this._iterator.MoveNext();
			}

			// Token: 0x04003893 RID: 14483
			private IIterator<T> _iterator;
		}
	}
}
