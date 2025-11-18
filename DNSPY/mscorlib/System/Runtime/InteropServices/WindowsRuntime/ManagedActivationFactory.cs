using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x020009F4 RID: 2548
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	internal sealed class ManagedActivationFactory : IActivationFactory, IManagedActivationFactory
	{
		// Token: 0x060064C5 RID: 25797 RVA: 0x0015709C File Offset: 0x0015529C
		[SecurityCritical]
		internal ManagedActivationFactory(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!(type is RuntimeType) || !type.IsExportedToWindowsRuntime)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_TypeNotActivatableViaWindowsRuntime", new object[] { type }), "type");
			}
			this.m_type = type;
		}

		// Token: 0x060064C6 RID: 25798 RVA: 0x001570FC File Offset: 0x001552FC
		public object ActivateInstance()
		{
			object obj;
			try
			{
				obj = Activator.CreateInstance(this.m_type);
			}
			catch (MissingMethodException)
			{
				throw new NotImplementedException();
			}
			catch (TargetInvocationException ex)
			{
				throw ex.InnerException;
			}
			return obj;
		}

		// Token: 0x060064C7 RID: 25799 RVA: 0x00157144 File Offset: 0x00155344
		void IManagedActivationFactory.RunClassConstructor()
		{
			RuntimeHelpers.RunClassConstructor(this.m_type.TypeHandle);
		}

		// Token: 0x04002D04 RID: 11524
		private Type m_type;
	}
}
