using System;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000A78 RID: 2680
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public abstract class EncodingProvider
	{
		// Token: 0x06006894 RID: 26772 RVA: 0x00161002 File Offset: 0x0015F202
		[__DynamicallyInvokable]
		public EncodingProvider()
		{
		}

		// Token: 0x06006895 RID: 26773
		[__DynamicallyInvokable]
		public abstract Encoding GetEncoding(string name);

		// Token: 0x06006896 RID: 26774
		[__DynamicallyInvokable]
		public abstract Encoding GetEncoding(int codepage);

		// Token: 0x06006897 RID: 26775 RVA: 0x0016100C File Offset: 0x0015F20C
		[__DynamicallyInvokable]
		public virtual Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(name);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(name).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x00161048 File Offset: 0x0015F248
		[__DynamicallyInvokable]
		public virtual Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = this.GetEncoding(codepage);
			if (encoding != null)
			{
				encoding = (Encoding)this.GetEncoding(codepage).Clone();
				encoding.EncoderFallback = encoderFallback;
				encoding.DecoderFallback = decoderFallback;
			}
			return encoding;
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x00161084 File Offset: 0x0015F284
		internal static void AddProvider(EncodingProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			object obj = EncodingProvider.s_InternalSyncObject;
			lock (obj)
			{
				if (EncodingProvider.s_providers == null)
				{
					EncodingProvider.s_providers = new EncodingProvider[] { provider };
				}
				else if (Array.IndexOf<EncodingProvider>(EncodingProvider.s_providers, provider) < 0)
				{
					EncodingProvider[] array = new EncodingProvider[EncodingProvider.s_providers.Length + 1];
					Array.Copy(EncodingProvider.s_providers, array, EncodingProvider.s_providers.Length);
					array[array.Length - 1] = provider;
					EncodingProvider.s_providers = array;
				}
			}
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x00161130 File Offset: 0x0015F330
		internal static Encoding GetEncodingFromProvider(int codepage)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x00161178 File Offset: 0x0015F378
		internal static Encoding GetEncodingFromProvider(string encodingName)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x001611C0 File Offset: 0x0015F3C0
		internal static Encoding GetEncodingFromProvider(int codepage, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(codepage, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x00161208 File Offset: 0x0015F408
		internal static Encoding GetEncodingFromProvider(string encodingName, EncoderFallback enc, DecoderFallback dec)
		{
			if (EncodingProvider.s_providers == null)
			{
				return null;
			}
			EncodingProvider[] array = EncodingProvider.s_providers;
			foreach (EncodingProvider encodingProvider in array)
			{
				Encoding encoding = encodingProvider.GetEncoding(encodingName, enc, dec);
				if (encoding != null)
				{
					return encoding;
				}
			}
			return null;
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x0016124F File Offset: 0x0015F44F
		// Note: this type is marked as 'beforefieldinit'.
		static EncodingProvider()
		{
		}

		// Token: 0x04002EC0 RID: 11968
		private static object s_InternalSyncObject = new object();

		// Token: 0x04002EC1 RID: 11969
		private static volatile EncodingProvider[] s_providers;
	}
}
