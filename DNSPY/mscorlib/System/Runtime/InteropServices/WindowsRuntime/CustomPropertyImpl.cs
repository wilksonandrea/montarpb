using System;
using System.Globalization;
using System.Reflection;
using System.Security;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A13 RID: 2579
	internal sealed class CustomPropertyImpl : ICustomProperty
	{
		// Token: 0x060065B4 RID: 26036 RVA: 0x00159804 File Offset: 0x00157A04
		public CustomPropertyImpl(PropertyInfo propertyInfo)
		{
			if (propertyInfo == null)
			{
				throw new ArgumentNullException("propertyInfo");
			}
			this.m_property = propertyInfo;
		}

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x060065B5 RID: 26037 RVA: 0x00159827 File Offset: 0x00157A27
		public string Name
		{
			get
			{
				return this.m_property.Name;
			}
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x060065B6 RID: 26038 RVA: 0x00159834 File Offset: 0x00157A34
		public bool CanRead
		{
			get
			{
				return this.m_property.GetGetMethod() != null;
			}
		}

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x060065B7 RID: 26039 RVA: 0x00159847 File Offset: 0x00157A47
		public bool CanWrite
		{
			get
			{
				return this.m_property.GetSetMethod() != null;
			}
		}

		// Token: 0x060065B8 RID: 26040 RVA: 0x0015985A File Offset: 0x00157A5A
		public object GetValue(object target)
		{
			return this.InvokeInternal(target, null, true);
		}

		// Token: 0x060065B9 RID: 26041 RVA: 0x00159865 File Offset: 0x00157A65
		public object GetValue(object target, object indexValue)
		{
			return this.InvokeInternal(target, new object[] { indexValue }, true);
		}

		// Token: 0x060065BA RID: 26042 RVA: 0x00159879 File Offset: 0x00157A79
		public void SetValue(object target, object value)
		{
			this.InvokeInternal(target, new object[] { value }, false);
		}

		// Token: 0x060065BB RID: 26043 RVA: 0x0015988E File Offset: 0x00157A8E
		public void SetValue(object target, object value, object indexValue)
		{
			this.InvokeInternal(target, new object[] { indexValue, value }, false);
		}

		// Token: 0x060065BC RID: 26044 RVA: 0x001598A8 File Offset: 0x00157AA8
		[SecuritySafeCritical]
		private object InvokeInternal(object target, object[] args, bool getValue)
		{
			IGetProxyTarget getProxyTarget = target as IGetProxyTarget;
			if (getProxyTarget != null)
			{
				target = getProxyTarget.GetTarget();
			}
			MethodInfo methodInfo = (getValue ? this.m_property.GetGetMethod(true) : this.m_property.GetSetMethod(true));
			if (methodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString(getValue ? "Arg_GetMethNotFnd" : "Arg_SetMethNotFnd"));
			}
			if (!methodInfo.IsPublic)
			{
				throw new MethodAccessException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Arg_MethodAccessException_WithMethodName"), methodInfo.ToString(), methodInfo.DeclaringType.FullName));
			}
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			if (runtimeMethodInfo == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeMethodInfo"));
			}
			return runtimeMethodInfo.UnsafeInvoke(target, BindingFlags.Default, null, args, null);
		}

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x060065BD RID: 26045 RVA: 0x00159966 File Offset: 0x00157B66
		public Type Type
		{
			get
			{
				return this.m_property.PropertyType;
			}
		}

		// Token: 0x04002D4B RID: 11595
		private PropertyInfo m_property;
	}
}
