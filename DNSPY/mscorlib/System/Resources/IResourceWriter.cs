using System;
using System.Runtime.InteropServices;

namespace System.Resources
{
	// Token: 0x0200038F RID: 911
	[ComVisible(true)]
	public interface IResourceWriter : IDisposable
	{
		// Token: 0x06002CF4 RID: 11508
		void AddResource(string name, string value);

		// Token: 0x06002CF5 RID: 11509
		void AddResource(string name, object value);

		// Token: 0x06002CF6 RID: 11510
		void AddResource(string name, byte[] value);

		// Token: 0x06002CF7 RID: 11511
		void Close();

		// Token: 0x06002CF8 RID: 11512
		void Generate();
	}
}
