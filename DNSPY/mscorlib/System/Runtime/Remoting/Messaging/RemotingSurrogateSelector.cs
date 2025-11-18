using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200087D RID: 2173
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	public class RemotingSurrogateSelector : ISurrogateSelector
	{
		// Token: 0x06005C54 RID: 23636 RVA: 0x001435BF File Offset: 0x001417BF
		public RemotingSurrogateSelector()
		{
			this._messageSurrogate = new MessageSurrogate(this);
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06005C56 RID: 23638 RVA: 0x001435F2 File Offset: 0x001417F2
		// (set) Token: 0x06005C55 RID: 23637 RVA: 0x001435E9 File Offset: 0x001417E9
		public MessageSurrogateFilter Filter
		{
			get
			{
				return this._filter;
			}
			set
			{
				this._filter = value;
			}
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x001435FC File Offset: 0x001417FC
		public void SetRootObject(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			this._rootObj = obj;
			SoapMessageSurrogate soapMessageSurrogate = this._messageSurrogate as SoapMessageSurrogate;
			if (soapMessageSurrogate != null)
			{
				soapMessageSurrogate.SetRootObject(this._rootObj);
			}
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x00143639 File Offset: 0x00141839
		public object GetRootObject()
		{
			return this._rootObj;
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x00143641 File Offset: 0x00141841
		[SecurityCritical]
		public virtual void ChainSelector(ISurrogateSelector selector)
		{
			this._next = selector;
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x0014364C File Offset: 0x0014184C
		[SecurityCritical]
		public virtual ISerializationSurrogate GetSurrogate(Type type, StreamingContext context, out ISurrogateSelector ssout)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (type.IsMarshalByRef)
			{
				ssout = this;
				return this._remotingSurrogate;
			}
			if (RemotingSurrogateSelector.s_IMethodCallMessageType.IsAssignableFrom(type) || RemotingSurrogateSelector.s_IMethodReturnMessageType.IsAssignableFrom(type))
			{
				ssout = this;
				return this._messageSurrogate;
			}
			if (RemotingSurrogateSelector.s_ObjRefType.IsAssignableFrom(type))
			{
				ssout = this;
				return this._objRefSurrogate;
			}
			if (this._next != null)
			{
				return this._next.GetSurrogate(type, context, out ssout);
			}
			ssout = null;
			return null;
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x001436D5 File Offset: 0x001418D5
		[SecurityCritical]
		public virtual ISurrogateSelector GetNextSelector()
		{
			return this._next;
		}

		// Token: 0x06005C5C RID: 23644 RVA: 0x001436DD File Offset: 0x001418DD
		public virtual void UseSoapFormat()
		{
			this._messageSurrogate = new SoapMessageSurrogate(this);
			((SoapMessageSurrogate)this._messageSurrogate).SetRootObject(this._rootObj);
		}

		// Token: 0x06005C5D RID: 23645 RVA: 0x00143701 File Offset: 0x00141901
		// Note: this type is marked as 'beforefieldinit'.
		static RemotingSurrogateSelector()
		{
		}

		// Token: 0x040029B6 RID: 10678
		private static Type s_IMethodCallMessageType = typeof(IMethodCallMessage);

		// Token: 0x040029B7 RID: 10679
		private static Type s_IMethodReturnMessageType = typeof(IMethodReturnMessage);

		// Token: 0x040029B8 RID: 10680
		private static Type s_ObjRefType = typeof(ObjRef);

		// Token: 0x040029B9 RID: 10681
		private object _rootObj;

		// Token: 0x040029BA RID: 10682
		private ISurrogateSelector _next;

		// Token: 0x040029BB RID: 10683
		private RemotingSurrogate _remotingSurrogate = new RemotingSurrogate();

		// Token: 0x040029BC RID: 10684
		private ObjRefSurrogate _objRefSurrogate = new ObjRefSurrogate();

		// Token: 0x040029BD RID: 10685
		private ISerializationSurrogate _messageSurrogate;

		// Token: 0x040029BE RID: 10686
		private MessageSurrogateFilter _filter;
	}
}
