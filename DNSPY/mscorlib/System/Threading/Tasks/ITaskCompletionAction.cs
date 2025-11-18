using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000568 RID: 1384
	internal interface ITaskCompletionAction
	{
		// Token: 0x06004161 RID: 16737
		void Invoke(Task completingTask);
	}
}
