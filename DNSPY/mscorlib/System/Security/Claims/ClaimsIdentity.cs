using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.Claims
{
	// Token: 0x0200031D RID: 797
	[ComVisible(true)]
	[Serializable]
	public class ClaimsIdentity : IIdentity
	{
		// Token: 0x06002833 RID: 10291 RVA: 0x00092C59 File Offset: 0x00090E59
		public ClaimsIdentity()
			: this(null)
		{
		}

		// Token: 0x06002834 RID: 10292 RVA: 0x00092C62 File Offset: 0x00090E62
		public ClaimsIdentity(IIdentity identity)
			: this(identity, null)
		{
		}

		// Token: 0x06002835 RID: 10293 RVA: 0x00092C6C File Offset: 0x00090E6C
		public ClaimsIdentity(IEnumerable<Claim> claims)
			: this(null, claims, null, null, null)
		{
		}

		// Token: 0x06002836 RID: 10294 RVA: 0x00092C79 File Offset: 0x00090E79
		public ClaimsIdentity(string authenticationType)
			: this(null, null, authenticationType, null, null)
		{
		}

		// Token: 0x06002837 RID: 10295 RVA: 0x00092C86 File Offset: 0x00090E86
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType)
			: this(null, claims, authenticationType, null, null)
		{
		}

		// Token: 0x06002838 RID: 10296 RVA: 0x00092C93 File Offset: 0x00090E93
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims)
			: this(identity, claims, null, null, null)
		{
		}

		// Token: 0x06002839 RID: 10297 RVA: 0x00092CA0 File Offset: 0x00090EA0
		public ClaimsIdentity(string authenticationType, string nameType, string roleType)
			: this(null, null, authenticationType, nameType, roleType)
		{
		}

		// Token: 0x0600283A RID: 10298 RVA: 0x00092CAD File Offset: 0x00090EAD
		public ClaimsIdentity(IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
			: this(null, claims, authenticationType, nameType, roleType)
		{
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x00092CBB File Offset: 0x00090EBB
		public ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType)
			: this(identity, claims, authenticationType, nameType, roleType, true)
		{
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x00092CCC File Offset: 0x00090ECC
		internal ClaimsIdentity(IIdentity identity, IEnumerable<Claim> claims, string authenticationType, string nameType, string roleType, bool checkAuthType)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			bool flag = false;
			bool flag2 = false;
			if (checkAuthType && identity != null && string.IsNullOrEmpty(authenticationType))
			{
				if (identity is WindowsIdentity)
				{
					try
					{
						this.m_authenticationType = identity.AuthenticationType;
						goto IL_85;
					}
					catch (UnauthorizedAccessException)
					{
						this.m_authenticationType = null;
						goto IL_85;
					}
				}
				this.m_authenticationType = identity.AuthenticationType;
			}
			else
			{
				this.m_authenticationType = authenticationType;
			}
			IL_85:
			if (!string.IsNullOrEmpty(nameType))
			{
				this.m_nameType = nameType;
				flag = true;
			}
			if (!string.IsNullOrEmpty(roleType))
			{
				this.m_roleType = roleType;
				flag2 = true;
			}
			ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
			if (claimsIdentity != null)
			{
				this.m_label = claimsIdentity.m_label;
				if (!flag)
				{
					this.m_nameType = claimsIdentity.m_nameType;
				}
				if (!flag2)
				{
					this.m_roleType = claimsIdentity.m_roleType;
				}
				this.m_bootstrapContext = claimsIdentity.m_bootstrapContext;
				if (claimsIdentity.Actor != null)
				{
					if (this.IsCircular(claimsIdentity.Actor))
					{
						throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
					}
					if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
					{
						this.m_actor = claimsIdentity.Actor.Clone();
					}
					else
					{
						this.m_actor = claimsIdentity.Actor;
					}
				}
				if (claimsIdentity is WindowsIdentity && !(this is WindowsIdentity))
				{
					this.SafeAddClaims(claimsIdentity.Claims);
				}
				else
				{
					this.SafeAddClaims(claimsIdentity.m_instanceClaims);
				}
				if (claimsIdentity.m_userSerializationData != null)
				{
					this.m_userSerializationData = claimsIdentity.m_userSerializationData.Clone() as byte[];
				}
			}
			else if (identity != null && !string.IsNullOrEmpty(identity.Name))
			{
				this.SafeAddClaim(new Claim(this.m_nameType, identity.Name, "http://www.w3.org/2001/XMLSchema#string", "LOCAL AUTHORITY", "LOCAL AUTHORITY", this));
			}
			if (claims != null)
			{
				this.SafeAddClaims(claims);
			}
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00092EB0 File Offset: 0x000910B0
		public ClaimsIdentity(BinaryReader reader)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Initialize(reader);
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x00092F10 File Offset: 0x00091110
		protected ClaimsIdentity(ClaimsIdentity other)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			if (other.m_actor != null)
			{
				this.m_actor = other.m_actor.Clone();
			}
			this.m_authenticationType = other.m_authenticationType;
			this.m_bootstrapContext = other.m_bootstrapContext;
			this.m_label = other.m_label;
			this.m_nameType = other.m_nameType;
			this.m_roleType = other.m_roleType;
			if (other.m_userSerializationData != null)
			{
				this.m_userSerializationData = other.m_userSerializationData.Clone() as byte[];
			}
			this.SafeAddClaims(other.m_instanceClaims);
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x00092FE8 File Offset: 0x000911E8
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info, StreamingContext context)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, context, true);
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0009304C File Offset: 0x0009124C
		[SecurityCritical]
		protected ClaimsIdentity(SerializationInfo info)
		{
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
			this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			this.m_version = "1.0";
			base..ctor();
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.Deserialize(info, default(StreamingContext), false);
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x000930B5 File Offset: 0x000912B5
		public virtual string AuthenticationType
		{
			get
			{
				return this.m_authenticationType;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x000930BD File Offset: 0x000912BD
		public virtual bool IsAuthenticated
		{
			get
			{
				return !string.IsNullOrEmpty(this.m_authenticationType);
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x000930CD File Offset: 0x000912CD
		// (set) Token: 0x06002844 RID: 10308 RVA: 0x000930D5 File Offset: 0x000912D5
		public ClaimsIdentity Actor
		{
			get
			{
				return this.m_actor;
			}
			set
			{
				if (value != null && this.IsCircular(value))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
				}
				this.m_actor = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x000930FA File Offset: 0x000912FA
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x00093102 File Offset: 0x00091302
		public object BootstrapContext
		{
			get
			{
				return this.m_bootstrapContext;
			}
			[SecurityCritical]
			set
			{
				this.m_bootstrapContext = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x0009310C File Offset: 0x0009130C
		public virtual IEnumerable<Claim> Claims
		{
			get
			{
				int num;
				for (int i = 0; i < this.m_instanceClaims.Count; i = num + 1)
				{
					yield return this.m_instanceClaims[i];
					num = i;
				}
				if (this.m_externalClaims != null)
				{
					for (int i = 0; i < this.m_externalClaims.Count; i = num + 1)
					{
						if (this.m_externalClaims[i] != null)
						{
							foreach (Claim claim in this.m_externalClaims[i])
							{
								yield return claim;
							}
							IEnumerator<Claim> enumerator = null;
						}
						num = i;
					}
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x00093129 File Offset: 0x00091329
		protected virtual byte[] CustomSerializationData
		{
			get
			{
				return this.m_userSerializationData;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06002849 RID: 10313 RVA: 0x00093131 File Offset: 0x00091331
		internal Collection<IEnumerable<Claim>> ExternalClaims
		{
			[FriendAccessAllowed]
			get
			{
				return this.m_externalClaims;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x00093139 File Offset: 0x00091339
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x00093141 File Offset: 0x00091341
		public string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x0009314C File Offset: 0x0009134C
		public virtual string Name
		{
			get
			{
				Claim claim = this.FindFirst(this.m_nameType);
				if (claim != null)
				{
					return claim.Value;
				}
				return null;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x00093171 File Offset: 0x00091371
		public string NameClaimType
		{
			get
			{
				return this.m_nameType;
			}
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x00093179 File Offset: 0x00091379
		public string RoleClaimType
		{
			get
			{
				return this.m_roleType;
			}
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00093184 File Offset: 0x00091384
		public virtual ClaimsIdentity Clone()
		{
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(this.m_instanceClaims);
			claimsIdentity.m_authenticationType = this.m_authenticationType;
			claimsIdentity.m_bootstrapContext = this.m_bootstrapContext;
			claimsIdentity.m_label = this.m_label;
			claimsIdentity.m_nameType = this.m_nameType;
			claimsIdentity.m_roleType = this.m_roleType;
			if (this.Actor != null)
			{
				if (this.IsCircular(this.Actor))
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperationException_ActorGraphCircular"));
				}
				if (!AppContextSwitches.SetActorAsReferenceWhenCopyingClaimsIdentity)
				{
					claimsIdentity.Actor = this.Actor.Clone();
				}
				else
				{
					claimsIdentity.Actor = this.Actor;
				}
			}
			return claimsIdentity;
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00093228 File Offset: 0x00091428
		[SecurityCritical]
		public virtual void AddClaim(Claim claim)
		{
			if (claim == null)
			{
				throw new ArgumentNullException("claim");
			}
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00093260 File Offset: 0x00091460
		[SecurityCritical]
		public virtual void AddClaims(IEnumerable<Claim> claims)
		{
			if (claims == null)
			{
				throw new ArgumentNullException("claims");
			}
			foreach (Claim claim in claims)
			{
				if (claim != null)
				{
					this.AddClaim(claim);
				}
			}
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x000932BC File Offset: 0x000914BC
		[SecurityCritical]
		public virtual bool TryRemoveClaim(Claim claim)
		{
			bool flag = false;
			for (int i = 0; i < this.m_instanceClaims.Count; i++)
			{
				if (this.m_instanceClaims[i] == claim)
				{
					this.m_instanceClaims.RemoveAt(i);
					flag = true;
					break;
				}
			}
			return flag;
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00093301 File Offset: 0x00091501
		[SecurityCritical]
		public virtual void RemoveClaim(Claim claim)
		{
			if (!this.TryRemoveClaim(claim))
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ClaimCannotBeRemoved", new object[] { claim }));
			}
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00093328 File Offset: 0x00091528
		[SecuritySafeCritical]
		private void SafeAddClaims(IEnumerable<Claim> claims)
		{
			foreach (Claim claim in claims)
			{
				if (claim.Subject == this)
				{
					this.m_instanceClaims.Add(claim);
				}
				else
				{
					this.m_instanceClaims.Add(claim.Clone(this));
				}
			}
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00093394 File Offset: 0x00091594
		[SecuritySafeCritical]
		private void SafeAddClaim(Claim claim)
		{
			if (claim.Subject == this)
			{
				this.m_instanceClaims.Add(claim);
				return;
			}
			this.m_instanceClaims.Add(claim.Clone(this));
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x000933C0 File Offset: 0x000915C0
		public virtual IEnumerable<Claim> FindAll(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00093430 File Offset: 0x00091630
		public virtual IEnumerable<Claim> FindAll(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<Claim> list = new List<Claim>();
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					list.Add(claim);
				}
			}
			return list.AsReadOnly();
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x000934AC File Offset: 0x000916AC
		public virtual bool HasClaim(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00093510 File Offset: 0x00091710
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
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase) && string.Equals(claim.Value, value, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x000935A0 File Offset: 0x000917A0
		public virtual Claim FindFirst(Predicate<Claim> match)
		{
			if (match == null)
			{
				throw new ArgumentNullException("match");
			}
			foreach (Claim claim in this.Claims)
			{
				if (match(claim))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00093604 File Offset: 0x00091804
		public virtual Claim FindFirst(string type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (Claim claim in this.Claims)
			{
				if (claim != null && string.Equals(claim.Type, type, StringComparison.OrdinalIgnoreCase))
				{
					return claim;
				}
			}
			return null;
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00093674 File Offset: 0x00091874
		[OnSerializing]
		[SecurityCritical]
		private void OnSerializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_serializedClaims = this.SerializeClaims();
			this.m_serializedNameType = this.m_nameType;
			this.m_serializedRoleType = this.m_roleType;
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x000936A4 File Offset: 0x000918A4
		[OnDeserialized]
		[SecurityCritical]
		private void OnDeserializedMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.m_serializedClaims))
			{
				this.DeserializeClaims(this.m_serializedClaims);
				this.m_serializedClaims = null;
			}
			this.m_nameType = (string.IsNullOrEmpty(this.m_serializedNameType) ? "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name" : this.m_serializedNameType);
			this.m_roleType = (string.IsNullOrEmpty(this.m_serializedRoleType) ? "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" : this.m_serializedRoleType);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x0009371A File Offset: 0x0009191A
		[OnDeserializing]
		private void OnDeserializingMethod(StreamingContext context)
		{
			if (this is ISerializable)
			{
				return;
			}
			this.m_instanceClaims = new List<Claim>();
			this.m_externalClaims = new Collection<IEnumerable<Claim>>();
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x0009373C File Offset: 0x0009193C
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			info.AddValue("System.Security.ClaimsIdentity.version", this.m_version);
			if (!string.IsNullOrEmpty(this.m_authenticationType))
			{
				info.AddValue("System.Security.ClaimsIdentity.authenticationType", this.m_authenticationType);
			}
			info.AddValue("System.Security.ClaimsIdentity.nameClaimType", this.m_nameType);
			info.AddValue("System.Security.ClaimsIdentity.roleClaimType", this.m_roleType);
			if (!string.IsNullOrEmpty(this.m_label))
			{
				info.AddValue("System.Security.ClaimsIdentity.label", this.m_label);
			}
			if (this.m_actor != null)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream, this.m_actor, null, false);
					info.AddValue("System.Security.ClaimsIdentity.actor", Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length));
				}
			}
			info.AddValue("System.Security.ClaimsIdentity.claims", this.SerializeClaims());
			if (this.m_bootstrapContext != null)
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					binaryFormatter.Serialize(memoryStream2, this.m_bootstrapContext, null, false);
					info.AddValue("System.Security.ClaimsIdentity.bootstrapContext", Convert.ToBase64String(memoryStream2.GetBuffer(), 0, (int)memoryStream2.Length));
				}
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x00093888 File Offset: 0x00091A88
		[SecurityCritical]
		private void DeserializeClaims(string serializedClaims)
		{
			if (!string.IsNullOrEmpty(serializedClaims))
			{
				using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(serializedClaims)))
				{
					this.m_instanceClaims = (List<Claim>)new BinaryFormatter().Deserialize(memoryStream, null, false);
					for (int i = 0; i < this.m_instanceClaims.Count; i++)
					{
						this.m_instanceClaims[i].Subject = this;
					}
				}
			}
			if (this.m_instanceClaims == null)
			{
				this.m_instanceClaims = new List<Claim>();
			}
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x00093918 File Offset: 0x00091B18
		[SecurityCritical]
		private string SerializeClaims()
		{
			string text;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_instanceClaims, null, false);
				text = Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return text;
		}

		// Token: 0x06002862 RID: 10338 RVA: 0x00093970 File Offset: 0x00091B70
		private bool IsCircular(ClaimsIdentity subject)
		{
			if (this == subject)
			{
				return true;
			}
			ClaimsIdentity claimsIdentity = subject;
			while (claimsIdentity.Actor != null)
			{
				if (this == claimsIdentity.Actor)
				{
					return true;
				}
				claimsIdentity = claimsIdentity.Actor;
			}
			return false;
		}

		// Token: 0x06002863 RID: 10339 RVA: 0x000939A4 File Offset: 0x00091BA4
		private void Initialize(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			ClaimsIdentity.SerializationMask serializationMask = (ClaimsIdentity.SerializationMask)reader.ReadInt32();
			if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
			{
				this.m_authenticationType = reader.ReadString();
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
			{
				this.m_bootstrapContext = reader.ReadString();
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
			{
				this.m_nameType = reader.ReadString();
			}
			else
			{
				this.m_nameType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
			{
				this.m_roleType = reader.ReadString();
			}
			else
			{
				this.m_roleType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
			{
				int num = reader.ReadInt32();
				for (int i = 0; i < num; i++)
				{
					Claim claim = new Claim(reader, this);
					this.m_instanceClaims.Add(claim);
				}
			}
		}

		// Token: 0x06002864 RID: 10340 RVA: 0x00093A57 File Offset: 0x00091C57
		protected virtual Claim CreateClaim(BinaryReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			return new Claim(reader, this);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00093A6E File Offset: 0x00091C6E
		public virtual void WriteTo(BinaryWriter writer)
		{
			this.WriteTo(writer, null);
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00093A78 File Offset: 0x00091C78
		protected virtual void WriteTo(BinaryWriter writer, byte[] userData)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			int num = 0;
			ClaimsIdentity.SerializationMask serializationMask = ClaimsIdentity.SerializationMask.None;
			if (this.m_authenticationType != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.AuthenticationType;
				num++;
			}
			if (this.m_bootstrapContext != null)
			{
				string text = this.m_bootstrapContext as string;
				if (text != null)
				{
					serializationMask |= ClaimsIdentity.SerializationMask.BootstrapConext;
					num++;
				}
			}
			if (!string.Equals(this.m_nameType, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.NameClaimType;
				num++;
			}
			if (!string.Equals(this.m_roleType, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", StringComparison.Ordinal))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.RoleClaimType;
				num++;
			}
			if (!string.IsNullOrWhiteSpace(this.m_label))
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasLabel;
				num++;
			}
			if (this.m_instanceClaims.Count > 0)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.HasClaims;
				num++;
			}
			if (this.m_actor != null)
			{
				serializationMask |= ClaimsIdentity.SerializationMask.Actor;
				num++;
			}
			if (userData != null && userData.Length != 0)
			{
				num++;
				serializationMask |= ClaimsIdentity.SerializationMask.UserData;
			}
			writer.Write((int)serializationMask);
			writer.Write(num);
			if ((serializationMask & ClaimsIdentity.SerializationMask.AuthenticationType) == ClaimsIdentity.SerializationMask.AuthenticationType)
			{
				writer.Write(this.m_authenticationType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.BootstrapConext) == ClaimsIdentity.SerializationMask.BootstrapConext)
			{
				writer.Write(this.m_bootstrapContext as string);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.NameClaimType) == ClaimsIdentity.SerializationMask.NameClaimType)
			{
				writer.Write(this.m_nameType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.RoleClaimType) == ClaimsIdentity.SerializationMask.RoleClaimType)
			{
				writer.Write(this.m_roleType);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasLabel) == ClaimsIdentity.SerializationMask.HasLabel)
			{
				writer.Write(this.m_label);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.HasClaims) == ClaimsIdentity.SerializationMask.HasClaims)
			{
				writer.Write(this.m_instanceClaims.Count);
				foreach (Claim claim in this.m_instanceClaims)
				{
					claim.WriteTo(writer);
				}
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.Actor) == ClaimsIdentity.SerializationMask.Actor)
			{
				this.m_actor.WriteTo(writer);
			}
			if ((serializationMask & ClaimsIdentity.SerializationMask.UserData) == ClaimsIdentity.SerializationMask.UserData)
			{
				writer.Write(userData.Length);
				writer.Write(userData);
			}
			writer.Flush();
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x00093C58 File Offset: 0x00091E58
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, SerializationFormatter = true)]
		private void Deserialize(SerializationInfo info, StreamingContext context, bool useContext)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			BinaryFormatter binaryFormatter;
			if (useContext)
			{
				binaryFormatter = new BinaryFormatter(null, context);
			}
			else
			{
				binaryFormatter = new BinaryFormatter();
			}
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 959168042U)
				{
					if (num <= 623923795U)
					{
						if (num != 373632733U)
						{
							if (num == 623923795U)
							{
								if (name == "System.Security.ClaimsIdentity.roleClaimType")
								{
									this.m_roleType = info.GetString("System.Security.ClaimsIdentity.roleClaimType");
								}
							}
						}
						else if (name == "System.Security.ClaimsIdentity.label")
						{
							this.m_label = info.GetString("System.Security.ClaimsIdentity.label");
						}
					}
					else if (num != 656336169U)
					{
						if (num == 959168042U)
						{
							if (name == "System.Security.ClaimsIdentity.nameClaimType")
							{
								this.m_nameType = info.GetString("System.Security.ClaimsIdentity.nameClaimType");
							}
						}
					}
					else if (name == "System.Security.ClaimsIdentity.authenticationType")
					{
						this.m_authenticationType = info.GetString("System.Security.ClaimsIdentity.authenticationType");
					}
				}
				else if (num <= 1476368026U)
				{
					if (num != 1453716852U)
					{
						if (num != 1476368026U)
						{
							continue;
						}
						if (!(name == "System.Security.ClaimsIdentity.actor"))
						{
							continue;
						}
						using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.actor"))))
						{
							this.m_actor = (ClaimsIdentity)binaryFormatter.Deserialize(memoryStream, null, false);
							continue;
						}
					}
					else if (!(name == "System.Security.ClaimsIdentity.claims"))
					{
						continue;
					}
					this.DeserializeClaims(info.GetString("System.Security.ClaimsIdentity.claims"));
				}
				else if (num != 2480284791U)
				{
					if (num == 3659022112U)
					{
						if (name == "System.Security.ClaimsIdentity.bootstrapContext")
						{
							using (MemoryStream memoryStream2 = new MemoryStream(Convert.FromBase64String(info.GetString("System.Security.ClaimsIdentity.bootstrapContext"))))
							{
								this.m_bootstrapContext = binaryFormatter.Deserialize(memoryStream2, null, false);
							}
						}
					}
				}
				else if (name == "System.Security.ClaimsIdentity.version")
				{
					string @string = info.GetString("System.Security.ClaimsIdentity.version");
				}
			}
		}

		// Token: 0x04000F8D RID: 3981
		[NonSerialized]
		private byte[] m_userSerializationData;

		// Token: 0x04000F8E RID: 3982
		[NonSerialized]
		private const string PreFix = "System.Security.ClaimsIdentity.";

		// Token: 0x04000F8F RID: 3983
		[NonSerialized]
		private const string ActorKey = "System.Security.ClaimsIdentity.actor";

		// Token: 0x04000F90 RID: 3984
		[NonSerialized]
		private const string AuthenticationTypeKey = "System.Security.ClaimsIdentity.authenticationType";

		// Token: 0x04000F91 RID: 3985
		[NonSerialized]
		private const string BootstrapContextKey = "System.Security.ClaimsIdentity.bootstrapContext";

		// Token: 0x04000F92 RID: 3986
		[NonSerialized]
		private const string ClaimsKey = "System.Security.ClaimsIdentity.claims";

		// Token: 0x04000F93 RID: 3987
		[NonSerialized]
		private const string LabelKey = "System.Security.ClaimsIdentity.label";

		// Token: 0x04000F94 RID: 3988
		[NonSerialized]
		private const string NameClaimTypeKey = "System.Security.ClaimsIdentity.nameClaimType";

		// Token: 0x04000F95 RID: 3989
		[NonSerialized]
		private const string RoleClaimTypeKey = "System.Security.ClaimsIdentity.roleClaimType";

		// Token: 0x04000F96 RID: 3990
		[NonSerialized]
		private const string VersionKey = "System.Security.ClaimsIdentity.version";

		// Token: 0x04000F97 RID: 3991
		[NonSerialized]
		public const string DefaultIssuer = "LOCAL AUTHORITY";

		// Token: 0x04000F98 RID: 3992
		[NonSerialized]
		public const string DefaultNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";

		// Token: 0x04000F99 RID: 3993
		[NonSerialized]
		public const string DefaultRoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		// Token: 0x04000F9A RID: 3994
		[NonSerialized]
		private List<Claim> m_instanceClaims;

		// Token: 0x04000F9B RID: 3995
		[NonSerialized]
		private Collection<IEnumerable<Claim>> m_externalClaims;

		// Token: 0x04000F9C RID: 3996
		[NonSerialized]
		private string m_nameType;

		// Token: 0x04000F9D RID: 3997
		[NonSerialized]
		private string m_roleType;

		// Token: 0x04000F9E RID: 3998
		[OptionalField(VersionAdded = 2)]
		private string m_version;

		// Token: 0x04000F9F RID: 3999
		[OptionalField(VersionAdded = 2)]
		private ClaimsIdentity m_actor;

		// Token: 0x04000FA0 RID: 4000
		[OptionalField(VersionAdded = 2)]
		private string m_authenticationType;

		// Token: 0x04000FA1 RID: 4001
		[OptionalField(VersionAdded = 2)]
		private object m_bootstrapContext;

		// Token: 0x04000FA2 RID: 4002
		[OptionalField(VersionAdded = 2)]
		private string m_label;

		// Token: 0x04000FA3 RID: 4003
		[OptionalField(VersionAdded = 2)]
		private string m_serializedNameType;

		// Token: 0x04000FA4 RID: 4004
		[OptionalField(VersionAdded = 2)]
		private string m_serializedRoleType;

		// Token: 0x04000FA5 RID: 4005
		[OptionalField(VersionAdded = 2)]
		private string m_serializedClaims;

		// Token: 0x02000B54 RID: 2900
		private enum SerializationMask
		{
			// Token: 0x040033F3 RID: 13299
			None,
			// Token: 0x040033F4 RID: 13300
			AuthenticationType,
			// Token: 0x040033F5 RID: 13301
			BootstrapConext,
			// Token: 0x040033F6 RID: 13302
			NameClaimType = 4,
			// Token: 0x040033F7 RID: 13303
			RoleClaimType = 8,
			// Token: 0x040033F8 RID: 13304
			HasClaims = 16,
			// Token: 0x040033F9 RID: 13305
			HasLabel = 32,
			// Token: 0x040033FA RID: 13306
			Actor = 64,
			// Token: 0x040033FB RID: 13307
			UserData = 128
		}

		// Token: 0x02000B55 RID: 2901
		[CompilerGenerated]
		private sealed class <get_Claims>d__51 : IEnumerable<Claim>, IEnumerable, IEnumerator<Claim>, IDisposable, IEnumerator
		{
			// Token: 0x06006BB3 RID: 27571 RVA: 0x001747A7 File Offset: 0x001729A7
			[DebuggerHidden]
			public <get_Claims>d__51(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06006BB4 RID: 27572 RVA: 0x001747C4 File Offset: 0x001729C4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 2)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x06006BB5 RID: 27573 RVA: 0x001747FC File Offset: 0x001729FC
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.<>1__state;
					ClaimsIdentity claimsIdentity = this;
					int num2;
					switch (num)
					{
					case 0:
						this.<>1__state = -1;
						i = 0;
						break;
					case 1:
						this.<>1__state = -1;
						num2 = i;
						i = num2 + 1;
						break;
					case 2:
						this.<>1__state = -3;
						goto IL_FE;
					default:
						return false;
					}
					if (i < claimsIdentity.m_instanceClaims.Count)
					{
						this.<>2__current = claimsIdentity.m_instanceClaims[i];
						this.<>1__state = 1;
						return true;
					}
					if (claimsIdentity.m_externalClaims != null)
					{
						i = 0;
						goto IL_128;
					}
					goto IL_13E;
					IL_FE:
					if (enumerator.MoveNext())
					{
						Claim claim = enumerator.Current;
						this.<>2__current = claim;
						this.<>1__state = 2;
						return true;
					}
					this.<>m__Finally1();
					enumerator = null;
					IL_118:
					num2 = i;
					i = num2 + 1;
					IL_128:
					if (i < claimsIdentity.m_externalClaims.Count)
					{
						if (claimsIdentity.m_externalClaims[i] != null)
						{
							enumerator = claimsIdentity.m_externalClaims[i].GetEnumerator();
							this.<>1__state = -3;
							goto IL_FE;
						}
						goto IL_118;
					}
					IL_13E:
					flag = false;
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06006BB6 RID: 27574 RVA: 0x00174970 File Offset: 0x00172B70
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x17001229 RID: 4649
			// (get) Token: 0x06006BB7 RID: 27575 RVA: 0x0017498C File Offset: 0x00172B8C
			Claim IEnumerator<Claim>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BB8 RID: 27576 RVA: 0x00174994 File Offset: 0x00172B94
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700122A RID: 4650
			// (get) Token: 0x06006BB9 RID: 27577 RVA: 0x0017499B File Offset: 0x00172B9B
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006BBA RID: 27578 RVA: 0x001749A4 File Offset: 0x00172BA4
			[DebuggerHidden]
			IEnumerator<Claim> IEnumerable<Claim>.GetEnumerator()
			{
				ClaimsIdentity.<get_Claims>d__51 <get_Claims>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_Claims>d__ = this;
				}
				else
				{
					<get_Claims>d__ = new ClaimsIdentity.<get_Claims>d__51(0);
					<get_Claims>d__.<>4__this = this;
				}
				return <get_Claims>d__;
			}

			// Token: 0x06006BBB RID: 27579 RVA: 0x001749E7 File Offset: 0x00172BE7
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Security.Claims.Claim>.GetEnumerator();
			}

			// Token: 0x040033FC RID: 13308
			private int <>1__state;

			// Token: 0x040033FD RID: 13309
			private Claim <>2__current;

			// Token: 0x040033FE RID: 13310
			private int <>l__initialThreadId;

			// Token: 0x040033FF RID: 13311
			public ClaimsIdentity <>4__this;

			// Token: 0x04003400 RID: 13312
			private int <i>5__2;

			// Token: 0x04003401 RID: 13313
			private IEnumerator<Claim> <>7__wrap2;
		}
	}
}
