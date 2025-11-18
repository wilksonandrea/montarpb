using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000629 RID: 1577
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public abstract class TypeInfo : Type, IReflectableType
	{
		// Token: 0x060048F8 RID: 18680 RVA: 0x00107AE8 File Offset: 0x00105CE8
		[FriendAccessAllowed]
		internal TypeInfo()
		{
		}

		// Token: 0x060048F9 RID: 18681 RVA: 0x00107AF0 File Offset: 0x00105CF0
		[__DynamicallyInvokable]
		TypeInfo IReflectableType.GetTypeInfo()
		{
			return this;
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x00107AF3 File Offset: 0x00105CF3
		[__DynamicallyInvokable]
		public virtual Type AsType()
		{
			return this;
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060048FB RID: 18683 RVA: 0x00107AF6 File Offset: 0x00105CF6
		[__DynamicallyInvokable]
		public virtual Type[] GenericTypeParameters
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.IsGenericTypeDefinition)
				{
					return this.GetGenericArguments();
				}
				return Type.EmptyTypes;
			}
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x00107B0C File Offset: 0x00105D0C
		[__DynamicallyInvokable]
		public virtual bool IsAssignableFrom(TypeInfo typeInfo)
		{
			if (typeInfo == null)
			{
				return false;
			}
			if (this == typeInfo)
			{
				return true;
			}
			if (typeInfo.IsSubclassOf(this))
			{
				return true;
			}
			if (base.IsInterface)
			{
				return typeInfo.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(typeInfo))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x00107B77 File Offset: 0x00105D77
		[__DynamicallyInvokable]
		public virtual EventInfo GetDeclaredEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x00107B82 File Offset: 0x00105D82
		[__DynamicallyInvokable]
		public virtual FieldInfo GetDeclaredField(string name)
		{
			return this.GetField(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x00107B8D File Offset: 0x00105D8D
		[__DynamicallyInvokable]
		public virtual MethodInfo GetDeclaredMethod(string name)
		{
			return base.GetMethod(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x00107B98 File Offset: 0x00105D98
		[__DynamicallyInvokable]
		public virtual IEnumerable<MethodInfo> GetDeclaredMethods(string name)
		{
			foreach (MethodInfo methodInfo in this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
			{
				if (methodInfo.Name == name)
				{
					yield return methodInfo;
				}
			}
			MethodInfo[] array = null;
			yield break;
		}

		// Token: 0x06004901 RID: 18689 RVA: 0x00107BB0 File Offset: 0x00105DB0
		[__DynamicallyInvokable]
		public virtual TypeInfo GetDeclaredNestedType(string name)
		{
			Type nestedType = this.GetNestedType(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (nestedType == null)
			{
				return null;
			}
			return nestedType.GetTypeInfo();
		}

		// Token: 0x06004902 RID: 18690 RVA: 0x00107BD8 File Offset: 0x00105DD8
		[__DynamicallyInvokable]
		public virtual PropertyInfo GetDeclaredProperty(string name)
		{
			return base.GetProperty(name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x00107BE3 File Offset: 0x00105DE3
		[__DynamicallyInvokable]
		public virtual IEnumerable<ConstructorInfo> DeclaredConstructors
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06004904 RID: 18692 RVA: 0x00107BED File Offset: 0x00105DED
		[__DynamicallyInvokable]
		public virtual IEnumerable<EventInfo> DeclaredEvents
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetEvents(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06004905 RID: 18693 RVA: 0x00107BF7 File Offset: 0x00105DF7
		[__DynamicallyInvokable]
		public virtual IEnumerable<FieldInfo> DeclaredFields
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x06004906 RID: 18694 RVA: 0x00107C01 File Offset: 0x00105E01
		[__DynamicallyInvokable]
		public virtual IEnumerable<MemberInfo> DeclaredMembers
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMembers(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x00107C0B File Offset: 0x00105E0B
		[__DynamicallyInvokable]
		public virtual IEnumerable<MethodInfo> DeclaredMethods
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x06004908 RID: 18696 RVA: 0x00107C18 File Offset: 0x00105E18
		[__DynamicallyInvokable]
		public virtual IEnumerable<TypeInfo> DeclaredNestedTypes
		{
			[__DynamicallyInvokable]
			get
			{
				foreach (Type type in this.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					yield return type.GetTypeInfo();
				}
				Type[] array = null;
				yield break;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x06004909 RID: 18697 RVA: 0x00107C35 File Offset: 0x00105E35
		[__DynamicallyInvokable]
		public virtual IEnumerable<PropertyInfo> DeclaredProperties
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x0600490A RID: 18698 RVA: 0x00107C3F File Offset: 0x00105E3F
		[__DynamicallyInvokable]
		public virtual IEnumerable<Type> ImplementedInterfaces
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetInterfaces();
			}
		}

		// Token: 0x02000C3D RID: 3133
		[CompilerGenerated]
		private sealed class <GetDeclaredMethods>d__9 : IEnumerable<MethodInfo>, IEnumerable, IEnumerator<MethodInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06007043 RID: 28739 RVA: 0x00182BDA File Offset: 0x00180DDA
			[DebuggerHidden]
			public <GetDeclaredMethods>d__9(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06007044 RID: 28740 RVA: 0x00182BF4 File Offset: 0x00180DF4
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06007045 RID: 28741 RVA: 0x00182BF8 File Offset: 0x00180DF8
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypeInfo typeInfo = this;
				if (num == 0)
				{
					this.<>1__state = -1;
					array = typeInfo.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
					goto IL_7B;
				}
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				IL_6D:
				i++;
				IL_7B:
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				MethodInfo methodInfo = array[i];
				if (methodInfo.Name == name)
				{
					this.<>2__current = methodInfo;
					this.<>1__state = 1;
					return true;
				}
				goto IL_6D;
			}

			// Token: 0x1700133E RID: 4926
			// (get) Token: 0x06007046 RID: 28742 RVA: 0x00182C98 File Offset: 0x00180E98
			MethodInfo IEnumerator<MethodInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06007047 RID: 28743 RVA: 0x00182CA0 File Offset: 0x00180EA0
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700133F RID: 4927
			// (get) Token: 0x06007048 RID: 28744 RVA: 0x00182CA7 File Offset: 0x00180EA7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06007049 RID: 28745 RVA: 0x00182CB0 File Offset: 0x00180EB0
			[DebuggerHidden]
			IEnumerator<MethodInfo> IEnumerable<MethodInfo>.GetEnumerator()
			{
				TypeInfo.<GetDeclaredMethods>d__9 <GetDeclaredMethods>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<GetDeclaredMethods>d__ = this;
				}
				else
				{
					<GetDeclaredMethods>d__ = new TypeInfo.<GetDeclaredMethods>d__9(0);
					<GetDeclaredMethods>d__.<>4__this = this;
				}
				<GetDeclaredMethods>d__.name = name;
				return <GetDeclaredMethods>d__;
			}

			// Token: 0x0600704A RID: 28746 RVA: 0x00182CFF File Offset: 0x00180EFF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>.GetEnumerator();
			}

			// Token: 0x04003743 RID: 14147
			private int <>1__state;

			// Token: 0x04003744 RID: 14148
			private MethodInfo <>2__current;

			// Token: 0x04003745 RID: 14149
			private int <>l__initialThreadId;

			// Token: 0x04003746 RID: 14150
			public TypeInfo <>4__this;

			// Token: 0x04003747 RID: 14151
			private string name;

			// Token: 0x04003748 RID: 14152
			public string <>3__name;

			// Token: 0x04003749 RID: 14153
			private MethodInfo[] <>7__wrap1;

			// Token: 0x0400374A RID: 14154
			private int <>7__wrap2;
		}

		// Token: 0x02000C3E RID: 3134
		[CompilerGenerated]
		private sealed class <get_DeclaredNestedTypes>d__23 : IEnumerable<TypeInfo>, IEnumerable, IEnumerator<TypeInfo>, IDisposable, IEnumerator
		{
			// Token: 0x0600704B RID: 28747 RVA: 0x00182D07 File Offset: 0x00180F07
			[DebuggerHidden]
			public <get_DeclaredNestedTypes>d__23(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
			}

			// Token: 0x0600704C RID: 28748 RVA: 0x00182D21 File Offset: 0x00180F21
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x0600704D RID: 28749 RVA: 0x00182D24 File Offset: 0x00180F24
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				TypeInfo typeInfo = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -1;
					i++;
				}
				else
				{
					this.<>1__state = -1;
					array = typeInfo.GetNestedTypes(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					i = 0;
				}
				if (i >= array.Length)
				{
					array = null;
					return false;
				}
				Type type = array[i];
				this.<>2__current = type.GetTypeInfo();
				this.<>1__state = 1;
				return true;
			}

			// Token: 0x17001340 RID: 4928
			// (get) Token: 0x0600704E RID: 28750 RVA: 0x00182DB6 File Offset: 0x00180FB6
			TypeInfo IEnumerator<TypeInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0600704F RID: 28751 RVA: 0x00182DBE File Offset: 0x00180FBE
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17001341 RID: 4929
			// (get) Token: 0x06007050 RID: 28752 RVA: 0x00182DC5 File Offset: 0x00180FC5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06007051 RID: 28753 RVA: 0x00182DD0 File Offset: 0x00180FD0
			[DebuggerHidden]
			IEnumerator<TypeInfo> IEnumerable<TypeInfo>.GetEnumerator()
			{
				TypeInfo.<get_DeclaredNestedTypes>d__23 <get_DeclaredNestedTypes>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Environment.CurrentManagedThreadId)
				{
					this.<>1__state = 0;
					<get_DeclaredNestedTypes>d__ = this;
				}
				else
				{
					<get_DeclaredNestedTypes>d__ = new TypeInfo.<get_DeclaredNestedTypes>d__23(0);
					<get_DeclaredNestedTypes>d__.<>4__this = this;
				}
				return <get_DeclaredNestedTypes>d__;
			}

			// Token: 0x06007052 RID: 28754 RVA: 0x00182E13 File Offset: 0x00181013
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.Reflection.TypeInfo>.GetEnumerator();
			}

			// Token: 0x0400374B RID: 14155
			private int <>1__state;

			// Token: 0x0400374C RID: 14156
			private TypeInfo <>2__current;

			// Token: 0x0400374D RID: 14157
			private int <>l__initialThreadId;

			// Token: 0x0400374E RID: 14158
			public TypeInfo <>4__this;

			// Token: 0x0400374F RID: 14159
			private Type[] <>7__wrap1;

			// Token: 0x04003750 RID: 14160
			private int <>7__wrap2;
		}
	}
}
