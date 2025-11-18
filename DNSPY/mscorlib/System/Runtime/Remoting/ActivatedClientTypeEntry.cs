using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C3 RID: 1987
	[ComVisible(true)]
	public class ActivatedClientTypeEntry : TypeEntry
	{
		// Token: 0x060055FB RID: 22011 RVA: 0x00130E14 File Offset: 0x0012F014
		public ActivatedClientTypeEntry(string typeName, string assemblyName, string appUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._appUrl = appUrl;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x00130E68 File Offset: 0x0012F068
		public ActivatedClientTypeEntry(Type type, string appUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (appUrl == null)
			{
				throw new ArgumentNullException("appUrl");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
			this._appUrl = appUrl;
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x060055FD RID: 22013 RVA: 0x00130EE1 File Offset: 0x0012F0E1
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060055FE RID: 22014 RVA: 0x00130EEC File Offset: 0x0012F0EC
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060055FF RID: 22015 RVA: 0x00130F18 File Offset: 0x0012F118
		// (set) Token: 0x06005600 RID: 22016 RVA: 0x00130F20 File Offset: 0x0012F120
		public IContextAttribute[] ContextAttributes
		{
			get
			{
				return this._contextAttributes;
			}
			set
			{
				this._contextAttributes = value;
			}
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x00130F29 File Offset: 0x0012F129
		public override string ToString()
		{
			return string.Concat(new string[] { "type='", base.TypeName, ", ", base.AssemblyName, "'; appUrl=", this._appUrl });
		}

		// Token: 0x04002788 RID: 10120
		private string _appUrl;

		// Token: 0x04002789 RID: 10121
		private IContextAttribute[] _contextAttributes;
	}
}
