using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009B0 RID: 2480
	internal class ComEventsMethod
	{
		// Token: 0x06006320 RID: 25376 RVA: 0x00151B41 File Offset: 0x0014FD41
		internal ComEventsMethod(int dispid)
		{
			this._delegateWrappers = null;
			this._dispid = dispid;
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x00151B57 File Offset: 0x0014FD57
		internal static ComEventsMethod Find(ComEventsMethod methods, int dispid)
		{
			while (methods != null && methods._dispid != dispid)
			{
				methods = methods._next;
			}
			return methods;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x00151B70 File Offset: 0x0014FD70
		internal static ComEventsMethod Add(ComEventsMethod methods, ComEventsMethod method)
		{
			method._next = methods;
			return method;
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x00151B7C File Offset: 0x0014FD7C
		internal static ComEventsMethod Remove(ComEventsMethod methods, ComEventsMethod method)
		{
			if (methods == method)
			{
				methods = methods._next;
			}
			else
			{
				ComEventsMethod comEventsMethod = methods;
				while (comEventsMethod != null && comEventsMethod._next != method)
				{
					comEventsMethod = comEventsMethod._next;
				}
				if (comEventsMethod != null)
				{
					comEventsMethod._next = method._next;
				}
			}
			return methods;
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06006324 RID: 25380 RVA: 0x00151BBE File Offset: 0x0014FDBE
		internal int DispId
		{
			get
			{
				return this._dispid;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06006325 RID: 25381 RVA: 0x00151BC6 File Offset: 0x0014FDC6
		internal bool Empty
		{
			get
			{
				return this._delegateWrappers == null || this._delegateWrappers.Length == 0;
			}
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x00151BDC File Offset: 0x0014FDDC
		internal void AddDelegate(Delegate d)
		{
			int num = 0;
			if (this._delegateWrappers != null)
			{
				num = this._delegateWrappers.Length;
			}
			for (int i = 0; i < num; i++)
			{
				if (this._delegateWrappers[i].Delegate.GetType() == d.GetType())
				{
					this._delegateWrappers[i].Delegate = Delegate.Combine(this._delegateWrappers[i].Delegate, d);
					return;
				}
			}
			ComEventsMethod.DelegateWrapper[] array = new ComEventsMethod.DelegateWrapper[num + 1];
			if (num > 0)
			{
				this._delegateWrappers.CopyTo(array, 0);
			}
			ComEventsMethod.DelegateWrapper delegateWrapper = new ComEventsMethod.DelegateWrapper(d);
			array[num] = delegateWrapper;
			this._delegateWrappers = array;
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x00151C74 File Offset: 0x0014FE74
		internal void RemoveDelegate(Delegate d)
		{
			int num = this._delegateWrappers.Length;
			int num2 = -1;
			for (int i = 0; i < num; i++)
			{
				if (this._delegateWrappers[i].Delegate.GetType() == d.GetType())
				{
					num2 = i;
					break;
				}
			}
			if (num2 < 0)
			{
				return;
			}
			Delegate @delegate = Delegate.Remove(this._delegateWrappers[num2].Delegate, d);
			if (@delegate != null)
			{
				this._delegateWrappers[num2].Delegate = @delegate;
				return;
			}
			if (num == 1)
			{
				this._delegateWrappers = null;
				return;
			}
			ComEventsMethod.DelegateWrapper[] array = new ComEventsMethod.DelegateWrapper[num - 1];
			int j;
			for (j = 0; j < num2; j++)
			{
				array[j] = this._delegateWrappers[j];
			}
			while (j < num - 1)
			{
				array[j] = this._delegateWrappers[j + 1];
				j++;
			}
			this._delegateWrappers = array;
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x00151D44 File Offset: 0x0014FF44
		internal object Invoke(object[] args)
		{
			object obj = null;
			ComEventsMethod.DelegateWrapper[] delegateWrappers = this._delegateWrappers;
			foreach (ComEventsMethod.DelegateWrapper delegateWrapper in delegateWrappers)
			{
				if (delegateWrapper != null && delegateWrapper.Delegate != null)
				{
					obj = delegateWrapper.Invoke(args);
				}
			}
			return obj;
		}

		// Token: 0x04002CBF RID: 11455
		private ComEventsMethod.DelegateWrapper[] _delegateWrappers;

		// Token: 0x04002CC0 RID: 11456
		private int _dispid;

		// Token: 0x04002CC1 RID: 11457
		private ComEventsMethod _next;

		// Token: 0x02000C9E RID: 3230
		internal class DelegateWrapper
		{
			// Token: 0x06007126 RID: 28966 RVA: 0x001853F8 File Offset: 0x001835F8
			public DelegateWrapper(Delegate d)
			{
				this._d = d;
			}

			// Token: 0x17001366 RID: 4966
			// (get) Token: 0x06007127 RID: 28967 RVA: 0x00185407 File Offset: 0x00183607
			// (set) Token: 0x06007128 RID: 28968 RVA: 0x0018540F File Offset: 0x0018360F
			public Delegate Delegate
			{
				get
				{
					return this._d;
				}
				set
				{
					this._d = value;
				}
			}

			// Token: 0x06007129 RID: 28969 RVA: 0x00185418 File Offset: 0x00183618
			public object Invoke(object[] args)
			{
				if (this._d == null)
				{
					return null;
				}
				if (!this._once)
				{
					this.PreProcessSignature();
					this._once = true;
				}
				if (this._cachedTargetTypes != null && this._expectedParamsCount == args.Length)
				{
					for (int i = 0; i < this._expectedParamsCount; i++)
					{
						if (this._cachedTargetTypes[i] != null)
						{
							args[i] = Enum.ToObject(this._cachedTargetTypes[i], args[i]);
						}
					}
				}
				return this._d.DynamicInvoke(args);
			}

			// Token: 0x0600712A RID: 28970 RVA: 0x00185498 File Offset: 0x00183698
			private void PreProcessSignature()
			{
				ParameterInfo[] parameters = this._d.Method.GetParameters();
				this._expectedParamsCount = parameters.Length;
				Type[] array = new Type[this._expectedParamsCount];
				bool flag = false;
				for (int i = 0; i < this._expectedParamsCount; i++)
				{
					ParameterInfo parameterInfo = parameters[i];
					if (parameterInfo.ParameterType.IsByRef && parameterInfo.ParameterType.HasElementType && parameterInfo.ParameterType.GetElementType().IsEnum)
					{
						flag = true;
						array[i] = parameterInfo.ParameterType.GetElementType();
					}
				}
				if (flag)
				{
					this._cachedTargetTypes = array;
				}
			}

			// Token: 0x04003865 RID: 14437
			private Delegate _d;

			// Token: 0x04003866 RID: 14438
			private bool _once;

			// Token: 0x04003867 RID: 14439
			private int _expectedParamsCount;

			// Token: 0x04003868 RID: 14440
			private Type[] _cachedTargetTypes;
		}
	}
}
