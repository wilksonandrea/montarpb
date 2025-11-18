using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200084F RID: 2127
	[SecurityCritical]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		// Token: 0x06005A37 RID: 23095 RVA: 0x0013D493 File Offset: 0x0013B693
		public TransportHeaders()
		{
			this._headerList = new ArrayList(6);
		}

		// Token: 0x17000F00 RID: 3840
		public object this[object key]
		{
			[SecurityCritical]
			get
			{
				string text = (string)key;
				foreach (object obj in this._headerList)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (string.Compare((string)dictionaryEntry.Key, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			[SecurityCritical]
			set
			{
				if (key == null)
				{
					return;
				}
				string text = (string)key;
				for (int i = this._headerList.Count - 1; i >= 0; i--)
				{
					string text2 = (string)((DictionaryEntry)this._headerList[i]).Key;
					if (string.Compare(text2, text, StringComparison.OrdinalIgnoreCase) == 0)
					{
						this._headerList.RemoveAt(i);
						break;
					}
				}
				if (value != null)
				{
					this._headerList.Add(new DictionaryEntry(key, value));
				}
			}
		}

		// Token: 0x06005A3A RID: 23098 RVA: 0x0013D5AA File Offset: 0x0013B7AA
		[SecurityCritical]
		public IEnumerator GetEnumerator()
		{
			return this._headerList.GetEnumerator();
		}

		// Token: 0x04002905 RID: 10501
		private ArrayList _headerList;
	}
}
