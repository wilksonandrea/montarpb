using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security.Claims
{
	// Token: 0x0200031E RID: 798
	[ComVisible(true)]
	[Serializable]
	public class ClaimsPrincipal : IPrincipal
	{
		// Token: 0x06002868 RID: 10344 RVA: 0x00093EE4 File Offset: 0x000920E4
		private static ClaimsIdentity SelectPrimaryIdentity(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			ClaimsIdentity claimsIdentity = null;
			foreach (ClaimsIdentity claimsIdentity2 in identities)
			{
				if (claimsIdentity2 is WindowsIdentity)
				{
					claimsIdentity = claimsIdentity2;
					break;
				}
				if (claimsIdentity == null)
				{
					claimsIdentity = claimsIdentity2;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x00093F48 File Offset: 0x00092148
		private static ClaimsPrincipal SelectClaimsPrincipal()
		{
			ClaimsPrincipal claimsPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;
			if (claimsPrincipal != null)
			{
				return claimsPrincipal;
			}
			return new ClaimsPrincipal(Thread.CurrentPrincipal);
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x0600286A RID: 10346 RVA: 0x00093F6F File Offset: 0x0009216F
		// (set) Token: 0x0600286B RID: 10347 RVA: 0x00093F76 File Offset: 0x00092176
		public static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> PrimaryIdentitySelector
		{
			get
			{
				return ClaimsPrincipal.s_identitySelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_identitySelector = value;
			}
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x0600286C RID: 10348 RVA: 0x00093F7E File Offset: 0x0009217E
		// (set) Token: 0x0600286D RID: 10349 RVA: 0x00093F85 File Offset: 0x00092185
		public static Func<ClaimsPrincipal> ClaimsPrincipalSelector
		{
			get
			{
				return ClaimsPrincipal.s_principalSelector;
			}
			[SecurityCritical]
			set
			{
				ClaimsPrincipal.s_principalSelector = value;
			}
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x00093F8D File Offset: 0x0009218D
		public ClaimsPrincipal()
		{
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x00093FAB File Offset: 0x000921AB
		public ClaimsPrincipal(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00093FE4 File Offset: 0x000921E4
		public ClaimsPrincipal(IIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_identities.Add(claimsIdentity);
				return;
			}
			this.m_identities.Add(new ClaimsIdentity(identity));
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00094044 File Offset: 0x00092244
		public ClaimsPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			ClaimsPrincipal claimsPrincipal = principal as ClaimsPrincipal;
			if (claimsPrincipal == null)
			{
				this.m_identities.Add(new ClaimsIdentity(principal.Identity));
				return;
			}
			if (claimsPrincipal.Identities != null)
			{
				this.m_identities.AddRange(claimsPrincipal.Identities);
			}
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x000940B5 File Offset: 0x000922B5
		public ClaimsPrincipal(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		// Token: 0x06002873 RID: 10355 RVA: 0x000940E8 File Offset: 0x000922E8
		[SecurityCritical]
		protected ClaimsPrincipal(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context);
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06002874 RID: 10356 RVA: 0x0009411C File Offset: 0x0009231C
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x06002875 RID: 10357 RVA: 0x00094124 File Offset: 0x00092324
		public virtual ClaimsPrincipal Clone()
		{
			return new ClaimsPrincipal(this);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0009412C File Offset: 0x0009232C
		protected virtual ClaimsIdentity CreateClaimsIdentity(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new ClaimsIdentity(reader);
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x00094142 File Offset: 0x00092342
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaimsIdentities = this.SerializeIdentities();
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x00094159 File Offset: 0x00092359
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.DeserializeIdentities(this.m_serializedClaimsIdentities);
			this.m_serializedClaimsIdentities = null;
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x00094177 File Offset: 0x00092377
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("System.Security.ClaimsPrincipal.Identities", this.SerializeIdentities());
			info.AddValue("System.Security.ClaimsPrincipal.Version", this.m_version);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x000941AC File Offset: 0x000923AC
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "System.Security.ClaimsPrincipal.Identities"))
				{
					if (name == "System.Security.ClaimsPrincipal.Version")
					{
						this.m_version = info.GetString("System.Security.ClaimsPrincipal.Version");
					}
				}
				else
				{
					this.DeserializeIdentities(info.GetString("System.Security.ClaimsPrincipal.Identities"));
				}
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x00094220 File Offset: 0x00092420
		[SecurityCritical]
		private void DeserializeIdentities(string identities)
		{
			this.m_identities = new List<ClaimsIdentity>();
			if (!string.IsNullOrEmpty(identities))
			{
				List<string> list = null;
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(identities)))
				{
					list = (List<string>)binaryFormatter.Deserialize(memoryStream, null, false);
					for (int i = 0; i < list.Count; i += 2)
					{
						ClaimsIdentity claimsIdentity = null;
						using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(list[i + 1])))
						{
							claimsIdentity = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream2, null, false);
						}
						if (!string.IsNullOrEmpty(list[i]))
						{
							long num;
							if (!long.TryParse(list[i], NumberStyles.Integer, NumberFormatInfo.InvariantInfo, out num))
							{
								throw new SerializationException(Environment.GetResourceString("Serialization_CorruptedStream"));
							}
							claimsIdentity = new WindowsIdentity(claimsIdentity, new IntPtr(num));
						}
						this.m_identities.Add(claimsIdentity);
					}
				}
			}
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x00094330 File Offset: 0x00092530
		[SecurityCritical]
		private string SerializeIdentities()
		{
			List<string> list = new List<string>();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			foreach (ClaimsIdentity claimsIdentity in this.m_identities)
			{
				if (claimsIdentity.GetType() == typeof(WindowsIdentity))
				{
					WindowsIdentity windowsIdentity = claimsIdentity as WindowsIdentity;
					list.Add(windowsIdentity.GetTokenInternal().ToInt64().ToString(NumberFormatInfo.InvariantInfo));
					using (MemoryStream memoryStream = new MemoryStream())
					{
						binaryFormatter.Serialize(memoryStream, windowsIdentity.CloneAsBase(), null, false);
						list.Add(Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
						continue;
					}
				}
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					list.Add("");
					binaryFormatter.Serialize(memoryStream2, claimsIdentity, null, false);
					list.Add(Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
			string text;
			using (MemoryStream memoryStream3 = new MemoryStream())
			{
				binaryFormatter.Serialize(memoryStream3, list, null, false);
				text = Convert.ToBase64String(memoryStream3.GetBuffer(), 0, (int)memoryStream3.Length);
			}
			return text;
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x000944B8 File Offset: 0x000926B8
		[SecurityCritical]
		public virtual void AddIdentity(ClaimsIdentity identity)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			this.m_identities.Add(identity);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x000944D4 File Offset: 0x000926D4
		[SecurityCritical]
		public virtual void AddIdentities(IEnumerable<ClaimsIdentity> identities)
		{
			if (identities == null)
			{
				throw new ArgumentNullException("identities");
			}
			this.m_identities.AddRange(identities);
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600287F RID: 10367 RVA: 0x000944F0 File Offset: 0x000926F0
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				foreach (ClaimsIdentity claimsIdentity in this.Identities)
				{
					foreach (Claim claim in claimsIdentity.Claims)
					{
						yield return claim;
					}
					IEnumerator<Claim> enumerator2 = null;
				}
				IEnumerator<ClaimsIdentity> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06002880 RID: 10368 RVA: 0x0009450D File Offset: 0x0009270D
		public static ClaimsPrincipal Current
		{
			get
			{
				if (ClaimsPrincipal.s_principalSelector != null)
				{
					return ClaimsPrincipal.s_principalSelector();
				}
				return ClaimsPrincipal.SelectClaimsPrincipal();
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x00094528 File Offset: 0x00092728
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim claim in claimsIdentity.FindAll(match))
					{
						list.Add(claim);
					}
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x000945CC File Offset: 0x000927CC
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					foreach (Claim claim in claimsIdentity.FindAll(type))
					{
						list.Add(claim);
					}
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x00094670 File Offset: 0x00092870
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			Claim claim = null;
			foreach (ClaimsIdentity claimsIdentity in this.Identities)
			{
				if (claimsIdentity != null)
				{
					claim = claimsIdentity.FindFirst(match);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x000946DC File Offset: 0x000928DC
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Claim claim = null;
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null)
				{
					claim = this.m_identities[i].FindFirst(type);
					if (claim != null)
					{
						return claim;
					}
				}
			}
			return claim;
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x00094738 File Offset: 0x00092938
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(match))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x00094790 File Offset: 0x00092990
		public virtual bool HasClaim(string type, string value)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(type, value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06002887 RID: 10375 RVA: 0x000947F5 File Offset: 0x000929F5
		public virtual IEnumerable<ClaimsIdentity> Identities
		{
			get
			{
				return this.m_identities.AsReadOnly();
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06002888 RID: 10376 RVA: 0x00094802 File Offset: 0x00092A02
		public virtual IIdentity Identity
		{
			get
			{
				if (ClaimsPrincipal.s_identitySelector != null)
				{
					return ClaimsPrincipal.s_identitySelector(this.m_identities);
				}
				return ClaimsPrincipal.SelectPrimaryIdentity(this.m_identities);
			}
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x00094828 File Offset: 0x00092A28
		public virtual bool IsInRole(string role)
		{
			for (int i = 0; i < this.m_identities.Count; i++)
			{
				if (this.m_identities[i] != null && this.m_identities[i].HasClaim(this.m_identities[i].RoleClaimType, role))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x00094884 File Offset: 0x00092A84
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			ClaimsPrincipal.SerializationMask serializationMask = (ClaimsPrincipal.SerializationMask)reader.ReadInt32();
			int num = reader.ReadInt32();
			int num2 = 0;
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				num2++;
				int num3 = reader.ReadInt32();
				for (int i = 0; i < num3; i++)
				{
					this.m_identities.Add(this.CreateClaimsIdentity(reader));
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				int num4 = reader.ReadInt32();
				this.m_userSerializationData = reader.ReadBytes(num4);
				num2++;
			}
			for (int j = num2; j < num; j++)
			{
				reader.ReadString();
			}
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x00094919 File Offset: 0x00092B19
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x00094924 File Offset: 0x00092B24
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsPrincipal.SerializationMask serializationMask = ClaimsPrincipal.SerializationMask.None;
			if (this.m_identities.Count > 0)
			{
				serializationMask |= ClaimsPrincipal.SerializationMask.HasIdentities;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsPrincipal.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsPrincipal.SerializationMask.HasIdentities) == ClaimsPrincipal.SerializationMask.HasIdentities)
			{
				writer.Write(this.m_identities.Count);
				foreach (ClaimsIdentity claimsIdentity in this.m_identities)
				{
					claimsIdentity.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsPrincipal.SerializationMask.UserData) == ClaimsPrincipal.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x000949F0 File Offset: 0x00092BF0
		// Note: this type is marked as 'beforefieldinit'.
		static ClaimsPrincipal()
		{
		}

		// Token: 0x04000FA6 RID: 4006
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04000FA7 RID: 4007
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsPrincipal.";

		// Token: 0x04000FA8 RID: 4008
		[NonSerialized]
		private const string IdentitiesKey = "System.Security.ClaimsPrincipal.Identities";

		// Token: 0x04000FA9 RID: 4009
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsPrincipal.Version";

		// Token: 0x04000FAA RID: 4010
		[OptionalField(VersionAdded = 2)]
		private string m_version = "1.0";

		// Token: 0x04000FAB RID: 4011
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaimsIdentities;

		// Token: 0x04000FAC RID: 4012
		[NonSerialized]
		private List<ClaimsIdentity> m_identities = new List<ClaimsIdentity>();

		// Token: 0x04000FAD RID: 4013
		[NonSerialized]
		private static Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity> s_identitySelector = new Func<IEnumerable<ClaimsIdentity>, ClaimsIdentity>(ClaimsPrincipal.SelectPrimaryIdentity);

		// Token: 0x04000FAE RID: 4014
		[NonSerialized]
		private static Func<ClaimsPrincipal> s_principalSelector = ClaimsPrincipal.ClaimsPrincipalSelector;

		// Token: 0x02000B56 RID: 2902
		private enum SerializationMask
		{
			// Token: 0x04003403 RID: 13315
			None,
			// Token: 0x04003404 RID: 13316
			HasIdentities,
			// Token: 0x04003405 RID: 13317
			UserData
		}

		// Token: 0x02000B57 RID: 2903
		[CompilerGenerated]
		private sealed class <get_Claims>d__37 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x06006BBC RID: 27580 RVA: 0x001749EF File Offset: 0x00172BEF
			[DebuggerHidden]
			public <get_Claims>d__37(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006BBD RID: 27581 RVA: 0x00174A0C File Offset: 0x00172C0C
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num - -4 <= 1 || num == 1)
				{
					try
					{
						if (num == -4 || num == 1)
						{
							try
							{
							}
							finally
							{
								this.<>m__Finally2();
							}
						}
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06006BBE RID: 27582 RVA: 0x00174A64 File Offset: 0x00172C64
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					ClaimsPrincipal claimsPrincipal = this;
					if (num == 0)
					{
						this.<>1__state = -1;
						enumerator = claimsPrincipal.Identities.GetEnumerator();
						this.<>1__state = -3;
						goto IL_A7;
					}
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -4;
					IL_8D:
					if (enumerator2.MoveNext())
					{
						Claim claim = enumerator2.Current;
						this.<>2__current = claim;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally2();
					enumerator2 = null;
					IL_A7:
					if (enumerator.MoveNext())
					{
						ClaimsIdentity claimsIdentity = enumerator.Current;
						enumerator2 = claimsIdentity.Claims.GetEnumerator();
						this.<>1__state = -4;
						goto IL_8D;
					}
					this.<>m__Finally1();
					enumerator = null;
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06006BBF RID: 27583 RVA: 0x00174B50 File Offset: 0x00172D50
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x06006BC0 RID: 27584 RVA: 0x00174B6C File Offset: 0x00172D6C
			private void <>m__Finally2()
			{
				this.<>1__state = -3;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x1700122B RID: 4651
			// (get) Token: 0x06006BC1 RID: 27585 RVA: 0x00174B89 File Offset: 0x00172D89
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BC2 RID: 27586 RVA: 0x00174B91 File Offset: 0x00172D91
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700122C RID: 4652
			// (get) Token: 0x06006BC3 RID: 27587 RVA: 0x00174B98 File Offset: 0x00172D98
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BC4 RID: 27588 RVA: 0x00174BA0 File Offset: 0x00172DA0
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				ClaimsPrincipal.<get_Claims>d__37 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new ClaimsPrincipal.<get_Claims>d__37(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x06006BC5 RID: 27589 RVA: 0x00174BE3 File Offset: 0x00172DE3
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x04003406 RID: 13318
			private int <>1__state;

			// Token: 0x04003407 RID: 13319
			private Claim <>2__current;

			// Token: 0x04003408 RID: 13320
			private int <>l__initialThreadId;

			// Token: 0x04003409 RID: 13321
			public ClaimsPrincipal <>4__this;

			// Token: 0x0400340A RID: 13322
			private IEnumerator<ClaimsIdentity> <>7__wrap1;

			// Token: 0x0400340B RID: 13323
			private IEnumerator<Claim> <>7__wrap2;
		}
	}
}
