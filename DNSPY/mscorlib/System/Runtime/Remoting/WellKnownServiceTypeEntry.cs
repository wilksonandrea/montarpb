using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C6 RID: 1990
	[ComVisible(true)]
	public class WellKnownServiceTypeEntry : TypeEntry
	{
		// Token: 0x0600560F RID: 22031 RVA: 0x001311F4 File Offset: 0x0012F3F4
		public WellKnownServiceTypeEntry(string typeName, string assemblyName, string objectUri, WellKnownObjectMode mode)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x00131250 File Offset: 0x0012F450
		public WellKnownServiceTypeEntry(Type type, string objectUri, WellKnownObjectMode mode)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUri == null)
			{
				throw new ArgumentNullException("objectUri");
			}
			if (!(type is RuntimeType))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = type.Module.Assembly.FullName;
			this._objectUri = objectUri;
			this._mode = mode;
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06005611 RID: 22033 RVA: 0x001312CD File Offset: 0x0012F4CD
		public string ObjectUri
		{
			get
			{
				return this._objectUri;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06005612 RID: 22034 RVA: 0x001312D5 File Offset: 0x0012F4D5
		public WellKnownObjectMode Mode
		{
			get
			{
				return this._mode;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06005613 RID: 22035 RVA: 0x001312E0 File Offset: 0x0012F4E0
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06005614 RID: 22036 RVA: 0x0013130C File Offset: 0x0012F50C
		// (set) Token: 0x06005615 RID: 22037 RVA: 0x00131314 File Offset: 0x0012F514
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

		// Token: 0x06005616 RID: 22038 RVA: 0x00131320 File Offset: 0x0012F520
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"type='",
				base.TypeName,
				", ",
				base.AssemblyName,
				"'; objectUri=",
				this._objectUri,
				"; mode=",
				this._mode.ToString()
			});
		}

		// Token: 0x0400278D RID: 10125
		private string _objectUri;

		// Token: 0x0400278E RID: 10126
		private WellKnownObjectMode _mode;

		// Token: 0x0400278F RID: 10127
		private IContextAttribute[] _contextAttributes;
	}
}
