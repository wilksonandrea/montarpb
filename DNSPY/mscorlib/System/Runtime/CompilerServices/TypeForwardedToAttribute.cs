using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E2 RID: 2274
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class TypeForwardedToAttribute : Attribute
	{
		// Token: 0x06005DDE RID: 24030 RVA: 0x001497C3 File Offset: 0x001479C3
		[__DynamicallyInvokable]
		public TypeForwardedToAttribute(Type destination)
		{
			this._destination = destination;
		}

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x06005DDF RID: 24031 RVA: 0x001497D2 File Offset: 0x001479D2
		[__DynamicallyInvokable]
		public Type Destination
		{
			[__DynamicallyInvokable]
			get
			{
				return this._destination;
			}
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x001497DC File Offset: 0x001479DC
		[SecurityCritical]
		internal static TypeForwardedToAttribute[] GetCustomAttribute(RuntimeAssembly assembly)
		{
			Type[] array = null;
			RuntimeAssembly.GetForwardedTypes(assembly.GetNativeHandle(), JitHelpers.GetObjectHandleOnStack<Type[]>(ref array));
			TypeForwardedToAttribute[] array2 = new TypeForwardedToAttribute[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new TypeForwardedToAttribute(array[i]);
			}
			return array2;
		}

		// Token: 0x04002A41 RID: 10817
		private Type _destination;
	}
}
