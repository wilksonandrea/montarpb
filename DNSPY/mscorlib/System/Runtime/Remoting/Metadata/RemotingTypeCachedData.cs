using System;
using System.Reflection;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D5 RID: 2005
	internal class RemotingTypeCachedData : RemotingCachedData
	{
		// Token: 0x060056D8 RID: 22232 RVA: 0x001342EF File Offset: 0x001324EF
		internal RemotingTypeCachedData(RuntimeType ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x00134300 File Offset: 0x00132500
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapTypeAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapTypeAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x0013434C File Offset: 0x0013254C
		internal MethodBase GetLastCalledMethod(string newMeth)
		{
			RemotingTypeCachedData.LastCalledMethodClass lastMethodCalled = this._lastMethodCalled;
			if (lastMethodCalled == null)
			{
				return null;
			}
			string methodName = lastMethodCalled.methodName;
			MethodBase mb = lastMethodCalled.MB;
			if (mb == null || methodName == null)
			{
				return null;
			}
			if (methodName.Equals(newMeth))
			{
				return mb;
			}
			return null;
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x00134390 File Offset: 0x00132590
		internal void SetLastCalledMethod(string newMethName, MethodBase newMB)
		{
			this._lastMethodCalled = new RemotingTypeCachedData.LastCalledMethodClass
			{
				methodName = newMethName,
				MB = newMB
			};
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x060056DC RID: 22236 RVA: 0x001343B8 File Offset: 0x001325B8
		internal TypeInfo TypeInfo
		{
			[SecurityCritical]
			get
			{
				if (this._typeInfo == null)
				{
					this._typeInfo = new TypeInfo(this.RI);
				}
				return this._typeInfo;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x001343D9 File Offset: 0x001325D9
		internal string QualifiedTypeName
		{
			[SecurityCritical]
			get
			{
				if (this._qualifiedTypeName == null)
				{
					this._qualifiedTypeName = RemotingServices.DetermineDefaultQualifiedTypeName(this.RI);
				}
				return this._qualifiedTypeName;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x060056DE RID: 22238 RVA: 0x001343FA File Offset: 0x001325FA
		internal string AssemblyName
		{
			get
			{
				if (this._assemblyName == null)
				{
					this._assemblyName = this.RI.Module.Assembly.FullName;
				}
				return this._assemblyName;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x060056DF RID: 22239 RVA: 0x00134425 File Offset: 0x00132625
		internal string SimpleAssemblyName
		{
			[SecurityCritical]
			get
			{
				if (this._simpleAssemblyName == null)
				{
					this._simpleAssemblyName = this.RI.GetRuntimeAssembly().GetSimpleName();
				}
				return this._simpleAssemblyName;
			}
		}

		// Token: 0x040027C0 RID: 10176
		private RuntimeType RI;

		// Token: 0x040027C1 RID: 10177
		private RemotingTypeCachedData.LastCalledMethodClass _lastMethodCalled;

		// Token: 0x040027C2 RID: 10178
		private TypeInfo _typeInfo;

		// Token: 0x040027C3 RID: 10179
		private string _qualifiedTypeName;

		// Token: 0x040027C4 RID: 10180
		private string _assemblyName;

		// Token: 0x040027C5 RID: 10181
		private string _simpleAssemblyName;

		// Token: 0x02000C70 RID: 3184
		private class LastCalledMethodClass
		{
			// Token: 0x060070B3 RID: 28851 RVA: 0x001846B1 File Offset: 0x001828B1
			public LastCalledMethodClass()
			{
			}

			// Token: 0x040037EC RID: 14316
			public string methodName;

			// Token: 0x040037ED RID: 14317
			public MethodBase MB;
		}
	}
}
