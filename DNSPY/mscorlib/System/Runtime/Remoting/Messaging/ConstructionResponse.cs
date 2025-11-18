using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000870 RID: 2160
	[SecurityCritical]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005BD4 RID: 23508 RVA: 0x00142005 File Offset: 0x00140205
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm)
			: base(h, mcm)
		{
		}

		// Token: 0x06005BD5 RID: 23509 RVA: 0x0014200F File Offset: 0x0014020F
		internal ConstructionResponse(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005BD6 RID: 23510 RVA: 0x0014201C File Offset: 0x0014021C
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new CRMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}
	}
}
