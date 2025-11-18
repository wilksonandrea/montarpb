using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;
using Microsoft.Win32;

namespace System.Text
{
	// Token: 0x02000A75 RID: 2677
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class Encoding : ICloneable
	{
		// Token: 0x06006826 RID: 26662 RVA: 0x0015F8DC File Offset: 0x0015DADC
		[__DynamicallyInvokable]
		protected Encoding()
			: this(0)
		{
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x0015F8E5 File Offset: 0x0015DAE5
		[__DynamicallyInvokable]
		protected Encoding(int codePage)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.SetDefaultFallbacks();
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x0015F910 File Offset: 0x0015DB10
		[__DynamicallyInvokable]
		protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			this.m_isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this.m_codePage = codePage;
			this.encoderFallback = encoderFallback ?? new InternalEncoderBestFitFallback(this);
			this.decoderFallback = decoderFallback ?? new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x0015F962 File Offset: 0x0015DB62
		internal virtual void SetDefaultFallbacks()
		{
			this.encoderFallback = new InternalEncoderBestFitFallback(this);
			this.decoderFallback = new InternalDecoderBestFitFallback(this);
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x0015F97C File Offset: 0x0015DB7C
		internal void OnDeserializing()
		{
			this.encoderFallback = null;
			this.decoderFallback = null;
			this.m_isReadOnly = true;
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x0015F993 File Offset: 0x0015DB93
		internal void OnDeserialized()
		{
			if (this.encoderFallback == null || this.decoderFallback == null)
			{
				this.m_deserializedFromEverett = true;
				this.SetDefaultFallbacks();
			}
			this.dataItem = null;
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x0015F9B9 File Offset: 0x0015DBB9
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.OnDeserializing();
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x0015F9C1 File Offset: 0x0015DBC1
		[OnDeserialized]
		private void OnDeserialized(StreamingContext ctx)
		{
			this.OnDeserialized();
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x0015F9C9 File Offset: 0x0015DBC9
		[OnSerializing]
		private void OnSerializing(StreamingContext ctx)
		{
			this.dataItem = null;
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x0015F9D4 File Offset: 0x0015DBD4
		internal void DeserializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.m_codePage = (int)info.GetValue("m_codePage", typeof(int));
			this.dataItem = null;
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
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x0015FAA0 File Offset: 0x0015DCA0
		internal void SerializeEncoding(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_isReadOnly", this.m_isReadOnly);
			info.AddValue("encoderFallback", this.EncoderFallback);
			info.AddValue("decoderFallback", this.DecoderFallback);
			info.AddValue("m_codePage", this.m_codePage);
			info.AddValue("dataItem", null);
			info.AddValue("Encoding+m_codePage", this.m_codePage);
			info.AddValue("Encoding+dataItem", null);
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x0015FB28 File Offset: 0x0015DD28
		[__DynamicallyInvokable]
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x0015FB44 File Offset: 0x0015DD44
		[__DynamicallyInvokable]
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
		{
			if (srcEncoding == null || dstEncoding == null)
			{
				throw new ArgumentNullException((srcEncoding == null) ? "srcEncoding" : "dstEncoding", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06006833 RID: 26675 RVA: 0x0015FBA0 File Offset: 0x0015DDA0
		private static object InternalSyncObject
		{
			get
			{
				if (Encoding.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref Encoding.s_InternalSyncObject, obj, null);
				}
				return Encoding.s_InternalSyncObject;
			}
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x0015FBCC File Offset: 0x0015DDCC
		[SecurityCritical]
		[__DynamicallyInvokable]
		public static void RegisterProvider(EncodingProvider provider)
		{
			EncodingProvider.AddProvider(provider);
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x0015FBD4 File Offset: 0x0015DDD4
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(int codepage)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage);
			if (encoding != null)
			{
				return encoding;
			}
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", Environment.GetResourceString("ArgumentOutOfRange_Range", new object[] { 0, 65535 }));
			}
			if (Encoding.encodings != null)
			{
				encoding = (Encoding)Encoding.encodings[codepage];
			}
			if (encoding == null)
			{
				object internalSyncObject = Encoding.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (Encoding.encodings == null)
					{
						Encoding.encodings = new Hashtable();
					}
					if ((encoding = (Encoding)Encoding.encodings[codepage]) != null)
					{
						return encoding;
					}
					if (codepage <= 1200)
					{
						if (codepage <= 3)
						{
							if (codepage == 0)
							{
								encoding = Encoding.Default;
								goto IL_185;
							}
							if (codepage - 1 > 2)
							{
								goto IL_174;
							}
						}
						else if (codepage != 42)
						{
							if (codepage != 1200)
							{
								goto IL_174;
							}
							encoding = Encoding.Unicode;
							goto IL_185;
						}
						throw new ArgumentException(Environment.GetResourceString("Argument_CodepageNotSupported", new object[] { codepage }), "codepage");
					}
					if (codepage <= 1252)
					{
						if (codepage == 1201)
						{
							encoding = Encoding.BigEndianUnicode;
							goto IL_185;
						}
						if (codepage == 1252)
						{
							encoding = new SBCSCodePageEncoding(codepage);
							goto IL_185;
						}
					}
					else
					{
						if (codepage == 20127)
						{
							encoding = Encoding.ASCII;
							goto IL_185;
						}
						if (codepage == 28591)
						{
							encoding = Encoding.Latin1;
							goto IL_185;
						}
						if (codepage == 65001)
						{
							encoding = Encoding.UTF8;
							goto IL_185;
						}
					}
					IL_174:
					encoding = Encoding.GetEncodingCodePage(codepage);
					if (encoding == null)
					{
						encoding = Encoding.GetEncodingRare(codepage);
					}
					IL_185:
					Encoding.encodings.Add(codepage, encoding);
				}
				return encoding;
			}
			return encoding;
		}

		// Token: 0x06006836 RID: 26678 RVA: 0x0015FDA4 File Offset: 0x0015DFA4
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback);
			if (encoding != null)
			{
				return encoding;
			}
			encoding = Encoding.GetEncoding(codepage);
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = encoderFallback;
			encoding2.DecoderFallback = decoderFallback;
			return encoding2;
		}

		// Token: 0x06006837 RID: 26679 RVA: 0x0015FDE4 File Offset: 0x0015DFE4
		[SecurityCritical]
		private static Encoding GetEncodingRare(int codepage)
		{
			if (codepage <= 50229)
			{
				if (codepage <= 12000)
				{
					if (codepage == 10003)
					{
						return new DBCSCodePageEncoding(10003, 20949);
					}
					if (codepage == 10008)
					{
						return new DBCSCodePageEncoding(10008, 20936);
					}
					if (codepage != 12000)
					{
						goto IL_192;
					}
					return Encoding.UTF32;
				}
				else
				{
					if (codepage == 12001)
					{
						return new UTF32Encoding(true, true);
					}
					if (codepage == 38598)
					{
						return new SBCSCodePageEncoding(codepage, 28598);
					}
					switch (codepage)
					{
					case 50220:
					case 50221:
					case 50222:
					case 50225:
						break;
					case 50223:
					case 50224:
					case 50226:
					case 50228:
						goto IL_192;
					case 50227:
						goto IL_150;
					case 50229:
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_CodePage50229"));
					default:
						goto IL_192;
					}
				}
			}
			else if (codepage <= 51949)
			{
				if (codepage == 51932)
				{
					return new EUCJPEncoding();
				}
				if (codepage == 51936)
				{
					goto IL_150;
				}
				if (codepage != 51949)
				{
					goto IL_192;
				}
				return new DBCSCodePageEncoding(codepage, 20949);
			}
			else if (codepage <= 54936)
			{
				if (codepage != 52936)
				{
					if (codepage != 54936)
					{
						goto IL_192;
					}
					return new GB18030Encoding();
				}
			}
			else
			{
				if (codepage - 57002 <= 9)
				{
					return new ISCIIEncoding(codepage);
				}
				if (codepage == 65000)
				{
					return Encoding.UTF7;
				}
				goto IL_192;
			}
			return new ISO2022Encoding(codepage);
			IL_150:
			return new DBCSCodePageEncoding(codepage, 936);
			IL_192:
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[] { codepage }));
		}

		// Token: 0x06006838 RID: 26680 RVA: 0x0015FFA4 File Offset: 0x0015E1A4
		[SecurityCritical]
		private static Encoding GetEncodingCodePage(int CodePage)
		{
			int codePageByteSize = BaseCodePageEncoding.GetCodePageByteSize(CodePage);
			if (codePageByteSize == 1)
			{
				return new SBCSCodePageEncoding(CodePage);
			}
			if (codePageByteSize == 2)
			{
				return new DBCSCodePageEncoding(CodePage);
			}
			return null;
		}

		// Token: 0x06006839 RID: 26681 RVA: 0x0015FFD0 File Offset: 0x0015E1D0
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(string name)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
		}

		// Token: 0x0600683A RID: 26682 RVA: 0x0015FFF4 File Offset: 0x0015E1F4
		[__DynamicallyInvokable]
		public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encodingFromProvider = EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback);
			if (encodingFromProvider != null)
			{
				return encodingFromProvider;
			}
			return Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
		}

		// Token: 0x0600683B RID: 26683 RVA: 0x0016001C File Offset: 0x0015E21C
		public static EncodingInfo[] GetEncodings()
		{
			return EncodingTable.GetEncodings();
		}

		// Token: 0x0600683C RID: 26684 RVA: 0x00160023 File Offset: 0x0015E223
		[__DynamicallyInvokable]
		public virtual byte[] GetPreamble()
		{
			return EmptyArray<byte>.Value;
		}

		// Token: 0x0600683D RID: 26685 RVA: 0x0016002C File Offset: 0x0015E22C
		private void GetDataItem()
		{
			if (this.dataItem == null)
			{
				this.dataItem = EncodingTable.GetCodePageDataItem(this.m_codePage);
				if (this.dataItem == null)
				{
					throw new NotSupportedException(Environment.GetResourceString("NotSupported_NoCodepageData", new object[] { this.m_codePage }));
				}
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600683E RID: 26686 RVA: 0x0016007E File Offset: 0x0015E27E
		public virtual string BodyName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.BodyName;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x0600683F RID: 26687 RVA: 0x00160099 File Offset: 0x0015E299
		[__DynamicallyInvokable]
		public virtual string EncodingName
		{
			[__DynamicallyInvokable]
			get
			{
				return Environment.GetResourceString("Globalization.cp_" + this.m_codePage.ToString());
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x06006840 RID: 26688 RVA: 0x001600B5 File Offset: 0x0015E2B5
		public virtual string HeaderName
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.HeaderName;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x06006841 RID: 26689 RVA: 0x001600D0 File Offset: 0x0015E2D0
		[__DynamicallyInvokable]
		public virtual string WebName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.WebName;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x06006842 RID: 26690 RVA: 0x001600EB File Offset: 0x0015E2EB
		public virtual int WindowsCodePage
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return this.dataItem.UIFamilyCodePage;
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06006843 RID: 26691 RVA: 0x00160106 File Offset: 0x0015E306
		public virtual bool IsBrowserDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 2U) > 0U;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06006844 RID: 26692 RVA: 0x00160126 File Offset: 0x0015E326
		public virtual bool IsBrowserSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 512U) > 0U;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06006845 RID: 26693 RVA: 0x0016014A File Offset: 0x0015E34A
		public virtual bool IsMailNewsDisplay
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 1U) > 0U;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06006846 RID: 26694 RVA: 0x0016016A File Offset: 0x0015E36A
		public virtual bool IsMailNewsSave
		{
			get
			{
				if (this.dataItem == null)
				{
					this.GetDataItem();
				}
				return (this.dataItem.Flags & 256U) > 0U;
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06006847 RID: 26695 RVA: 0x0016018E File Offset: 0x0015E38E
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual bool IsSingleByte
		{
			[__DynamicallyInvokable]
			get
			{
				return false;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06006848 RID: 26696 RVA: 0x00160191 File Offset: 0x0015E391
		// (set) Token: 0x06006849 RID: 26697 RVA: 0x00160199 File Offset: 0x0015E399
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public EncoderFallback EncoderFallback
		{
			[__DynamicallyInvokable]
			get
			{
				return this.encoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoderFallback = value;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x0600684A RID: 26698 RVA: 0x001601C8 File Offset: 0x0015E3C8
		// (set) Token: 0x0600684B RID: 26699 RVA: 0x001601D0 File Offset: 0x0015E3D0
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public DecoderFallback DecoderFallback
		{
			[__DynamicallyInvokable]
			get
			{
				return this.decoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_ReadOnly"));
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.decoderFallback = value;
			}
		}

		// Token: 0x0600684C RID: 26700 RVA: 0x00160200 File Offset: 0x0015E400
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding.m_isReadOnly = false;
			return encoding;
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x0600684D RID: 26701 RVA: 0x00160221 File Offset: 0x0015E421
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x0600684E RID: 26702 RVA: 0x00160229 File Offset: 0x0015E429
		[__DynamicallyInvokable]
		public static Encoding ASCII
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.asciiEncoding == null)
				{
					Encoding.asciiEncoding = new ASCIIEncoding();
				}
				return Encoding.asciiEncoding;
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600684F RID: 26703 RVA: 0x00160247 File Offset: 0x0015E447
		private static Encoding Latin1
		{
			get
			{
				if (Encoding.latin1Encoding == null)
				{
					Encoding.latin1Encoding = new Latin1Encoding();
				}
				return Encoding.latin1Encoding;
			}
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x00160265 File Offset: 0x0015E465
		[__DynamicallyInvokable]
		public virtual int GetByteCount(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetByteCount(chars, 0, chars.Length);
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x0016028C File Offset: 0x0015E48C
		[__DynamicallyInvokable]
		public virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char[] array = s.ToCharArray();
			return this.GetByteCount(array, 0, array.Length);
		}

		// Token: 0x06006852 RID: 26706
		[__DynamicallyInvokable]
		public abstract int GetByteCount(char[] chars, int index, int count);

		// Token: 0x06006853 RID: 26707 RVA: 0x001602BC File Offset: 0x0015E4BC
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count);
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x00160322 File Offset: 0x0015E522
		[SecurityCritical]
		internal unsafe virtual int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			return this.GetByteCount(chars, count);
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x0016032C File Offset: 0x0015E52C
		[__DynamicallyInvokable]
		public virtual byte[] GetBytes(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetBytes(chars, 0, chars.Length);
		}

		// Token: 0x06006856 RID: 26710 RVA: 0x00160354 File Offset: 0x0015E554
		[__DynamicallyInvokable]
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] array = new byte[this.GetByteCount(chars, index, count)];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		// Token: 0x06006857 RID: 26711
		[__DynamicallyInvokable]
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		// Token: 0x06006858 RID: 26712 RVA: 0x00160380 File Offset: 0x0015E580
		[__DynamicallyInvokable]
		public virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", Environment.GetResourceString("ArgumentNull_String"));
			}
			int byteCount = this.GetByteCount(s);
			byte[] array = new byte[byteCount];
			int bytes = this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		// Token: 0x06006859 RID: 26713 RVA: 0x001603C6 File Offset: 0x0015E5C6
		[__DynamicallyInvokable]
		public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x0600685A RID: 26714 RVA: 0x001603E8 File Offset: 0x0015E5E8
		[SecurityCritical]
		internal unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			return this.GetBytes(chars, charCount, bytes, byteCount);
		}

		// Token: 0x0600685B RID: 26715 RVA: 0x001603F8 File Offset: 0x0015E5F8
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		// Token: 0x0600685C RID: 26716 RVA: 0x001604A8 File Offset: 0x0015E6A8
		[__DynamicallyInvokable]
		public virtual int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetCharCount(bytes, 0, bytes.Length);
		}

		// Token: 0x0600685D RID: 26717
		[__DynamicallyInvokable]
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x0600685E RID: 26718 RVA: 0x001604D0 File Offset: 0x0015E6D0
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x00160533 File Offset: 0x0015E733
		[SecurityCritical]
		internal unsafe virtual int GetCharCount(byte* bytes, int count, DecoderNLS decoder)
		{
			return this.GetCharCount(bytes, count);
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x0016053D File Offset: 0x0015E73D
		[__DynamicallyInvokable]
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetChars(bytes, 0, bytes.Length);
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x00160564 File Offset: 0x0015E764
		[__DynamicallyInvokable]
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] array = new char[this.GetCharCount(bytes, index, count)];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		// Token: 0x06006862 RID: 26722
		[__DynamicallyInvokable]
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x06006863 RID: 26723 RVA: 0x00160590 File Offset: 0x0015E790
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x00160640 File Offset: 0x0015E840
		[SecurityCritical]
		internal unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS decoder)
		{
			return this.GetChars(bytes, byteCount, chars, charCount);
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x0016064D File Offset: 0x0015E84D
		[SecurityCritical]
		[CLSCompliant(false)]
		[ComVisible(false)]
		public unsafe string GetString(byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			return string.CreateStringFromEncoding(bytes, byteCount, this);
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x06006866 RID: 26726 RVA: 0x0016068A File Offset: 0x0015E88A
		[__DynamicallyInvokable]
		public virtual int CodePage
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_codePage;
			}
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x00160692 File Offset: 0x0015E892
		[ComVisible(false)]
		public bool IsAlwaysNormalized()
		{
			return this.IsAlwaysNormalized(NormalizationForm.FormC);
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x0016069B File Offset: 0x0015E89B
		[ComVisible(false)]
		public virtual bool IsAlwaysNormalized(NormalizationForm form)
		{
			return false;
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x0016069E File Offset: 0x0015E89E
		[__DynamicallyInvokable]
		public virtual Decoder GetDecoder()
		{
			return new Encoding.DefaultDecoder(this);
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x001606A8 File Offset: 0x0015E8A8
		[SecurityCritical]
		private static Encoding CreateDefaultEncoding()
		{
			int acp = Win32Native.GetACP();
			Encoding encoding;
			if (acp == 1252)
			{
				encoding = new SBCSCodePageEncoding(acp);
			}
			else if (acp == 65001)
			{
				encoding = Encoding.s_defaultUtf8EncodingNoBom;
			}
			else
			{
				encoding = Encoding.GetEncoding(acp);
			}
			return encoding;
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x0600686B RID: 26731 RVA: 0x001606E4 File Offset: 0x0015E8E4
		public static Encoding Default
		{
			[SecuritySafeCritical]
			get
			{
				if (Encoding.defaultEncoding == null)
				{
					Encoding.defaultEncoding = Encoding.CreateDefaultEncoding();
				}
				return Encoding.defaultEncoding;
			}
		}

		// Token: 0x0600686C RID: 26732 RVA: 0x00160702 File Offset: 0x0015E902
		[__DynamicallyInvokable]
		public virtual Encoder GetEncoder()
		{
			return new Encoding.DefaultEncoder(this);
		}

		// Token: 0x0600686D RID: 26733
		[__DynamicallyInvokable]
		public abstract int GetMaxByteCount(int charCount);

		// Token: 0x0600686E RID: 26734
		[__DynamicallyInvokable]
		public abstract int GetMaxCharCount(int byteCount);

		// Token: 0x0600686F RID: 26735 RVA: 0x0016070A File Offset: 0x0015E90A
		[__DynamicallyInvokable]
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", Environment.GetResourceString("ArgumentNull_Array"));
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06006870 RID: 26736 RVA: 0x0016072F File Offset: 0x0015E92F
		[__DynamicallyInvokable]
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06006871 RID: 26737 RVA: 0x0016073F File Offset: 0x0015E93F
		[__DynamicallyInvokable]
		public static Encoding Unicode
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.unicodeEncoding == null)
				{
					Encoding.unicodeEncoding = new UnicodeEncoding(false, true);
				}
				return Encoding.unicodeEncoding;
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06006872 RID: 26738 RVA: 0x0016075F File Offset: 0x0015E95F
		[__DynamicallyInvokable]
		public static Encoding BigEndianUnicode
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.bigEndianUnicode == null)
				{
					Encoding.bigEndianUnicode = new UnicodeEncoding(true, true);
				}
				return Encoding.bigEndianUnicode;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06006873 RID: 26739 RVA: 0x0016077F File Offset: 0x0015E97F
		[__DynamicallyInvokable]
		public static Encoding UTF7
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.utf7Encoding == null)
				{
					Encoding.utf7Encoding = new UTF7Encoding();
				}
				return Encoding.utf7Encoding;
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06006874 RID: 26740 RVA: 0x0016079D File Offset: 0x0015E99D
		[__DynamicallyInvokable]
		public static Encoding UTF8
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.utf8Encoding == null)
				{
					Encoding.utf8Encoding = new UTF8Encoding(true);
				}
				return Encoding.utf8Encoding;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06006875 RID: 26741 RVA: 0x001607BC File Offset: 0x0015E9BC
		[__DynamicallyInvokable]
		public static Encoding UTF32
		{
			[__DynamicallyInvokable]
			get
			{
				if (Encoding.utf32Encoding == null)
				{
					Encoding.utf32Encoding = new UTF32Encoding(false, true);
				}
				return Encoding.utf32Encoding;
			}
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x001607DC File Offset: 0x0015E9DC
		[__DynamicallyInvokable]
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && (this.m_codePage == encoding.m_codePage && this.EncoderFallback.Equals(encoding.EncoderFallback)) && this.DecoderFallback.Equals(encoding.DecoderFallback);
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x00160829 File Offset: 0x0015EA29
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return this.m_codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
		}

		// Token: 0x06006878 RID: 26744 RVA: 0x00160849 File Offset: 0x0015EA49
		internal virtual char[] GetBestFitUnicodeToBytesData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x06006879 RID: 26745 RVA: 0x00160850 File Offset: 0x0015EA50
		internal virtual char[] GetBestFitBytesToUnicodeData()
		{
			return EmptyArray<char>.Value;
		}

		// Token: 0x0600687A RID: 26746 RVA: 0x00160857 File Offset: 0x0015EA57
		internal void ThrowBytesOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowBytes", new object[]
			{
				this.EncodingName,
				this.EncoderFallback.GetType()
			}), "bytes");
		}

