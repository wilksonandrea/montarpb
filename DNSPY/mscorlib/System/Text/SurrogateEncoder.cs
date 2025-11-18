using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A81 RID: 2689
	[Serializable]
	internal sealed class SurrogateEncoder : ISerializable, IObjectReference
	{
		// Token: 0x060068E1 RID: 26849 RVA: 0x00163E67 File Offset: 0x00162067
		internal SurrogateEncoder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
		}

		// Token: 0x060068E2 RID: 26850 RVA: 0x00163E9D File Offset: 0x0016209D
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			return this.realEncoding.GetEncoder();
		}

		// Token: 0x060068E3 RID: 26851 RVA: 0x00163EAA File Offset: 0x001620AA
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002F15 RID: 12053
		[NonSerialized]
		private Encoding realEncoding;
	}
}
