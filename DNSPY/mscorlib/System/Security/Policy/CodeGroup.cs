using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200034B RID: 843
	[ComVisible(true)]
	[Serializable]
	public abstract class CodeGroup
	{
		// Token: 0x060029D8 RID: 10712 RVA: 0x0009A669 File Offset: 0x00098869
		internal CodeGroup()
		{
		}

		// Token: 0x060029D9 RID: 10713 RVA: 0x0009A674 File Offset: 0x00098874
		internal CodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
		{
			this.m_membershipCondition = membershipCondition;
			this.m_policy = new PolicyStatement();
			this.m_policy.SetPermissionSetNoCopy(permSet);
			this.m_children = ArrayList.Synchronized(new ArrayList());
			this.m_element = null;
			this.m_parentLevel = null;
		}

		// Token: 0x060029DA RID: 10714 RVA: 0x0009A6C4 File Offset: 0x000988C4
		protected CodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
		{
			if (membershipCondition == null)
			{
				throw new ArgumentNullException("membershipCondition");
			}
			if (policy == null)
			{
				this.m_policy = null;
			}
			else
			{
				this.m_policy = policy.Copy();
			}
			this.m_membershipCondition = membershipCondition.Copy();
			this.m_children = ArrayList.Synchronized(new ArrayList());
			this.m_element = null;
			this.m_parentLevel = null;
		}

		// Token: 0x060029DB RID: 10715 RVA: 0x0009A728 File Offset: 0x00098928
		[SecuritySafeCritical]
		public void AddChild(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				this.m_children.Add(group.Copy());
			}
		}

		// Token: 0x060029DC RID: 10716 RVA: 0x0009A78C File Offset: 0x0009898C
		[SecurityCritical]
		internal void AddChildInternal(CodeGroup group)
		{
			if (group == null)
			{
				throw new ArgumentNullException("group");
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				this.m_children.Add(group);
			}
		}

		// Token: 0x060029DD RID: 10717 RVA: 0x0009A7EC File Offset: 0x000989EC
		[SecuritySafeCritical]
		public void RemoveChild(CodeGroup group)
		{
			if (group == null)
			{
				return;
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			lock (this)
			{
				int num = this.m_children.IndexOf(group);
				if (num != -1)
				{
					this.m_children.RemoveAt(num);
				}
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060029DE RID: 10718 RVA: 0x0009A850 File Offset: 0x00098A50
		// (set) Token: 0x060029DF RID: 10719 RVA: 0x0009A8D8 File Offset: 0x00098AD8
		public IList Children
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_children == null)
				{
					this.ParseChildren();
				}
				IList list2;
				lock (this)
				{
					IList list = new ArrayList(this.m_children.Count);
					foreach (object obj in this.m_children)
					{
						list.Add(((CodeGroup)obj).Copy());
					}
					list2 = list;
				}
				return list2;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Children");
				}
				ArrayList arrayList = ArrayList.Synchronized(new ArrayList(value.Count));
				foreach (object obj in value)
				{
					CodeGroup codeGroup = obj as CodeGroup;
					if (codeGroup == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_CodeGroupChildrenMustBeCodeGroups"));
					}
					arrayList.Add(codeGroup.Copy());
				}
				this.m_children = arrayList;
			}
		}

		// Token: 0x060029E0 RID: 10720 RVA: 0x0009A948 File Offset: 0x00098B48
		[SecurityCritical]
		internal IList GetChildrenInternal()
		{
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			return this.m_children;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060029E1 RID: 10721 RVA: 0x0009A95E File Offset: 0x00098B5E
		// (set) Token: 0x060029E2 RID: 10722 RVA: 0x0009A981 File Offset: 0x00098B81
		public IMembershipCondition MembershipCondition
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_membershipCondition == null && this.m_element != null)
				{
					this.ParseMembershipCondition();
				}
				return this.m_membershipCondition.Copy();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("MembershipCondition");
				}
				this.m_membershipCondition = value.Copy();
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060029E3 RID: 10723 RVA: 0x0009A99D File Offset: 0x00098B9D
		// (set) Token: 0x060029E4 RID: 10724 RVA: 0x0009A9CA File Offset: 0x00098BCA
		public PolicyStatement PolicyStatement
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy != null)
				{
					return this.m_policy.Copy();
				}
				return null;
			}
			set
			{
				if (value != null)
				{
					this.m_policy = value.Copy();
					return;
				}
				this.m_policy = null;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060029E5 RID: 10725 RVA: 0x0009A9E3 File Offset: 0x00098BE3
		// (set) Token: 0x060029E6 RID: 10726 RVA: 0x0009A9EB File Offset: 0x00098BEB
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060029E7 RID: 10727 RVA: 0x0009A9F4 File Offset: 0x00098BF4
		// (set) Token: 0x060029E8 RID: 10728 RVA: 0x0009A9FC File Offset: 0x00098BFC
		public string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x060029E9 RID: 10729
		public abstract PolicyStatement Resolve(Evidence evidence);

		// Token: 0x060029EA RID: 10730
		public abstract CodeGroup ResolveMatchingCodeGroups(Evidence evidence);

		// Token: 0x060029EB RID: 10731
		public abstract CodeGroup Copy();

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x0009AA08 File Offset: 0x00098C08
		public virtual string PermissionSetName
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy == null)
				{
					return null;
				}
				NamedPermissionSet namedPermissionSet = this.m_policy.GetPermissionSetNoCopy() as NamedPermissionSet;
				if (namedPermissionSet != null)
				{
					return namedPermissionSet.Name;
				}
				return null;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060029ED RID: 10733 RVA: 0x0009AA51 File Offset: 0x00098C51
		public virtual string AttributeString
		{
			get
			{
				if (this.m_policy == null && this.m_element != null)
				{
					this.ParsePolicy();
				}
				if (this.m_policy != null)
				{
					return this.m_policy.AttributeString;
				}
				return null;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060029EE RID: 10734
		public abstract string MergeLogic { get; }

		// Token: 0x060029EF RID: 10735 RVA: 0x0009AA7E File Offset: 0x00098C7E
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x060029F0 RID: 10736 RVA: 0x0009AA87 File Offset: 0x00098C87
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x060029F1 RID: 10737 RVA: 0x0009AA91 File Offset: 0x00098C91
		[SecuritySafeCritical]
		public SecurityElement ToXml(PolicyLevel level)
		{
			return this.ToXml(level, this.GetTypeName());
		}

		// Token: 0x060029F2 RID: 10738 RVA: 0x0009AAA0 File Offset: 0x00098CA0
		internal virtual string GetTypeName()
		{
			return base.GetType().FullName;
		}

		// Token: 0x060029F3 RID: 10739 RVA: 0x0009AAB0 File Offset: 0x00098CB0
		[SecurityCritical]
		internal SecurityElement ToXml(PolicyLevel level, string policyClassName)
		{
			if (this.m_membershipCondition == null && this.m_element != null)
			{
				this.ParseMembershipCondition();
			}
			if (this.m_children == null)
			{
				this.ParseChildren();
			}
			if (this.m_policy == null && this.m_element != null)
			{
				this.ParsePolicy();
			}
			SecurityElement securityElement = new SecurityElement("CodeGroup");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), policyClassName);
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(this.m_membershipCondition.ToXml(level));
			if (this.m_policy != null)
			{
				PermissionSet permissionSetNoCopy = this.m_policy.GetPermissionSetNoCopy();
				NamedPermissionSet namedPermissionSet = permissionSetNoCopy as NamedPermissionSet;
				if (namedPermissionSet != null && level != null && level.GetNamedPermissionSetInternal(namedPermissionSet.Name) != null)
				{
					securityElement.AddAttribute("PermissionSetName", namedPermissionSet.Name);
				}
				else if (!permissionSetNoCopy.IsEmpty())
				{
					securityElement.AddChild(permissionSetNoCopy.ToXml());
				}
				if (this.m_policy.Attributes != PolicyStatementAttribute.Nothing)
				{
					securityElement.AddAttribute("Attributes", XMLUtil.BitFieldEnumToString(typeof(PolicyStatementAttribute), this.m_policy.Attributes));
				}
			}
			if (this.m_children.Count > 0)
			{
				lock (this)
				{
					foreach (object obj in this.m_children)
					{
						securityElement.AddChild(((CodeGroup)obj).ToXml(level));
					}
				}
			}
			if (this.m_name != null)
			{
				securityElement.AddAttribute("Name", SecurityElement.Escape(this.m_name));
			}
			if (this.m_description != null)
			{
				securityElement.AddAttribute("Description", SecurityElement.Escape(this.m_description));
			}
			this.CreateXml(securityElement, level);
			return securityElement;
		}

		// Token: 0x060029F4 RID: 10740 RVA: 0x0009AC70 File Offset: 0x00098E70
		protected virtual void CreateXml(SecurityElement element, PolicyLevel level)
		{
		}

		// Token: 0x060029F5 RID: 10741 RVA: 0x0009AC74 File Offset: 0x00098E74
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			lock (this)
			{
				this.m_element = e;
				this.m_parentLevel = level;
				this.m_children = null;
				this.m_membershipCondition = null;
				this.m_policy = null;
				this.m_name = e.Attribute("Name");
				this.m_description = e.Attribute("Description");
				this.ParseXml(e, level);
			}
		}

		// Token: 0x060029F6 RID: 10742 RVA: 0x0009AD04 File Offset: 0x00098F04
		protected virtual void ParseXml(SecurityElement e, PolicyLevel level)
		{
		}

		// Token: 0x060029F7 RID: 10743 RVA: 0x0009AD08 File Offset: 0x00098F08
		[SecurityCritical]
		private bool ParseMembershipCondition(bool safeLoad)
		{
			bool flag2;
			lock (this)
			{
				IMembershipCondition membershipCondition = null;
				SecurityElement securityElement = this.m_element.SearchForChildByTag("IMembershipCondition");
				if (securityElement == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_InvalidXMLElement"), "IMembershipCondition", base.GetType().FullName));
				}
				try
				{
					membershipCondition = XMLUtil.CreateMembershipCondition(securityElement);
					if (membershipCondition == null)
					{
						return false;
					}
				}
				catch (Exception ex)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"), ex);
				}
				membershipCondition.FromXml(securityElement, this.m_parentLevel);
				this.m_membershipCondition = membershipCondition;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060029F8 RID: 10744 RVA: 0x0009ADCC File Offset: 0x00098FCC
		[SecurityCritical]
		private void ParseMembershipCondition()
		{
			this.ParseMembershipCondition(false);
		}

		// Token: 0x060029F9 RID: 10745 RVA: 0x0009ADD8 File Offset: 0x00098FD8
		[SecurityCritical]
		internal void ParseChildren()
		{
			lock (this)
			{
				ArrayList arrayList = ArrayList.Synchronized(new ArrayList());
				if (this.m_element != null && this.m_element.InternalChildren != null)
				{
					this.m_element.Children = (ArrayList)this.m_element.InternalChildren.Clone();
					ArrayList arrayList2 = ArrayList.Synchronized(new ArrayList());
					Evidence evidence = new Evidence();
					int num = this.m_element.InternalChildren.Count;
					int i = 0;
					while (i < num)
					{
						SecurityElement securityElement = (SecurityElement)this.m_element.Children[i];
						if (securityElement.Tag.Equals("CodeGroup"))
						{
							CodeGroup codeGroup = XMLUtil.CreateCodeGroup(securityElement);
							if (codeGroup != null)
							{
								codeGroup.FromXml(securityElement, this.m_parentLevel);
								if (this.ParseMembershipCondition(true))
								{
									codeGroup.Resolve(evidence);
									codeGroup.MembershipCondition.Check(evidence);
									arrayList.Add(codeGroup);
									i++;
								}
								else
								{
									this.m_element.InternalChildren.RemoveAt(i);
									num = this.m_element.InternalChildren.Count;
									arrayList2.Add(new CodeGroupPositionMarker(i, arrayList.Count, securityElement));
								}
							}
							else
							{
								this.m_element.InternalChildren.RemoveAt(i);
								num = this.m_element.InternalChildren.Count;
								arrayList2.Add(new CodeGroupPositionMarker(i, arrayList.Count, securityElement));
							}
						}
						else
						{
							i++;
						}
					}
					foreach (object obj in arrayList2)
					{
						CodeGroupPositionMarker codeGroupPositionMarker = (CodeGroupPositionMarker)obj;
						CodeGroup codeGroup2 = XMLUtil.CreateCodeGroup(codeGroupPositionMarker.element);
						if (codeGroup2 == null)
						{
							throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Argument_FailedCodeGroup"), codeGroupPositionMarker.element.Attribute("class")));
						}
						codeGroup2.FromXml(codeGroupPositionMarker.element, this.m_parentLevel);
						codeGroup2.Resolve(evidence);
						codeGroup2.MembershipCondition.Check(evidence);
						arrayList.Insert(codeGroupPositionMarker.groupIndex, codeGroup2);
						this.m_element.InternalChildren.Insert(codeGroupPositionMarker.elementIndex, codeGroupPositionMarker.element);
					}
				}
				this.m_children = arrayList;
			}
		}

		// Token: 0x060029FA RID: 10746 RVA: 0x0009B058 File Offset: 0x00099258
		private void ParsePolicy()
		{
			for (;;)
			{
				PolicyStatement policyStatement = new PolicyStatement();
				bool flag = false;
				SecurityElement securityElement = new SecurityElement("PolicyStatement");
				securityElement.AddAttribute("version", "1");
				SecurityElement element = this.m_element;
				lock (this)
				{
					if (this.m_element != null)
					{
						string text = this.m_element.Attribute("PermissionSetName");
						if (text != null)
						{
							securityElement.AddAttribute("PermissionSetName", text);
							flag = true;
						}
						else
						{
							SecurityElement securityElement2 = this.m_element.SearchForChildByTag("PermissionSet");
							if (securityElement2 != null)
							{
								securityElement.AddChild(securityElement2);
								flag = true;
							}
							else
							{
								securityElement.AddChild(new PermissionSet(false).ToXml());
								flag = true;
							}
						}
						string text2 = this.m_element.Attribute("Attributes");
						if (text2 != null)
						{
							securityElement.AddAttribute("Attributes", text2);
							flag = true;
						}
					}
				}
				if (flag)
				{
					policyStatement.FromXml(securityElement, this.m_parentLevel);
				}
				else
				{
					policyStatement.PermissionSet = null;
				}
				lock (this)
				{
					if (element == this.m_element && this.m_policy == null)
					{
						this.m_policy = policyStatement;
					}
					else if (this.m_policy == null)
					{
						continue;
					}
				}
				break;
			}
			if (this.m_policy != null && this.m_children != null)
			{
				IMembershipCondition membershipCondition = this.m_membershipCondition;
			}
		}

		// Token: 0x060029FB RID: 10747 RVA: 0x0009B1C8 File Offset: 0x000993C8
		[SecuritySafeCritical]
		public override bool Equals(object o)
		{
			CodeGroup codeGroup = o as CodeGroup;
			if (codeGroup != null && base.GetType().Equals(codeGroup.GetType()) && object.Equals(this.m_name, codeGroup.m_name) && object.Equals(this.m_description, codeGroup.m_description))
			{
				if (this.m_membershipCondition == null && this.m_element != null)
				{
					this.ParseMembershipCondition();
				}
				if (codeGroup.m_membershipCondition == null && codeGroup.m_element != null)
				{
					codeGroup.ParseMembershipCondition();
				}
				if (object.Equals(this.m_membershipCondition, codeGroup.m_membershipCondition))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060029FC RID: 10748 RVA: 0x0009B25C File Offset: 0x0009945C
		[SecuritySafeCritical]
		public bool Equals(CodeGroup cg, bool compareChildren)
		{
			if (!this.Equals(cg))
			{
				return false;
			}
			if (compareChildren)
			{
				if (this.m_children == null)
				{
					this.ParseChildren();
				}
				if (cg.m_children == null)
				{
					cg.ParseChildren();
				}
				ArrayList arrayList = new ArrayList(this.m_children);
				ArrayList arrayList2 = new ArrayList(cg.m_children);
				if (arrayList.Count != arrayList2.Count)
				{
					return false;
				}
				for (int i = 0; i < arrayList.Count; i++)
				{
					if (!((CodeGroup)arrayList[i]).Equals((CodeGroup)arrayList2[i], true))
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060029FD RID: 10749 RVA: 0x0009B2F0 File Offset: 0x000994F0
		[SecuritySafeCritical]
		public override int GetHashCode()
		{
			if (this.m_membershipCondition == null && this.m_element != null)
			{
				this.ParseMembershipCondition();
			}
			if (this.m_name != null || this.m_membershipCondition != null)
			{
				return ((this.m_name == null) ? 0 : this.m_name.GetHashCode()) + ((this.m_membershipCondition == null) ? 0 : this.m_membershipCondition.GetHashCode());
			}
			return base.GetType().GetHashCode();
		}

		// Token: 0x04001128 RID: 4392
		private IMembershipCondition m_membershipCondition;

		// Token: 0x04001129 RID: 4393
		private IList m_children;

		// Token: 0x0400112A RID: 4394
		private PolicyStatement m_policy;

		// Token: 0x0400112B RID: 4395
		private SecurityElement m_element;

		// Token: 0x0400112C RID: 4396
		private PolicyLevel m_parentLevel;

		// Token: 0x0400112D RID: 4397
		private string m_name;

		// Token: 0x0400112E RID: 4398
		private string m_description;
	}
}
