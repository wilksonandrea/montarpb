using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Threading;

namespace System.Security
{
	// Token: 0x020001DA RID: 474
	[ComVisible(true)]
	[Serializable]
	public sealed class NamedPermissionSet : PermissionSet
	{
		// Token: 0x06001CAA RID: 7338 RVA: 0x00061FF1 File Offset: 0x000601F1
		internal NamedPermissionSet()
		{
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x00061FF9 File Offset: 0x000601F9
		public NamedPermissionSet(string name)
		{
			NamedPermissionSet.CheckName(name);
			this.m_name = name;
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x0006200E File Offset: 0x0006020E
		public NamedPermissionSet(string name, PermissionState state)
			: base(state)
		{
			NamedPermissionSet.CheckName(name);
			this.m_name = name;
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x00062024 File Offset: 0x00060224
		public NamedPermissionSet(string name, PermissionSet permSet)
			: base(permSet)
		{
			NamedPermissionSet.CheckName(name);
			this.m_name = name;
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x0006203A File Offset: 0x0006023A
		public NamedPermissionSet(NamedPermissionSet permSet)
			: base(permSet)
		{
			this.m_name = permSet.m_name;
			this.m_description = permSet.Description;
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x0006205B File Offset: 0x0006025B
		internal NamedPermissionSet(SecurityElement permissionSetXml)
			: base(PermissionState.None)
		{
			this.FromXml(permissionSetXml);
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001CB0 RID: 7344 RVA: 0x0006206B File Offset: 0x0006026B
		// (set) Token: 0x06001CB1 RID: 7345 RVA: 0x00062073 File Offset: 0x00060273
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				NamedPermissionSet.CheckName(value);
				this.m_name = value;
			}
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00062082 File Offset: 0x00060282
		private static void CheckName(string name)
		{
			if (name == null || name.Equals(""))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NPMSInvalidName"));
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06001CB3 RID: 7347 RVA: 0x000620A4 File Offset: 0x000602A4
		// (set) Token: 0x06001CB4 RID: 7348 RVA: 0x000620CC File Offset: 0x000602CC
		public string Description
		{
			get
			{
				if (this.m_descrResource != null)
				{
					this.m_description = Environment.GetResourceString(this.m_descrResource);
					this.m_descrResource = null;
				}
				return this.m_description;
			}
			set
			{
				this.m_description = value;
				this.m_descrResource = null;
			}
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x000620DC File Offset: 0x000602DC
		public override PermissionSet Copy()
		{
			return new NamedPermissionSet(this);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000620E4 File Offset: 0x000602E4
		public NamedPermissionSet Copy(string name)
		{
			return new NamedPermissionSet(this)
			{
				Name = name
			};
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00062100 File Offset: 0x00060300
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.ToXml("System.Security.NamedPermissionSet");
			if (this.m_name != null && !this.m_name.Equals(""))
			{
				securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_name));
			}
			if (this.Description != null && !this.Description.Equals(""))
			{
				securityElement.AddAttribute("Description", SecurityElement.Escape(this.Description));
			}
			return securityElement;
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x0006217A File Offset: 0x0006037A
		public override void FromXml(SecurityElement et)
		{
			this.FromXml(et, false, false);
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00062188 File Offset: 0x00060388
		internal override void FromXml(SecurityElement et, bool allowInternalOnly, bool ignoreTypeLoadFailures)
		{
			if (et == null)
			{
				throw new ArgumentNullException("et");
			}
			string text = et.Attribute("Name");
			this.m_name = ((text == null) ? null : text);
			text = et.Attribute("Description");
			this.m_description = ((text == null) ? "" : text);
			this.m_descrResource = null;
			base.FromXml(et, allowInternalOnly, ignoreTypeLoadFailures);
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x000621EC File Offset: 0x000603EC
		internal void FromXmlNameOnly(SecurityElement et)
		{
			string text = et.Attribute("Name");
			this.m_name = ((text == null) ? null : text);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00062212 File Offset: 0x00060412
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x0006221B File Offset: 0x0006041B
		[ComVisible(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x00062224 File Offset: 0x00060424
		private static object InternalSyncObject
		{
			get
			{
				if (NamedPermissionSet.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange(ref NamedPermissionSet.s_InternalSyncObject, obj, null);
				}
				return NamedPermissionSet.s_InternalSyncObject;
			}
		}

		// Token: 0x04000A05 RID: 2565
		private string m_name;

		// Token: 0x04000A06 RID: 2566
		private string m_description;

		// Token: 0x04000A07 RID: 2567
		[OptionalField(VersionAdded = 2)]
		internal string m_descrResource;

		// Token: 0x04000A08 RID: 2568
		private static object s_InternalSyncObject;
	}
}
