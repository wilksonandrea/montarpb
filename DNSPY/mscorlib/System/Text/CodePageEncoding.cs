using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.Text
{
	// Token: 0x02000A5E RID: 2654
	[Serializable]
	internal sealed class CodePageEncoding : ISerializable, IObjectReference
	{
		// Token: 0x06006764 RID: 26468 RVA: 0x0015D044 File Offset: 0x0015B244
		internal CodePageEncoding(SerializationInfo info, StreamingContext context)
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

		// Token: 0x06006765 RID: 26469 RVA: 0x0015D108 File Offset: 0x0015B308
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

		// Token: 0x06006766 RID: 26470 RVA: 0x0015D174 File Offset: 0x0015B374
		[SecurityCritical]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
		}

		// Token: 0x04002E3C RID: 11836
		[NonSerialized]
		private int m_codePage;

		// Token: 0x04002E3D RID: 11837
		[NonSerialized]
		private bool m_isReadOnly;

		// Token: 0x04002E3E RID: 11838
		[NonSerialized]
		private bool m_deserializedFromEverett;

		// Token: 0x04002E3F RID: 11839
		[NonSerialized]
		private EncoderFallback encoderFallback;

		// Token: 0x04002E40 RID: 11840
		[NonSerialized]
		private DecoderFallback decoderFallback;

		// Token: 0x04002E41 RID: 11841
		[NonSerialized]
		private Encoding realEncoding;

		// Token: 0x02000CB1 RID: 3249
		[Serializable]
		internal sealed class Decoder : ISerializable, IObjectReference
		{
			// Token: 0x0600715D RID: 29021 RVA: 0x00185DAF File Offset: 0x00183FAF
			internal Decoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.realEncoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
			}

			// Token: 0x0600715E RID: 29022 RVA: 0x00185DE5 File Offset: 0x00183FE5
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				return this.realEncoding.GetDecoder();
			}

			// Token: 0x0600715F RID: 29023 RVA: 0x00185DF2 File Offset: 0x00183FF2
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_ExecutionEngineException"));
			}

			// Token: 0x040038AB RID: 14507
			[NonSerialized]
			private Encoding realEncoding;
		}
	}
}
