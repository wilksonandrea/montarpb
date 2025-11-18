using System;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C4 RID: 2500
	internal class EventSinkHelperWriter
	{
		// Token: 0x060063B1 RID: 25521 RVA: 0x00153DF0 File Offset: 0x00151FF0
		public EventSinkHelperWriter(ModuleBuilder OutputModule, Type InputType, Type EventItfType)
		{
			this.m_InputType = InputType;
			this.m_OutputModule = OutputModule;
			this.m_EventItfType = EventItfType;
		}

		// Token: 0x060063B2 RID: 25522 RVA: 0x00153E10 File Offset: 0x00152010
		public Type Perform()
		{
			Type[] array = new Type[] { this.m_InputType };
			string text = null;
			string text2 = NameSpaceExtractor.ExtractNameSpace(this.m_EventItfType.FullName);
			if (text2 != "")
			{
				text = text2 + ".";
			}
			text = text + this.m_InputType.Name + EventSinkHelperWriter.GeneratedTypeNamePostfix;
			TypeBuilder typeBuilder = TCEAdapterGenerator.DefineUniqueType(text, TypeAttributes.Public | TypeAttributes.Sealed, null, array, this.m_OutputModule);
			TCEAdapterGenerator.SetHiddenAttribute(typeBuilder);
			TCEAdapterGenerator.SetClassInterfaceTypeToNone(typeBuilder);
			MethodInfo[] propertyMethods = TCEAdapterGenerator.GetPropertyMethods(this.m_InputType);
			foreach (MethodInfo methodInfo in propertyMethods)
			{
				this.DefineBlankMethod(typeBuilder, methodInfo);
			}
			MethodInfo[] nonPropertyMethods = TCEAdapterGenerator.GetNonPropertyMethods(this.m_InputType);
			FieldBuilder[] array3 = new FieldBuilder[nonPropertyMethods.Length];
			for (int j = 0; j < nonPropertyMethods.Length; j++)
			{
				if (this.m_InputType == nonPropertyMethods[j].DeclaringType)
				{
					MethodInfo method = this.m_EventItfType.GetMethod("add_" + nonPropertyMethods[j].Name);
					ParameterInfo[] parameters = method.GetParameters();
					Type parameterType = parameters[0].ParameterType;
					array3[j] = typeBuilder.DefineField("m_" + nonPropertyMethods[j].Name + "Delegate", parameterType, FieldAttributes.Public);
					this.DefineEventMethod(typeBuilder, nonPropertyMethods[j], parameterType, array3[j]);
				}
			}
			FieldBuilder fieldBuilder = typeBuilder.DefineField("m_dwCookie", typeof(int), FieldAttributes.Public);
			this.DefineConstructor(typeBuilder, fieldBuilder, array3);
			return typeBuilder.CreateType();
		}

		// Token: 0x060063B3 RID: 25523 RVA: 0x00153FA4 File Offset: 0x001521A4
		private void DefineBlankMethod(TypeBuilder OutputTypeBuilder, MethodInfo Method)
		{
			ParameterInfo[] parameters = Method.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod(Method.Name, Method.Attributes & ~MethodAttributes.Abstract, Method.CallingConvention, Method.ReturnType, array);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			this.AddReturn(Method.ReturnType, ilgenerator, methodBuilder);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x060063B4 RID: 25524 RVA: 0x00154028 File Offset: 0x00152228
		private void DefineEventMethod(TypeBuilder OutputTypeBuilder, MethodInfo Method, Type DelegateCls, FieldBuilder fbDelegate)
		{
			MethodInfo method = DelegateCls.GetMethod("Invoke");
			Type returnType = Method.ReturnType;
			ParameterInfo[] parameters = Method.GetParameters();
			Type[] array;
			if (parameters != null)
			{
				array = new Type[parameters.Length];
				for (int i = 0; i < parameters.Length; i++)
				{
					array[i] = parameters[i].ParameterType;
				}
			}
			else
			{
				array = null;
			}
			MethodAttributes methodAttributes = MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Virtual;
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod(Method.Name, methodAttributes, CallingConventions.Standard, returnType, array);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			Label label = ilgenerator.DefineLabel();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbDelegate);
			ilgenerator.Emit(OpCodes.Brfalse, label);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldfld, fbDelegate);
			ParameterInfo[] parameters2 = Method.GetParameters();
			for (int j = 0; j < parameters2.Length; j++)
			{
				ilgenerator.Emit(OpCodes.Ldarg, (short)(j + 1));
			}
			ilgenerator.Emit(OpCodes.Callvirt, method);
			ilgenerator.Emit(OpCodes.Ret);
			ilgenerator.MarkLabel(label);
			this.AddReturn(returnType, ilgenerator, methodBuilder);
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x060063B5 RID: 25525 RVA: 0x00154150 File Offset: 0x00152350
		private void AddReturn(Type ReturnType, ILGenerator il, MethodBuilder Meth)
		{
			if (!(ReturnType == typeof(void)))
			{
				if (ReturnType.IsPrimitive)
				{
					switch (Type.GetTypeCode(ReturnType))
					{
					case TypeCode.Boolean:
					case TypeCode.Char:
					case TypeCode.SByte:
					case TypeCode.Byte:
					case TypeCode.Int16:
					case TypeCode.UInt16:
					case TypeCode.Int32:
					case TypeCode.UInt32:
						il.Emit(OpCodes.Ldc_I4_0);
						return;
					case TypeCode.Int64:
					case TypeCode.UInt64:
						il.Emit(OpCodes.Ldc_I4_0);
						il.Emit(OpCodes.Conv_I8);
						return;
					case TypeCode.Single:
						il.Emit(OpCodes.Ldc_R4, 0);
						return;
					case TypeCode.Double:
						il.Emit(OpCodes.Ldc_R4, 0);
						il.Emit(OpCodes.Conv_R8);
						return;
					default:
						if (ReturnType == typeof(IntPtr))
						{
							il.Emit(OpCodes.Ldc_I4_0);
							return;
						}
						break;
					}
				}
				else
				{
					if (ReturnType.IsValueType)
					{
						Meth.InitLocals = true;
						LocalBuilder localBuilder = il.DeclareLocal(ReturnType);
						il.Emit(OpCodes.Ldloc_S, localBuilder);
						return;
					}
					il.Emit(OpCodes.Ldnull);
				}
			}
		}

		// Token: 0x060063B6 RID: 25526 RVA: 0x00154254 File Offset: 0x00152454
		private void DefineConstructor(TypeBuilder OutputTypeBuilder, FieldBuilder fbCookie, FieldBuilder[] afbDelegates)
		{
			ConstructorInfo constructor = typeof(object).GetConstructor(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, new Type[0], null);
			MethodBuilder methodBuilder = OutputTypeBuilder.DefineMethod(".ctor", MethodAttributes.Private | MethodAttributes.FamANDAssem | MethodAttributes.SpecialName, CallingConventions.Standard, null, null);
			ILGenerator ilgenerator = methodBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ldarg, 0);
			ilgenerator.Emit(OpCodes.Ldc_I4, 0);
			ilgenerator.Emit(OpCodes.Stfld, fbCookie);
			for (int i = 0; i < afbDelegates.Length; i++)
			{
				if (afbDelegates[i] != null)
				{
					ilgenerator.Emit(OpCodes.Ldarg, 0);
					ilgenerator.Emit(OpCodes.Ldnull);
					ilgenerator.Emit(OpCodes.Stfld, afbDelegates[i]);
				}
			}
			ilgenerator.Emit(OpCodes.Ret);
		}

		// Token: 0x060063B7 RID: 25527 RVA: 0x0015431B File Offset: 0x0015251B
		// Note: this type is marked as 'beforefieldinit'.
		static EventSinkHelperWriter()
		{
		}

		// Token: 0x04002CD4 RID: 11476
		public static readonly string GeneratedTypeNamePostfix = "_SinkHelper";

		// Token: 0x04002CD5 RID: 11477
		private Type m_InputType;

		// Token: 0x04002CD6 RID: 11478
		private Type m_EventItfType;

		// Token: 0x04002CD7 RID: 11479
		private ModuleBuilder m_OutputModule;
	}
}
