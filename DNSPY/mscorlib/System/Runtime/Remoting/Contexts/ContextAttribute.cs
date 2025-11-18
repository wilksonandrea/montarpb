using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x02000811 RID: 2065
	[SecurityCritical]
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ContextAttribute : Attribute, IContextAttribute, IContextProperty
	{
		// Token: 0x060058CF RID: 22735 RVA: 0x00138C83 File Offset: 0x00136E83
		public ContextAttribute(string name)
		{
			this.AttributeName = name;
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x060058D0 RID: 22736 RVA: 0x00138C92 File Offset: 0x00136E92
		public virtual string Name
		{
			[SecurityCritical]
			get
			{
				return this.AttributeName;
			}
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x00138C9A File Offset: 0x00136E9A
		[SecurityCritical]
		public virtual bool IsNewContextOK(Context newCtx)
		{
			return true;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x00138C9D File Offset: 0x00136E9D
		[SecurityCritical]
		public virtual void Freeze(Context newContext)
		{
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x00138CA0 File Offset: 0x00136EA0
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			IContextProperty contextProperty = o as IContextProperty;
			return contextProperty != null && this.AttributeName.Equals(contextProperty.Name);
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x00138CCA File Offset: 0x00136ECA
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			return this.AttributeName.GetHashCode();
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x00138CD8 File Offset: 0x00136ED8
		[SecurityCritical]
		public virtual bool IsContextOK(Context ctx, IConstructionCallMessage ctorMsg)
		{
			if (ctx == null)
			{
				throw new ArgumentNullException("ctx");
			}
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			if (!ctorMsg.ActivationType.IsContextful)
			{
				return true;
			}
			object property = ctx.GetProperty(this.AttributeName);
			return property != null && this.Equals(property);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x00138D2C File Offset: 0x00136F2C
		[SecurityCritical]
		public virtual void GetPropertiesForNewContext(IConstructionCallMessage ctorMsg)
		{
			if (ctorMsg == null)
			{
				throw new ArgumentNullException("ctorMsg");
			}
			ctorMsg.ContextProperties.Add(this);
		}

		// Token: 0x0400287C RID: 10364
		protected string AttributeName;
	}
}
