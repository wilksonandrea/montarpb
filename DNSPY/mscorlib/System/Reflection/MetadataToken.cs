using System;
using System.Globalization;

namespace System.Reflection
{
	// Token: 0x020005FE RID: 1534
	[Serializable]
	internal struct MetadataToken
	{
		// Token: 0x06004683 RID: 18051 RVA: 0x001027DA File Offset: 0x001009DA
		public static implicit operator int(MetadataToken token)
		{
			return token.Value;
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x001027E2 File Offset: 0x001009E2
		public static implicit operator MetadataToken(int token)
		{
			return new MetadataToken(token);
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x001027EC File Offset: 0x001009EC
		public static bool IsTokenOfType(int token, params MetadataTokenType[] types)
		{
			for (int i = 0; i < types.Length; i++)
			{
				if ((MetadataTokenType)((long)token & (long)((ulong)(-16777216))) == types[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x00102819 File Offset: 0x00100A19
		public static bool IsNullToken(int token)
		{
			return (token & 16777215) == 0;
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x00102825 File Offset: 0x00100A25
		public MetadataToken(int token)
		{
			this.Value = token;
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06004688 RID: 18056 RVA: 0x0010282E File Offset: 0x00100A2E
		public bool IsGlobalTypeDefToken
		{
			get
			{
				return this.Value == 33554433;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x0010283D File Offset: 0x00100A3D
		public MetadataTokenType TokenType
		{
			get
			{
				return (MetadataTokenType)((long)this.Value & (long)((ulong)(-16777216)));
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x0010284E File Offset: 0x00100A4E
		public bool IsTypeRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeRef;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x0010285D File Offset: 0x00100A5D
		public bool IsTypeDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeDef;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600468C RID: 18060 RVA: 0x0010286C File Offset: 0x00100A6C
		public bool IsFieldDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.FieldDef;
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x0010287B File Offset: 0x00100A7B
		public bool IsMethodDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodDef;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x0600468E RID: 18062 RVA: 0x0010288A File Offset: 0x00100A8A
		public bool IsMemberRef
		{
			get
			{
				return this.TokenType == MetadataTokenType.MemberRef;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x00102899 File Offset: 0x00100A99
		public bool IsEvent
		{
			get
			{
				return this.TokenType == MetadataTokenType.Event;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06004690 RID: 18064 RVA: 0x001028A8 File Offset: 0x00100AA8
		public bool IsProperty
		{
			get
			{
				return this.TokenType == MetadataTokenType.Property;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06004691 RID: 18065 RVA: 0x001028B7 File Offset: 0x00100AB7
		public bool IsParamDef
		{
			get
			{
				return this.TokenType == MetadataTokenType.ParamDef;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06004692 RID: 18066 RVA: 0x001028C6 File Offset: 0x00100AC6
		public bool IsTypeSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.TypeSpec;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x001028D5 File Offset: 0x00100AD5
		public bool IsMethodSpec
		{
			get
			{
				return this.TokenType == MetadataTokenType.MethodSpec;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06004694 RID: 18068 RVA: 0x001028E4 File Offset: 0x00100AE4
		public bool IsString
		{
			get
			{
				return this.TokenType == MetadataTokenType.String;
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06004695 RID: 18069 RVA: 0x001028F3 File Offset: 0x00100AF3
		public bool IsSignature
		{
			get
			{
				return this.TokenType == MetadataTokenType.Signature;
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06004696 RID: 18070 RVA: 0x00102902 File Offset: 0x00100B02
		public bool IsModule
		{
			get
			{
				return this.TokenType == MetadataTokenType.Module;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06004697 RID: 18071 RVA: 0x0010290D File Offset: 0x00100B0D
		public bool IsAssembly
		{
			get
			{
				return this.TokenType == MetadataTokenType.Assembly;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x0010291C File Offset: 0x00100B1C
		public bool IsGenericPar
		{
			get
			{
				return this.TokenType == MetadataTokenType.GenericPar;
			}
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x0010292B File Offset: 0x00100B2B
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "0x{0:x8}", this.Value);
		}

		// Token: 0x04001D57 RID: 7511
		public int Value;
	}
}
