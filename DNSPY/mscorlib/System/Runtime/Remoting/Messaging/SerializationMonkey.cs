using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Security;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200086F RID: 2159
	[Serializable]
	internal class SerializationMonkey : ISerializable, IFieldInfo
	{
		// Token: 0x06005BCE RID: 23502 RVA: 0x00141FBD File Offset: 0x001401BD
		[SecurityCritical]
		internal SerializationMonkey(SerializationInfo info, StreamingContext ctx)
		{
			this._obj.RootSetObjectData(info, ctx);
		}

		// Token: 0x06005BCF RID: 23503 RVA: 0x00141FD2 File Offset: 0x001401D2
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_Method"));
		}

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005BD0 RID: 23504 RVA: 0x00141FE3 File Offset: 0x001401E3
		// (set) Token: 0x06005BD1 RID: 23505 RVA: 0x00141FEB File Offset: 0x001401EB
		public string[] FieldNames
		{
			[SecurityCritical]
			get
			{
				return this.fieldNames;
			}
			[SecurityCritical]
			set
			{
				this.fieldNames = value;
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005BD2 RID: 23506 RVA: 0x00141FF4 File Offset: 0x001401F4
		// (set) Token: 0x06005BD3 RID: 23507 RVA: 0x00141FFC File Offset: 0x001401FC
		public Type[] FieldTypes
		{
			[SecurityCritical]
			get
			{
				return this.fieldTypes;
			}
			[SecurityCritical]
			set
			{
				this.fieldTypes = value;
			}
		}

		// Token: 0x04002987 RID: 10631
		internal ISerializationRootObject _obj;

		// Token: 0x04002988 RID: 10632
		internal string[] fieldNames;

		// Token: 0x04002989 RID: 10633
		internal Type[] fieldTypes;
	}
}
