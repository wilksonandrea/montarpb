using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Security.Policy
{
	// Token: 0x0200036E RID: 878
	[ComVisible(true)]
	[Obsolete("This type is obsolete and will be removed in a future release of the .NET Framework. See http://go.microsoft.com/fwlink/?LinkID=155570 for more information.")]
	[Serializable]
	public sealed class UnionCodeGroup : CodeGroup, IUnionSemanticCodeGroup
	{
		// Token: 0x06002B7C RID: 11132 RVA: 0x000A210E File Offset: 0x000A030E
		internal UnionCodeGroup()
		{
		}

		// Token: 0x06002B7D RID: 11133 RVA: 0x000A2116 File Offset: 0x000A0316
		internal UnionCodeGroup(IMembershipCondition membershipCondition, PermissionSet permSet)
			: base(membershipCondition, permSet)
		{
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x000A2120 File Offset: 0x000A0320
		public UnionCodeGroup(IMembershipCondition membershipCondition, PolicyStatement policy)
			: base(membershipCondition, policy)
		{
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x000A212C File Offset: 0x000A032C
		[SecuritySafeCritical]
		public override PolicyStatement Resolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			object obj = null;
			if (PolicyManager.CheckMembershipCondition(base.MembershipCondition, evidence, out obj))
			{
				PolicyStatement policyStatement = base.PolicyStatement;
				IDelayEvaluatedEvidence delayEvaluatedEvidence = obj as IDelayEvaluatedEvidence;
				bool flag = delayEvaluatedEvidence != null && !delayEvaluatedEvidence.IsVerified;
				if (flag)
				{
					policyStatement.AddDependentEvidence(delayEvaluatedEvidence);
				}
				bool flag2 = false;
				IEnumerator enumerator = base.Children.GetEnumerator();
				while (enumerator.MoveNext() && !flag2)
				{
					PolicyStatement policyStatement2 = PolicyManager.ResolveCodeGroup(enumerator.Current as CodeGroup, evidence);
					if (policyStatement2 != null)
					{
						policyStatement.InplaceUnion(policyStatement2);
						if ((policyStatement2.Attributes & PolicyStatementAttribute.Exclusive) == PolicyStatementAttribute.Exclusive)
						{
							flag2 = true;
						}
					}
				}
				return policyStatement;
			}
			return null;
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x000A21D2 File Offset: 0x000A03D2
		PolicyStatement IUnionSemanticCodeGroup.InternalResolve(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				return base.PolicyStatement;
			}
			return null;
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x000A21F8 File Offset: 0x000A03F8
		public override CodeGroup ResolveMatchingCodeGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			if (base.MembershipCondition.Check(evidence))
			{
				CodeGroup codeGroup = this.Copy();
				codeGroup.Children = new ArrayList();
				foreach (object obj in base.Children)
				{
					CodeGroup codeGroup2 = ((CodeGroup)obj).ResolveMatchingCodeGroups(evidence);
					if (codeGroup2 != null)
					{
						codeGroup.AddChild(codeGroup2);
					}
				}
				return codeGroup;
			}
			return null;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000A2268 File Offset: 0x000A0468
		public override CodeGroup Copy()
		{
			UnionCodeGroup unionCodeGroup = new UnionCodeGroup();
			unionCodeGroup.MembershipCondition = base.MembershipCondition;
			unionCodeGroup.PolicyStatement = base.PolicyStatement;
			unionCodeGroup.Name = base.Name;
			unionCodeGroup.Description = base.Description;
			foreach (object obj in base.Children)
			{
				unionCodeGroup.AddChild((CodeGroup)obj);
			}
			return unionCodeGroup;
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002B83 RID: 11139 RVA: 0x000A22D3 File Offset: 0x000A04D3
		public override string MergeLogic
		{
			get
			{
				return Environment.GetResourceString("MergeLogic_Union");
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x000A22DF File Offset: 0x000A04DF
		internal override string GetTypeName()
		{
			return "System.Security.Policy.UnionCodeGroup";
		}
	}
}
