using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008E6 RID: 2278
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x06005DEA RID: 24042 RVA: 0x0014988A File Offset: 0x00147A8A
		private RuntimeWrappedException(object thrownObject)
			: base(Environment.GetResourceString("RuntimeWrappedException"))
		{
			base.SetErrorCode(-2146233026);
			this.m_wrappedException = thrownObject;
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x001498AE File Offset: 0x00147AAE
		public object WrappedException
		{
			get
			{
				return this.m_wrappedException;
			}
		}

		// Token: 0x06005DEC RID: 24044 RVA: 0x001498B6 File Offset: 0x00147AB6
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this.m_wrappedException, typeof(object));
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x001498E9 File Offset: 0x00147AE9
		internal RuntimeWrappedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x04002A45 RID: 10821
		private object m_wrappedException;
	}
}
