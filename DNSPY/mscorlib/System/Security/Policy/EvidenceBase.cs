using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x0200034E RID: 846
	[ComVisible(true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	[Serializable]
	public abstract class EvidenceBase
	{
		// Token: 0x06002A40 RID: 10816 RVA: 0x0009C8D0 File Offset: 0x0009AAD0
		protected EvidenceBase()
		{
			if (!base.GetType().IsSerializable)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Policy_EvidenceMustBeSerializable"));
			}
		}

		// Token: 0x06002A41 RID: 10817 RVA: 0x0009C8F8 File Offset: 0x0009AAF8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		public virtual EvidenceBase Clone()
		{
			EvidenceBase evidenceBase;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, this);
				memoryStream.Position = 0L;
				evidenceBase = binaryFormatter.Deserialize(memoryStream) as EvidenceBase;
			}
			return evidenceBase;
		}
	}
}
