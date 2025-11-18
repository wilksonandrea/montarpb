using System;

namespace System.Reflection
{
	// Token: 0x020005B0 RID: 1456
	[Serializable]
	internal class __Filters
	{
		// Token: 0x0600436E RID: 17262 RVA: 0x000FA474 File Offset: 0x000F8674
		public virtual bool FilterTypeName(Type cls, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
			}
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				return cls.Name.StartsWith(text, StringComparison.Ordinal);
			}
			return cls.Name.Equals(text);
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x000FA4E8 File Offset: 0x000F86E8
		public virtual bool FilterTypeNameIgnoreCase(Type cls, object filterCriteria)
		{
			if (filterCriteria == null || !(filterCriteria is string))
			{
				throw new InvalidFilterCriteriaException(Environment.GetResourceString("RFLCT.FltCritString"));
			}
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				text = text.Substring(0, text.Length - 1);
				string name = cls.Name;
				return name.Length >= text.Length && string.Compare(name, 0, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return string.Compare(text, cls.Name, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x000FA57D File Offset: 0x000F877D
		public __Filters()
		{
		}
	}
}
