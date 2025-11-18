using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Security.AccessControl
{
	// Token: 0x0200022B RID: 555
	[Serializable]
	public sealed class PrivilegeNotHeldException : UnauthorizedAccessException, ISerializable
	{
		// Token: 0x06002015 RID: 8213 RVA: 0x00070CAE File Offset: 0x0006EEAE
		public PrivilegeNotHeldException()
			: base(Environment.GetResourceString("PrivilegeNotHeld_Default"))
		{
		}

		// Token: 0x06002016 RID: 8214 RVA: 0x00070CC0 File Offset: 0x0006EEC0
		public PrivilegeNotHeldException(string privilege)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), privilege))
		{
			this._privilegeName = privilege;
		}

		// Token: 0x06002017 RID: 8215 RVA: 0x00070CE4 File Offset: 0x0006EEE4
		public PrivilegeNotHeldException(string privilege, Exception inner)
			: base(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("PrivilegeNotHeld_Named"), privilege), inner)
		{
			this._privilegeName = privilege;
		}

		// Token: 0x06002018 RID: 8216 RVA: 0x00070D09 File Offset: 0x0006EF09
		internal PrivilegeNotHeldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._privilegeName = info.GetString("PrivilegeName");
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x00070D24 File Offset: 0x0006EF24
		public string PrivilegeName
		{
			get
			{
				return this._privilegeName;
			}
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x00070D2C File Offset: 0x0006EF2C
		[SecurityCritical]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("PrivilegeName", this._privilegeName, typeof(string));
		}

		// Token: 0x04000B90 RID: 2960
		private readonly string _privilegeName;
	}
}
