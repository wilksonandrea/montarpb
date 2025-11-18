using System;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	// Token: 0x0200063F RID: 1599
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_ILGenerator))]
	[ComVisible(true)]
	public class ILGenerator : _ILGenerator
	{
		// Token: 0x06004AA1 RID: 19105 RVA: 0x0010D798 File Offset: 0x0010B998
		internal static int[] EnlargeArray(int[] incoming)
		{
			int[] array = new int[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0010D7BC File Offset: 0x0010B9BC
		private static byte[] EnlargeArray(byte[] incoming)
		{
			byte[] array = new byte[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x0010D7E0 File Offset: 0x0010B9E0
		private static byte[] EnlargeArray(byte[] incoming, int requiredSize)
		{
			byte[] array = new byte[requiredSize];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AA4 RID: 19108 RVA: 0x0010D800 File Offset: 0x0010BA00
		private static __FixupData[] EnlargeArray(__FixupData[] incoming)
		{
			__FixupData[] array = new __FixupData[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x06004AA5 RID: 19109 RVA: 0x0010D824 File Offset: 0x0010BA24
		private static __ExceptionInfo[] EnlargeArray(__ExceptionInfo[] incoming)
		{
			__ExceptionInfo[] array = new __ExceptionInfo[incoming.Length * 2];
			Array.Copy(incoming, array, incoming.Length);
			return array;
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06004AA6 RID: 19110 RVA: 0x0010D847 File Offset: 0x0010BA47
		internal int CurrExcStackCount
		{
			get
			{
				return this.m_currExcStackCount;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06004AA7 RID: 19111 RVA: 0x0010D84F File Offset: 0x0010BA4F
		internal __ExceptionInfo[] CurrExcStack
		{
			get
			{
				return this.m_currExcStack;
			}
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x0010D857 File Offset: 0x0010BA57
		internal ILGenerator(MethodInfo methodBuilder)
			: this(methodBuilder, 64)
		{
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x0010D864 File Offset: 0x0010BA64
		internal ILGenerator(MethodInfo methodBuilder, int size)
		{
			if (size < 16)
			{
				this.m_ILStream = new byte[16];
			}
			else
			{
				this.m_ILStream = new byte[size];
			}
			this.m_length = 0;
			this.m_labelCount = 0;
			this.m_fixupCount = 0;
			this.m_labelList = null;
			this.m_fixupData = null;
			this.m_exceptions = null;
			this.m_exceptionCount = 0;
			this.m_currExcStack = null;
			this.m_currExcStackCount = 0;
			this.m_RelocFixupList = null;
			this.m_RelocFixupCount = 0;
			this.m_ScopeTree = new ScopeTree();
			this.m_LineNumberInfo = new LineNumberInfo();
			this.m_methodBuilder = methodBuilder;
			this.m_localCount = 0;
			MethodBuilder methodBuilder2 = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder2 == null)
			{
				this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(null);
				return;
			}
			this.m_localSignature = SignatureHelper.GetLocalVarSigHelper(methodBuilder2.GetTypeBuilder().Module);
		}

		// Token: 0x06004AAA RID: 19114 RVA: 0x0010D940 File Offset: 0x0010BB40
		internal virtual void RecordTokenFixup()
		{
			if (this.m_RelocFixupList == null)
			{
				this.m_RelocFixupList = new int[8];
			}
			else if (this.m_RelocFixupList.Length <= this.m_RelocFixupCount)
			{
				this.m_RelocFixupList = ILGenerator.EnlargeArray(this.m_RelocFixupList);
			}
			int[] relocFixupList = this.m_RelocFixupList;
			int relocFixupCount = this.m_RelocFixupCount;
			this.m_RelocFixupCount = relocFixupCount + 1;
			relocFixupList[relocFixupCount] = this.m_length;
		}

		// Token: 0x06004AAB RID: 19115 RVA: 0x0010D9A4 File Offset: 0x0010BBA4
		internal void InternalEmit(OpCode opcode)
		{
			int num;
			if (opcode.Size != 1)
			{
				byte[] ilstream = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream[num] = (byte)(opcode.Value >> 8);
			}
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)opcode.Value;
			this.UpdateStackSize(opcode, opcode.StackChange());
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x0010DA0C File Offset: 0x0010BC0C
		internal void UpdateStackSize(OpCode opcode, int stackchange)
		{
			this.m_maxMidStackCur += stackchange;
			if (this.m_maxMidStackCur > this.m_maxMidStack)
			{
				this.m_maxMidStack = this.m_maxMidStackCur;
			}
			else if (this.m_maxMidStackCur < 0)
			{
				this.m_maxMidStackCur = 0;
			}
			if (opcode.EndsUncondJmpBlk())
			{
				this.m_maxStackSize += this.m_maxMidStack;
				this.m_maxMidStack = 0;
				this.m_maxMidStackCur = 0;
			}
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x0010DA7D File Offset: 0x0010BC7D
		[SecurityCritical]
		private int GetMethodToken(MethodBase method, Type[] optionalParameterTypes, bool useMethodDef)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMethodTokenInternal(method, optionalParameterTypes, useMethodDef);
		}

		// Token: 0x06004AAE RID: 19118 RVA: 0x0010DA97 File Offset: 0x0010BC97
		[SecurityCritical]
		internal virtual SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			return this.GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, 0);
		}

		// Token: 0x06004AAF RID: 19119 RVA: 0x0010DAA5 File Offset: 0x0010BCA5
		[SecurityCritical]
		private SignatureHelper GetMemberRefSignature(CallingConventions call, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, int cGenericParameters)
		{
			return ((ModuleBuilder)this.m_methodBuilder.Module).GetMemberRefSignature(call, returnType, parameterTypes, optionalParameterTypes, cGenericParameters);
		}

		// Token: 0x06004AB0 RID: 19120 RVA: 0x0010DAC4 File Offset: 0x0010BCC4
		internal byte[] BakeByteArray()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
			}
			if (this.m_length == 0)
			{
				return null;
			}
			int length = this.m_length;
			byte[] array = new byte[length];
			Array.Copy(this.m_ILStream, array, length);
			for (int i = 0; i < this.m_fixupCount; i++)
			{
				int num = this.GetLabelPos(this.m_fixupData[i].m_fixupLabel) - (this.m_fixupData[i].m_fixupPos + this.m_fixupData[i].m_fixupInstSize);
				if (this.m_fixupData[i].m_fixupInstSize == 1)
				{
					if (num < -128 || num > 127)
					{
						throw new NotSupportedException(Environment.GetResourceString("NotSupported_IllegalOneByteBranch", new object[]
						{
							this.m_fixupData[i].m_fixupPos,
							num
						}));
					}
					if (num < 0)
					{
						array[this.m_fixupData[i].m_fixupPos] = (byte)(256 + num);
					}
					else
					{
						array[this.m_fixupData[i].m_fixupPos] = (byte)num;
					}
				}
				else
				{
					ILGenerator.PutInteger4InArray(num, this.m_fixupData[i].m_fixupPos, array);
				}
			}
			return array;
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x0010DC0C File Offset: 0x0010BE0C
		internal __ExceptionInfo[] GetExceptions()
		{
			if (this.m_currExcStackCount != 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_UnclosedExceptionBlock"));
			}
			if (this.m_exceptionCount == 0)
			{
				return null;
			}
			__ExceptionInfo[] array = new __ExceptionInfo[this.m_exceptionCount];
			Array.Copy(this.m_exceptions, array, this.m_exceptionCount);
			ILGenerator.SortExceptions(array);
			return array;
		}

		// Token: 0x06004AB2 RID: 19122 RVA: 0x0010DC60 File Offset: 0x0010BE60
		internal void EnsureCapacity(int size)
		{
			if (this.m_length + size >= this.m_ILStream.Length)
			{
				if (this.m_length + size >= 2 * this.m_ILStream.Length)
				{
					this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream, this.m_length + size);
					return;
				}
				this.m_ILStream = ILGenerator.EnlargeArray(this.m_ILStream);
			}
		}

		// Token: 0x06004AB3 RID: 19123 RVA: 0x0010DCBE File Offset: 0x0010BEBE
		internal void PutInteger4(int value)
		{
			this.m_length = ILGenerator.PutInteger4InArray(value, this.m_length, this.m_ILStream);
		}

		// Token: 0x06004AB4 RID: 19124 RVA: 0x0010DCD8 File Offset: 0x0010BED8
		private static int PutInteger4InArray(int value, int startPos, byte[] array)
		{
			array[startPos++] = (byte)value;
			array[startPos++] = (byte)(value >> 8);
			array[startPos++] = (byte)(value >> 16);
			array[startPos++] = (byte)(value >> 24);
			return startPos;
		}

		// Token: 0x06004AB5 RID: 19125 RVA: 0x0010DD0C File Offset: 0x0010BF0C
		private int GetLabelPos(Label lbl)
		{
			int labelValue = lbl.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelCount)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadLabel"));
			}
			if (this.m_labelList[labelValue] < 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_BadLabelContent"));
			}
			return this.m_labelList[labelValue];
		}

		// Token: 0x06004AB6 RID: 19126 RVA: 0x0010DD64 File Offset: 0x0010BF64
		private void AddFixup(Label lbl, int pos, int instSize)
		{
			if (this.m_fixupData == null)
			{
				this.m_fixupData = new __FixupData[8];
			}
			else if (this.m_fixupData.Length <= this.m_fixupCount)
			{
				this.m_fixupData = ILGenerator.EnlargeArray(this.m_fixupData);
			}
			this.m_fixupData[this.m_fixupCount].m_fixupPos = pos;
			this.m_fixupData[this.m_fixupCount].m_fixupLabel = lbl;
			this.m_fixupData[this.m_fixupCount].m_fixupInstSize = instSize;
			this.m_fixupCount++;
		}

		// Token: 0x06004AB7 RID: 19127 RVA: 0x0010DDFB File Offset: 0x0010BFFB
		internal int GetMaxStackSize()
		{
			return this.m_maxStackSize;
		}

		// Token: 0x06004AB8 RID: 19128 RVA: 0x0010DE04 File Offset: 0x0010C004
		private static void SortExceptions(__ExceptionInfo[] exceptions)
		{
			int num = exceptions.Length;
			for (int i = 0; i < num; i++)
			{
				int num2 = i;
				for (int j = i + 1; j < num; j++)
				{
					if (exceptions[num2].IsInner(exceptions[j]))
					{
						num2 = j;
					}
				}
				__ExceptionInfo _ExceptionInfo = exceptions[i];
				exceptions[i] = exceptions[num2];
				exceptions[num2] = _ExceptionInfo;
			}
		}

		// Token: 0x06004AB9 RID: 19129 RVA: 0x0010DE54 File Offset: 0x0010C054
		internal int[] GetTokenFixups()
		{
			if (this.m_RelocFixupCount == 0)
			{
				return null;
			}
			int[] array = new int[this.m_RelocFixupCount];
			Array.Copy(this.m_RelocFixupList, array, this.m_RelocFixupCount);
			return array;
		}

		// Token: 0x06004ABA RID: 19130 RVA: 0x0010DE8A File Offset: 0x0010C08A
		public virtual void Emit(OpCode opcode)
		{
			this.EnsureCapacity(3);
			this.InternalEmit(opcode);
		}

		// Token: 0x06004ABB RID: 19131 RVA: 0x0010DE9C File Offset: 0x0010C09C
		public virtual void Emit(OpCode opcode, byte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int length = this.m_length;
			this.m_length = length + 1;
			ilstream[length] = arg;
		}

		// Token: 0x06004ABC RID: 19132 RVA: 0x0010DED0 File Offset: 0x0010C0D0
		[CLSCompliant(false)]
		public void Emit(OpCode opcode, sbyte arg)
		{
			this.EnsureCapacity(4);
			this.InternalEmit(opcode);
			int num;
			if (arg < 0)
			{
				byte[] ilstream = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream[num] = (byte)(256 + (int)arg);
				return;
			}
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)arg;
		}

		// Token: 0x06004ABD RID: 19133 RVA: 0x0010DF2C File Offset: 0x0010C12C
		public virtual void Emit(OpCode opcode, short arg)
		{
			this.EnsureCapacity(5);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int num = this.m_length;
			this.m_length = num + 1;
			ilstream[num] = (byte)arg;
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)(arg >> 8);
		}

		// Token: 0x06004ABE RID: 19134 RVA: 0x0010DF7D File Offset: 0x0010C17D
		public virtual void Emit(OpCode opcode, int arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(arg);
		}

		// Token: 0x06004ABF RID: 19135 RVA: 0x0010DF94 File Offset: 0x0010C194
		[SecuritySafeCritical]
		public virtual void Emit(OpCode opcode, MethodInfo meth)
		{
			if (meth == null)
			{
				throw new ArgumentNullException("meth");
			}
			if (opcode.Equals(OpCodes.Call) || opcode.Equals(OpCodes.Callvirt) || opcode.Equals(OpCodes.Newobj))
			{
				this.EmitCall(opcode, meth, null);
				return;
			}
			int num = 0;
			bool flag = opcode.Equals(OpCodes.Ldtoken) || opcode.Equals(OpCodes.Ldftn) || opcode.Equals(OpCodes.Ldvirtftn);
			int methodToken = this.GetMethodToken(meth, null, flag);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x06004AC0 RID: 19136 RVA: 0x0010E044 File Offset: 0x0010C244
		[SecuritySafeCritical]
		public virtual void EmitCalli(OpCode opcode, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes)
		{
			int num = 0;
			if (optionalParameterTypes != null && (callingConvention & CallingConventions.VarArgs) == (CallingConventions)0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_NotAVarArgCallingConvention"));
			}
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			SignatureHelper memberRefSignature = this.GetMemberRefSignature(callingConvention, returnType, parameterTypes, optionalParameterTypes);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			if ((callingConvention & CallingConventions.HasThis) == CallingConventions.HasThis)
			{
				num--;
			}
			num--;
			this.UpdateStackSize(OpCodes.Calli, num);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(memberRefSignature).Token);
		}

		// Token: 0x06004AC1 RID: 19137 RVA: 0x0010E100 File Offset: 0x0010C300
		public virtual void EmitCalli(OpCode opcode, CallingConvention unmanagedCallConv, Type returnType, Type[] parameterTypes)
		{
			int num = 0;
			int num2 = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			if (parameterTypes != null)
			{
				num2 = parameterTypes.Length;
			}
			SignatureHelper methodSigHelper = SignatureHelper.GetMethodSigHelper(moduleBuilder, unmanagedCallConv, returnType);
			if (parameterTypes != null)
			{
				for (int i = 0; i < num2; i++)
				{
					methodSigHelper.AddArgument(parameterTypes[i]);
				}
			}
			if (returnType != typeof(void))
			{
				num++;
			}
			if (parameterTypes != null)
			{
				num -= num2;
			}
			num--;
			this.UpdateStackSize(OpCodes.Calli, num);
			this.EnsureCapacity(7);
			this.Emit(OpCodes.Calli);
			this.RecordTokenFixup();
			this.PutInteger4(moduleBuilder.GetSignatureToken(methodSigHelper).Token);
		}

		// Token: 0x06004AC2 RID: 19138 RVA: 0x0010E1B0 File Offset: 0x0010C3B0
		[SecuritySafeCritical]
		public virtual void EmitCall(OpCode opcode, MethodInfo methodInfo, Type[] optionalParameterTypes)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (!opcode.Equals(OpCodes.Call) && !opcode.Equals(OpCodes.Callvirt) && !opcode.Equals(OpCodes.Newobj))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotMethodCallOpcode"), "opcode");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(methodInfo, optionalParameterTypes, false);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (methodInfo.ReturnType != typeof(void))
			{
				num++;
			}
			Type[] parameterTypes = methodInfo.GetParameterTypes();
			if (parameterTypes != null)
			{
				num -= parameterTypes.Length;
			}
			if (!(methodInfo is SymbolMethod) && !methodInfo.IsStatic && !opcode.Equals(OpCodes.Newobj))
			{
				num--;
			}
			if (optionalParameterTypes != null)
			{
				num -= optionalParameterTypes.Length;
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x06004AC3 RID: 19139 RVA: 0x0010E298 File Offset: 0x0010C498
		public virtual void Emit(OpCode opcode, SignatureHelper signature)
		{
			if (signature == null)
			{
				throw new ArgumentNullException("signature");
			}
			int num = 0;
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetSignatureToken(signature).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				num -= signature.ArgumentCount;
				num--;
				this.UpdateStackSize(opcode, num);
			}
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x06004AC4 RID: 19140 RVA: 0x0010E314 File Offset: 0x0010C514
		[SecuritySafeCritical]
		[ComVisible(true)]
		public virtual void Emit(OpCode opcode, ConstructorInfo con)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			int num = 0;
			int methodToken = this.GetMethodToken(con, null, true);
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.StackBehaviourPush == StackBehaviour.Varpush)
			{
				num++;
			}
			if (opcode.StackBehaviourPop == StackBehaviour.Varpop)
			{
				Type[] parameterTypes = con.GetParameterTypes();
				if (parameterTypes != null)
				{
					num -= parameterTypes.Length;
				}
			}
			this.UpdateStackSize(opcode, num);
			this.RecordTokenFixup();
			this.PutInteger4(methodToken);
		}

		// Token: 0x06004AC5 RID: 19141 RVA: 0x0010E390 File Offset: 0x0010C590
		[SecuritySafeCritical]
		public virtual void Emit(OpCode opcode, Type cls)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int num;
			if (opcode == OpCodes.Ldtoken && cls != null && cls.IsGenericTypeDefinition)
			{
				num = moduleBuilder.GetTypeToken(cls).Token;
			}
			else
			{
				num = moduleBuilder.GetTypeTokenInternal(cls).Token;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(num);
		}

		// Token: 0x06004AC6 RID: 19142 RVA: 0x0010E40C File Offset: 0x0010C60C
		public virtual void Emit(OpCode opcode, long arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			byte[] ilstream = this.m_ILStream;
			int num = this.m_length;
			this.m_length = num + 1;
			ilstream[num] = (byte)arg;
			byte[] ilstream2 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream2[num] = (byte)(arg >> 8);
			byte[] ilstream3 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream3[num] = (byte)(arg >> 16);
			byte[] ilstream4 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream4[num] = (byte)(arg >> 24);
			byte[] ilstream5 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream5[num] = (byte)(arg >> 32);
			byte[] ilstream6 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream6[num] = (byte)(arg >> 40);
			byte[] ilstream7 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream7[num] = (byte)(arg >> 48);
			byte[] ilstream8 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream8[num] = (byte)(arg >> 56);
		}

		// Token: 0x06004AC7 RID: 19143 RVA: 0x0010E50C File Offset: 0x0010C70C
		[SecuritySafeCritical]
		public unsafe virtual void Emit(OpCode opcode, float arg)
		{
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			uint num = *(uint*)(&arg);
			byte[] ilstream = this.m_ILStream;
			int num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream[num2] = (byte)num;
			byte[] ilstream2 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream2[num2] = (byte)(num >> 8);
			byte[] ilstream3 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream3[num2] = (byte)(num >> 16);
			byte[] ilstream4 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream4[num2] = (byte)(num >> 24);
		}

		// Token: 0x06004AC8 RID: 19144 RVA: 0x0010E59C File Offset: 0x0010C79C
		[SecuritySafeCritical]
		public unsafe virtual void Emit(OpCode opcode, double arg)
		{
			this.EnsureCapacity(11);
			this.InternalEmit(opcode);
			ulong num = (ulong)(*(long*)(&arg));
			byte[] ilstream = this.m_ILStream;
			int num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream[num2] = (byte)num;
			byte[] ilstream2 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream2[num2] = (byte)(num >> 8);
			byte[] ilstream3 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream3[num2] = (byte)(num >> 16);
			byte[] ilstream4 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream4[num2] = (byte)(num >> 24);
			byte[] ilstream5 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream5[num2] = (byte)(num >> 32);
			byte[] ilstream6 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream6[num2] = (byte)(num >> 40);
			byte[] ilstream7 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream7[num2] = (byte)(num >> 48);
			byte[] ilstream8 = this.m_ILStream;
			num2 = this.m_length;
			this.m_length = num2 + 1;
			ilstream8[num2] = (byte)(num >> 56);
		}

		// Token: 0x06004AC9 RID: 19145 RVA: 0x0010E6A4 File Offset: 0x0010C8A4
		public virtual void Emit(OpCode opcode, Label label)
		{
			int labelValue = label.GetLabelValue();
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (OpCodes.TakesSingleByteArgument(opcode))
			{
				this.AddFixup(label, this.m_length, 1);
				this.m_length++;
				return;
			}
			this.AddFixup(label, this.m_length, 4);
			this.m_length += 4;
		}

		// Token: 0x06004ACA RID: 19146 RVA: 0x0010E708 File Offset: 0x0010C908
		public virtual void Emit(OpCode opcode, Label[] labels)
		{
			if (labels == null)
			{
				throw new ArgumentNullException("labels");
			}
			int num = labels.Length;
			this.EnsureCapacity(num * 4 + 7);
			this.InternalEmit(opcode);
			this.PutInteger4(num);
			int i = num * 4;
			int num2 = 0;
			while (i > 0)
			{
				this.AddFixup(labels[num2], this.m_length, i);
				this.m_length += 4;
				i -= 4;
				num2++;
			}
		}

		// Token: 0x06004ACB RID: 19147 RVA: 0x0010E778 File Offset: 0x0010C978
		public virtual void Emit(OpCode opcode, FieldInfo field)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetFieldToken(field).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.RecordTokenFixup();
			this.PutInteger4(token);
		}

		// Token: 0x06004ACC RID: 19148 RVA: 0x0010E7C4 File Offset: 0x0010C9C4
		public virtual void Emit(OpCode opcode, string str)
		{
			ModuleBuilder moduleBuilder = (ModuleBuilder)this.m_methodBuilder.Module;
			int token = moduleBuilder.GetStringConstant(str).Token;
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			this.PutInteger4(token);
		}

		// Token: 0x06004ACD RID: 19149 RVA: 0x0010E808 File Offset: 0x0010CA08
		public virtual void Emit(OpCode opcode, LocalBuilder local)
		{
			if (local == null)
			{
				throw new ArgumentNullException("local");
			}
			int localIndex = local.GetLocalIndex();
			if (local.GetMethodBuilder() != this.m_methodBuilder)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_UnmatchedMethodForLocal"), "local");
			}
			if (opcode.Equals(OpCodes.Ldloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Ldloc_0;
					break;
				case 1:
					opcode = OpCodes.Ldloc_1;
					break;
				case 2:
					opcode = OpCodes.Ldloc_2;
					break;
				case 3:
					opcode = OpCodes.Ldloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Ldloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Stloc))
			{
				switch (localIndex)
				{
				case 0:
					opcode = OpCodes.Stloc_0;
					break;
				case 1:
					opcode = OpCodes.Stloc_1;
					break;
				case 2:
					opcode = OpCodes.Stloc_2;
					break;
				case 3:
					opcode = OpCodes.Stloc_3;
					break;
				default:
					if (localIndex <= 255)
					{
						opcode = OpCodes.Stloc_S;
					}
					break;
				}
			}
			else if (opcode.Equals(OpCodes.Ldloca) && localIndex <= 255)
			{
				opcode = OpCodes.Ldloca_S;
			}
			this.EnsureCapacity(7);
			this.InternalEmit(opcode);
			if (opcode.OperandType == OperandType.InlineNone)
			{
				return;
			}
			int num;
			if (!OpCodes.TakesSingleByteArgument(opcode))
			{
				byte[] ilstream = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream[num] = (byte)localIndex;
				byte[] ilstream2 = this.m_ILStream;
				num = this.m_length;
				this.m_length = num + 1;
				ilstream2[num] = (byte)(localIndex >> 8);
				return;
			}
			if (localIndex > 255)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_BadInstructionOrIndexOutOfBound"));
			}
			byte[] ilstream3 = this.m_ILStream;
			num = this.m_length;
			this.m_length = num + 1;
			ilstream3[num] = (byte)localIndex;
		}

		// Token: 0x06004ACE RID: 19150 RVA: 0x0010E9C0 File Offset: 0x0010CBC0
		public virtual Label BeginExceptionBlock()
		{
			if (this.m_exceptions == null)
			{
				this.m_exceptions = new __ExceptionInfo[2];
			}
			if (this.m_currExcStack == null)
			{
				this.m_currExcStack = new __ExceptionInfo[2];
			}
			if (this.m_exceptionCount >= this.m_exceptions.Length)
			{
				this.m_exceptions = ILGenerator.EnlargeArray(this.m_exceptions);
			}
			if (this.m_currExcStackCount >= this.m_currExcStack.Length)
			{
				this.m_currExcStack = ILGenerator.EnlargeArray(this.m_currExcStack);
			}
			Label label = this.DefineLabel();
			__ExceptionInfo _ExceptionInfo = new __ExceptionInfo(this.m_length, label);
			__ExceptionInfo[] exceptions = this.m_exceptions;
			int num = this.m_exceptionCount;
			this.m_exceptionCount = num + 1;
			exceptions[num] = _ExceptionInfo;
			__ExceptionInfo[] currExcStack = this.m_currExcStack;
			num = this.m_currExcStackCount;
			this.m_currExcStackCount = num + 1;
			currExcStack[num] = _ExceptionInfo;
			return label;
		}

		// Token: 0x06004ACF RID: 19151 RVA: 0x0010EA80 File Offset: 0x0010CC80
		public virtual void EndExceptionBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			this.m_currExcStack[this.m_currExcStackCount - 1] = null;
			this.m_currExcStackCount--;
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int currentState = _ExceptionInfo.GetCurrentState();
			if (currentState == 1 || currentState == 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Argument_BadExceptionCodeGen"));
			}
			if (currentState == 2)
			{
				this.Emit(OpCodes.Leave, endLabel);
			}
			else if (currentState == 3 || currentState == 4)
			{
				this.Emit(OpCodes.Endfinally);
			}
			if (this.m_labelList[endLabel.GetLabelValue()] == -1)
			{
				this.MarkLabel(endLabel);
			}
			else
			{
				this.MarkLabel(_ExceptionInfo.GetFinallyEndLabel());
			}
			_ExceptionInfo.Done(this.m_length);
		}

		// Token: 0x06004AD0 RID: 19152 RVA: 0x0010EB50 File Offset: 0x0010CD50
		public virtual void BeginExceptFilterBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			_ExceptionInfo.MarkFilterAddr(this.m_length);
		}

		// Token: 0x06004AD1 RID: 19153 RVA: 0x0010EBA4 File Offset: 0x0010CDA4
		public virtual void BeginCatchBlock(Type exceptionType)
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			if (_ExceptionInfo.GetCurrentState() == 1)
			{
				if (exceptionType != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_ShouldNotSpecifyExceptionType"));
				}
				this.Emit(OpCodes.Endfilter);
			}
			else
			{
				if (exceptionType == null)
				{
					throw new ArgumentNullException("exceptionType");
				}
				Label endLabel = _ExceptionInfo.GetEndLabel();
				this.Emit(OpCodes.Leave, endLabel);
			}
			_ExceptionInfo.MarkCatchAddr(this.m_length, exceptionType);
		}

		// Token: 0x06004AD2 RID: 19154 RVA: 0x0010EC3C File Offset: 0x0010CE3C
		public virtual void BeginFaultBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			Label endLabel = _ExceptionInfo.GetEndLabel();
			this.Emit(OpCodes.Leave, endLabel);
			_ExceptionInfo.MarkFaultAddr(this.m_length);
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x0010EC90 File Offset: 0x0010CE90
		public virtual void BeginFinallyBlock()
		{
			if (this.m_currExcStackCount == 0)
			{
				throw new NotSupportedException(Environment.GetResourceString("Argument_NotInExceptionBlock"));
			}
			__ExceptionInfo _ExceptionInfo = this.m_currExcStack[this.m_currExcStackCount - 1];
			int currentState = _ExceptionInfo.GetCurrentState();
			Label endLabel = _ExceptionInfo.GetEndLabel();
			int num = 0;
			if (currentState != 0)
			{
				this.Emit(OpCodes.Leave, endLabel);
				num = this.m_length;
			}
			this.MarkLabel(endLabel);
			Label label = this.DefineLabel();
			_ExceptionInfo.SetFinallyEndLabel(label);
			this.Emit(OpCodes.Leave, label);
			if (num == 0)
			{
				num = this.m_length;
			}
			_ExceptionInfo.MarkFinallyAddr(this.m_length, num);
		}

		// Token: 0x06004AD4 RID: 19156 RVA: 0x0010ED28 File Offset: 0x0010CF28
		public virtual Label DefineLabel()
		{
			if (this.m_labelList == null)
			{
				this.m_labelList = new int[4];
			}
			if (this.m_labelCount >= this.m_labelList.Length)
			{
				this.m_labelList = ILGenerator.EnlargeArray(this.m_labelList);
			}
			this.m_labelList[this.m_labelCount] = -1;
			int labelCount = this.m_labelCount;
			this.m_labelCount = labelCount + 1;
			return new Label(labelCount);
		}

		// Token: 0x06004AD5 RID: 19157 RVA: 0x0010ED90 File Offset: 0x0010CF90
		public virtual void MarkLabel(Label loc)
		{
			int labelValue = loc.GetLabelValue();
			if (labelValue < 0 || labelValue >= this.m_labelList.Length)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidLabel"));
			}
			if (this.m_labelList[labelValue] != -1)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_RedefinedLabel"));
			}
			this.m_labelList[labelValue] = this.m_length;
		}

		// Token: 0x06004AD6 RID: 19158 RVA: 0x0010EDF0 File Offset: 0x0010CFF0
		public virtual void ThrowException(Type excType)
		{
			if (excType == null)
			{
				throw new ArgumentNullException("excType");
			}
			if (!excType.IsSubclassOf(typeof(Exception)) && excType != typeof(Exception))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_NotExceptionType"));
			}
			ConstructorInfo constructor = excType.GetConstructor(Type.EmptyTypes);
			if (constructor == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MissingDefaultConstructor"));
			}
			this.Emit(OpCodes.Newobj, constructor);
			this.Emit(OpCodes.Throw);
		}

		// Token: 0x06004AD7 RID: 19159 RVA: 0x0010EE84 File Offset: 0x0010D084
		public virtual void EmitWriteLine(string value)
		{
			this.Emit(OpCodes.Ldstr, value);
			Type[] array = new Type[] { typeof(string) };
			MethodInfo method = typeof(Console).GetMethod("WriteLine", array);
			this.Emit(OpCodes.Call, method);
		}

		// Token: 0x06004AD8 RID: 19160 RVA: 0x0010EED4 File Offset: 0x0010D0D4
		public virtual void EmitWriteLine(LocalBuilder localBuilder)
		{
			if (this.m_methodBuilder == null)
			{
				throw new ArgumentException(Environment.GetResourceString("InvalidOperation_BadILGeneratorUsage"));
			}
			MethodInfo method = typeof(Console).GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			this.Emit(OpCodes.Ldloc, localBuilder);
			Type[] array = new Type[1];
			object localType = localBuilder.LocalType;
			if (localType is TypeBuilder || localType is EnumBuilder)
			{
				throw new ArgumentException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
			}
			array[0] = (Type)localType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "localBuilder");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x0010EFA4 File Offset: 0x0010D1A4
		public virtual void EmitWriteLine(FieldInfo fld)
		{
			if (fld == null)
			{
				throw new ArgumentNullException("fld");
			}
			MethodInfo method = typeof(Console).GetMethod("get_Out");
			this.Emit(OpCodes.Call, method);
			if ((fld.Attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
			{
				this.Emit(OpCodes.Ldsfld, fld);
			}
			else
			{
				this.Emit(OpCodes.Ldarg, 0);
				this.Emit(OpCodes.Ldfld, fld);
			}
			Type[] array = new Type[1];
			object fieldType = fld.FieldType;
			if (fieldType is TypeBuilder || fieldType is EnumBuilder)
			{
				throw new NotSupportedException(Environment.GetResourceString("NotSupported_OutputStreamUsingTypeBuilder"));
			}
			array[0] = (Type)fieldType;
			MethodInfo method2 = typeof(TextWriter).GetMethod("WriteLine", array);
			if (method2 == null)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmitWriteLineType"), "fld");
			}
			this.Emit(OpCodes.Callvirt, method2);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x0010F08E File Offset: 0x0010D28E
		public virtual LocalBuilder DeclareLocal(Type localType)
		{
			return this.DeclareLocal(localType, false);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x0010F098 File Offset: 0x0010D298
		public virtual LocalBuilder DeclareLocal(Type localType, bool pinned)
		{
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			if (methodBuilder.IsTypeCreated())
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_TypeHasBeenCreated"));
			}
			if (localType == null)
			{
				throw new ArgumentNullException("localType");
			}
			if (methodBuilder.m_bIsBaked)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_MethodBaked"));
			}
			this.m_localSignature.AddArgument(localType, pinned);
			LocalBuilder localBuilder = new LocalBuilder(this.m_localCount, localType, methodBuilder, pinned);
			this.m_localCount++;
			return localBuilder;
		}

		// Token: 0x06004ADC RID: 19164 RVA: 0x0010F130 File Offset: 0x0010D330
		public virtual void UsingNamespace(string usingNamespace)
		{
			if (usingNamespace == null)
			{
				throw new ArgumentNullException("usingNamespace");
			}
			if (usingNamespace.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_EmptyName"), "usingNamespace");
			}
			MethodBuilder methodBuilder = this.m_methodBuilder as MethodBuilder;
			if (methodBuilder == null)
			{
				throw new NotSupportedException();
			}
			int currentActiveScopeIndex = methodBuilder.GetILGenerator().m_ScopeTree.GetCurrentActiveScopeIndex();
			if (currentActiveScopeIndex == -1)
			{
				methodBuilder.m_localSymInfo.AddUsingNamespace(usingNamespace);
				return;
			}
			this.m_ScopeTree.AddUsingNamespaceToCurrentScope(usingNamespace);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x0010F1B1 File Offset: 0x0010D3B1
		public virtual void MarkSequencePoint(ISymbolDocumentWriter document, int startLine, int startColumn, int endLine, int endColumn)
		{
			if (startLine == 0 || startLine < 0 || endLine == 0 || endLine < 0)
			{
				throw new ArgumentOutOfRangeException("startLine");
			}
			this.m_LineNumberInfo.AddLineNumberInfo(document, this.m_length, startLine, startColumn, endLine, endColumn);
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0010F1E6 File Offset: 0x0010D3E6
		public virtual void BeginScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Open, this.m_length);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x0010F1FA File Offset: 0x0010D3FA
		public virtual void EndScope()
		{
			this.m_ScopeTree.AddScopeInfo(ScopeAction.Close, this.m_length);
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06004AE0 RID: 19168 RVA: 0x0010F20E File Offset: 0x0010D40E
		public virtual int ILOffset
		{
			get
			{
				return this.m_length;
			}
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x0010F216 File Offset: 0x0010D416
		void _ILGenerator.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x0010F21D File Offset: 0x0010D41D
		void _ILGenerator.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AE3 RID: 19171 RVA: 0x0010F224 File Offset: 0x0010D424
		void _ILGenerator.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004AE4 RID: 19172 RVA: 0x0010F22B File Offset: 0x0010D42B
		void _ILGenerator.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001EC6 RID: 7878
		private const int defaultSize = 16;

		// Token: 0x04001EC7 RID: 7879
		private const int DefaultFixupArraySize = 8;

		// Token: 0x04001EC8 RID: 7880
		private const int DefaultLabelArraySize = 4;

		// Token: 0x04001EC9 RID: 7881
		private const int DefaultExceptionArraySize = 2;

		// Token: 0x04001ECA RID: 7882
		private int m_length;

		// Token: 0x04001ECB RID: 7883
		private byte[] m_ILStream;

		// Token: 0x04001ECC RID: 7884
		private int[] m_labelList;

		// Token: 0x04001ECD RID: 7885
		private int m_labelCount;

		// Token: 0x04001ECE RID: 7886
		private __FixupData[] m_fixupData;

		// Token: 0x04001ECF RID: 7887
		private int m_fixupCount;

		// Token: 0x04001ED0 RID: 7888
		private int[] m_RelocFixupList;

		// Token: 0x04001ED1 RID: 7889
		private int m_RelocFixupCount;

		// Token: 0x04001ED2 RID: 7890
		private int m_exceptionCount;

		// Token: 0x04001ED3 RID: 7891
		private int m_currExcStackCount;

		// Token: 0x04001ED4 RID: 7892
		private __ExceptionInfo[] m_exceptions;

		// Token: 0x04001ED5 RID: 7893
		private __ExceptionInfo[] m_currExcStack;

		// Token: 0x04001ED6 RID: 7894
		internal ScopeTree m_ScopeTree;

		// Token: 0x04001ED7 RID: 7895
		internal LineNumberInfo m_LineNumberInfo;

		// Token: 0x04001ED8 RID: 7896
		internal MethodInfo m_methodBuilder;

		// Token: 0x04001ED9 RID: 7897
		internal int m_localCount;

		// Token: 0x04001EDA RID: 7898
		internal SignatureHelper m_localSignature;

		// Token: 0x04001EDB RID: 7899
		private int m_maxStackSize;

		// Token: 0x04001EDC RID: 7900
		private int m_maxMidStack;

		// Token: 0x04001EDD RID: 7901
		private int m_maxMidStackCur;
	}
}
