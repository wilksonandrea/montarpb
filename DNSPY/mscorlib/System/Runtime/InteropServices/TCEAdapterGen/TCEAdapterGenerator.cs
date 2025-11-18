using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices.TCEAdapterGen
{
	// Token: 0x020009C7 RID: 2503
	internal class TCEAdapterGenerator
	{
		// Token: 0x060063BE RID: 25534 RVA: 0x00154400 File Offset: 0x00152600
		public void Process(ModuleBuilder ModBldr, ArrayList EventItfList)
		{
			this.m_Module = ModBldr;
			int count = EventItfList.Count;
			for (int i = 0; i < count; i++)
			{
				EventItfInfo eventItfInfo = (EventItfInfo)EventItfList[i];
				Type eventItfType = eventItfInfo.GetEventItfType();
				Type srcItfType = eventItfInfo.GetSrcItfType();
				string eventProviderName = eventItfInfo.GetEventProviderName();
				Type type = new EventSinkHelperWriter(this.m_Module, srcItfType, eventItfType).Perform();
				new EventProviderWriter(this.m_Module, eventProviderName, eventItfType, srcItfType, type).Perform();
			}
		}

		// Token: 0x060063BF RID: 25535 RVA: 0x00154478 File Offset: 0x00152678
		internal static void SetClassInterfaceTypeToNone(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_NoClassItfCABuilder == null)
			{
				Type[] array = new Type[] { typeof(ClassInterfaceType) };
				ConstructorInfo constructor = typeof(ClassInterfaceAttribute).GetConstructor(array);
				TCEAdapterGenerator.s_NoClassItfCABuilder = new CustomAttributeBuilder(constructor, new object[] { ClassInterfaceType.None });
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_NoClassItfCABuilder);
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x001544E0 File Offset: 0x001526E0
		internal static TypeBuilder DefineUniqueType(string strInitFullName, TypeAttributes attrs, Type BaseType, Type[] aInterfaceTypes, ModuleBuilder mb)
		{
			string text = strInitFullName;
			int num = 2;
			while (mb.GetType(text) != null)
			{
				text = strInitFullName + "_" + num.ToString();
				num++;
			}
			return mb.DefineType(text, attrs, BaseType, aInterfaceTypes);
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x00154528 File Offset: 0x00152728
		internal static void SetHiddenAttribute(TypeBuilder tb)
		{
			if (TCEAdapterGenerator.s_HiddenCABuilder == null)
			{
				Type[] array = new Type[] { typeof(TypeLibTypeFlags) };
				ConstructorInfo constructor = typeof(TypeLibTypeAttribute).GetConstructor(array);
				TCEAdapterGenerator.s_HiddenCABuilder = new CustomAttributeBuilder(constructor, new object[] { TypeLibTypeFlags.FHidden });
			}
			tb.SetCustomAttribute(TCEAdapterGenerator.s_HiddenCABuilder);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x00154590 File Offset: 0x00152790
		internal static MethodInfo[] GetNonPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList(methods);
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo methodInfo in accessors)
				{
					for (int k = 0; k < arrayList.Count; k++)
					{
						if ((MethodInfo)arrayList[k] == methodInfo)
						{
							arrayList.RemoveAt(k);
						}
					}
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x00154640 File Offset: 0x00152840
		internal static MethodInfo[] GetPropertyMethods(Type type)
		{
			MethodInfo[] methods = type.GetMethods();
			ArrayList arrayList = new ArrayList();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				MethodInfo[] accessors = propertyInfo.GetAccessors();
				foreach (MethodInfo methodInfo in accessors)
				{
					arrayList.Add(methodInfo);
				}
			}
			MethodInfo[] array3 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array3);
			return array3;
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x001546C2 File Offset: 0x001528C2
		public TCEAdapterGenerator()
		{
		}

		// Token: 0x04002CDE RID: 11486
		private ModuleBuilder m_Module;

		// Token: 0x04002CDF RID: 11487
		private Hashtable m_SrcItfToSrcItfInfoMap = new Hashtable();

		// Token: 0x04002CE0 RID: 11488
		private static volatile CustomAttributeBuilder s_NoClassItfCABuilder;

		// Token: 0x04002CE1 RID: 11489
		private static volatile CustomAttributeBuilder s_HiddenCABuilder;
	}
}
