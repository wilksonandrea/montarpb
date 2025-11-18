using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200021A RID: 538
	public sealed class FileSystemAuditRule : AuditRule
	{
		// Token: 0x06001F3F RID: 7999 RVA: 0x0006D420 File Offset: 0x0006B620
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, AuditFlags flags)
			: this(identity, fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F40 RID: 8000 RVA: 0x0006D42D File Offset: 0x0006B62D
		public FileSystemAuditRule(IdentityReference identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06001F41 RID: 8001 RVA: 0x0006D442 File Offset: 0x0006B642
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, AuditFlags flags)
			: this(new NTAccount(identity), fileSystemRights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F42 RID: 8002 RVA: 0x0006D454 File Offset: 0x0006B654
		public FileSystemAuditRule(string identity, FileSystemRights fileSystemRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), FileSystemAuditRule.AccessMaskFromRights(fileSystemRights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x0006D46E File Offset: 0x0006B66E
		internal FileSystemAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x0006D47F File Offset: 0x0006B67F
		private static int AccessMaskFromRights(FileSystemRights fileSystemRights)
		{
			if (fileSystemRights < (FileSystemRights)0 || fileSystemRights > FileSystemRights.FullControl)
			{
				throw new ArgumentOutOfRangeException("fileSystemRights", Environment.GetResourceString("Argument_InvalidEnumValue", new object[] { fileSystemRights, "FileSystemRights" }));
			}
			return (int)fileSystemRights;
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06001F45 RID: 8005 RVA: 0x0006D4BA File Offset: 0x0006B6BA
		public FileSystemRights FileSystemRights
		{
			get
			{
				return FileSystemAccessRule.RightsFromAccessMask(base.AccessMask);
			}
		}
	}
}
