using System;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Security;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x0200079C RID: 1948
	internal sealed class ValueFixup
	{
		// Token: 0x0600545D RID: 21597 RVA: 0x001290D1 File Offset: 0x001272D1
		internal ValueFixup(Array arrayObj, int[] indexMap)
		{
			this.valueFixupEnum = ValueFixupEnum.Array;
			this.arrayObj = arrayObj;
			this.indexMap = indexMap;
		}

		// Token: 0x0600545E RID: 21598 RVA: 0x001290EE File Offset: 0x001272EE
		internal ValueFixup(object memberObject, string memberName, ReadObjectInfo objectInfo)
		{
			this.valueFixupEnum = ValueFixupEnum.Member;
			this.memberObject = memberObject;
			this.memberName = memberName;
			this.objectInfo = objectInfo;
		}

		// Token: 0x0600545F RID: 21599 RVA: 0x00129114 File Offset: 0x00127314
		[SecurityCritical]
		internal void Fixup(ParseRecord record, ParseRecord parent)
		{
			object prnewObj = record.PRnewObj;
			switch (this.valueFixupEnum)
			{
			case ValueFixupEnum.Array:
				this.arrayObj.SetValue(prnewObj, this.indexMap);
				return;
			case ValueFixupEnum.Header:
			{
				Type typeFromHandle = typeof(Header);
				if (ValueFixup.valueInfo == null)
				{
					MemberInfo[] member = typeFromHandle.GetMember("Value");
					if (member.Length != 1)
					{
						throw new SerializationException(Environment.GetResourceString("Serialization_HeaderReflection", new object[] { member.Length }));
					}
					ValueFixup.valueInfo = member[0];
				}
				FormatterServices.SerializationSetValue(ValueFixup.valueInfo, this.header, prnewObj);
				return;
			}
			case ValueFixupEnum.Member:
			{
				if (this.objectInfo.isSi)
				{
					this.objectInfo.objectManager.RecordDelayedFixup(parent.PRobjectId, this.memberName, record.PRobjectId);
					return;
				}
				MemberInfo memberInfo = this.objectInfo.GetMemberInfo(this.memberName);
				if (memberInfo != null)
				{
					this.objectInfo.objectManager.RecordFixup(parent.PRobjectId, memberInfo, record.PRobjectId);
				}
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x04002660 RID: 9824
		internal ValueFixupEnum valueFixupEnum;

		// Token: 0x04002661 RID: 9825
		internal Array arrayObj;

		// Token: 0x04002662 RID: 9826
		internal int[] indexMap;

		// Token: 0x04002663 RID: 9827
		internal object header;

		// Token: 0x04002664 RID: 9828
		internal object memberObject;

		// Token: 0x04002665 RID: 9829
		internal static volatile MemberInfo valueInfo;

		// Token: 0x04002666 RID: 9830
		internal ReadObjectInfo objectInfo;

		// Token: 0x04002667 RID: 9831
		internal string memberName;
	}
}