		// Token: 0x0600687B RID: 26747 RVA: 0x0016088A File Offset: 0x0015EA8A
		[SecurityCritical]
		internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
		{
			if (encoder == null || encoder.m_throwOnOverflow || nothingEncoded)
			{
				if (encoder != null && encoder.InternalHasFallbackBuffer)
				{
					encoder.FallbackBuffer.InternalReset();
				}
				this.ThrowBytesOverflow();
			}
			encoder.ClearMustFlush();
		}

		// Token: 0x0600687C RID: 26748 RVA: 0x001608BE File Offset: 0x0015EABE
		internal void ThrowCharsOverflow()
		{
			throw new ArgumentException(Environment.GetResourceString("Argument_EncodingConversionOverflowChars", new object[]
			{
				this.EncodingName,
				this.DecoderFallback.GetType()
			}), "chars");
		}

		// Token: 0x0600687D RID: 26749 RVA: 0x001608F1 File Offset: 0x0015EAF1
		[SecurityCritical]
		internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
		{
			if (decoder == null || decoder.m_throwOnOverflow || nothingDecoded)
			{
				if (decoder != null && decoder.InternalHasFallbackBuffer)
				{
					decoder.FallbackBuffer.InternalReset();
				}
				this.ThrowCharsOverflow();
			}
			decoder.ClearMustFlush();
		}

