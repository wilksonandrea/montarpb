using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System
{
	// Token: 0x0200014B RID: 331
	internal sealed class TypeNameParser : IDisposable
	{
		// Token: 0x060014B5 RID: 5301
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _CreateTypeNameParser(string typeName, ObjectHandleOnStack retHandle, bool throwOnError);

		// Token: 0x060014B6 RID: 5302
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetNames(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

		// Token: 0x060014B7 RID: 5303
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetTypeArguments(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

		// Token: 0x060014B8 RID: 5304
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetModifiers(SafeTypeNameParserHandle pTypeNameParser, ObjectHandleOnStack retArray);

		// Token: 0x060014B9 RID: 5305
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _GetAssemblyName(SafeTypeNameParserHandle pTypeNameParser, StringHandleOnStack retString);

		// Token: 0x060014BA RID: 5306 RVA: 0x0003D61C File Offset: 0x0003B81C
		[SecuritySafeCritical]
		internal static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (typeName.Length > 0 && typeName[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
			}
			Type type = null;
			SafeTypeNameParserHandle safeTypeNameParserHandle = TypeNameParser.CreateTypeNameParser(typeName, throwOnError);
			if (safeTypeNameParserHandle != null)
			{
				using (TypeNameParser typeNameParser = new TypeNameParser(safeTypeNameParserHandle))
				{
					type = typeNameParser.ConstructType(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
				}
			}
			return type;
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0003D698 File Offset: 0x0003B898
		[SecuritySafeCritical]
		private TypeNameParser(SafeTypeNameParserHandle handle)
		{
			this.m_NativeParser = handle;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0003D6A7 File Offset: 0x0003B8A7
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.m_NativeParser.Dispose();
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0003D6B4 File Offset: 0x0003B8B4
		[SecuritySafeCritical]
		private unsafe Type ConstructType(Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			Assembly assembly = null;
			string assemblyName = this.GetAssemblyName();
			if (assemblyName.Length > 0)
			{
				assembly = TypeNameParser.ResolveAssembly(assemblyName, assemblyResolver, throwOnError, ref stackMark);
				if (assembly == null)
				{
					return null;
				}
			}
			string[] names = this.GetNames();
			if (names == null)
			{
				if (throwOnError)
				{
					throw new TypeLoadException(Environment.GetResourceString("Arg_TypeLoadNullStr"));
				}
				return null;
			}
			else
			{
				Type type = TypeNameParser.ResolveType(assembly, names, typeResolver, throwOnError, ignoreCase, ref stackMark);
				if (type == null)
				{
					return null;
				}
				SafeTypeNameParserHandle[] typeArguments = this.GetTypeArguments();
				Type[] array = null;
				if (typeArguments != null)
				{
					array = new Type[typeArguments.Length];
					for (int i = 0; i < typeArguments.Length; i++)
					{
						using (TypeNameParser typeNameParser = new TypeNameParser(typeArguments[i]))
						{
							array[i] = typeNameParser.ConstructType(assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackMark);
						}
						if (array[i] == null)
						{
							return null;
						}
					}
				}
				int[] modifiers = this.GetModifiers();
				int[] array2;
				int* ptr;
				if ((array2 = modifiers) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				IntPtr intPtr = new IntPtr((void*)ptr);
				return RuntimeTypeHandle.GetTypeHelper(type, array, intPtr, (modifiers == null) ? 0 : modifiers.Length);
			}
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
		[SecuritySafeCritical]
		private static Assembly ResolveAssembly(string asmName, Func<AssemblyName, Assembly> assemblyResolver, bool throwOnError, ref StackCrawlMark stackMark)
		{
			Assembly assembly = null;
			if (assemblyResolver == null)
			{
				if (throwOnError)
				{
					return RuntimeAssembly.InternalLoad(asmName, null, ref stackMark, false);
				}
				try
				{
					return RuntimeAssembly.InternalLoad(asmName, null, ref stackMark, false);
				}
				catch (FileNotFoundException)
				{
					return null;
				}
			}
			assembly = assemblyResolver(new AssemblyName(asmName));
			if (assembly == null && throwOnError)
			{
				throw new FileNotFoundException(Environment.GetResourceString("FileNotFound_ResolveAssembly", new object[] { asmName }));
			}
			return assembly;
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0003D858 File Offset: 0x0003BA58
		private static Type ResolveType(Assembly assembly, string[] names, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase, ref StackCrawlMark stackMark)
		{
			string text = TypeNameParser.EscapeTypeName(names[0]);
			Type type;
			if (typeResolver != null)
			{
				type = typeResolver(assembly, text, ignoreCase);
				if (type == null && throwOnError)
				{
					string text2 = ((assembly == null) ? Environment.GetResourceString("TypeLoad_ResolveType", new object[] { text }) : Environment.GetResourceString("TypeLoad_ResolveTypeFromAssembly", new object[] { text, assembly.FullName }));
					throw new TypeLoadException(text2);
				}
			}
			else if (assembly == null)
			{
				type = RuntimeType.GetType(text, throwOnError, ignoreCase, false, ref stackMark);
			}
			else
			{
				type = assembly.GetType(text, throwOnError, ignoreCase);
			}
			if (type != null)
			{
				BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic;
				if (ignoreCase)
				{
					bindingFlags |= BindingFlags.IgnoreCase;
				}
				int i = 1;
				while (i < names.Length)
				{
					type = type.GetNestedType(names[i], bindingFlags);
					if (type == null)
					{
						if (throwOnError)
						{
							throw new TypeLoadException(Environment.GetResourceString("TypeLoad_ResolveNestedType", new object[]
							{
								names[i],
								names[i - 1]
							}));
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return type;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x0003D958 File Offset: 0x0003BB58
		private static string EscapeTypeName(string name)
		{
			if (name.IndexOfAny(TypeNameParser.SPECIAL_CHARS) < 0)
			{
				return name;
			}
			StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
			foreach (char c in name)
			{
				if (Array.IndexOf<char>(TypeNameParser.SPECIAL_CHARS, c) >= 0)
				{
					stringBuilder.Append('\\');
				}
				stringBuilder.Append(c);
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x0003D9C0 File Offset: 0x0003BBC0
		[SecuritySafeCritical]
		private static SafeTypeNameParserHandle CreateTypeNameParser(string typeName, bool throwOnError)
		{
			SafeTypeNameParserHandle safeTypeNameParserHandle = null;
			TypeNameParser._CreateTypeNameParser(typeName, JitHelpers.GetObjectHandleOnStack<SafeTypeNameParserHandle>(ref safeTypeNameParserHandle), throwOnError);
			return safeTypeNameParserHandle;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0003D9E0 File Offset: 0x0003BBE0
		[SecuritySafeCritical]
		private string[] GetNames()
		{
			string[] array = null;
			TypeNameParser._GetNames(this.m_NativeParser, JitHelpers.GetObjectHandleOnStack<string[]>(ref array));
			return array;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0003DA04 File Offset: 0x0003BC04
		[SecuritySafeCritical]
		private SafeTypeNameParserHandle[] GetTypeArguments()
		{
			SafeTypeNameParserHandle[] array = null;
			TypeNameParser._GetTypeArguments(this.m_NativeParser, JitHelpers.GetObjectHandleOnStack<SafeTypeNameParserHandle[]>(ref array));
			return array;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x0003DA28 File Offset: 0x0003BC28
		[SecuritySafeCritical]
		private int[] GetModifiers()
		{
			int[] array = null;
			TypeNameParser._GetModifiers(this.m_NativeParser, JitHelpers.GetObjectHandleOnStack<int[]>(ref array));
			return array;
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		[SecuritySafeCritical]
		private string GetAssemblyName()
		{
			string text = null;
			TypeNameParser._GetAssemblyName(this.m_NativeParser, JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0003DA6E File Offset: 0x0003BC6E
		// Note: this type is marked as 'beforefieldinit'.
		static TypeNameParser()
		{
		}

		// Token: 0x040006D5 RID: 1749
		[SecurityCritical]
		private SafeTypeNameParserHandle m_NativeParser;

		// Token: 0x040006D6 RID: 1750
		private static readonly char[] SPECIAL_CHARS = new char[] { ',', '[', ']', '&', '*', '+', '\\' };
	}
}
