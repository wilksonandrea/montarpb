using System;
using System.Configuration.Assemblies;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Reflection
{
	// Token: 0x020005C9 RID: 1481
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_AssemblyName))]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class AssemblyName : _AssemblyName, ICloneable, ISerializable, IDeserializationCallback
	{
		// Token: 0x06004484 RID: 17540 RVA: 0x000FC3AE File Offset: 0x000FA5AE
		[__DynamicallyInvokable]
		public AssemblyName()
		{
			this._HashAlgorithm = AssemblyHashAlgorithm.None;
			this._VersionCompatibility = AssemblyVersionCompatibility.SameMachine;
			this._Flags = AssemblyNameFlags.None;
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x000FC3CB File Offset: 0x000FA5CB
		// (set) Token: 0x06004486 RID: 17542 RVA: 0x000FC3D3 File Offset: 0x000FA5D3
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Name;
			}
			[__DynamicallyInvokable]
			set
			{
				this._Name = value;
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06004487 RID: 17543 RVA: 0x000FC3DC File Offset: 0x000FA5DC
		// (set) Token: 0x06004488 RID: 17544 RVA: 0x000FC3E4 File Offset: 0x000FA5E4
		[__DynamicallyInvokable]
		public Version Version
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Version;
			}
			[__DynamicallyInvokable]
			set
			{
				this._Version = value;
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06004489 RID: 17545 RVA: 0x000FC3ED File Offset: 0x000FA5ED
		// (set) Token: 0x0600448A RID: 17546 RVA: 0x000FC3F5 File Offset: 0x000FA5F5
		[__DynamicallyInvokable]
		public CultureInfo CultureInfo
		{
			[__DynamicallyInvokable]
			get
			{
				return this._CultureInfo;
			}
			[__DynamicallyInvokable]
			set
			{
				this._CultureInfo = value;
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x0600448B RID: 17547 RVA: 0x000FC3FE File Offset: 0x000FA5FE
		// (set) Token: 0x0600448C RID: 17548 RVA: 0x000FC415 File Offset: 0x000FA615
		[__DynamicallyInvokable]
		public string CultureName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this._CultureInfo != null)
				{
					return this._CultureInfo.Name;
				}
				return null;
			}
			[__DynamicallyInvokable]
			set
			{
				this._CultureInfo = ((value == null) ? null : new CultureInfo(value));
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x0600448D RID: 17549 RVA: 0x000FC429 File Offset: 0x000FA629
		// (set) Token: 0x0600448E RID: 17550 RVA: 0x000FC431 File Offset: 0x000FA631
		public string CodeBase
		{
			get
			{
				return this._CodeBase;
			}
			set
			{
				this._CodeBase = value;
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x0600448F RID: 17551 RVA: 0x000FC43A File Offset: 0x000FA63A
		public string EscapedCodeBase
		{
			[SecuritySafeCritical]
			get
			{
				if (this._CodeBase == null)
				{
					return null;
				}
				return AssemblyName.EscapeCodeBase(this._CodeBase);
			}
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x000FC454 File Offset: 0x000FA654
		// (set) Token: 0x06004491 RID: 17553 RVA: 0x000FC474 File Offset: 0x000FA674
		[__DynamicallyInvokable]
		public ProcessorArchitecture ProcessorArchitecture
		{
			[__DynamicallyInvokable]
			get
			{
				int num = (int)((this._Flags & (AssemblyNameFlags)112) >> 4);
				if (num > 5)
				{
					num = 0;
				}
				return (ProcessorArchitecture)num;
			}
			[__DynamicallyInvokable]
			set
			{
				int num = (int)(value & (ProcessorArchitecture)7);
				if (num <= 5)
				{
					this._Flags = (AssemblyNameFlags)((long)this._Flags & (long)((ulong)(-241)));
					this._Flags |= (AssemblyNameFlags)(num << 4);
				}
			}
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000FC4B0 File Offset: 0x000FA6B0
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x000FC4D4 File Offset: 0x000FA6D4
		[ComVisible(false)]
		[__DynamicallyInvokable]
		public AssemblyContentType ContentType
		{
			[__DynamicallyInvokable]
			get
			{
				int num = (int)((this._Flags & (AssemblyNameFlags)3584) >> 9);
				if (num > 1)
				{
					num = 0;
				}
				return (AssemblyContentType)num;
			}
			[__DynamicallyInvokable]
			set
			{
				int num = (int)(value & (AssemblyContentType)7);
				if (num <= 1)
				{
					this._Flags = (AssemblyNameFlags)((long)this._Flags & (long)((ulong)(-3585)));
					this._Flags |= (AssemblyNameFlags)(num << 9);
				}
			}
		}

		// Token: 0x06004494 RID: 17556 RVA: 0x000FC510 File Offset: 0x000FA710
		public object Clone()
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.Init(this._Name, this._PublicKey, this._PublicKeyToken, this._Version, this._CultureInfo, this._HashAlgorithm, this._VersionCompatibility, this._CodeBase, this._Flags, this._StrongNameKeyPair);
			assemblyName._HashForControl = this._HashForControl;
			assemblyName._HashAlgorithmForControl = this._HashAlgorithmForControl;
			return assemblyName;
		}

		// Token: 0x06004495 RID: 17557 RVA: 0x000FC580 File Offset: 0x000FA780
		[SecuritySafeCritical]
		public static AssemblyName GetAssemblyName(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			string fullPathInternal = Path.GetFullPathInternal(assemblyFile);
			new FileIOPermission(FileIOPermissionAccess.PathDiscovery, fullPathInternal).Demand();
			return AssemblyName.nGetFileInformation(fullPathInternal);
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x000FC5B4 File Offset: 0x000FA7B4
		internal void SetHashControl(byte[] hash, AssemblyHashAlgorithm hashAlgorithm)
		{
			this._HashForControl = hash;
			this._HashAlgorithmForControl = hashAlgorithm;
		}

		// Token: 0x06004497 RID: 17559 RVA: 0x000FC5C4 File Offset: 0x000FA7C4
		[__DynamicallyInvokable]
		public byte[] GetPublicKey()
		{
			return this._PublicKey;
		}

		// Token: 0x06004498 RID: 17560 RVA: 0x000FC5CC File Offset: 0x000FA7CC
		[__DynamicallyInvokable]
		public void SetPublicKey(byte[] publicKey)
		{
			this._PublicKey = publicKey;
			if (publicKey == null)
			{
				this._Flags &= ~AssemblyNameFlags.PublicKey;
				return;
			}
			this._Flags |= AssemblyNameFlags.PublicKey;
		}

		// Token: 0x06004499 RID: 17561 RVA: 0x000FC5F6 File Offset: 0x000FA7F6
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public byte[] GetPublicKeyToken()
		{
			if (this._PublicKeyToken == null)
			{
				this._PublicKeyToken = this.nGetPublicKeyToken();
			}
			return this._PublicKeyToken;
		}

		// Token: 0x0600449A RID: 17562 RVA: 0x000FC612 File Offset: 0x000FA812
		[__DynamicallyInvokable]
		public void SetPublicKeyToken(byte[] publicKeyToken)
		{
			this._PublicKeyToken = publicKeyToken;
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x0600449B RID: 17563 RVA: 0x000FC61B File Offset: 0x000FA81B
		// (set) Token: 0x0600449C RID: 17564 RVA: 0x000FC629 File Offset: 0x000FA829
		[__DynamicallyInvokable]
		public AssemblyNameFlags Flags
		{
			[__DynamicallyInvokable]
			get
			{
				return this._Flags & (AssemblyNameFlags)(-3825);
			}
			[__DynamicallyInvokable]
			set
			{
				this._Flags &= (AssemblyNameFlags)3824;
				this._Flags |= value & (AssemblyNameFlags)(-3825);
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x0600449D RID: 17565 RVA: 0x000FC651 File Offset: 0x000FA851
		// (set) Token: 0x0600449E RID: 17566 RVA: 0x000FC659 File Offset: 0x000FA859
		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return this._HashAlgorithm;
			}
			set
			{
				this._HashAlgorithm = value;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x0600449F RID: 17567 RVA: 0x000FC662 File Offset: 0x000FA862
		// (set) Token: 0x060044A0 RID: 17568 RVA: 0x000FC66A File Offset: 0x000FA86A
		public AssemblyVersionCompatibility VersionCompatibility
		{
			get
			{
				return this._VersionCompatibility;
			}
			set
			{
				this._VersionCompatibility = value;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060044A1 RID: 17569 RVA: 0x000FC673 File Offset: 0x000FA873
		// (set) Token: 0x060044A2 RID: 17570 RVA: 0x000FC67B File Offset: 0x000FA87B
		public StrongNameKeyPair KeyPair
		{
			get
			{
				return this._StrongNameKeyPair;
			}
			set
			{
				this._StrongNameKeyPair = value;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060044A3 RID: 17571 RVA: 0x000FC684 File Offset: 0x000FA884
		[__DynamicallyInvokable]
		public string FullName
		{
			[SecuritySafeCritical]
			[__DynamicallyInvokable]
			get
			{
				string text = this.nToString();
				if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8 && string.IsNullOrEmpty(text))
				{
					return base.ToString();
				}
				return text;
			}
		}

		// Token: 0x060044A4 RID: 17572 RVA: 0x000FC6B0 File Offset: 0x000FA8B0
		[__DynamicallyInvokable]
		public override string ToString()
		{
			string fullName = this.FullName;
			if (fullName == null)
			{
				return base.ToString();
			}
			return fullName;
		}

		// Token: 0x060044A5 RID: 17573 RVA: 0x000FC6D0 File Offset: 0x000FA8D0
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("_Name", this._Name);
			info.AddValue("_PublicKey", this._PublicKey, typeof(byte[]));
			info.AddValue("_PublicKeyToken", this._PublicKeyToken, typeof(byte[]));
			info.AddValue("_CultureInfo", (this._CultureInfo == null) ? (-1) : this._CultureInfo.LCID);
			info.AddValue("_CodeBase", this._CodeBase);
			info.AddValue("_Version", this._Version);
			info.AddValue("_HashAlgorithm", this._HashAlgorithm, typeof(AssemblyHashAlgorithm));
			info.AddValue("_HashAlgorithmForControl", this._HashAlgorithmForControl, typeof(AssemblyHashAlgorithm));
			info.AddValue("_StrongNameKeyPair", this._StrongNameKeyPair, typeof(StrongNameKeyPair));
			info.AddValue("_VersionCompatibility", this._VersionCompatibility, typeof(AssemblyVersionCompatibility));
			info.AddValue("_Flags", this._Flags, typeof(AssemblyNameFlags));
			info.AddValue("_HashForControl", this._HashForControl, typeof(byte[]));
		}

		// Token: 0x060044A6 RID: 17574 RVA: 0x000FC82C File Offset: 0x000FAA2C
		public void OnDeserialization(object sender)
		{
			if (this.m_siInfo == null)
			{
				return;
			}
			this._Name = this.m_siInfo.GetString("_Name");
			this._PublicKey = (byte[])this.m_siInfo.GetValue("_PublicKey", typeof(byte[]));
			this._PublicKeyToken = (byte[])this.m_siInfo.GetValue("_PublicKeyToken", typeof(byte[]));
			int @int = this.m_siInfo.GetInt32("_CultureInfo");
			if (@int != -1)
			{
				this._CultureInfo = new CultureInfo(@int);
			}
			this._CodeBase = this.m_siInfo.GetString("_CodeBase");
			this._Version = (Version)this.m_siInfo.GetValue("_Version", typeof(Version));
			this._HashAlgorithm = (AssemblyHashAlgorithm)this.m_siInfo.GetValue("_HashAlgorithm", typeof(AssemblyHashAlgorithm));
			this._StrongNameKeyPair = (StrongNameKeyPair)this.m_siInfo.GetValue("_StrongNameKeyPair", typeof(StrongNameKeyPair));
			this._VersionCompatibility = (AssemblyVersionCompatibility)this.m_siInfo.GetValue("_VersionCompatibility", typeof(AssemblyVersionCompatibility));
			this._Flags = (AssemblyNameFlags)this.m_siInfo.GetValue("_Flags", typeof(AssemblyNameFlags));
			try
			{
				this._HashAlgorithmForControl = (AssemblyHashAlgorithm)this.m_siInfo.GetValue("_HashAlgorithmForControl", typeof(AssemblyHashAlgorithm));
				this._HashForControl = (byte[])this.m_siInfo.GetValue("_HashForControl", typeof(byte[]));
			}
			catch (SerializationException)
			{
				this._HashAlgorithmForControl = AssemblyHashAlgorithm.None;
				this._HashForControl = null;
			}
			this.m_siInfo = null;
		}

		// Token: 0x060044A7 RID: 17575 RVA: 0x000FCA08 File Offset: 0x000FAC08
		internal AssemblyName(SerializationInfo info, StreamingContext context)
		{
			this.m_siInfo = info;
		}

		// Token: 0x060044A8 RID: 17576 RVA: 0x000FCA18 File Offset: 0x000FAC18
		[SecuritySafeCritical]
		[__DynamicallyInvokable]
		public AssemblyName(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0 || assemblyName[0] == '\0')
			{
				throw new ArgumentException(Environment.GetResourceString("Format_StringZeroLength"));
			}
			this._Name = assemblyName;
			this.nInit();
		}

		// Token: 0x060044A9 RID: 17577 RVA: 0x000FCA67 File Offset: 0x000FAC67
		[SecuritySafeCritical]
		public static bool ReferenceMatchesDefinition(AssemblyName reference, AssemblyName definition)
		{
			return reference == definition || AssemblyName.ReferenceMatchesDefinitionInternal(reference, definition, true);
		}

		// Token: 0x060044AA RID: 17578
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ReferenceMatchesDefinitionInternal(AssemblyName reference, AssemblyName definition, bool parse);

		// Token: 0x060044AB RID: 17579
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void nInit(out RuntimeAssembly assembly, bool forIntrospection, bool raiseResolveEvent);

		// Token: 0x060044AC RID: 17580 RVA: 0x000FCA78 File Offset: 0x000FAC78
		[SecurityCritical]
		internal void nInit()
		{
			RuntimeAssembly runtimeAssembly = null;
			this.nInit(out runtimeAssembly, false, false);
		}

		// Token: 0x060044AD RID: 17581 RVA: 0x000FCA91 File Offset: 0x000FAC91
		internal void SetProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm)
		{
			this.ProcessorArchitecture = AssemblyName.CalculateProcArchIndex(pek, ifm, this._Flags);
		}

		// Token: 0x060044AE RID: 17582 RVA: 0x000FCAA8 File Offset: 0x000FACA8
		internal static ProcessorArchitecture CalculateProcArchIndex(PortableExecutableKinds pek, ImageFileMachine ifm, AssemblyNameFlags flags)
		{
			if ((flags & (AssemblyNameFlags)240) == (AssemblyNameFlags)112)
			{
				return ProcessorArchitecture.None;
			}
			if ((pek & PortableExecutableKinds.PE32Plus) == PortableExecutableKinds.PE32Plus)
			{
				if (ifm != ImageFileMachine.I386)
				{
					if (ifm == ImageFileMachine.IA64)
					{
						return ProcessorArchitecture.IA64;
					}
					if (ifm == ImageFileMachine.AMD64)
					{
						return ProcessorArchitecture.Amd64;
					}
				}
				else if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
				{
					return ProcessorArchitecture.MSIL;
				}
			}
			else if (ifm == ImageFileMachine.I386)
			{
				if ((pek & PortableExecutableKinds.Required32Bit) == PortableExecutableKinds.Required32Bit)
				{
					return ProcessorArchitecture.X86;
				}
				if ((pek & PortableExecutableKinds.ILOnly) == PortableExecutableKinds.ILOnly)
				{
					return ProcessorArchitecture.MSIL;
				}
				return ProcessorArchitecture.X86;
			}
			else if (ifm == ImageFileMachine.ARM)
			{
				return ProcessorArchitecture.Arm;
			}
			return ProcessorArchitecture.None;
		}

		// Token: 0x060044AF RID: 17583 RVA: 0x000FCB14 File Offset: 0x000FAD14
		internal void Init(string name, byte[] publicKey, byte[] publicKeyToken, Version version, CultureInfo cultureInfo, AssemblyHashAlgorithm hashAlgorithm, AssemblyVersionCompatibility versionCompatibility, string codeBase, AssemblyNameFlags flags, StrongNameKeyPair keyPair)
		{
			this._Name = name;
			if (publicKey != null)
			{
				this._PublicKey = new byte[publicKey.Length];
				Array.Copy(publicKey, this._PublicKey, publicKey.Length);
			}
			if (publicKeyToken != null)
			{
				this._PublicKeyToken = new byte[publicKeyToken.Length];
				Array.Copy(publicKeyToken, this._PublicKeyToken, publicKeyToken.Length);
			}
			if (version != null)
			{
				this._Version = (Version)version.Clone();
			}
			this._CultureInfo = cultureInfo;
			this._HashAlgorithm = hashAlgorithm;
			this._VersionCompatibility = versionCompatibility;
			this._CodeBase = codeBase;
			this._Flags = flags;
			this._StrongNameKeyPair = keyPair;
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x000FCBB4 File Offset: 0x000FADB4
		void _AssemblyName.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000FCBBB File Offset: 0x000FADBB
		void _AssemblyName.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000FCBC2 File Offset: 0x000FADC2
		void _AssemblyName.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044B3 RID: 17587 RVA: 0x000FCBC9 File Offset: 0x000FADC9
		void _AssemblyName.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060044B4 RID: 17588 RVA: 0x000FCBD0 File Offset: 0x000FADD0
		internal string GetNameWithPublicKey()
		{
			byte[] publicKey = this.GetPublicKey();
			return this.Name + ", PublicKey=" + Hex.EncodeHexString(publicKey);
		}

		// Token: 0x060044B5 RID: 17589
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern AssemblyName nGetFileInformation(string s);

		// Token: 0x060044B6 RID: 17590
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string nToString();

		// Token: 0x060044B7 RID: 17591
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern byte[] nGetPublicKeyToken();

		// Token: 0x060044B8 RID: 17592
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string EscapeCodeBase(string codeBase);

		// Token: 0x04001C1A RID: 7194
		private string _Name;

		// Token: 0x04001C1B RID: 7195
		private byte[] _PublicKey;

		// Token: 0x04001C1C RID: 7196
		private byte[] _PublicKeyToken;

		// Token: 0x04001C1D RID: 7197
		private CultureInfo _CultureInfo;

		// Token: 0x04001C1E RID: 7198
		private string _CodeBase;

		// Token: 0x04001C1F RID: 7199
		private Version _Version;

		// Token: 0x04001C20 RID: 7200
		private StrongNameKeyPair _StrongNameKeyPair;

		// Token: 0x04001C21 RID: 7201
		private SerializationInfo m_siInfo;

		// Token: 0x04001C22 RID: 7202
		private byte[] _HashForControl;

		// Token: 0x04001C23 RID: 7203
		private AssemblyHashAlgorithm _HashAlgorithm;

		// Token: 0x04001C24 RID: 7204
		private AssemblyHashAlgorithm _HashAlgorithmForControl;

		// Token: 0x04001C25 RID: 7205
		private AssemblyVersionCompatibility _VersionCompatibility;

		// Token: 0x04001C26 RID: 7206
		private AssemblyNameFlags _Flags;
	}
}
