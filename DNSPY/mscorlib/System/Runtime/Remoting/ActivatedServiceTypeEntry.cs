using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C4 RID: 1988
	[ComVisible(true)]
	public class ActivatedServiceTypeEntry : TypeEntry
	{
		// Token: 0x06005602 RID: 22018 RVA: 0x00130F69 File Offset: 0x0012F169
		public ActivatedServiceTypeEntry(string typeName, string assemblyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x00130F9C File Offset: 0x0012F19C
		public ActivatedServiceTypeEntry(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeAssembly"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06005604 RID: 22020 RVA: 0x00131000 File Offset: 0x0012F200
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06005605 RID: 22021 RVA: 0x0013102C File Offset: 0x0012F22C
		// (set) Token: 0x06005606 RID: 22022 RVA: 0x00131034 File Offset: 0x0012F234
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

		// Token: 0x06005607 RID: 22023 RVA: 0x0013103D File Offset: 0x0012F23D
		public override string ToString()
		{
			return string.Concat(new string[] { "type='", base.TypeName, ", ", base.AssemblyName, "'" });
		}

		// Token: 0x0400278A RID: 10122
		private IContextAttribute[] _contextAttributes;
	}
}
