using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008C8 RID: 2248
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = false)]
	internal sealed class TypeDependencyAttribute : Attribute
	{
		// Token: 0x06005DC8 RID: 24008 RVA: 0x001496F8 File Offset: 0x001478F8
		public TypeDependencyAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this.typeName = typeName;
		}

		// Token: 0x04002A3B RID: 10811
		private string typeName;
	}
}
