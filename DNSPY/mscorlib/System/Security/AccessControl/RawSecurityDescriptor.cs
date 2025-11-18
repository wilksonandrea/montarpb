using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.Win32;

namespace System.Security.AccessControl
{
	// Token: 0x02000239 RID: 569
	public sealed class RawSecurityDescriptor : GenericSecurityDescriptor
	{
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06002060 RID: 8288 RVA: 0x00071712 File Offset: 0x0006F912
		internal override GenericAcl GenericSacl
		{
			get
			{
				return this._sacl;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06002061 RID: 8289 RVA: 0x0007171A File Offset: 0x0006F91A
		internal override GenericAcl GenericDacl
		{
			get
			{
				return this._dacl;
			}
		}

		// Token: 0x06002062 RID: 8290 RVA: 0x00071722 File Offset: 0x0006F922
		private void CreateFromParts(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.SetFlags(flags);
			this.Owner = owner;
			this.Group = group;
			this.SystemAcl = systemAcl;
			this.DiscretionaryAcl = discretionaryAcl;
			this.ResourceManagerControl = 0;
		}

		// Token: 0x06002063 RID: 8291 RVA: 0x00071750 File Offset: 0x0006F950
		public RawSecurityDescriptor(ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
		{
			this.CreateFromParts(flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x06002064 RID: 8292 RVA: 0x00071765 File Offset: 0x0006F965
		[SecuritySafeCritical]
		public RawSecurityDescriptor(string sddlForm)
			: this(RawSecurityDescriptor.BinaryFormFromSddlForm(sddlForm), 0)
		{
		}

		// Token: 0x06002065 RID: 8293 RVA: 0x00071774 File Offset: 0x0006F974
		public RawSecurityDescriptor(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (binaryForm.Length - offset < 20)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("ArgumentOutOfRange_ArrayTooSmall"));
			}
			if (binaryForm[offset] != GenericSecurityDescriptor.Revision)
			{
				throw new ArgumentOutOfRangeException("binaryForm", Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorRevision"));
			}
			byte b = binaryForm[offset + 1];
			ControlFlags controlFlags = (ControlFlags)((int)binaryForm[offset + 2] + ((int)binaryForm[offset + 3] << 8));
			if ((controlFlags & ControlFlags.SelfRelative) == ControlFlags.None)
			{
				throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSecurityDescriptorSelfRelativeForm"), "binaryForm");
			}
			int num = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 4);
			SecurityIdentifier securityIdentifier;
			if (num != 0)
			{
				securityIdentifier = new SecurityIdentifier(binaryForm, offset + num);
			}
			else
			{
				securityIdentifier = null;
			}
			int num2 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 8);
			SecurityIdentifier securityIdentifier2;
			if (num2 != 0)
			{
				securityIdentifier2 = new SecurityIdentifier(binaryForm, offset + num2);
			}
			else
			{
				securityIdentifier2 = null;
			}
			int num3 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 12);
			RawAcl rawAcl;
			if ((controlFlags & ControlFlags.SystemAclPresent) != ControlFlags.None && num3 != 0)
			{
				rawAcl = new RawAcl(binaryForm, offset + num3);
			}
			else
			{
				rawAcl = null;
			}
			int num4 = GenericSecurityDescriptor.UnmarshalInt(binaryForm, offset + 16);
			RawAcl rawAcl2;
			if ((controlFlags & ControlFlags.DiscretionaryAclPresent) != ControlFlags.None && num4 != 0)
			{
				rawAcl2 = new RawAcl(binaryForm, offset + num4);
			}
			else
			{
				rawAcl2 = null;
			}
			this.CreateFromParts(controlFlags, securityIdentifier, securityIdentifier2, rawAcl, rawAcl2);
			if ((controlFlags & ControlFlags.RMControlValid) != ControlFlags.None)
			{
				this.ResourceManagerControl = b;
			}
		}

		// Token: 0x06002066 RID: 8294 RVA: 0x000718C4 File Offset: 0x0006FAC4
		[SecurityCritical]
		private static byte[] BinaryFormFromSddlForm(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			IntPtr zero = IntPtr.Zero;
			uint num = 0U;
			byte[] array = null;
			try
			{
				if (1 != Win32Native.ConvertStringSdToSd(sddlForm, (uint)GenericSecurityDescriptor.Revision, out zero, ref num))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 87 || lastWin32Error == 1336 || lastWin32Error == 1338 || lastWin32Error == 1305)
					{
						throw new ArgumentException(Environment.GetResourceString("ArgumentException_InvalidSDSddlForm"), "sddlForm");
					}
					if (lastWin32Error == 8)
					{
						throw new OutOfMemoryException();
					}
					if (lastWin32Error == 1337)
					{
						throw new ArgumentException(Environment.GetResourceString("AccessControl_InvalidSidInSDDLString"), "sddlForm");
					}
					if (lastWin32Error != 0)
					{
						throw new SystemException();
					}
				}
				array = new byte[num];
				Marshal.Copy(zero, array, 0, (int)num);
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					Win32Native.LocalFree(zero);
				}
			}
			return array;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06002067 RID: 8295 RVA: 0x0007199C File Offset: 0x0006FB9C
		public override ControlFlags ControlFlags
		{
			get
			{
				return this._flags;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06002068 RID: 8296 RVA: 0x000719A4 File Offset: 0x0006FBA4
		// (set) Token: 0x06002069 RID: 8297 RVA: 0x000719AC File Offset: 0x0006FBAC
		public override SecurityIdentifier Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600206A RID: 8298 RVA: 0x000719B5 File Offset: 0x0006FBB5
		// (set) Token: 0x0600206B RID: 8299 RVA: 0x000719BD File Offset: 0x0006FBBD
		public override SecurityIdentifier Group
		{
			get
			{
				return this._group;
			}
			set
			{
				this._group = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600206C RID: 8300 RVA: 0x000719C6 File Offset: 0x0006FBC6
		// (set) Token: 0x0600206D RID: 8301 RVA: 0x000719CE File Offset: 0x0006FBCE
		public RawAcl SystemAcl
		{
			get
			{
				return this._sacl;
			}
			set
			{
				this._sacl = value;
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600206E RID: 8302 RVA: 0x000719D7 File Offset: 0x0006FBD7
		// (set) Token: 0x0600206F RID: 8303 RVA: 0x000719DF File Offset: 0x0006FBDF
		public RawAcl DiscretionaryAcl
		{
			get
			{
				return this._dacl;
			}
			set
			{
				this._dacl = value;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06002070 RID: 8304 RVA: 0x000719E8 File Offset: 0x0006FBE8
		// (set) Token: 0x06002071 RID: 8305 RVA: 0x000719F0 File Offset: 0x0006FBF0
		public byte ResourceManagerControl
		{
			get
			{
				return this._rmControl;
			}
			set
			{
				this._rmControl = value;
			}
		}

		// Token: 0x06002072 RID: 8306 RVA: 0x000719F9 File Offset: 0x0006FBF9
		public void SetFlags(ControlFlags flags)
		{
			this._flags = flags | ControlFlags.SelfRelative;
		}

		// Token: 0x04000BC7 RID: 3015
		private SecurityIdentifier _owner;

		// Token: 0x04000BC8 RID: 3016
		private SecurityIdentifier _group;

		// Token: 0x04000BC9 RID: 3017
		private ControlFlags _flags;

		// Token: 0x04000BCA RID: 3018
		private RawAcl _sacl;

		// Token: 0x04000BCB RID: 3019
		private RawAcl _dacl;

		// Token: 0x04000BCC RID: 3020
		private byte _rmControl;
	}
}
