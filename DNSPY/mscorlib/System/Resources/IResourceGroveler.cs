using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System.Resources
{
	// Token: 0x0200038D RID: 909
	internal interface IResourceGroveler
	{
		// Token: 0x06002CF0 RID: 11504
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists, ref StackCrawlMark stackMark);

		// Token: 0x06002CF1 RID: 11505
		bool HasNeutralResources(CultureInfo culture, string defaultResName);
	}
}
