using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200091B RID: 2331
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[ComVisible(true)]
	public sealed class TypeLibImportClassAttribute : Attribute
	{
		// Token: 0x06006007 RID: 24583 RVA: 0x0014B500 File Offset: 0x00149700
		public TypeLibImportClassAttribute(Type importClass)
		{
			this._importClassName = importClass.ToString();
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06006008 RID: 24584 RVA: 0x0014B514 File Offset: 0x00149714
		public string Value
		{
			get
			{
				return this._importClassName;
			}
		}

		// Token: 0x04002A7A RID: 10874
		internal string _importClassName;
	}
}
