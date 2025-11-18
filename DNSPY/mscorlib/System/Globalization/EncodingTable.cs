using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020003B8 RID: 952
	internal static class EncodingTable
	{
		// Token: 0x06002F54 RID: 12116 RVA: 0x000B58E4 File Offset: 0x000B3AE4
		[SecuritySafeCritical]
		static EncodingTable()
		{
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000B5934 File Offset: 0x000B3B34
		[SecuritySafeCritical]
		private unsafe static int internalGetCodePageFromName(string name)
		{
			int i = 0;
			int num = EncodingTable.lastEncodingItem;
			while (num - i > 3)
			{
				int num2 = (num - i) / 2 + i;
				int num3 = string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[num2].webName);
				if (num3 == 0)
				{
					return (int)EncodingTable.encodingDataPtr[num2].codePage;
				}
				if (num3 < 0)
				{
					num = num2;
				}
				else
				{
					i = num2;
				}
			}
			while (i <= num)
			{
				if (string.nativeCompareOrdinalIgnoreCaseWC(name, EncodingTable.encodingDataPtr[i].webName) == 0)
				{
					return (int)EncodingTable.encodingDataPtr[i].codePage;
				}
				i++;
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_EncodingNotSupported"), name), "name");
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000B59F0 File Offset: 0x000B3BF0
		[SecuritySafeCritical]
		internal unsafe static EncodingInfo[] GetEncodings()
		{
			if (EncodingTable.lastCodePageItem == 0)
			{
				int num = 0;
				while (EncodingTable.codePageDataPtr[num].codePage != 0)
				{
					num++;
				}
				EncodingTable.lastCodePageItem = num;
			}
			EncodingInfo[] array = new EncodingInfo[EncodingTable.lastCodePageItem];
			for (int i = 0; i < EncodingTable.lastCodePageItem; i++)
			{
				array[i] = new EncodingInfo((int)EncodingTable.codePageDataPtr[i].codePage, CodePageDataItem.CreateString(EncodingTable.codePageDataPtr[i].Names, 0U), Environment.GetResourceString("Globalization.cp_" + EncodingTable.codePageDataPtr[i].codePage.ToString()));
			}
			return array;
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000B5AAC File Offset: 0x000B3CAC
		internal static int GetCodePageFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = EncodingTable.hashByName[name];
			if (obj != null)
			{
				return (int)obj;
			}
			int num = EncodingTable.internalGetCodePageFromName(name);
			EncodingTable.hashByName[name] = num;
			return num;
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000B5AF8 File Offset: 0x000B3CF8
		[SecuritySafeCritical]
		internal unsafe static CodePageDataItem GetCodePageDataItem(int codepage)
		{
			CodePageDataItem codePageDataItem = (CodePageDataItem)EncodingTable.hashByCodePage[codepage];
			if (codePageDataItem != null)
			{
				return codePageDataItem;
			}
			int num = 0;
			int codePage;
			while ((codePage = (int)EncodingTable.codePageDataPtr[num].codePage) != 0)
			{
				if (codePage == codepage)
				{
					codePageDataItem = new CodePageDataItem(num);
					EncodingTable.hashByCodePage[codepage] = codePageDataItem;
					return codePageDataItem;
				}
				num++;
			}
			return null;
		}

		// Token: 0x06002F59 RID: 12121
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern InternalEncodingDataItem* GetEncodingData();

		// Token: 0x06002F5A RID: 12122
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetNumEncodingItems();

		// Token: 0x06002F5B RID: 12123
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern InternalCodePageDataItem* GetCodePageData();

		// Token: 0x06002F5C RID: 12124
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern byte* nativeCreateOpenFileMapping(string inSectionName, int inBytesToAllocate, out IntPtr mappedFileHandle);

		// Token: 0x04001416 RID: 5142
		private static int lastEncodingItem = EncodingTable.GetNumEncodingItems() - 1;

		// Token: 0x04001417 RID: 5143
		private static volatile int lastCodePageItem;

		// Token: 0x04001418 RID: 5144
		[SecurityCritical]
		internal unsafe static InternalEncodingDataItem* encodingDataPtr = EncodingTable.GetEncodingData();

		// Token: 0x04001419 RID: 5145
		[SecurityCritical]
		internal unsafe static InternalCodePageDataItem* codePageDataPtr = EncodingTable.GetCodePageData();

		// Token: 0x0400141A RID: 5146
		private static Hashtable hashByName = Hashtable.Synchronized(new Hashtable(StringComparer.OrdinalIgnoreCase));

		// Token: 0x0400141B RID: 5147
		private static Hashtable hashByCodePage = Hashtable.Synchronized(new Hashtable());
	}
}
