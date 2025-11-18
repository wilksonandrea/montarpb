using System;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	// Token: 0x02000A0D RID: 2573
	[Guid("7C925755-3E48-42B4-8677-76372267033F")]
	[ComImport]
	internal interface ICustomPropertyProvider
	{
		// Token: 0x06006586 RID: 25990
		ICustomProperty GetCustomProperty(string name);

		// Token: 0x06006587 RID: 25991
		ICustomProperty GetIndexedProperty(string name, Type indexParameterType);

		// Token: 0x06006588 RID: 25992
		string GetStringRepresentation();

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x06006589 RID: 25993
		Type Type { get; }
	}
}