		// Token: 0x0600687E RID: 26750 RVA: 0x00160925 File Offset: 0x0015EB25
		// Note: this type is marked as 'beforefieldinit'.
		static Encoding()
		{
		}

		// Token: 0x04002E7E RID: 11902
		private static readonly UTF8Encoding.UTF8EncodingSealed s_defaultUtf8EncodingNoBom = new UTF8Encoding.UTF8EncodingSealed(false);

		// Token: 0x04002E7F RID: 11903
		private static volatile Encoding defaultEncoding;

		// Token: 0x04002E80 RID: 11904
		private static volatile Encoding unicodeEncoding;

		// Token: 0x04002E81 RID: 11905
		private static volatile Encoding bigEndianUnicode;

		// Token: 0x04002E82 RID: 11906
		private static volatile Encoding utf7Encoding;

		// Token: 0x04002E83 RID: 11907
		private static volatile Encoding utf8Encoding;

		// Token: 0x04002E84 RID: 11908
		private static volatile Encoding utf32Encoding;

		// Token: 0x04002E85 RID: 11909
		private static volatile Encoding asciiEncoding;

		// Token: 0x04002E86 RID: 11910
		private static volatile Encoding latin1Encoding;

		// Token: 0x04002E87 RID: 11911
		private static volatile Hashtable encodings;

