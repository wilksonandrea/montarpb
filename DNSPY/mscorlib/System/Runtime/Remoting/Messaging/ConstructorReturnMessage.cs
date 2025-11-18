using System;
using System.Collections;
using System.Runtime.Remoting.Activation;
using System.Security;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000861 RID: 2145
	[SecurityCritical]
	internal class ConstructorReturnMessage : ReturnMessage, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x06005ADB RID: 23259 RVA: 0x0013E9F7 File Offset: 0x0013CBF7
		public ConstructorReturnMessage(MarshalByRefObject o, object[] outArgs, int outArgsCount, LogicalCallContext callCtx, IConstructionCallMessage ccm)
			: base(o, outArgs, outArgsCount, callCtx, ccm)
		{
			this._o = o;
			this._iFlags = 1;
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x0013EA14 File Offset: 0x0013CC14
		public ConstructorReturnMessage(Exception e, IConstructionCallMessage ccm)
			: base(e, ccm)
		{
		}

		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06005ADD RID: 23261 RVA: 0x0013EA1E File Offset: 0x0013CC1E
		public override object ReturnValue
		{
			[SecurityCritical]
			get
			{
				if (this._iFlags == 1)
				{
					return RemotingServices.MarshalInternal(this._o, null, null);
				}
				return base.ReturnValue;
			}
		}

		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06005ADE RID: 23262 RVA: 0x0013EA40 File Offset: 0x0013CC40
		public override IDictionary Properties
		{
			[SecurityCritical]
			get
			{
				if (this._properties == null)
				{
					object obj = new CRMDictionary(this, new Hashtable());
					Interlocked.CompareExchange(ref this._properties, obj, null);
				}
				return (IDictionary)this._properties;
			}
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x0013EA7A File Offset: 0x0013CC7A
		internal object GetObject()
		{
			return this._o;
		}

		// Token: 0x04002931 RID: 10545
		private const int Intercept = 1;

		// Token: 0x04002932 RID: 10546
		private MarshalByRefObject _o;

		// Token: 0x04002933 RID: 10547
		private int _iFlags;
	}
}
