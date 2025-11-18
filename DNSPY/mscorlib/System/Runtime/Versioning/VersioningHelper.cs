using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.Runtime.Versioning
{
	// Token: 0x02000725 RID: 1829
	public static class VersioningHelper
	{
		// Token: 0x06005163 RID: 20835
		[SecuritySafeCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetRuntimeId();

		// Token: 0x06005164 RID: 20836 RVA: 0x0011EB77 File Offset: 0x0011CD77
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to)
		{
			return VersioningHelper.MakeVersionSafeName(name, from, to, null);
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0011EB84 File Offset: 0x0011CD84
		[SecuritySafeCritical]
		public static string MakeVersionSafeName(string name, ResourceScope from, ResourceScope to, Type type)
		{
			ResourceScope resourceScope = from & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			ResourceScope resourceScope2 = to & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library);
			if (resourceScope > resourceScope2)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_ResourceScopeWrongDirection", new object[] { resourceScope, resourceScope2 }), "from");
			}
			SxSRequirements requirements = VersioningHelper.GetRequirements(to, from);
			if ((requirements & (SxSRequirements.AssemblyName | SxSRequirements.TypeName)) != SxSRequirements.None && type == null)
			{
				throw new ArgumentNullException("type", Environment.GetResourceString("ArgumentNull_TypeRequiredByResourceScope"));
			}
			StringBuilder stringBuilder = new StringBuilder(name);
			char c = '_';
			if ((requirements & SxSRequirements.ProcessID) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append('p');
				stringBuilder.Append(Win32Native.GetCurrentProcessId());
			}
			if ((requirements & SxSRequirements.CLRInstanceID) != SxSRequirements.None)
			{
				string clrinstanceString = VersioningHelper.GetCLRInstanceString();
				stringBuilder.Append(c);
				stringBuilder.Append('r');
				stringBuilder.Append(clrinstanceString);
			}
			if ((requirements & SxSRequirements.AppDomainID) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append("ad");
				stringBuilder.Append(AppDomain.CurrentDomain.Id);
			}
			if ((requirements & SxSRequirements.TypeName) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append(type.Name);
			}
			if ((requirements & SxSRequirements.AssemblyName) != SxSRequirements.None)
			{
				stringBuilder.Append(c);
				stringBuilder.Append(type.Assembly.FullName);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0011ECBC File Offset: 0x0011CEBC
		private static string GetCLRInstanceString()
		{
			return VersioningHelper.GetRuntimeId().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x0011ECDC File Offset: 0x0011CEDC
		private static SxSRequirements GetRequirements(ResourceScope consumeAsScope, ResourceScope calleeScope)
		{
			SxSRequirements sxSRequirements = SxSRequirements.None;
			switch (calleeScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
			{
			case ResourceScope.Machine:
				switch (consumeAsScope & (ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library))
				{
				case ResourceScope.Machine:
					goto IL_9F;
				case ResourceScope.Process:
					sxSRequirements |= SxSRequirements.ProcessID;
					goto IL_9F;
				case ResourceScope.AppDomain:
					sxSRequirements |= SxSRequirements.AppDomainID | SxSRequirements.ProcessID | SxSRequirements.CLRInstanceID;
					goto IL_9F;
				}
				throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeTypeBits", new object[] { consumeAsScope }), "consumeAsScope");
			case ResourceScope.Process:
				if ((consumeAsScope & ResourceScope.AppDomain) != ResourceScope.None)
				{
					sxSRequirements |= SxSRequirements.AppDomainID | SxSRequirements.CLRInstanceID;
					goto IL_9F;
				}
				goto IL_9F;
			case ResourceScope.AppDomain:
				goto IL_9F;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeTypeBits", new object[] { calleeScope }), "calleeScope");
			IL_9F:
			ResourceScope resourceScope = calleeScope & (ResourceScope.Private | ResourceScope.Assembly);
			if (resourceScope != ResourceScope.None)
			{
				if (resourceScope != ResourceScope.Private)
				{
					if (resourceScope != ResourceScope.Assembly)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeVisibilityBits", new object[] { calleeScope }), "calleeScope");
					}
					if ((consumeAsScope & ResourceScope.Private) != ResourceScope.None)
					{
						sxSRequirements |= SxSRequirements.TypeName;
					}
				}
			}
			else
			{
				ResourceScope resourceScope2 = consumeAsScope & (ResourceScope.Private | ResourceScope.Assembly);
				if (resourceScope2 != ResourceScope.None)
				{
					if (resourceScope2 != ResourceScope.Private)
					{
						if (resourceScope2 != ResourceScope.Assembly)
						{
							throw new ArgumentException(Environment.GetResourceString("Argument_BadResourceScopeVisibilityBits", new object[] { consumeAsScope }), "consumeAsScope");
						}
						sxSRequirements |= SxSRequirements.AssemblyName;
					}
					else
					{
						sxSRequirements |= SxSRequirements.AssemblyName | SxSRequirements.TypeName;
					}
				}
			}
			return sxSRequirements;
		}

		// Token: 0x04002429 RID: 9257
		private const ResourceScope ResTypeMask = ResourceScope.Machine | ResourceScope.Process | ResourceScope.AppDomain | ResourceScope.Library;

		// Token: 0x0400242A RID: 9258
		private const ResourceScope VisibilityMask = ResourceScope.Private | ResourceScope.Assembly;
	}
}