		// Token: 0x04002E88 RID: 11912
		private const int MIMECONTF_MAILNEWS = 1;

		// Token: 0x04002E89 RID: 11913
		private const int MIMECONTF_BROWSER = 2;

		// Token: 0x04002E8A RID: 11914
		private const int MIMECONTF_SAVABLE_MAILNEWS = 256;

		// Token: 0x04002E8B RID: 11915
		private const int MIMECONTF_SAVABLE_BROWSER = 512;

		// Token: 0x04002E8C RID: 11916
		private const int CodePageDefault = 0;

		// Token: 0x04002E8D RID: 11917
		private const int CodePageNoOEM = 1;

		// Token: 0x04002E8E RID: 11918
		private const int CodePageNoMac = 2;

		// Token: 0x04002E8F RID: 11919
		private const int CodePageNoThread = 3;

		// Token: 0x04002E90 RID: 11920
		private const int CodePageNoSymbol = 42;

		// Token: 0x04002E91 RID: 11921
		private const int CodePageUnicode = 1200;

		// Token: 0x04002E92 RID: 11922
		private const int CodePageBigEndian = 1201;

		// Token: 0x04002E93 RID: 11923
		private const int CodePageWindows1252 = 1252;

		// Token: 0x04002E94 RID: 11924
		private const int CodePageMacGB2312 = 10008;

