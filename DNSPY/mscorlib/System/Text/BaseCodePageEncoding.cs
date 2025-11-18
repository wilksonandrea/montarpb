using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Text
{
	// Token: 0x02000A5D RID: 2653
	[Serializable]
	internal abstract class BaseCodePageEncoding : EncodingNLS, ISerializable
	{
		// Token: 0x06006755 RID: 26453 RVA: 0x0015CD5D File Offset: 0x0015AF5D
		[SecuritySafeCritical]
		static unsafe BaseCodePageEncoding()
		{
		}

		// Token: 0x06006756 RID: 26454 RVA: 0x0015CD7D File Offset: 0x0015AF7D
		[SecurityCritical]
		internal BaseCodePageEncoding(int codepage)
			: this(codepage, codepage)
		{
		}

		// Token: 0x06006757 RID: 26455 RVA: 0x0015CD87 File Offset: 0x0015AF87
		[SecurityCritical]
		internal BaseCodePageEncoding(int codepage, int dataCodePage)
		{
			this.bFlagDataTable = true;
			this.pCodePage = null;
			base..ctor((codepage == 0) ? Win32Native.GetACP() : codepage);
			this.dataTableCodePage = dataCodePage;
			this.LoadCodePageTables();
		}

		// Token: 0x06006758 RID: 26456 RVA: 0x0015CDB6 File Offset: 0x0015AFB6
		[SecurityCritical]
		internal BaseCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			this.bFlagDataTable = true;
			this.pCodePage = null;
			base..ctor(0);
			throw new ArgumentNullException("this");
		}

		// Token: 0x06006759 RID: 26457 RVA: 0x0015CDD8 File Offset: 0x0015AFD8
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.SerializeEncoding(info, context);
			info.AddValue(this.m_bUseMlangTypeForSerialization ? "m_maxByteSize" : "maxCharSize", this.IsSingleByte ? 1 : 2);
			info.SetType(this.m_bUseMlangTypeForSerialization ? typeof(MLangCodePageEncoding) : typeof(CodePageEncoding));
		}

		// Token: 0x0600675A RID: 26458 RVA: 0x0015CE38 File Offset: 0x0015B038
		[SecurityCritical]
		private unsafe void LoadCodePageTables()
		{
			BaseCodePageEncoding.CodePageHeader* ptr = BaseCodePageEncoding.FindCodePage(this.dataTableCodePage);
			if (ptr == null)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[] { this.CodePage }));
			}
			this.pCodePage = ptr;
			this.LoadManagedCodePage();
		}

		// Token: 0x0600675B RID: 26459 RVA: 0x0015CE88 File Offset: 0x0015B088
		[SecurityCritical]
		private unsafe static BaseCodePageEncoding.CodePageHeader* FindCodePage(int codePage)
		{
			for (int i = 0; i < (int)BaseCodePageEncoding.m_pCodePageFileHeader->CodePageCount; i++)
			{
				BaseCodePageEncoding.CodePageIndex* ptr = &BaseCodePageEncoding.m_pCodePageFileHeader->CodePages + i;
				if ((int)ptr->CodePage == codePage)
				{
					return (BaseCodePageEncoding.CodePageHeader*)(BaseCodePageEncoding.m_pCodePageFileHeader + ptr->Offset / sizeof(BaseCodePageEncoding.CodePageDataFileHeader));
				}
			}
			return null;
		}

		// Token: 0x0600675C RID: 26460 RVA: 0x0015CEDC File Offset: 0x0015B0DC
		[SecurityCritical]
		internal unsafe static int GetCodePageByteSize(int codePage)
		{
			BaseCodePageEncoding.CodePageHeader* ptr = BaseCodePageEncoding.FindCodePage(codePage);
			if (ptr == null)
			{
				return 0;
			}
			return (int)ptr->ByteCount;
		}

		// Token: 0x0600675D RID: 26461
		[SecurityCritical]
		protected abstract void LoadManagedCodePage();

		// Token: 0x0600675E RID: 26462 RVA: 0x0015CF00 File Offset: 0x0015B100
		[SecurityCritical]
		protected unsafe byte* GetSharedMemory(int iSize)
		{
			string memorySectionName = this.GetMemorySectionName();
			IntPtr intPtr;
			byte* ptr = EncodingTable.nativeCreateOpenFileMapping(memorySectionName, iSize, out intPtr);
			if (ptr == null)
			{
				throw new OutOfMemoryException(Environment.GetResourceString("Arg_OutOfMemoryException"));
			}
			if (intPtr != IntPtr.Zero)
			{
				this.safeMemorySectionHandle = new SafeViewOfFileHandle((IntPtr)((void*)ptr), true);
				this.safeFileMappingHandle = new SafeFileMappingHandle(intPtr, true);
			}
			return ptr;
		}

		// Token: 0x0600675F RID: 26463 RVA: 0x0015CF60 File Offset: 0x0015B160
		[SecurityCritical]
		protected unsafe virtual string GetMemorySectionName()
		{
			int num = (this.bFlagDataTable ? this.dataTableCodePage : this.CodePage);
			return string.Format(CultureInfo.InvariantCulture, "NLS_CodePage_{0}_{1}_{2}_{3}_{4}", new object[]
			{
				num,
				this.pCodePage->VersionMajor,
				this.pCodePage->VersionMinor,
				this.pCodePage->VersionRevision,
				this.pCodePage->VersionBuild
			});
		}

		// Token: 0x06006760 RID: 26464
		[SecurityCritical]
		protected abstract void ReadBestFitTable();

		// Token: 0x06006761 RID: 26465 RVA: 0x0015CFF0 File Offset: 0x0015B1F0
		[SecuritySafeCritical]
		internal override char[] GetBestFitUnicodeToBytesData()
		{
			if (this.arrayUnicodeBestFit == null)
			{
				this.ReadBestFitTable();
			}
			return this.arrayUnicodeBestFit;
		}

		// Token: 0x06006762 RID: 26466 RVA: 0x0015D006 File Offset: 0x0015B206
		[SecuritySafeCritical]
		internal override char[] GetBestFitBytesToUnicodeData()
		{
			if (this.arrayBytesBestFit == null)
			{
				this.ReadBestFitTable();
			}
			return this.arrayBytesBestFit;
		}

		// Token: 0x06006763 RID: 26467 RVA: 0x0015D01C File Offset: 0x0015B21C
		[SecurityCritical]
		internal void CheckMemorySection()
		{
			if (this.safeMemorySectionHandle != null && this.safeMemorySectionHandle.DangerousGetHandle() == IntPtr.Zero)
			{
				this.LoadManagedCodePage();
			}
		}

		// Token: 0x04002E31 RID: 11825
		internal const string CODE_PAGE_DATA_FILE_NAME = "codepages.nlp";

		// Token: 0x04002E32 RID: 11826
		[NonSerialized]
		protected int dataTableCodePage;

		// Token: 0x04002E33 RID: 11827
		[NonSerialized]
		protected bool bFlagDataTable;

		// Token: 0x04002E34 RID: 11828
		[NonSerialized]
		protected int iExtraBytes;

		// Token: 0x04002E35 RID: 11829
		[NonSerialized]
		protected char[] arrayUnicodeBestFit;

		// Token: 0x04002E36 RID: 11830
		[NonSerialized]
		protected char[] arrayBytesBestFit;

		// Token: 0x04002E37 RID: 11831
		[NonSerialized]
		protected bool m_bUseMlangTypeForSerialization;

		// Token: 0x04002E38 RID: 11832
		[SecurityCritical]
		private unsafe static BaseCodePageEncoding.CodePageDataFileHeader* m_pCodePageFileHeader = (BaseCodePageEncoding.CodePageDataFileHeader*)GlobalizationAssembly.GetGlobalizationResourceBytePtr(typeof(CharUnicodeInfo).Assembly, "codepages.nlp");

		// Token: 0x04002E39 RID: 11833
		[SecurityCritical]
		[NonSerialized]
		protected unsafe BaseCodePageEncoding.CodePageHeader* pCodePage;

		// Token: 0x04002E3A RID: 11834
		[SecurityCritical]
		[NonSerialized]
		protected SafeViewOfFileHandle safeMemorySectionHandle;

		// Token: 0x04002E3B RID: 11835
		[SecurityCritical]
		[NonSerialized]
		protected SafeFileMappingHandle safeFileMappingHandle;

		// Token: 0x02000CAE RID: 3246
		[StructLayout(LayoutKind.Explicit)]
		internal struct CodePageDataFileHeader
		{
			// Token: 0x04003898 RID: 14488
			[FieldOffset(0)]
			internal char TableName;

			// Token: 0x04003899 RID: 14489
			[FieldOffset(32)]
			internal ushort Version;

			// Token: 0x0400389A RID: 14490
			[FieldOffset(40)]
			internal short CodePageCount;

			// Token: 0x0400389B RID: 14491
			[FieldOffset(42)]
			internal short unused1;

			// Token: 0x0400389C RID: 14492
			[FieldOffset(44)]
			internal BaseCodePageEncoding.CodePageIndex CodePages;
		}

		// Token: 0x02000CAF RID: 3247
		[StructLayout(LayoutKind.Explicit, Pack = 2)]
		internal struct CodePageIndex
		{
			// Token: 0x0400389D RID: 14493
			[FieldOffset(0)]
			internal char CodePageName;

			// Token: 0x0400389E RID: 14494
			[FieldOffset(32)]
			internal short CodePage;

			// Token: 0x0400389F RID: 14495
			[FieldOffset(34)]
			internal short ByteCount;

			// Token: 0x040038A0 RID: 14496
			[FieldOffset(36)]
			internal int Offset;
		}

		// Token: 0x02000CB0 RID: 3248
		[StructLayout(LayoutKind.Explicit)]
		internal struct CodePageHeader
		{
			// Token: 0x040038A1 RID: 14497
			[FieldOffset(0)]
			internal char CodePageName;

			// Token: 0x040038A2 RID: 14498
			[FieldOffset(32)]
			internal ushort VersionMajor;

			// Token: 0x040038A3 RID: 14499
			[FieldOffset(34)]
			internal ushort VersionMinor;

			// Token: 0x040038A4 RID: 14500
			[FieldOffset(36)]
			internal ushort VersionRevision;

			// Token: 0x040038A5 RID: 14501
			[FieldOffset(38)]
			internal ushort VersionBuild;

			// Token: 0x040038A6 RID: 14502
			[FieldOffset(40)]
			internal short CodePage;

			// Token: 0x040038A7 RID: 14503
			[FieldOffset(42)]
			internal short ByteCount;

			// Token: 0x040038A8 RID: 14504
			[FieldOffset(44)]
			internal char UnicodeReplace;

			// Token: 0x040038A9 RID: 14505
			[FieldOffset(46)]
			internal ushort ByteReplace;

			// Token: 0x040038AA RID: 14506
			[FieldOffset(48)]
			internal short FirstDataWord;
		}
	}
}
