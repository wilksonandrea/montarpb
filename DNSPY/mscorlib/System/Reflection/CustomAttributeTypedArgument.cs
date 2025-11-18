using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection
{
	// Token: 0x020005D8 RID: 1496
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public struct CustomAttributeTypedArgument
	{
		// Token: 0x0600455B RID: 17755 RVA: 0x000FEC04 File Offset: 0x000FCE04
		public static bool operator ==(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600455C RID: 17756 RVA: 0x000FEC19 File Offset: 0x000FCE19
		public static bool operator !=(CustomAttributeTypedArgument left, CustomAttributeTypedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600455D RID: 17757 RVA: 0x000FEC34 File Offset: 0x000FCE34
		private static Type CustomAttributeEncodingToType(CustomAttributeEncoding encodedType)
		{
			if (encodedType <= CustomAttributeEncoding.Array)
			{
				switch (encodedType)
				{
				case CustomAttributeEncoding.Boolean:
					return typeof(bool);
				case CustomAttributeEncoding.Char:
					return typeof(char);
				case CustomAttributeEncoding.SByte:
					return typeof(sbyte);
				case CustomAttributeEncoding.Byte:
					return typeof(byte);
				case CustomAttributeEncoding.Int16:
					return typeof(short);
				case CustomAttributeEncoding.UInt16:
					return typeof(ushort);
				case CustomAttributeEncoding.Int32:
					return typeof(int);
				case CustomAttributeEncoding.UInt32:
					return typeof(uint);
				case CustomAttributeEncoding.Int64:
					return typeof(long);
				case CustomAttributeEncoding.UInt64:
					return typeof(ulong);
				case CustomAttributeEncoding.Float:
					return typeof(float);
				case CustomAttributeEncoding.Double:
					return typeof(double);
				case CustomAttributeEncoding.String:
					return typeof(string);
				default:
					if (encodedType == CustomAttributeEncoding.Array)
					{
						return typeof(Array);
					}
					break;
				}
			}
			else
			{
				if (encodedType == CustomAttributeEncoding.Type)
				{
					return typeof(Type);
				}
				if (encodedType == CustomAttributeEncoding.Object)
				{
					return typeof(object);
				}
				if (encodedType == CustomAttributeEncoding.Enum)
				{
					return typeof(Enum);
				}
			}
			throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)encodedType }), "encodedType");
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000FED80 File Offset: 0x000FCF80
		[SecuritySafeCritical]
		private unsafe static object EncodedValueToRawValue(long val, CustomAttributeEncoding encodedType)
		{
			switch (encodedType)
			{
			case CustomAttributeEncoding.Boolean:
				return (byte)val > 0;
			case CustomAttributeEncoding.Char:
				return (char)val;
			case CustomAttributeEncoding.SByte:
				return (sbyte)val;
			case CustomAttributeEncoding.Byte:
				return (byte)val;
			case CustomAttributeEncoding.Int16:
				return (short)val;
			case CustomAttributeEncoding.UInt16:
				return (ushort)val;
			case CustomAttributeEncoding.Int32:
				return (int)val;
			case CustomAttributeEncoding.UInt32:
				return (uint)val;
			case CustomAttributeEncoding.Int64:
				return val;
			case CustomAttributeEncoding.UInt64:
				return (ulong)val;
			case CustomAttributeEncoding.Float:
				return *(float*)(&val);
			case CustomAttributeEncoding.Double:
				return *(double*)(&val);
			default:
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)val }), "val");
			}
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000FEE50 File Offset: 0x000FD050
		private static RuntimeType ResolveType(RuntimeModule scope, string typeName)
		{
			RuntimeType typeByNameUsingCARules = RuntimeTypeHandle.GetTypeByNameUsingCARules(typeName, scope);
			if (typeByNameUsingCARules == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Arg_CATypeResolutionFailed"), typeName));
			}
			return typeByNameUsingCARules;
		}

		// Token: 0x06004560 RID: 17760 RVA: 0x000FEE8A File Offset: 0x000FD08A
		public CustomAttributeTypedArgument(Type argumentType, object value)
		{
			if (argumentType == null)
			{
				throw new ArgumentNullException("argumentType");
			}
			this.m_value = ((value == null) ? null : CustomAttributeTypedArgument.CanonicalizeValue(value));
			this.m_argumentType = argumentType;
		}

		// Token: 0x06004561 RID: 17761 RVA: 0x000FEEB9 File Offset: 0x000FD0B9
		public CustomAttributeTypedArgument(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.m_value = CustomAttributeTypedArgument.CanonicalizeValue(value);
			this.m_argumentType = value.GetType();
		}

		// Token: 0x06004562 RID: 17762 RVA: 0x000FEEE1 File Offset: 0x000FD0E1
		private static object CanonicalizeValue(object value)
		{
			if (value.GetType().IsEnum)
			{
				return ((Enum)value).GetValue();
			}
			return value;
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x000FEF00 File Offset: 0x000FD100
		internal CustomAttributeTypedArgument(RuntimeModule scope, CustomAttributeEncodedArgument encodedArg)
		{
			CustomAttributeEncoding customAttributeEncoding = encodedArg.CustomAttributeType.EncodedType;
			if (customAttributeEncoding == CustomAttributeEncoding.Undefined)
			{
				throw new ArgumentException("encodedArg");
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum)
			{
				this.m_argumentType = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, encodedArg.CustomAttributeType.EncodedEnumType);
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.String)
			{
				this.m_argumentType = typeof(string);
				this.m_value = encodedArg.StringValue;
				return;
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Type)
			{
				this.m_argumentType = typeof(Type);
				this.m_value = null;
				if (encodedArg.StringValue != null)
				{
					this.m_value = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.StringValue);
					return;
				}
			}
			else if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				customAttributeEncoding = encodedArg.CustomAttributeType.EncodedArrayType;
				Type type;
				if (customAttributeEncoding == CustomAttributeEncoding.Enum)
				{
					type = CustomAttributeTypedArgument.ResolveType(scope, encodedArg.CustomAttributeType.EnumName);
				}
				else
				{
					type = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				}
				this.m_argumentType = type.MakeArrayType();
				if (encodedArg.ArrayValue == null)
				{
					this.m_value = null;
					return;
				}
				CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[encodedArg.ArrayValue.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new CustomAttributeTypedArgument(scope, encodedArg.ArrayValue[i]);
				}
				this.m_value = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				return;
			}
			else
			{
				this.m_argumentType = CustomAttributeTypedArgument.CustomAttributeEncodingToType(customAttributeEncoding);
				this.m_value = CustomAttributeTypedArgument.EncodedValueToRawValue(encodedArg.PrimitiveValue, customAttributeEncoding);
			}
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x000FF08E File Offset: 0x000FD28E
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x000FF098 File Offset: 0x000FD298
		internal string ToString(bool typed)
		{
			if (this.m_argumentType == null)
			{
				return base.ToString();
			}
			if (this.ArgumentType.IsEnum)
			{
				return string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.FullName);
			}
			if (this.Value == null)
			{
				return string.Format(CultureInfo.CurrentCulture, typed ? "null" : "({0})null", this.ArgumentType.Name);
			}
			if (this.ArgumentType == typeof(string))
			{
				return string.Format(CultureInfo.CurrentCulture, "\"{0}\"", this.Value);
			}
			if (this.ArgumentType == typeof(char))
			{
				return string.Format(CultureInfo.CurrentCulture, "'{0}'", this.Value);
			}
			if (this.ArgumentType == typeof(Type))
			{
				return string.Format(CultureInfo.CurrentCulture, "typeof({0})", ((Type)this.Value).FullName);
			}
			if (this.ArgumentType.IsArray)
			{
				IList<CustomAttributeTypedArgument> list = this.Value as IList<CustomAttributeTypedArgument>;
				Type elementType = this.ArgumentType.GetElementType();
				string text = string.Format(CultureInfo.CurrentCulture, "new {0}[{1}] {{ ", elementType.IsEnum ? elementType.FullName : elementType.Name, list.Count);
				for (int i = 0; i < list.Count; i++)
				{
					text += string.Format(CultureInfo.CurrentCulture, (i == 0) ? "{0}" : ", {0}", list[i].ToString(elementType != typeof(object)));
				}
				return text + " }";
			}
			return string.Format(CultureInfo.CurrentCulture, typed ? "{0}" : "({1}){0}", this.Value, this.ArgumentType.Name);
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000FF29E File Offset: 0x000FD49E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004567 RID: 17767 RVA: 0x000FF2B0 File Offset: 0x000FD4B0
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06004568 RID: 17768 RVA: 0x000FF2C0 File Offset: 0x000FD4C0
		[__DynamicallyInvokable]
		public Type ArgumentType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_argumentType;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x000FF2C8 File Offset: 0x000FD4C8
		[__DynamicallyInvokable]
		public object Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04001C6A RID: 7274
		private object m_value;

		// Token: 0x04001C6B RID: 7275
		private Type m_argumentType;
	}
}