		// Token: 0x04002E95 RID: 11925
		private const int CodePageGB2312 = 20936;

		// Token: 0x04002E96 RID: 11926
		private const int CodePageMacKorean = 10003;

		// Token: 0x04002E97 RID: 11927
		private const int CodePageDLLKorean = 20949;

		// Token: 0x04002E98 RID: 11928
		private const int ISO2022JP = 50220;

		// Token: 0x04002E99 RID: 11929
		private const int ISO2022JPESC = 50221;

		// Token: 0x04002E9A RID: 11930
		private const int ISO2022JPSISO = 50222;

		// Token: 0x04002E9B RID: 11931
		private const int ISOKorean = 50225;

		// Token: 0x04002E9C RID: 11932
		private const int ISOSimplifiedCN = 50227;

		// Token: 0x04002E9D RID: 11933
		private const int EUCJP = 51932;

		// Token: 0x04002E9E RID: 11934
		private const int ChineseHZ = 52936;

		// Token: 0x04002E9F RID: 11935
		private const int DuplicateEUCCN = 51936;

		// Token: 0x04002EA0 RID: 11936
		private const int EUCCN = 936;

		// Token: 0x04002EA1 RID: 11937
		private const int EUCKR = 51949;

		// Token: 0x04002EA2 RID: 11938
		internal const int CodePageASCII = 20127;

		// Token: 0x04002EA3 RID: 11939
		internal const int ISO_8859_1 = 28591;

		// Token: 0x04002EA4 RID: 11940
		private const int ISCIIAssemese = 57006;

		// Token: 0x04002EA5 RID: 11941
		private const int ISCIIBengali = 57003;

		// Token: 0x04002EA6 RID: 11942
		private const int ISCIIDevanagari = 57002;

		// Token: 0x04002EA7 RID: 11943
		private const int ISCIIGujarathi = 57010;

		// Token: 0x04002EA8 RID: 11944
		private const int ISCIIKannada = 57008;

		// Token: 0x04002EA9 RID: 11945
		private const int ISCIIMalayalam = 57009;

		// Token: 0x04002EAA RID: 11946
		private const int ISCIIOriya = 57007;

		// Token: 0x04002EAB RID: 11947
		private const int ISCIIPanjabi = 57011;

		// Token: 0x04002EAC RID: 11948
		private const int ISCIITamil = 57004;

		// Token: 0x04002EAD RID: 11949
		private const int ISCIITelugu = 57005;

		// Token: 0x04002EAE RID: 11950
		private const int GB18030 = 54936;

		// Token: 0x04002EAF RID: 11951
		private const int ISO_8859_8I = 38598;

		// Token: 0x04002EB0 RID: 11952
		private const int ISO_8859_8_Visual = 28598;

		// Token: 0x04002EB1 RID: 11953
		private const int ENC50229 = 50229;

		// Token: 0x04002EB2 RID: 11954
		private const int CodePageUTF7 = 65000;

		// Token: 0x04002EB3 RID: 11955
		private const int CodePageUTF8 = 65001;

		// Token: 0x04002EB4 RID: 11956
		private const int CodePageUTF32 = 12000;

		// Token: 0x04002EB5 RID: 11957
		private const int CodePageUTF32BE = 12001;

