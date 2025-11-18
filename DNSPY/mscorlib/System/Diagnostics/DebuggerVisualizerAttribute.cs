using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003F0 RID: 1008
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerVisualizerAttribute : Attribute
	{
		// Token: 0x06003327 RID: 13095 RVA: 0x000C4BA6 File Offset: 0x000C2DA6
		public DebuggerVisualizerAttribute(string visualizerTypeName)
		{
			this.visualizerName = visualizerTypeName;
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x000C4BB5 File Offset: 0x000C2DB5
		public DebuggerVisualizerAttribute(string visualizerTypeName, string visualizerObjectSourceTypeName)
		{
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x000C4BCB File Offset: 0x000C2DCB
		public DebuggerVisualizerAttribute(string visualizerTypeName, Type visualizerObjectSource)
		{
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizerTypeName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x000C4BFA File Offset: 0x000C2DFA
		public DebuggerVisualizerAttribute(Type visualizer)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x000C4C24 File Offset: 0x000C2E24
		public DebuggerVisualizerAttribute(Type visualizer, Type visualizerObjectSource)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			if (visualizerObjectSource == null)
			{
				throw new ArgumentNullException("visualizerObjectSource");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSource.AssemblyQualifiedName;
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x000C4C77 File Offset: 0x000C2E77
		public DebuggerVisualizerAttribute(Type visualizer, string visualizerObjectSourceTypeName)
		{
			if (visualizer == null)
			{
				throw new ArgumentNullException("visualizer");
			}
			this.visualizerName = visualizer.AssemblyQualifiedName;
			this.visualizerObjectSourceName = visualizerObjectSourceTypeName;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000C4CA6 File Offset: 0x000C2EA6
		public string VisualizerObjectSourceTypeName
		{
			get
			{
				return this.visualizerObjectSourceName;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x000C4CAE File Offset: 0x000C2EAE
		public string VisualizerTypeName
		{
			get
			{
				return this.visualizerName;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C4CB6 File Offset: 0x000C2EB6
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x000C4CBE File Offset: 0x000C2EBE
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C4CF0 File Offset: 0x000C2EF0
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x000C4CC7 File Offset: 0x000C2EC7
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000C4D01 File Offset: 0x000C2F01
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000C4CF8 File Offset: 0x000C2EF8
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040016B1 RID: 5809
		private string visualizerObjectSourceName;

		// Token: 0x040016B2 RID: 5810
		private string visualizerName;

		// Token: 0x040016B3 RID: 5811
		private string description;

		// Token: 0x040016B4 RID: 5812
		private string targetName;

		// Token: 0x040016B5 RID: 5813
		private Type target;
	}
}
