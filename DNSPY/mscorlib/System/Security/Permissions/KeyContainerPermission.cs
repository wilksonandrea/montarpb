using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000318 RID: 792
	[ComVisible(true)]
	[Serializable]
	public sealed class KeyContainerPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060027E4 RID: 10212 RVA: 0x00090C80 File Offset: 0x0008EE80
		public KeyContainerPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_flags = KeyContainerPermissionFlags.AllFlags;
			}
			else
			{
				if (state != PermissionState.None)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
				}
				this.m_flags = KeyContainerPermissionFlags.NoFlags;
			}
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
		}

		// Token: 0x060027E5 RID: 10213 RVA: 0x00090CD1 File Offset: 0x0008EED1
		public KeyContainerPermission(KeyContainerPermissionFlags flags)
		{
			KeyContainerPermission.VerifyFlags(flags);
			this.m_flags = flags;
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
		}

		// Token: 0x060027E6 RID: 10214 RVA: 0x00090CF8 File Offset: 0x0008EEF8
		public KeyContainerPermission(KeyContainerPermissionFlags flags, KeyContainerPermissionAccessEntry[] accessList)
		{
			if (accessList == null)
			{
				throw new ArgumentNullException("accessList");
			}
			KeyContainerPermission.VerifyFlags(flags);
			this.m_flags = flags;
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
			for (int i = 0; i < accessList.Length; i++)
			{
				this.m_accessEntries.Add(accessList[i]);
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x00090D54 File Offset: 0x0008EF54
		public KeyContainerPermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x00090D5C File Offset: 0x0008EF5C
		public KeyContainerPermissionAccessEntryCollection AccessEntries
		{
			get
			{
				return this.m_accessEntries;
			}
		}

		// Token: 0x060027E9 RID: 10217 RVA: 0x00090D64 File Offset: 0x0008EF64
		public bool IsUnrestricted()
		{
			if (this.m_flags != KeyContainerPermissionFlags.AllFlags)
			{
				return false;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				if ((keyContainerPermissionAccessEntry.Flags & KeyContainerPermissionFlags.AllFlags) != KeyContainerPermissionFlags.AllFlags)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x00090DB4 File Offset: 0x0008EFB4
		private bool IsEmpty()
		{
			if (this.Flags == KeyContainerPermissionFlags.NoFlags)
			{
				foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
				{
					if (keyContainerPermissionAccessEntry.Flags != KeyContainerPermissionFlags.NoFlags)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x00090DF4 File Offset: 0x0008EFF4
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if ((this.m_flags & keyContainerPermission.m_flags) != this.m_flags)
			{
				return false;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				KeyContainerPermissionFlags applicableFlags = KeyContainerPermission.GetApplicableFlags(keyContainerPermissionAccessEntry, keyContainerPermission);
				if ((keyContainerPermissionAccessEntry.Flags & applicableFlags) != keyContainerPermissionAccessEntry.Flags)
				{
					return false;
				}
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				KeyContainerPermissionFlags applicableFlags2 = KeyContainerPermission.GetApplicableFlags(keyContainerPermissionAccessEntry2, this);
				if ((applicableFlags2 & keyContainerPermissionAccessEntry2.Flags) != applicableFlags2)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x00090ECC File Offset: 0x0008F0CC
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if (this.IsEmpty() || keyContainerPermission.IsEmpty())
			{
				return null;
			}
			KeyContainerPermissionFlags keyContainerPermissionFlags = keyContainerPermission.m_flags & this.m_flags;
			KeyContainerPermission keyContainerPermission2 = new KeyContainerPermission(keyContainerPermissionFlags);
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndIntersect(keyContainerPermissionAccessEntry, keyContainerPermission);
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndIntersect(keyContainerPermissionAccessEntry2, this);
			}
			if (!keyContainerPermission2.IsEmpty())
			{
				return keyContainerPermission2;
			}
			return null;
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x00090F98 File Offset: 0x0008F198
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			KeyContainerPermission keyContainerPermission = (KeyContainerPermission)target;
			if (this.IsUnrestricted() || keyContainerPermission.IsUnrestricted())
			{
				return new KeyContainerPermission(PermissionState.Unrestricted);
			}
			KeyContainerPermissionFlags keyContainerPermissionFlags = this.m_flags | keyContainerPermission.m_flags;
			KeyContainerPermission keyContainerPermission2 = new KeyContainerPermission(keyContainerPermissionFlags);
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndUnion(keyContainerPermissionAccessEntry, keyContainerPermission);
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 in keyContainerPermission.AccessEntries)
			{
				keyContainerPermission2.AddAccessEntryAndUnion(keyContainerPermissionAccessEntry2, this);
			}
			if (!keyContainerPermission2.IsEmpty())
			{
				return keyContainerPermission2;
			}
			return null;
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x0009106C File Offset: 0x0008F26C
		public override IPermission Copy()
		{
			if (this.IsEmpty())
			{
				return null;
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(this.m_flags);
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
			{
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
			}
			return keyContainerPermission;
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000910BC File Offset: 0x0008F2BC
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.KeyContainerPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", this.m_flags.ToString());
				if (this.AccessEntries.Count > 0)
				{
					SecurityElement securityElement2 = new SecurityElement("AccessList");
					foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in this.AccessEntries)
					{
						SecurityElement securityElement3 = new SecurityElement("AccessEntry");
						securityElement3.AddAttribute("KeyStore", keyContainerPermissionAccessEntry.KeyStore);
						securityElement3.AddAttribute("ProviderName", keyContainerPermissionAccessEntry.ProviderName);
						securityElement3.AddAttribute("ProviderType", keyContainerPermissionAccessEntry.ProviderType.ToString(null, null));
						securityElement3.AddAttribute("KeyContainerName", keyContainerPermissionAccessEntry.KeyContainerName);
						securityElement3.AddAttribute("KeySpec", keyContainerPermissionAccessEntry.KeySpec.ToString(null, null));
						securityElement3.AddAttribute("Flags", keyContainerPermissionAccessEntry.Flags.ToString());
						securityElement2.AddChild(securityElement3);
					}
					securityElement.AddChild(securityElement2);
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000911FC File Offset: 0x0008F3FC
		public override void FromXml(SecurityElement securityElement)
		{
			CodeAccessPermission.ValidateElement(securityElement, this);
			if (XMLUtil.IsUnrestricted(securityElement))
			{
				this.m_flags = KeyContainerPermissionFlags.AllFlags;
				this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
				return;
			}
			this.m_flags = KeyContainerPermissionFlags.NoFlags;
			string text = securityElement.Attribute("Flags");
			if (text != null)
			{
				KeyContainerPermissionFlags keyContainerPermissionFlags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), text);
				KeyContainerPermission.VerifyFlags(keyContainerPermissionFlags);
				this.m_flags = keyContainerPermissionFlags;
			}
			this.m_accessEntries = new KeyContainerPermissionAccessEntryCollection(this.m_flags);
			if (securityElement.InternalChildren != null && securityElement.InternalChildren.Count != 0)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2 != null && string.Equals(securityElement2.Tag, "AccessList"))
					{
						this.AddAccessEntries(securityElement2);
					}
				}
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000912D2 File Offset: 0x0008F4D2
		int IBuiltInPermission.GetTokenIndex()
		{
			return KeyContainerPermission.GetTokenIndex();
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000912DC File Offset: 0x0008F4DC
		private void AddAccessEntries(SecurityElement securityElement)
		{
			if (securityElement.InternalChildren != null && securityElement.InternalChildren.Count != 0)
			{
				foreach (object obj in securityElement.Children)
				{
					SecurityElement securityElement2 = (SecurityElement)obj;
					if (securityElement2 != null && string.Equals(securityElement2.Tag, "AccessEntry"))
					{
						int count = securityElement2.m_lAttributes.Count;
						string text = null;
						string text2 = null;
						int num = -1;
						string text3 = null;
						int num2 = -1;
						KeyContainerPermissionFlags keyContainerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
						for (int i = 0; i < count; i += 2)
						{
							string text4 = (string)securityElement2.m_lAttributes[i];
							string text5 = (string)securityElement2.m_lAttributes[i + 1];
							if (string.Equals(text4, "KeyStore"))
							{
								text = text5;
							}
							if (string.Equals(text4, "ProviderName"))
							{
								text2 = text5;
							}
							else if (string.Equals(text4, "ProviderType"))
							{
								num = Convert.ToInt32(text5, null);
							}
							else if (string.Equals(text4, "KeyContainerName"))
							{
								text3 = text5;
							}
							else if (string.Equals(text4, "KeySpec"))
							{
								num2 = Convert.ToInt32(text5, null);
							}
							else if (string.Equals(text4, "Flags"))
							{
								keyContainerPermissionFlags = (KeyContainerPermissionFlags)Enum.Parse(typeof(KeyContainerPermissionFlags), text5);
							}
						}
						KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(text, text2, num, text3, num2, keyContainerPermissionFlags);
						this.AccessEntries.Add(keyContainerPermissionAccessEntry);
					}
				}
			}
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x00091458 File Offset: 0x0008F658
		private void AddAccessEntryAndUnion(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(accessEntry);
			keyContainerPermissionAccessEntry.Flags |= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
			this.AccessEntries.Add(keyContainerPermissionAccessEntry);
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00091490 File Offset: 0x0008F690
		private void AddAccessEntryAndIntersect(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(accessEntry);
			keyContainerPermissionAccessEntry.Flags &= KeyContainerPermission.GetApplicableFlags(accessEntry, target);
			this.AccessEntries.Add(keyContainerPermissionAccessEntry);
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000914C5 File Offset: 0x0008F6C5
		internal static void VerifyFlags(KeyContainerPermissionFlags flags)
		{
			if ((flags & ~(KeyContainerPermissionFlags.Create | KeyContainerPermissionFlags.Open | KeyContainerPermissionFlags.Delete | KeyContainerPermissionFlags.Import | KeyContainerPermissionFlags.Export | KeyContainerPermissionFlags.Sign | KeyContainerPermissionFlags.Decrypt | KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl)) != KeyContainerPermissionFlags.NoFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)flags }));
			}
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000914F0 File Offset: 0x0008F6F0
		private static KeyContainerPermissionFlags GetApplicableFlags(KeyContainerPermissionAccessEntry accessEntry, KeyContainerPermission target)
		{
			KeyContainerPermissionFlags keyContainerPermissionFlags = KeyContainerPermissionFlags.NoFlags;
			bool flag = true;
			int num = target.AccessEntries.IndexOf(accessEntry);
			if (num != -1)
			{
				return target.AccessEntries[num].Flags;
			}
			foreach (KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry in target.AccessEntries)
			{
				if (accessEntry.IsSubsetOf(keyContainerPermissionAccessEntry))
				{
					if (!flag)
					{
						keyContainerPermissionFlags &= keyContainerPermissionAccessEntry.Flags;
					}
					else
					{
						keyContainerPermissionFlags = keyContainerPermissionAccessEntry.Flags;
						flag = false;
					}
				}
			}
			if (flag)
			{
				keyContainerPermissionFlags = target.Flags;
			}
			return keyContainerPermissionFlags;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x00091572 File Offset: 0x0008F772
		private static int GetTokenIndex()
		{
			return 16;
		}

		// Token: 0x04000F74 RID: 3956
		private KeyContainerPermissionFlags m_flags;

		// Token: 0x04000F75 RID: 3957
		private KeyContainerPermissionAccessEntryCollection m_accessEntries;
	}
}
