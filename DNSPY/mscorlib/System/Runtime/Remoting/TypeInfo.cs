using System;
using System.Security;

namespace System.Runtime.Remoting
{
	// Token: 0x020007BA RID: 1978
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x06005592 RID: 21906 RVA: 0x0012FC08 File Offset: 0x0012DE08
		// (set) Token: 0x06005593 RID: 21907 RVA: 0x0012FC10 File Offset: 0x0012DE10
		public virtual string TypeName
		{
			[SecurityCritical]
			get
			{
				return this.serverType;
			}
			[SecurityCritical]
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x06005594 RID: 21908 RVA: 0x0012FC1C File Offset: 0x0012DE1C
		[SecurityCritical]
		public virtual bool CanCastTo(Type castType, object o)
		{
			if (null != castType)
			{
				if (castType == typeof(MarshalByRefObject) || castType == typeof(object))
				{
					return true;
				}
				if (castType.IsInterface)
				{
					return this.interfacesImplemented != null && this.CanCastTo(castType, this.InterfacesImplemented);
				}
				if (castType.IsMarshalByRef)
				{
					if (this.CompareTypes(castType, this.serverType))
					{
						return true;
					}
					if (this.serverHierarchy != null && this.CanCastTo(castType, this.ServerHierarchy))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06005595 RID: 21909 RVA: 0x0012FCAB File Offset: 0x0012DEAB
		[SecurityCritical]
		internal static string GetQualifiedTypeName(RuntimeType type)
		{
			if (type == null)
			{
				return null;
			}
			return RemotingServices.GetDefaultQualifiedTypeName(type);
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0012FCC0 File Offset: 0x0012DEC0
		internal static bool ParseTypeAndAssembly(string typeAndAssembly, out string typeName, out string assemName)
		{
			if (typeAndAssembly == null)
			{
				typeName = null;
				assemName = null;
				return false;
			}
			int num = typeAndAssembly.IndexOf(',');
			if (num == -1)
			{
				typeName = typeAndAssembly;
				assemName = null;
				return true;
			}
			typeName = typeAndAssembly.Substring(0, num);
			assemName = typeAndAssembly.Substring(num + 1).Trim();
			return true;
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0012FD08 File Offset: 0x0012DF08
		[SecurityCritical]
		internal TypeInfo(RuntimeType typeOfObj)
		{
			this.ServerType = TypeInfo.GetQualifiedTypeName(typeOfObj);
			RuntimeType runtimeType = (RuntimeType)typeOfObj.BaseType;
			int num = 0;
			while (runtimeType != typeof(MarshalByRefObject) && runtimeType != null)
			{
				runtimeType = (RuntimeType)runtimeType.BaseType;
				num++;
			}
			string[] array = null;
			if (num > 0)
			{
				array = new string[num];
				runtimeType = (RuntimeType)typeOfObj.BaseType;
				for (int i = 0; i < num; i++)
				{
					array[i] = TypeInfo.GetQualifiedTypeName(runtimeType);
					runtimeType = (RuntimeType)runtimeType.BaseType;
				}
			}
			this.ServerHierarchy = array;
			Type[] interfaces = typeOfObj.GetInterfaces();
			string[] array2 = null;
			bool isInterface = typeOfObj.IsInterface;
			if (interfaces.Length != 0 || isInterface)
			{
				array2 = new string[interfaces.Length + (isInterface ? 1 : 0)];
				for (int j = 0; j < interfaces.Length; j++)
				{
					array2[j] = TypeInfo.GetQualifiedTypeName((RuntimeType)interfaces[j]);
				}
				if (isInterface)
				{
					array2[array2.Length - 1] = TypeInfo.GetQualifiedTypeName(typeOfObj);
				}
			}
			this.InterfacesImplemented = array2;
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x06005598 RID: 21912 RVA: 0x0012FE17 File Offset: 0x0012E017
		// (set) Token: 0x06005599 RID: 21913 RVA: 0x0012FE1F File Offset: 0x0012E01F
		internal string ServerType
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600559A RID: 21914 RVA: 0x0012FE28 File Offset: 0x0012E028
		// (set) Token: 0x0600559B RID: 21915 RVA: 0x0012FE30 File Offset: 0x0012E030
		private string[] ServerHierarchy
		{
			get
			{
				return this.serverHierarchy;
			}
			set
			{
				this.serverHierarchy = value;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600559C RID: 21916 RVA: 0x0012FE39 File Offset: 0x0012E039
		// (set) Token: 0x0600559D RID: 21917 RVA: 0x0012FE41 File Offset: 0x0012E041
		private string[] InterfacesImplemented
		{
			get
			{
				return this.interfacesImplemented;
			}
			set
			{
				this.interfacesImplemented = value;
			}
		}

		// Token: 0x0600559E RID: 21918 RVA: 0x0012FE4C File Offset: 0x0012E04C
		[SecurityCritical]
		private bool CompareTypes(Type type1, string type2)
		{
			Type type3 = RemotingServices.InternalGetTypeFromQualifiedTypeName(type2);
			return type1 == type3;
		}

		// Token: 0x0600559F RID: 21919 RVA: 0x0012FE68 File Offset: 0x0012E068
		[SecurityCritical]
		private bool CanCastTo(Type castType, string[] types)
		{
			bool flag = false;
			if (null != castType)
			{
				for (int i = 0; i < types.Length; i++)
				{
					if (this.CompareTypes(castType, types[i]))
					{
						flag = true;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400276D RID: 10093
		private string serverType;

		// Token: 0x0400276E RID: 10094
		private string[] serverHierarchy;

		// Token: 0x0400276F RID: 10095
		private string[] interfacesImplemented;
	}
}