		// Token: 0x04002EB6 RID: 11958
		internal int m_codePage;

		// Token: 0x04002EB7 RID: 11959
		internal CodePageDataItem dataItem;

		// Token: 0x04002EB8 RID: 11960
		[NonSerialized]
		internal bool m_deserializedFromEverett;

		// Token: 0x04002EB9 RID: 11961
		[OptionalField(VersionAdded = 2)]
		private bool m_isReadOnly;

		// Token: 0x04002EBA RID: 11962
		[OptionalField(VersionAdded = 2)]
		internal EncoderFallback encoderFallback;

		// Token: 0x04002EBB RID: 11963
		[OptionalField(VersionAdded = 2)]
		internal DecoderFallback decoderFallback;

		// Token: 0x04002EBC RID: 11964
		private static object s_InternalSyncObject;

		// Token: 0x02000CB2 RID: 3250
		[Serializable]
		internal class DefaultEncoder : Encoder, ISerializable, IObjectReference
		{
			// Token: 0x06007160 RID: 29024 RVA: 0x00185E03 File Offset: 0x00184003
			public DefaultEncoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06007161 RID: 29025 RVA: 0x00185E1C File Offset: 0x0018401C
			internal DefaultEncoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.m_fallback = (EncoderFallback)info.GetValue("m_fallback", typeof(EncoderFallback));
					this.charLeftOver = (char)info.GetValue("charLeftOver", typeof(char));
				}
				catch (SerializationException)
				{
				}
			}

			// Token: 0x06007162 RID: 29026 RVA: 0x00185EB4 File Offset: 0x001840B4
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Encoder encoder = this.m_encoding.GetEncoder();
				if (this.m_fallback != null)
				{
					encoder.m_fallback = this.m_fallback;
				}
				if (this.charLeftOver != '\0')
				{
					EncoderNLS encoderNLS = encoder as EncoderNLS;
					if (encoderNLS != null)
					{
						encoderNLS.charLeftOver = this.charLeftOver;
					}
				}
				return encoder;
			}

			// Token: 0x06007163 RID: 29027 RVA: 0x00185F0A File Offset: 0x0018410A
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x06007164 RID: 29028 RVA: 0x00185F2B File Offset: 0x0018412B
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x06007165 RID: 29029 RVA: 0x00185F3B File Offset: 0x0018413B
			[SecurityCritical]
			public unsafe override int GetByteCount(char* chars, int count, bool flush)
			{
				return this.m_encoding.GetByteCount(chars, count);
			}

			// Token: 0x06007166 RID: 29030 RVA: 0x00185F4A File Offset: 0x0018414A
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			// Token: 0x06007167 RID: 29031 RVA: 0x00185F5E File Offset: 0x0018415E
			[SecurityCritical]
			public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
			{
				return this.m_encoding.GetBytes(chars, charCount, bytes, byteCount);
			}

			// Token: 0x040038AC RID: 14508
			private Encoding m_encoding;

			// Token: 0x040038AD RID: 14509
			[NonSerialized]
			private bool m_hasInitializedEncoding;

			// Token: 0x040038AE RID: 14510
			[NonSerialized]
			internal char charLeftOver;
		}

		// Token: 0x02000CB3 RID: 3251
		[Serializable]
		internal class DefaultDecoder : Decoder, ISerializable, IObjectReference
		{
			// Token: 0x06007168 RID: 29032 RVA: 0x00185F70 File Offset: 0x00184170
			public DefaultDecoder(Encoding encoding)
			{
				this.m_encoding = encoding;
				this.m_hasInitializedEncoding = true;
			}

			// Token: 0x06007169 RID: 29033 RVA: 0x00185F88 File Offset: 0x00184188
			internal DefaultDecoder(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				this.m_encoding = (Encoding)info.GetValue("encoding", typeof(Encoding));
				try
				{
					this.m_fallback = (DecoderFallback)info.GetValue("m_fallback", typeof(DecoderFallback));
				}
				catch (SerializationException)
				{
					this.m_fallback = null;
				}
			}

			// Token: 0x0600716A RID: 29034 RVA: 0x00186008 File Offset: 0x00184208
			[SecurityCritical]
			public object GetRealObject(StreamingContext context)
			{
				if (this.m_hasInitializedEncoding)
				{
					return this;
				}
				Decoder decoder = this.m_encoding.GetDecoder();
				if (this.m_fallback != null)
				{
					decoder.m_fallback = this.m_fallback;
				}
				return decoder;
			}

			// Token: 0x0600716B RID: 29035 RVA: 0x00186040 File Offset: 0x00184240
			[SecurityCritical]
			void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
			{
				if (info == null)
				{
					throw new ArgumentNullException("info");
				}
				info.AddValue("encoding", this.m_encoding);
			}

			// Token: 0x0600716C RID: 29036 RVA: 0x00186061 File Offset: 0x00184261
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.GetCharCount(bytes, index, count, false);
			}

			// Token: 0x0600716D RID: 29037 RVA: 0x0018606D File Offset: 0x0018426D
			public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x0600716E RID: 29038 RVA: 0x0018607D File Offset: 0x0018427D
			[SecurityCritical]
			public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
			{
				return this.m_encoding.GetCharCount(bytes, count);
			}

			// Token: 0x0600716F RID: 29039 RVA: 0x0018608C File Offset: 0x0018428C
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
			}

			// Token: 0x06007170 RID: 29040 RVA: 0x0018609C File Offset: 0x0018429C
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x06007171 RID: 29041 RVA: 0x001860B0 File Offset: 0x001842B0
			[SecurityCritical]
			public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
			{
				return this.m_encoding.GetChars(bytes, byteCount, chars, charCount);
			}

			// Token: 0x040038AF RID: 14511
			private Encoding m_encoding;

			// Token: 0x040038B0 RID: 14512
			[NonSerialized]
			private bool m_hasInitializedEncoding;
		}

		// Token: 0x02000CB4 RID: 3252
		internal class EncodingCharBuffer
		{
			// Token: 0x06007172 RID: 29042 RVA: 0x001860C4 File Offset: 0x001842C4
			[SecurityCritical]
			internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
			{
				this.enc = enc;
				this.decoder = decoder;
				this.chars = charStart;
				this.charStart = charStart;
				this.charEnd = charStart + charCount;
				this.byteStart = byteStart;
				this.bytes = byteStart;
				this.byteEnd = byteStart + byteCount;
				if (this.decoder == null)
				{
					this.fallbackBuffer = enc.DecoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.decoder.FallbackBuffer;
				}
				this.fallbackBuffer.InternalInitialize(this.bytes, this.charEnd);
			}

			// Token: 0x06007173 RID: 29043 RVA: 0x00186160 File Offset: 0x00184360
			[SecurityCritical]
			internal unsafe bool AddChar(char ch, int numBytes)
			{
				if (this.chars != null)
				{
					if (this.chars >= this.charEnd)
					{
						this.bytes -= numBytes;
						this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
						return false;
					}
					char* ptr = this.chars;
					this.chars = ptr + 1;
					*ptr = ch;
				}
				this.charCountResult++;
				return true;
			}

			// Token: 0x06007174 RID: 29044 RVA: 0x001861D9 File Offset: 0x001843D9
			[SecurityCritical]
			internal bool AddChar(char ch)
			{
				return this.AddChar(ch, 1);
			}

			// Token: 0x06007175 RID: 29045 RVA: 0x001861E4 File Offset: 0x001843E4
			[SecurityCritical]
			internal bool AddChar(char ch1, char ch2, int numBytes)
			{
				if (this.chars >= this.charEnd - 1)
				{
					this.bytes -= numBytes;
					this.enc.ThrowCharsOverflow(this.decoder, this.bytes == this.byteStart);
					return false;
				}
				return this.AddChar(ch1, numBytes) && this.AddChar(ch2, numBytes);
			}

			// Token: 0x06007176 RID: 29046 RVA: 0x00186247 File Offset: 0x00184447
			[SecurityCritical]
			internal void AdjustBytes(int count)
			{
				this.bytes += count;
			}

			// Token: 0x1700136F RID: 4975
			// (get) Token: 0x06007177 RID: 29047 RVA: 0x00186257 File Offset: 0x00184457
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.bytes < this.byteEnd;
				}
			}

			// Token: 0x06007178 RID: 29048 RVA: 0x00186267 File Offset: 0x00184467
			[SecurityCritical]
			internal bool EvenMoreData(int count)
			{
				return this.bytes == this.byteEnd - count;
			}

			// Token: 0x06007179 RID: 29049 RVA: 0x0018627C File Offset: 0x0018447C
			[SecurityCritical]
			internal unsafe byte GetNextByte()
			{
				if (this.bytes >= this.byteEnd)
				{
					return 0;
				}
				byte* ptr = this.bytes;
				this.bytes = ptr + 1;
				return *ptr;
			}

			// Token: 0x17001370 RID: 4976
			// (get) Token: 0x0600717A RID: 29050 RVA: 0x001862AB File Offset: 0x001844AB
			internal int BytesUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.bytes - this.byteStart));
				}
			}

			// Token: 0x0600717B RID: 29051 RVA: 0x001862C0 File Offset: 0x001844C0
			[SecurityCritical]
			internal bool Fallback(byte fallbackByte)
			{
				byte[] array = new byte[] { fallbackByte };
				return this.Fallback(array);
			}

			// Token: 0x0600717C RID: 29052 RVA: 0x001862E0 File Offset: 0x001844E0
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2)
			{
				byte[] array = new byte[] { byte1, byte2 };
				return this.Fallback(array);
			}

			// Token: 0x0600717D RID: 29053 RVA: 0x00186304 File Offset: 0x00184504
			[SecurityCritical]
			internal bool Fallback(byte byte1, byte byte2, byte byte3, byte byte4)
			{
				byte[] array = new byte[] { byte1, byte2, byte3, byte4 };
				return this.Fallback(array);
			}

			// Token: 0x0600717E RID: 29054 RVA: 0x00186330 File Offset: 0x00184530
			[SecurityCritical]
			internal unsafe bool Fallback(byte[] byteBuffer)
			{
				if (this.chars != null)
				{
					char* ptr = this.chars;
					if (!this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes, ref this.chars))
					{
						this.bytes -= byteBuffer.Length;
						this.fallbackBuffer.InternalReset();
						this.enc.ThrowCharsOverflow(this.decoder, this.chars == this.charStart);
						return false;
					}
					this.charCountResult += (int)((long)(this.chars - ptr));
				}
				else
				{
					this.charCountResult += this.fallbackBuffer.InternalFallback(byteBuffer, this.bytes);
				}
				return true;
			}

			// Token: 0x17001371 RID: 4977
			// (get) Token: 0x0600717F RID: 29055 RVA: 0x001863DF File Offset: 0x001845DF
			internal int Count
			{
				get
				{
					return this.charCountResult;
				}
			}

			// Token: 0x040038B1 RID: 14513
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x040038B2 RID: 14514
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x040038B3 RID: 14515
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x040038B4 RID: 14516
			private int charCountResult;

			// Token: 0x040038B5 RID: 14517
			private Encoding enc;

			// Token: 0x040038B6 RID: 14518
			private DecoderNLS decoder;

			// Token: 0x040038B7 RID: 14519
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x040038B8 RID: 14520
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x040038B9 RID: 14521
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x040038BA RID: 14522
			private DecoderFallbackBuffer fallbackBuffer;
		}

		// Token: 0x02000CB5 RID: 3253
		internal class EncodingByteBuffer
		{
			// Token: 0x06007180 RID: 29056 RVA: 0x001863E8 File Offset: 0x001845E8
			[SecurityCritical]
			internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
			{
				this.enc = inEncoding;
				this.encoder = inEncoder;
				this.charStart = inCharStart;
				this.chars = inCharStart;
				this.charEnd = inCharStart + inCharCount;
				this.bytes = inByteStart;
				this.byteStart = inByteStart;
				this.byteEnd = inByteStart + inByteCount;
				if (this.encoder == null)
				{
					this.fallbackBuffer = this.enc.EncoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this.encoder.FallbackBuffer;
					if (this.encoder.m_throwOnOverflow && this.encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_EncoderFallbackNotEmpty", new object[]
						{
							this.encoder.Encoding.EncodingName,
							this.encoder.Fallback.GetType()
						}));
					}
				}
				this.fallbackBuffer.InternalInitialize(this.chars, this.charEnd, this.encoder, this.bytes != null);
			}

			// Token: 0x06007181 RID: 29057 RVA: 0x00186500 File Offset: 0x00184700
			[SecurityCritical]
			internal unsafe bool AddByte(byte b, int moreBytesExpected)
			{
				if (this.bytes != null)
				{
					if (this.bytes >= this.byteEnd - moreBytesExpected)
					{
						this.MovePrevious(true);
						return false;
					}
					byte* ptr = this.bytes;
					this.bytes = ptr + 1;
					*ptr = b;
				}
				this.byteCountResult++;
				return true;
			}

			// Token: 0x06007182 RID: 29058 RVA: 0x00186552 File Offset: 0x00184752
			[SecurityCritical]
			internal bool AddByte(byte b1)
			{
				return this.AddByte(b1, 0);
			}

			// Token: 0x06007183 RID: 29059 RVA: 0x0018655C File Offset: 0x0018475C
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2)
			{
				return this.AddByte(b1, b2, 0);
			}

			// Token: 0x06007184 RID: 29060 RVA: 0x00186567 File Offset: 0x00184767
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
			{
				return this.AddByte(b1, 1 + moreBytesExpected) && this.AddByte(b2, moreBytesExpected);
			}

			// Token: 0x06007185 RID: 29061 RVA: 0x0018657F File Offset: 0x0018477F
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3)
			{
				return this.AddByte(b1, b2, b3, 0);
			}

			// Token: 0x06007186 RID: 29062 RVA: 0x0018658B File Offset: 0x0018478B
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, int moreBytesExpected)
			{
				return this.AddByte(b1, 2 + moreBytesExpected) && this.AddByte(b2, 1 + moreBytesExpected) && this.AddByte(b3, moreBytesExpected);
			}

			// Token: 0x06007187 RID: 29063 RVA: 0x001865B2 File Offset: 0x001847B2
			[SecurityCritical]
			internal bool AddByte(byte b1, byte b2, byte b3, byte b4)
			{
				return this.AddByte(b1, 3) && this.AddByte(b2, 2) && this.AddByte(b3, 1) && this.AddByte(b4, 0);
			}

			// Token: 0x06007188 RID: 29064 RVA: 0x001865E0 File Offset: 0x001847E0
			[SecurityCritical]
			internal void MovePrevious(bool bThrow)
			{
				if (this.fallbackBuffer.bFallingBack)
				{
					this.fallbackBuffer.MovePrevious();
				}
				else if (this.chars != this.charStart)
				{
					this.chars--;
				}
				if (bThrow)
				{
					this.enc.ThrowBytesOverflow(this.encoder, this.bytes == this.byteStart);
				}
			}

			// Token: 0x06007189 RID: 29065 RVA: 0x00186646 File Offset: 0x00184846
			[SecurityCritical]
			internal bool Fallback(char charFallback)
			{
				return this.fallbackBuffer.InternalFallback(charFallback, ref this.chars);
			}

			// Token: 0x17001372 RID: 4978
			// (get) Token: 0x0600718A RID: 29066 RVA: 0x0018665A File Offset: 0x0018485A
			internal bool MoreData
			{
				[SecurityCritical]
				get
				{
					return this.fallbackBuffer.Remaining > 0 || this.chars < this.charEnd;
				}
			}

			// Token: 0x0600718B RID: 29067 RVA: 0x0018667C File Offset: 0x0018487C
			[SecurityCritical]
			internal unsafe char GetNextChar()
			{
				char c = this.fallbackBuffer.InternalGetNextChar();
				if (c == '\0' && this.chars < this.charEnd)
				{
					char* ptr = this.chars;
					this.chars = ptr + 1;
					c = *ptr;
				}
				return c;
			}

			// Token: 0x17001373 RID: 4979
			// (get) Token: 0x0600718C RID: 29068 RVA: 0x001866BA File Offset: 0x001848BA
			internal int CharsUsed
			{
				[SecurityCritical]
				get
				{
					return (int)((long)(this.chars - this.charStart));
				}
			}

			// Token: 0x17001374 RID: 4980
			// (get) Token: 0x0600718D RID: 29069 RVA: 0x001866CD File Offset: 0x001848CD
			internal int Count
			{
				get
				{
					return this.byteCountResult;
				}
			}

			// Token: 0x040038BB RID: 14523
			[SecurityCritical]
			private unsafe byte* bytes;

			// Token: 0x040038BC RID: 14524
			[SecurityCritical]
			private unsafe byte* byteStart;

			// Token: 0x040038BD RID: 14525
			[SecurityCritical]
			private unsafe byte* byteEnd;

			// Token: 0x040038BE RID: 14526
			[SecurityCritical]
			private unsafe char* chars;

			// Token: 0x040038BF RID: 14527
			[SecurityCritical]
			private unsafe char* charStart;

			// Token: 0x040038C0 RID: 14528
			[SecurityCritical]
			private unsafe char* charEnd;

			// Token: 0x040038C1 RID: 14529
			private int byteCountResult;

			// Token: 0x040038C2 RID: 14530
			private Encoding enc;

			// Token: 0x040038C3 RID: 14531
			private EncoderNLS encoder;

			// Token: 0x040038C4 RID: 14532
			internal EncoderFallbackBuffer fallbackBuffer;
		}
	}
}
