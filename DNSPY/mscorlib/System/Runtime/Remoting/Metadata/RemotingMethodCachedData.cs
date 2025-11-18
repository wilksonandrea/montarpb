using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x020007D6 RID: 2006
	internal class RemotingMethodCachedData : RemotingCachedData
	{
		// Token: 0x060056E0 RID: 22240 RVA: 0x0013444B File Offset: 0x0013264B
		internal RemotingMethodCachedData(RuntimeMethodInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x0013445A File Offset: 0x0013265A
		internal RemotingMethodCachedData(RuntimeConstructorInfo ri)
		{
			this.RI = ri;
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x0013446C File Offset: 0x0013266C
		internal override SoapAttribute GetSoapAttributeNoLock()
		{
			object[] customAttributes = this.RI.GetCustomAttributes(typeof(SoapMethodAttribute), true);
			SoapAttribute soapAttribute;
			if (customAttributes != null && customAttributes.Length != 0)
			{
				soapAttribute = (SoapAttribute)customAttributes[0];
			}
			else
			{
				soapAttribute = new SoapMethodAttribute();
			}
			soapAttribute.SetReflectInfo(this.RI);
			return soapAttribute;
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x060056E3 RID: 22243 RVA: 0x001344B7 File Offset: 0x001326B7
		internal string TypeAndAssemblyName
		{
			[SecurityCritical]
			get
			{
				if (this._typeAndAssemblyName == null)
				{
					this.UpdateNames();
				}
				return this._typeAndAssemblyName;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x060056E4 RID: 22244 RVA: 0x001344CD File Offset: 0x001326CD
		internal string MethodName
		{
			[SecurityCritical]
			get
			{
				if (this._methodName == null)
				{
					this.UpdateNames();
				}
				return this._methodName;
			}
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x001344E4 File Offset: 0x001326E4
		[SecurityCritical]
		private void UpdateNames()
		{
			MethodBase ri = this.RI;
			this._methodName = ri.Name;
			if (ri.DeclaringType != null)
			{
				this._typeAndAssemblyName = RemotingServices.GetDefaultQualifiedTypeName((RuntimeType)ri.DeclaringType);
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x00134528 File Offset: 0x00132728
		internal ParameterInfo[] Parameters
		{
			get
			{
				if (this._parameters == null)
				{
					this._parameters = this.RI.GetParameters();
				}
				return this._parameters;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x060056E7 RID: 22247 RVA: 0x00134549 File Offset: 0x00132749
		internal int[] OutRefArgMap
		{
			get
			{
				if (this._outRefArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outRefArgMap;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x060056E8 RID: 22248 RVA: 0x0013455F File Offset: 0x0013275F
		internal int[] OutOnlyArgMap
		{
			get
			{
				if (this._outOnlyArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._outOnlyArgMap;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x060056E9 RID: 22249 RVA: 0x00134575 File Offset: 0x00132775
		internal int[] NonRefOutArgMap
		{
			get
			{
				if (this._nonRefOutArgMap == null)
				{
					this.GetArgMaps();
				}
				return this._nonRefOutArgMap;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x0013458B File Offset: 0x0013278B
		internal int[] MarshalRequestArgMap
		{
			get
			{
				if (this._marshalRequestMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalRequestMap;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x060056EB RID: 22251 RVA: 0x001345A1 File Offset: 0x001327A1
		internal int[] MarshalResponseArgMap
		{
			get
			{
				if (this._marshalResponseMap == null)
				{
					this.GetArgMaps();
				}
				return this._marshalResponseMap;
			}
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x001345B8 File Offset: 0x001327B8
		private void GetArgMaps()
		{
			lock (this)
			{
				if (this._inRefArgMap == null)
				{
					int[] array = null;
					int[] array2 = null;
					int[] array3 = null;
					int[] array4 = null;
					int[] array5 = null;
					int[] array6 = null;
					ArgMapper.GetParameterMaps(this.Parameters, out array, out array2, out array3, out array4, out array5, out array6);
					this._inRefArgMap = array;
					this._outRefArgMap = array2;
					this._outOnlyArgMap = array3;
					this._nonRefOutArgMap = array4;
					this._marshalRequestMap = array5;
					this._marshalResponseMap = array6;
				}
			}
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x0013464C File Offset: 0x0013284C
		internal bool IsOneWayMethod()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOneWay;
				object[] customAttributes = this.RI.GetCustomAttributes(typeof(OneWayAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOneWay;
				}
				this.flags |= methodCacheFlags;
				return (methodCacheFlags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > RemotingMethodCachedData.MethodCacheFlags.None;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOneWay) > RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x001346A8 File Offset: 0x001328A8
		internal bool IsOverloaded()
		{
			if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded) == RemotingMethodCachedData.MethodCacheFlags.None)
			{
				RemotingMethodCachedData.MethodCacheFlags methodCacheFlags = RemotingMethodCachedData.MethodCacheFlags.CheckedOverloaded;
				MethodBase ri = this.RI;
				RuntimeMethodInfo runtimeMethodInfo;
				if ((runtimeMethodInfo = ri as RuntimeMethodInfo) != null)
				{
					if (runtimeMethodInfo.IsOverloaded)
					{
						methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
					}
				}
				else
				{
					RuntimeConstructorInfo runtimeConstructorInfo;
					if (!((runtimeConstructorInfo = ri as RuntimeConstructorInfo) != null))
					{
						throw new NotSupportedException(Environment.GetResourceString("InvalidOperation_Method"));
					}
					if (runtimeConstructorInfo.IsOverloaded)
					{
						methodCacheFlags |= RemotingMethodCachedData.MethodCacheFlags.IsOverloaded;
					}
				}
				this.flags |= methodCacheFlags;
			}
			return (this.flags & RemotingMethodCachedData.MethodCacheFlags.IsOverloaded) > RemotingMethodCachedData.MethodCacheFlags.None;
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x060056EF RID: 22255 RVA: 0x00134734 File Offset: 0x00132934
		internal Type ReturnType
		{
			get
			{
				if ((this.flags & RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType) == RemotingMethodCachedData.MethodCacheFlags.None)
				{
					MethodInfo methodInfo = this.RI as MethodInfo;
					if (methodInfo != null)
					{
						Type returnType = methodInfo.ReturnType;
						if (returnType != typeof(void))
						{
							this._returnType = returnType;
						}
					}
					this.flags |= RemotingMethodCachedData.MethodCacheFlags.CheckedForReturnType;
				}
				return this._returnType;
			}
		}

		// Token: 0x040027C6 RID: 10182
		private MethodBase RI;

		// Token: 0x040027C7 RID: 10183
		private ParameterInfo[] _parameters;

		// Token: 0x040027C8 RID: 10184
		private RemotingMethodCachedData.MethodCacheFlags flags;

		// Token: 0x040027C9 RID: 10185
		private string _typeAndAssemblyName;

		// Token: 0x040027CA RID: 10186
		private string _methodName;

		// Token: 0x040027CB RID: 10187
		private Type _returnType;

		// Token: 0x040027CC RID: 10188
		private int[] _inRefArgMap;

		// Token: 0x040027CD RID: 10189
		private int[] _outRefArgMap;

		// Token: 0x040027CE RID: 10190
		private int[] _outOnlyArgMap;

		// Token: 0x040027CF RID: 10191
		private int[] _nonRefOutArgMap;

		// Token: 0x040027D0 RID: 10192
		private int[] _marshalRequestMap;

		// Token: 0x040027D1 RID: 10193
		private int[] _marshalResponseMap;

		// Token: 0x02000C71 RID: 3185
		[Flags]
		[Serializable]
		private enum MethodCacheFlags
		{
			// Token: 0x040037EF RID: 14319
			None = 0,
			// Token: 0x040037F0 RID: 14320
			CheckedOneWay = 1,
			// Token: 0x040037F1 RID: 14321
			IsOneWay = 2,
			// Token: 0x040037F2 RID: 14322
			CheckedOverloaded = 4,
			// Token: 0x040037F3 RID: 14323
			IsOverloaded = 8,
			// Token: 0x040037F4 RID: 14324
			CheckedForAsync = 16,
			// Token: 0x040037F5 RID: 14325
			CheckedForReturnType = 32
		}
	}
}
