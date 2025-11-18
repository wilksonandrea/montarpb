using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using Microsoft.Runtime.Hosting;

namespace System.Reflection
{
	// Token: 0x02000622 RID: 1570
	[ComVisible(true)]
	[Serializable]
	public class StrongNameKeyPair : IDeserializationCallback, ISerializable
	{
		// Token: 0x060048B0 RID: 18608 RVA: 0x0010744C File Offset: 0x0010564C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(FileStream keyPairFile)
		{
			if (keyPairFile == null)
			{
				throw new ArgumentNullException("keyPairFile");
			}
			int num = (int)keyPairFile.Length;
			this._keyPairArray = new byte[num];
			keyPairFile.Read(this._keyPairArray, 0, num);
			this._keyPairExported = true;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x00107497 File Offset: 0x00105697
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(byte[] keyPairArray)
		{
			if (keyPairArray == null)
			{
				throw new ArgumentNullException("keyPairArray");
			}
			this._keyPairArray = new byte[keyPairArray.Length];
			Array.Copy(keyPairArray, this._keyPairArray, keyPairArray.Length);
			this._keyPairExported = true;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x001074D1 File Offset: 0x001056D1
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public StrongNameKeyPair(string keyPairContainer)
		{
			if (keyPairContainer == null)
			{
				throw new ArgumentNullException("keyPairContainer");
			}
			this._keyPairContainer = keyPairContainer;
			this._keyPairExported = false;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x001074F8 File Offset: 0x001056F8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected StrongNameKeyPair(SerializationInfo info, StreamingContext context)
		{
			this._keyPairExported = (bool)info.GetValue("_keyPairExported", typeof(bool));
			this._keyPairArray = (byte[])info.GetValue("_keyPairArray", typeof(byte[]));
			this._keyPairContainer = (string)info.GetValue("_keyPairContainer", typeof(string));
			this._publicKey = (byte[])info.GetValue("_publicKey", typeof(byte[]));
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x060048B4 RID: 18612 RVA: 0x0010758C File Offset: 0x0010578C
		public byte[] PublicKey
		{
			[SecuritySafeCritical]
			get
			{
				if (this._publicKey == null)
				{
					this._publicKey = this.ComputePublicKey();
				}
				byte[] array = new byte[this._publicKey.Length];
				Array.Copy(this._publicKey, array, this._publicKey.Length);
				return array;
			}
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x001075D0 File Offset: 0x001057D0
		[SecurityCritical]
		private unsafe byte[] ComputePublicKey()
		{
			byte[] array = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				IntPtr zero = IntPtr.Zero;
				int num = 0;
				try
				{
					bool flag;
					if (this._keyPairExported)
					{
						flag = StrongNameHelpers.StrongNameGetPublicKey(null, this._keyPairArray, this._keyPairArray.Length, out zero, out num);
					}
					else
					{
						flag = StrongNameHelpers.StrongNameGetPublicKey(this._keyPairContainer, null, 0, out zero, out num);
					}
					if (!flag)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_StrongNameGetPublicKey"));
					}
					array = new byte[num];
					Buffer.Memcpy(array, 0, (byte*)zero.ToPointer(), 0, num);
				}
				finally
				{
					if (zero != IntPtr.Zero)
					{
						StrongNameHelpers.StrongNameFreeBuffer(zero);
					}
				}
			}
			return array;
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x00107684 File Offset: 0x00105884
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_keyPairExported", this._keyPairExported);
			info.AddValue("_keyPairArray", this._keyPairArray);
			info.AddValue("_keyPairContainer", this._keyPairContainer);
			info.AddValue("_publicKey", this._publicKey);
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x001076D5 File Offset: 0x001058D5
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x001076D7 File Offset: 0x001058D7
		private bool GetKeyPair(out object arrayOrContainer)
		{
			arrayOrContainer = (this._keyPairExported ? this._keyPairArray : this._keyPairContainer);
			return this._keyPairExported;
		}

		// Token: 0x04001E24 RID: 7716
		private bool _keyPairExported;

		// Token: 0x04001E25 RID: 7717
		private byte[] _keyPairArray;

		// Token: 0x04001E26 RID: 7718
		private string _keyPairContainer;

		// Token: 0x04001E27 RID: 7719
		private byte[] _publicKey;
	}
}
