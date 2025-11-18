using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A7B RID: 2683
	[Serializable]
	internal sealed class MLangCodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x060068B9 RID: 26809 RVA: 0x00161F30 File Offset: 0x00160130
		internal MLangCodePageEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			try
			{
				this.m_isReadOnly = (bool)info.GetValue("m_isReadOnly", typeof(bool));
				this.encoderFallback = (EncoderFallback)info.GetValue("encoderFallback", typeof(EncoderFallback));
				this.decoderFallback = (DecoderFallback)info.GetValue("decoderFallback", typeof(DecoderFallback));
			}
			catch (SerializationException)
			{
				this.m_deserializedFromEverett = true;
				this.m_isReadOnly = true;
			}
		}

		// Token: 0x060068BA RID: 26810 RVA: 0x00161FF4 File Offset: 0x001601F4
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			this.realEncoding = Encoding.GetEncoding(this.m_codePage);
			if (!this.m_deserializedFromEverett && !this.m_isReadOnly)
			{
				this.realEncoding = (Encoding)this.realEncoding.Clone();
				this.realEncoding.EncoderFallback = this.encoderFallback;
				this.realEncoding.DecoderFallback = this.decoderFallback;
			}
			return this.realEncoding;
		}

		// Token: 0x060068BB RID: 26811 RVA: 0x00162060 File Offset: 0x00160260
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002EDE RID: 11998
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04002EDF RID: 11999
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04002EE0 RID: 12000
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04002EE1 RID: 12001
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04002EE2 RID: 12002
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04002EE3 RID: 12003
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000CB8 RID: 3256
		[Serializable]
		internal sealed class MLangEncoder : ISerializable, IObjectReference
		{
			// Token: 0x06007194 RID: 29076 RVA: 0x001867BC File Offset: 0x001849BC
			internal MLangEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06007195 RID: 29077 RVA: 0x001867F2 File Offset: 0x001849F2
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetEncoder();
			}

			// Token: 0x06007196 RID: 29078 RVA: 0x001867FF File Offset: 0x001849FF
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040038CE RID: 14542
			[NonSerialized]
			private Encoding realEncoding;
		}

		// Token: 0x02000CB9 RID: 3257
		[Serializable]
		internal sealed class MLangDecoder : ISerializable, IObjectReference
		{
			// Token: 0x06007197 RID: 29079 RVA: 0x00186810 File Offset: 0x00184A10
			internal MLangDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("m_encoding", typeof(Encoding));
			}

			// Token: 0x06007198 RID: 29080 RVA: 0x00186846 File Offset: 0x00184A46
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x06007199 RID: 29081 RVA: 0x00186853 File Offset: 0x00184A53
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040038CF RID: 14543
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
