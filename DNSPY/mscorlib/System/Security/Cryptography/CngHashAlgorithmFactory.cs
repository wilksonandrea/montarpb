using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x02000251 RID: 593
	internal sealed class CngHashAlgorithmFactory<THashAlgorithm> where THashAlgorithm : HashAlgorithm
	{
		// Token: 0x0600210F RID: 8463 RVA: 0x00073054 File Offset: 0x00071254
		internal CngHashAlgorithmFactory(string fullTypeName)
		{
			this._fullTypeName = fullTypeName;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00073064 File Offset: 0x00071264
		internal THashAlgorithm CreateInstance()
		{
			THashAlgorithm thashAlgorithm = default(THashAlgorithm);
			if (!this._innerFactoryInitialized && !AppDomain.IsStillInEarlyInit())
			{
				this._innerFactory = CngHashAlgorithmFactory<THashAlgorithm>.FactoryBuilder.SafeCreateFactory(this._fullTypeName, out this._mostRecentException);
				this._innerFactoryInitialized = true;
			}
			if (this._innerFactoryInitialized)
			{
				try
				{
					try
					{
						Func<THashAlgorithm> innerFactory = this._innerFactory;
						thashAlgorithm = ((innerFactory != null) ? innerFactory() : default(THashAlgorithm));
					}
					catch (Exception ex)
					{
						this._mostRecentException = ex;
					}
				}
				catch
				{
				}
			}
			if (thashAlgorithm == null)
			{
				thashAlgorithm = (THashAlgorithm)((object)CryptoConfig.CreateFromName(this._fullTypeName));
			}
			return thashAlgorithm;
		}

		// Token: 0x04000BF1 RID: 3057
		private readonly string _fullTypeName;

		// Token: 0x04000BF2 RID: 3058
		private Func<THashAlgorithm> _innerFactory;

		// Token: 0x04000BF3 RID: 3059
		private volatile bool _innerFactoryInitialized;

		// Token: 0x04000BF4 RID: 3060
		private Exception _mostRecentException;

		// Token: 0x02000B44 RID: 2884
		private static class FactoryBuilder
		{
			// Token: 0x06006B8F RID: 27535 RVA: 0x0017382D File Offset: 0x00171A2D
			[MethodImpl(MethodImplOptions.NoInlining)]
			internal static Func<THashAlgorithm> SafeCreateFactory(string fullTypeName, out Exception exception)
			{
				return CngHashAlgorithmFactory<THashAlgorithm>.FactoryBuilder.Impl.SafeCreateFactory(fullTypeName, out exception);
			}

			// Token: 0x02000D03 RID: 3331
			private static class Impl
			{
				// Token: 0x060071F8 RID: 29176 RVA: 0x001887A4 File Offset: 0x001869A4
				internal static Func<THashAlgorithm> SafeCreateFactory(string fullTypeName, out Exception exception)
				{
					exception = null;
					try
					{
						try
						{
							Func<THashAlgorithm> func = CngHashAlgorithmFactory<THashAlgorithm>.FactoryBuilder.Impl.DangerousFetchFactoryFromSystemCore(fullTypeName);
							THashAlgorithm thashAlgorithm = func();
							if (thashAlgorithm != null)
							{
								thashAlgorithm.Dispose();
								return func;
							}
						}
						catch (Exception ex)
						{
							exception = ex;
						}
					}
					catch
					{
					}
					return null;
				}

				// Token: 0x060071F9 RID: 29177 RVA: 0x00188808 File Offset: 0x00186A08
				[SecuritySafeCritical]
				[ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.AllFlags)]
				private static Func<THashAlgorithm> DangerousFetchFactoryFromSystemCore(string fullTypeName)
				{
					Assembly assembly = Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089");
					Type type = assembly.GetType(fullTypeName + "Factory");
					MethodInfo method = type.GetMethod("CreateNew", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
					return (Func<THashAlgorithm>)method.CreateDelegate(typeof(Func<THashAlgorithm>));
				}
			}
		}
	}
}
