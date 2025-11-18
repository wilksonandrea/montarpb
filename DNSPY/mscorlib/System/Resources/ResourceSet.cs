using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Resources
{
	// Token: 0x0200039A RID: 922
	[ComVisible(true)]
	[Serializable]
	public class ResourceSet : IDisposable, IEnumerable
	{
		// Token: 0x06002D6D RID: 11629 RVA: 0x000AD3A5 File Offset: 0x000AB5A5
		protected ResourceSet()
		{
			this.CommonInit();
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000AD3B3 File Offset: 0x000AB5B3
		internal ResourceSet(bool junk)
		{
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000AD3BB File Offset: 0x000AB5BB
		public ResourceSet(string fileName)
		{
			this.Reader = new ResourceReader(fileName);
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000AD3DB File Offset: 0x000AB5DB
		[SecurityCritical]
		public ResourceSet(Stream stream)
		{
			this.Reader = new ResourceReader(stream);
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000AD3FB File Offset: 0x000AB5FB
		public ResourceSet(IResourceReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.Reader = reader;
			this.CommonInit();
			this.ReadResources();
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000AD424 File Offset: 0x000AB624
		private void CommonInit()
		{
			this.Table = new Hashtable();
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000AD431 File Offset: 0x000AB631
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000AD43C File Offset: 0x000AB63C
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				IResourceReader reader = this.Reader;
				this.Reader = null;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this.Reader = null;
			this._caseInsensitiveTable = null;
			this.Table = null;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000AD478 File Offset: 0x000AB678
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000AD481 File Offset: 0x000AB681
		public virtual Type GetDefaultReader()
		{
			return typeof(ResourceReader);
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000AD48D File Offset: 0x000AB68D
		public virtual Type GetDefaultWriter()
		{
			return typeof(ResourceWriter);
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000AD499 File Offset: 0x000AB699
		[ComVisible(false)]
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000AD4A1 File Offset: 0x000AB6A1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorHelper();
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000AD4AC File Offset: 0x000AB6AC
		private IDictionaryEnumerator GetEnumeratorHelper()
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return table.GetEnumerator();
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000AD4DC File Offset: 0x000AB6DC
		public virtual string GetString(string name)
		{
			object objectInternal = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)objectInternal;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[] { name }));
			}
			return text;
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000AD528 File Offset: 0x000AB728
		public virtual string GetString(string name, bool ignoreCase)
		{
			object obj = this.GetObjectInternal(name);
			string text;
			try
			{
				text = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[] { name }));
			}
			if (text != null || !ignoreCase)
			{
				return text;
			}
			obj = this.GetCaseInsensitiveObjectInternal(name);
			string text2;
			try
			{
				text2 = (string)obj;
			}
			catch (InvalidCastException)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ResourceNotString_Name", new object[] { name }));
			}
			return text2;
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000AD5B4 File Offset: 0x000AB7B4
		public virtual object GetObject(string name)
		{
			return this.GetObjectInternal(name);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000AD5C0 File Offset: 0x000AB7C0
		public virtual object GetObject(string name, bool ignoreCase)
		{
			object objectInternal = this.GetObjectInternal(name);
			if (objectInternal != null || !ignoreCase)
			{
				return objectInternal;
			}
			return this.GetCaseInsensitiveObjectInternal(name);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000AD5E4 File Offset: 0x000AB7E4
		protected virtual void ReadResources()
		{
			IDictionaryEnumerator enumerator = this.Reader.GetEnumerator();
			while (enumerator.MoveNext())
			{
				object value = enumerator.Value;
				this.Table.Add(enumerator.Key, value);
			}
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000AD620 File Offset: 0x000AB820
		private object GetObjectInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			return table[name];
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000AD660 File Offset: 0x000AB860
		private object GetCaseInsensitiveObjectInternal(string name)
		{
			Hashtable table = this.Table;
			if (table == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("ObjectDisposed_ResourceSet"));
			}
			Hashtable hashtable = this._caseInsensitiveTable;
			if (hashtable == null)
			{
				hashtable = new Hashtable(StringComparer.OrdinalIgnoreCase);
				IDictionaryEnumerator enumerator = table.GetEnumerator();
				while (enumerator.MoveNext())
				{
					hashtable.Add(enumerator.Key, enumerator.Value);
				}
				this._caseInsensitiveTable = hashtable;
			}
			return hashtable[name];
		}

		// Token: 0x0400126C RID: 4716
		[NonSerialized]
		protected IResourceReader Reader;

		// Token: 0x0400126D RID: 4717
		protected Hashtable Table;

		// Token: 0x0400126E RID: 4718
		private Hashtable _caseInsensitiveTable;
	}
}
