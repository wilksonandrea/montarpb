using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200062B RID: 1579
	internal sealed class InternalAssemblyBuilder : RuntimeAssembly
	{
		// Token: 0x0600490B RID: 18699 RVA: 0x00107C47 File Offset: 0x00105E47
		private InternalAssemblyBuilder()
		{
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x00107C4F File Offset: 0x00105E4F
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InternalAssemblyBuilder)
			{
				return this == obj;
			}
			return obj.Equals(this);
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x00107C6A File Offset: 0x00105E6A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600490E RID: 18702 RVA: 0x00107C72 File Offset: 0x00105E72
		public override string[] GetManifestResourceNames()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x0600490F RID: 18703 RVA: 0x00107C83 File Offset: 0x00105E83
		public override FileStream GetFile(string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x00107C94 File Offset: 0x00105E94
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00107CA5 File Offset: 0x00105EA5
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00107CB6 File Offset: 0x00105EB6
		public override Stream GetManifestResourceStream(string name)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x00107CC7 File Offset: 0x00105EC7
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x06004914 RID: 18708 RVA: 0x00107CD8 File Offset: 0x00105ED8
		public override string Location
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x06004915 RID: 18709 RVA: 0x00107CE9 File Offset: 0x00105EE9
		public override string CodeBase
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
			}
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x00107CFA File Offset: 0x00105EFA
		public override Type[] GetExportedTypes()
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_DynamicAssembly"));
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x06004917 RID: 18711 RVA: 0x00107D0B File Offset: 0x00105F0B
		public override string ImageRuntimeVersion
		{
			get
			{
				return RuntimeEnvironment.GetSystemVersion();
			}
		}
	}
}
