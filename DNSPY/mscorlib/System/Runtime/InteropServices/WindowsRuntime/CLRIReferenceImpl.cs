using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A03 RID: 2563
	internal sealed class CLRIReferenceImpl<T> : CLRIPropertyValueImpl, IReference<T>, IPropertyValue, ICustomPropertyProvider
	{
		// Token: 0x06006536 RID: 25910 RVA: 0x00158A4C File Offset: 0x00156C4C
		public CLRIReferenceImpl(PropertyType type, T obj)
			: base(type, obj)
		{
			this._value = obj;
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06006537 RID: 25911 RVA: 0x00158A62 File Offset: 0x00156C62
		public T Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06006538 RID: 25912 RVA: 0x00158A6A File Offset: 0x00156C6A
		public override string ToString()
		{
			if (this._value != null)
			{
				return this._value.ToString();
			}
			return base.ToString();
		}

		// Token: 0x06006539 RID: 25913 RVA: 0x00158A91 File Offset: 0x00156C91
		ICustomProperty ICustomPropertyProvider.GetCustomProperty(string name)
		{
			return ICustomPropertyProviderImpl.CreateProperty(this._value, name);
		}

		// Token: 0x0600653A RID: 25914 RVA: 0x00158AA4 File Offset: 0x00156CA4
		ICustomProperty ICustomPropertyProvider.GetIndexedProperty(string name, Type indexParameterType)
		{
			return ICustomPropertyProviderImpl.CreateIndexedProperty(this._value, name, indexParameterType);
		}

		// Token: 0x0600653B RID: 25915 RVA: 0x00158AB8 File Offset: 0x00156CB8
		string ICustomPropertyProvider.GetStringRepresentation()
		{
			return this._value.ToString();
		}

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x0600653C RID: 25916 RVA: 0x00158ACA File Offset: 0x00156CCA
		Type ICustomPropertyProvider.Type
		{
			get
			{
				return this._value.GetType();
			}
		}

		// Token: 0x0600653D RID: 25917 RVA: 0x00158ADC File Offset: 0x00156CDC
		[FriendAccessAllowed]
		internal static object UnboxHelper(object wrapper)
		{
			IReference<T> reference = (IReference<T>)wrapper;
			return reference.Value;
		}

		// Token: 0x04002D3C RID: 11580
		private T _value;
	}
}
