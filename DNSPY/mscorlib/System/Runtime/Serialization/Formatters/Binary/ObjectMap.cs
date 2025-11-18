using System;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000795 RID: 1941
	internal sealed class ObjectMap
	{
		// Token: 0x06005434 RID: 21556 RVA: 0x001285DC File Offset: 0x001267DC
		[SecurityCritical]
		internal ObjectMap(string objectName, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			this.objectName = objectName;
			this.objectType = objectType;
			this.memberNames = memberNames;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			this.objectInfo = objectReader.CreateReadObjectInfo(objectType);
			this.memberTypes = this.objectInfo.GetMemberTypes(memberNames, objectType);
			this.binaryTypeEnumA = new BinaryTypeEnum[this.memberTypes.Length];
			this.typeInformationA = new object[this.memberTypes.Length];
			for (int i = 0; i < this.memberTypes.Length; i++)
			{
				object obj = null;
				BinaryTypeEnum parserBinaryTypeInfo = BinaryConverter.GetParserBinaryTypeInfo(this.memberTypes[i], out obj);
				this.binaryTypeEnumA[i] = parserBinaryTypeInfo;
				this.typeInformationA[i] = obj;
			}
		}

		// Token: 0x06005435 RID: 21557 RVA: 0x001286A4 File Offset: 0x001268A4
		[SecurityCritical]
		internal ObjectMap(string objectName, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			this.objectName = objectName;
			this.memberNames = memberNames;
			this.binaryTypeEnumA = binaryTypeEnumA;
			this.typeInformationA = typeInformationA;
			this.objectReader = objectReader;
			this.objectId = objectId;
			this.assemblyInfo = assemblyInfo;
			if (assemblyInfo == null)
			{
				throw new SerializationException(Environment.GetResourceString("Serialization_Assembly", new object[] { objectName }));
			}
			this.objectType = objectReader.GetType(assemblyInfo, objectName);
			this.memberTypes = new Type[memberNames.Length];
			for (int i = 0; i < memberNames.Length; i++)
			{
				InternalPrimitiveTypeE internalPrimitiveTypeE;
				string text;
				Type type;
				bool flag;
				BinaryConverter.TypeFromInfo(binaryTypeEnumA[i], typeInformationA[i], objectReader, (BinaryAssemblyInfo)assemIdToAssemblyTable[memberAssemIds[i]], out internalPrimitiveTypeE, out text, out type, out flag);
				this.memberTypes[i] = type;
			}
			this.objectInfo = objectReader.CreateReadObjectInfo(this.objectType, memberNames, null);
			if (!this.objectInfo.isSi)
			{
				this.objectInfo.GetMemberTypes(memberNames, this.objectInfo.objectType);
			}
		}

		// Token: 0x06005436 RID: 21558 RVA: 0x001287A8 File Offset: 0x001269A8
		internal ReadObjectInfo CreateObjectInfo(ref SerializationInfo si, ref object[] memberData)
		{
			if (this.isInitObjectInfo)
			{
				this.isInitObjectInfo = false;
				this.objectInfo.InitDataStore(ref si, ref memberData);
				return this.objectInfo;
			}
			this.objectInfo.PrepareForReuse();
			this.objectInfo.InitDataStore(ref si, ref memberData);
			return this.objectInfo;
		}

		// Token: 0x06005437 RID: 21559 RVA: 0x001287F6 File Offset: 0x001269F6
		[SecurityCritical]
		internal static ObjectMap Create(string name, Type objectType, string[] memberNames, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo)
		{
			return new ObjectMap(name, objectType, memberNames, objectReader, objectId, assemblyInfo);
		}

		// Token: 0x06005438 RID: 21560 RVA: 0x00128808 File Offset: 0x00126A08
		[SecurityCritical]
		internal static ObjectMap Create(string name, string[] memberNames, BinaryTypeEnum[] binaryTypeEnumA, object[] typeInformationA, int[] memberAssemIds, ObjectReader objectReader, int objectId, BinaryAssemblyInfo assemblyInfo, SizedArray assemIdToAssemblyTable)
		{
			return new ObjectMap(name, memberNames, binaryTypeEnumA, typeInformationA, memberAssemIds, objectReader, objectId, assemblyInfo, assemIdToAssemblyTable);
		}

		// Token: 0x0400260B RID: 9739
		internal string objectName;

		// Token: 0x0400260C RID: 9740
		internal Type objectType;

		// Token: 0x0400260D RID: 9741
		internal BinaryTypeEnum[] binaryTypeEnumA;

		// Token: 0x0400260E RID: 9742
		internal object[] typeInformationA;

		// Token: 0x0400260F RID: 9743
		internal Type[] memberTypes;

		// Token: 0x04002610 RID: 9744
		internal string[] memberNames;

		// Token: 0x04002611 RID: 9745
		internal ReadObjectInfo objectInfo;

		// Token: 0x04002612 RID: 9746
		internal bool isInitObjectInfo = true;

		// Token: 0x04002613 RID: 9747
		internal ObjectReader objectReader;

		// Token: 0x04002614 RID: 9748
		internal int objectId;

		// Token: 0x04002615 RID: 9749
		internal BinaryAssemblyInfo assemblyInfo;
	}
}
