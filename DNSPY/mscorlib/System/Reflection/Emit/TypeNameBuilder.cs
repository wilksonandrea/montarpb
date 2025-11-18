using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x02000631 RID: 1585
	internal class TypeNameBuilder
	{
		// Token: 0x06004994 RID: 18836
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr CreateTypeNameBuilder();

		// Token: 0x06004995 RID: 18837
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ReleaseTypeNameBuilder(IntPtr pAQN);

		// Token: 0x06004996 RID: 18838
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void OpenGenericArguments(IntPtr tnb);

		// Token: 0x06004997 RID: 18839
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CloseGenericArguments(IntPtr tnb);

		// Token: 0x06004998 RID: 18840
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void OpenGenericArgument(IntPtr tnb);

		// Token: 0x06004999 RID: 18841
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CloseGenericArgument(IntPtr tnb);

		// Token: 0x0600499A RID: 18842
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddName(IntPtr tnb, string name);

		// Token: 0x0600499B RID: 18843
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddPointer(IntPtr tnb);

		// Token: 0x0600499C RID: 18844
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddByRef(IntPtr tnb);

		// Token: 0x0600499D RID: 18845
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddSzArray(IntPtr tnb);

		// Token: 0x0600499E RID: 18846
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddArray(IntPtr tnb, int rank);

		// Token: 0x0600499F RID: 18847
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void AddAssemblySpec(IntPtr tnb, string assemblySpec);

		// Token: 0x060049A0 RID: 18848
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void ToString(IntPtr tnb, StringHandleOnStack retString);

		// Token: 0x060049A1 RID: 18849
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void Clear(IntPtr tnb);

		// Token: 0x060049A2 RID: 18850 RVA: 0x0010A734 File Offset: 0x00108934
		[SecuritySafeCritical]
		internal static string ToString(Type type, TypeNameBuilder.Format format)
		{
			if ((format == TypeNameBuilder.Format.FullName || format == TypeNameBuilder.Format.AssemblyQualifiedName) && !type.IsGenericTypeDefinition && type.ContainsGenericParameters)
			{
				return null;
			}
			TypeNameBuilder typeNameBuilder = new TypeNameBuilder(TypeNameBuilder.CreateTypeNameBuilder());
			typeNameBuilder.Clear();
			typeNameBuilder.ConstructAssemblyQualifiedNameWorker(type, format);
			string text = typeNameBuilder.ToString();
			typeNameBuilder.Dispose();
			return text;
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0010A782 File Offset: 0x00108982
		private TypeNameBuilder(IntPtr typeNameBuilder)
		{
			this.m_typeNameBuilder = typeNameBuilder;
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x0010A791 File Offset: 0x00108991
		[SecurityCritical]
		internal void Dispose()
		{
			TypeNameBuilder.ReleaseTypeNameBuilder(this.m_typeNameBuilder);
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x0010A7A0 File Offset: 0x001089A0
		[SecurityCritical]
		private void AddElementType(Type elementType)
		{
			if (elementType.HasElementType)
			{
				this.AddElementType(elementType.GetElementType());
			}
			if (elementType.IsPointer)
			{
				this.AddPointer();
				return;
			}
			if (elementType.IsByRef)
			{
				this.AddByRef();
				return;
			}
			if (elementType.IsSzArray)
			{
				this.AddSzArray();
				return;
			}
			if (elementType.IsArray)
			{
				this.AddArray(elementType.GetArrayRank());
			}
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x0010A804 File Offset: 0x00108A04
		[SecurityCritical]
		private void ConstructAssemblyQualifiedNameWorker(Type type, TypeNameBuilder.Format format)
		{
			Type type2 = type;
			while (type2.HasElementType)
			{
				type2 = type2.GetElementType();
			}
			List<Type> list = new List<Type>();
			Type type3 = type2;
			while (type3 != null)
			{
				list.Add(type3);
				type3 = (type3.IsGenericParameter ? null : type3.DeclaringType);
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				Type type4 = list[i];
				string text = type4.Name;
				if (i == list.Count - 1 && type4.Namespace != null && type4.Namespace.Length != 0)
				{
					text = type4.Namespace + "." + text;
				}
				this.AddName(text);
			}
			if (type2.IsGenericType && (!type2.IsGenericTypeDefinition || format == TypeNameBuilder.Format.ToString))
			{
				Type[] genericArguments = type2.GetGenericArguments();
				this.OpenGenericArguments();
				for (int j = 0; j < genericArguments.Length; j++)
				{
					TypeNameBuilder.Format format2 = ((format == TypeNameBuilder.Format.FullName) ? TypeNameBuilder.Format.AssemblyQualifiedName : format);
					this.OpenGenericArgument();
					this.ConstructAssemblyQualifiedNameWorker(genericArguments[j], format2);
					this.CloseGenericArgument();
				}
				this.CloseGenericArguments();
			}
			this.AddElementType(type);
			if (format == TypeNameBuilder.Format.AssemblyQualifiedName)
			{
				this.AddAssemblySpec(type.Module.Assembly.FullName);
			}
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x0010A932 File Offset: 0x00108B32
		[SecurityCritical]
		private void OpenGenericArguments()
		{
			TypeNameBuilder.OpenGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0010A93F File Offset: 0x00108B3F
		[SecurityCritical]
		private void CloseGenericArguments()
		{
			TypeNameBuilder.CloseGenericArguments(this.m_typeNameBuilder);
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x0010A94C File Offset: 0x00108B4C
		[SecurityCritical]
		private void OpenGenericArgument()
		{
			TypeNameBuilder.OpenGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x0010A959 File Offset: 0x00108B59
		[SecurityCritical]
		private void CloseGenericArgument()
		{
			TypeNameBuilder.CloseGenericArgument(this.m_typeNameBuilder);
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0010A966 File Offset: 0x00108B66
		[SecurityCritical]
		private void AddName(string name)
		{
			TypeNameBuilder.AddName(this.m_typeNameBuilder, name);
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0010A974 File Offset: 0x00108B74
		[SecurityCritical]
		private void AddPointer()
		{
			TypeNameBuilder.AddPointer(this.m_typeNameBuilder);
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0010A981 File Offset: 0x00108B81
		[SecurityCritical]
		private void AddByRef()
		{
			TypeNameBuilder.AddByRef(this.m_typeNameBuilder);
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0010A98E File Offset: 0x00108B8E
		[SecurityCritical]
		private void AddSzArray()
		{
			TypeNameBuilder.AddSzArray(this.m_typeNameBuilder);
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x0010A99B File Offset: 0x00108B9B
		[SecurityCritical]
		private void AddArray(int rank)
		{
			TypeNameBuilder.AddArray(this.m_typeNameBuilder, rank);
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x0010A9A9 File Offset: 0x00108BA9
		[SecurityCritical]
		private void AddAssemblySpec(string assemblySpec)
		{
			TypeNameBuilder.AddAssemblySpec(this.m_typeNameBuilder, assemblySpec);
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x0010A9B8 File Offset: 0x00108BB8
		[SecuritySafeCritical]
		public override string ToString()
		{
			string text = null;
			TypeNameBuilder.ToString(this.m_typeNameBuilder, JitHelpers.GetStringHandleOnStack(ref text));
			return text;
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0010A9DA File Offset: 0x00108BDA
		[SecurityCritical]
		private void Clear()
		{
			TypeNameBuilder.Clear(this.m_typeNameBuilder);
		}

		// Token: 0x04001E8A RID: 7818
		private IntPtr m_typeNameBuilder;

		// Token: 0x02000C40 RID: 3136
		internal enum Format
		{
			// Token: 0x04003752 RID: 14162
			ToString,
			// Token: 0x04003753 RID: 14163
			FullName,
			// Token: 0x04003754 RID: 14164
			AssemblyQualifiedName
		}
	}
}
