using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Util;
using System.Threading;

namespace System.Security.Policy
{
	// Token: 0x02000376 RID: 886
	[ComVisible(true)]
	[Serializable]
	public sealed class HashMembershipCondition : ISerializable, IDeserializationCallback, IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IReportMatchMembershipCondition
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000A35B8 File Offset: 0x000A17B8
		private object InternalSyncObject
		{
			get
			{
				if (this.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref this.s_InternalSyncObject, obj, null);
				}
				return this.s_InternalSyncObject;
			}
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x000A35E7 File Offset: 0x000A17E7
		internal HashMembershipCondition()
		{
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x000A35F0 File Offset: 0x000A17F0
		private HashMembershipCondition(SerializationInfo info, StreamingContext context)
		{
			this.m_value = (byte[])info.GetValue("HashValue", typeof(byte[]));
			string text = (string)info.GetValue("HashAlgorithm", typeof(string));
			if (text != null)
			{
				this.m_hashAlg = HashAlgorithm.Create(text);
				return;
			}
			this.m_hashAlg = new SHA1Managed();
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x000A365C File Offset: 0x000A185C
		public HashMembershipCondition(HashAlgorithm hashAlg, byte[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (hashAlg == null)
			{
				throw new ArgumentNullException("hashAlg");
			}
			this.m_value = new byte[value.Length];
			Array.Copy(value, this.m_value, value.Length);
			this.m_hashAlg = hashAlg;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x000A36AF File Offset: 0x000A18AF
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("HashValue", this.HashValue);
			info.AddValue("HashAlgorithm", this.HashAlgorithm.ToString());
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x000A36D8 File Offset: 0x000A18D8
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06002BF3 RID: 11251 RVA: 0x000A36F1 File Offset: 0x000A18F1
		// (set) Token: 0x06002BF2 RID: 11250 RVA: 0x000A36DA File Offset: 0x000A18DA
		public HashAlgorithm HashAlgorithm
		{
			get
			{
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				return this.m_hashAlg;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("HashAlgorithm");
				}
				this.m_hashAlg = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x000A373C File Offset: 0x000A193C
		// (set) Token: 0x06002BF4 RID: 11252 RVA: 0x000A370F File Offset: 0x000A190F
		public byte[] HashValue
		{
			get
			{
				if (this.m_value == null && this.m_element != null)
				{
					this.ParseHashValue();
				}
				if (this.m_value == null)
				{
					return null;
				}
				byte[] array = new byte[this.m_value.Length];
				Array.Copy(this.m_value, array, this.m_value.Length);
				return array;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_value = new byte[value.Length];
				Array.Copy(value, this.m_value, value.Length);
			}
		}

		// Token: 0x06002BF6 RID: 11254 RVA: 0x000A378C File Offset: 0x000A198C
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000A37A4 File Offset: 0x000A19A4
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Hash hostEvidence = evidence.GetHostEvidence<Hash>();
			if (hostEvidence != null)
			{
				if (this.m_value == null && this.m_element != null)
				{
					this.ParseHashValue();
				}
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				byte[] array = null;
				object internalSyncObject = this.InternalSyncObject;
				lock (internalSyncObject)
				{
					array = hostEvidence.GenerateHash(this.m_hashAlg);
				}
				if (array != null && HashMembershipCondition.CompareArrays(array, this.m_value))
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000A3844 File Offset: 0x000A1A44
		public IMembershipCondition Copy()
		{
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			return new HashMembershipCondition(this.m_hashAlg, this.m_value);
		}

		// Token: 0x06002BF9 RID: 11257 RVA: 0x000A3883 File Offset: 0x000A1A83
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002BFA RID: 11258 RVA: 0x000A388C File Offset: 0x000A1A8C
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002BFB RID: 11259 RVA: 0x000A3898 File Offset: 0x000A1A98
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.HashMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_value != null)
			{
				securityElement.AddAttribute("HashValue", Hex.EncodeHexString(this.HashValue));
			}
			if (this.m_hashAlg != null)
			{
				securityElement.AddAttribute("HashAlgorithm", this.HashAlgorithm.GetType().FullName);
			}
			return securityElement;
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000A3940 File Offset: 0x000A1B40
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				this.m_element = e;
				this.m_value = null;
				this.m_hashAlg = null;
			}
		}

		// Token: 0x06002BFD RID: 11261 RVA: 0x000A39C0 File Offset: 0x000A1BC0
		public override bool Equals(object o)
		{
			HashMembershipCondition hashMembershipCondition = o as HashMembershipCondition;
			if (hashMembershipCondition != null)
			{
				if (this.m_hashAlg == null && this.m_element != null)
				{
					this.ParseHashAlgorithm();
				}
				if (hashMembershipCondition.m_hashAlg == null && hashMembershipCondition.m_element != null)
				{
					hashMembershipCondition.ParseHashAlgorithm();
				}
				if (this.m_hashAlg != null && hashMembershipCondition.m_hashAlg != null && this.m_hashAlg.GetType() == hashMembershipCondition.m_hashAlg.GetType())
				{
					if (this.m_value == null && this.m_element != null)
					{
						this.ParseHashValue();
					}
					if (hashMembershipCondition.m_value == null && hashMembershipCondition.m_element != null)
					{
						hashMembershipCondition.ParseHashValue();
					}
					if (this.m_value.Length != hashMembershipCondition.m_value.Length)
					{
						return false;
					}
					for (int i = 0; i < this.m_value.Length; i++)
					{
						if (this.m_value[i] != hashMembershipCondition.m_value[i])
						{
							return false;
						}
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x000A3AA4 File Offset: 0x000A1CA4
		public override int GetHashCode()
		{
			if (this.m_hashAlg == null && this.m_element != null)
			{
				this.ParseHashAlgorithm();
			}
			int num = ((this.m_hashAlg != null) ? this.m_hashAlg.GetType().GetHashCode() : 0);
			if (this.m_value == null && this.m_element != null)
			{
				this.ParseHashValue();
			}
			return num ^ HashMembershipCondition.GetByteArrayHashCode(this.m_value);
		}

		// Token: 0x06002BFF RID: 11263 RVA: 0x000A3B08 File Offset: 0x000A1D08
		public override string ToString()
		{
			if (this.m_hashAlg == null)
			{
				this.ParseHashAlgorithm();
			}
			return Environment.GetResourceString("Hash_ToString", new object[]
			{
				this.m_hashAlg.GetType().AssemblyQualifiedName,
				Hex.EncodeHexString(this.HashValue)
			});
		}

		// Token: 0x06002C00 RID: 11264 RVA: 0x000A3B54 File Offset: 0x000A1D54
		private void ParseHashValue()
		{
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("HashValue");
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_InvalidXMLElement", new object[]
						{
							"HashValue",
							base.GetType().FullName
						}));
					}
					this.m_value = Hex.DecodeHexString(text);
					if (this.m_value != null && this.m_hashAlg != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002C01 RID: 11265 RVA: 0x000A3C00 File Offset: 0x000A1E00
		private void ParseHashAlgorithm()
		{
			object internalSyncObject = this.InternalSyncObject;
			lock (internalSyncObject)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("HashAlgorithm");
					if (text != null)
					{
						this.m_hashAlg = HashAlgorithm.Create(text);
					}
					else
					{
						this.m_hashAlg = new SHA1Managed();
					}
					if (this.m_value != null && this.m_hashAlg != null)
					{
						this.m_element = null;
					}
				}
			}
		}

		// Token: 0x06002C02 RID: 11266 RVA: 0x000A3C88 File Offset: 0x000A1E88
		private static bool CompareArrays(byte[] first, byte[] second)
		{
			if (first.Length != second.Length)
			{
				return false;
			}
			int num = first.Length;
			for (int i = 0; i < num; i++)
			{
				if (first[i] != second[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000A3CBC File Offset: 0x000A1EBC
		private static int GetByteArrayHashCode(byte[] baData)
		{
			if (baData == null)
			{
				return 0;
			}
			int num = 0;
			for (int i = 0; i < baData.Length; i++)
			{
				num = (num << 8) ^ (int)baData[i] ^ (num >> 24);
			}
			return num;
		}

		// Token: 0x040011B4 RID: 4532
		private byte[] m_value;

		// Token: 0x040011B5 RID: 4533
		private HashAlgorithm m_hashAlg;

		// Token: 0x040011B6 RID: 4534
		private SecurityElement m_element;

		// Token: 0x040011B7 RID: 4535
		private object s_InternalSyncObject;

		// Token: 0x040011B8 RID: 4536
		private const string s_tagHashValue = "HashValue";

		// Token: 0x040011B9 RID: 4537
		private const string s_tagHashAlgorithm = "HashAlgorithm";
	}
}
