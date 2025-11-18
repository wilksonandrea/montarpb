using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000923 RID: 2339
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		// Token: 0x06006014 RID: 24596 RVA: 0x0014B597 File Offset: 0x00149797
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this._val = sourceInterfaces;
		}

		// Token: 0x06006015 RID: 24597 RVA: 0x0014B5A6 File Offset: 0x001497A6
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this._val = sourceInterface.FullName;
		}

		// Token: 0x06006016 RID: 24598 RVA: 0x0014B5BA File Offset: 0x001497BA
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this._val = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		// Token: 0x06006017 RID: 24599 RVA: 0x0014B5E0 File Offset: 0x001497E0
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName });
		}

		// Token: 0x06006018 RID: 24600 RVA: 0x0014B630 File Offset: 0x00149830
		[__DynamicallyInvokable]
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this._val = string.Concat(new string[] { sourceInterface1.FullName, "\0", sourceInterface2.FullName, "\0", sourceInterface3.FullName, "\0", sourceInterface4.FullName });
		}

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06006019 RID: 24601 RVA: 0x0014B691 File Offset: 0x00149891
		[__DynamicallyInvokable]
		public string Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._val;
			}
		}

		// Token: 0x04002A83 RID: 10883
		internal string _val;
	}
}
