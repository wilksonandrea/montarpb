using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.Remoting
{
	// Token: 0x020007C5 RID: 1989
	[ComVisible(true)]
	public class WellKnownClientTypeEntry : TypeEntry
	{
		// Token: 0x06005608 RID: 22024 RVA: 0x00131074 File Offset: 0x0012F274
		public WellKnownClientTypeEntry(string typeName, string assemblyName, string objectUrl)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			base.TypeName = typeName;
			base.AssemblyName = assemblyName;
			this._objectUrl = objectUrl;
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x001310C8 File Offset: 0x0012F2C8
		public WellKnownClientTypeEntry(Type type, string objectUrl)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (objectUrl == null)
			{
				throw new ArgumentNullException("objectUrl");
			}
			RuntimeType runtimeType = type as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MustBeRuntimeType"));
			}
			base.TypeName = type.FullName;
			base.AssemblyName = runtimeType.GetRuntimeAssembly().GetSimpleName();
			this._objectUrl = objectUrl;
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x0600560A RID: 22026 RVA: 0x00131141 File Offset: 0x0012F341
		public string ObjectUrl
		{
			get
			{
				return this._objectUrl;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x0600560B RID: 22027 RVA: 0x0013114C File Offset: 0x0012F34C
		public Type ObjectType
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				return RuntimeTypeHandle.GetTypeByName(base.TypeName + ", " + base.AssemblyName, ref stackCrawlMark);
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600560C RID: 22028 RVA: 0x00131178 File Offset: 0x0012F378
		// (set) Token: 0x0600560D RID: 22029 RVA: 0x00131180 File Offset: 0x0012F380
		public string ApplicationUrl
		{
			get
			{
				return this._appUrl;
			}
			set
			{
				this._appUrl = value;
			}
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x0013118C File Offset: 0x0012F38C
		public override string ToString()
		{
			string text = string.Concat(new string[] { "type='", base.TypeName, ", ", base.AssemblyName, "'; url=", this._objectUrl });
			if (this._appUrl != null)
			{
				text = text + "; appUrl=" + this._appUrl;
			}
			return text;
		}

		// Token: 0x0400278B RID: 10123
		private string _objectUrl;

		// Token: 0x0400278C RID: 10124
		private string _appUrl;
	}
}
