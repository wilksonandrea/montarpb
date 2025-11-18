using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerQuickstart
	{
		public long OwnerId
		{
			get;
			set;
		}

		public List<QuickstartModel> Quickjoins
		{
			get;
			set;
		}

		public PlayerQuickstart()
		{
			this.Quickjoins = new List<QuickstartModel>();
		}

		public QuickstartModel GetMapList(byte MapId)
		{
			QuickstartModel quickstartModel;
			lock (this.Quickjoins)
			{
				List<QuickstartModel>.Enumerator enumerator = this.Quickjoins.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						QuickstartModel current = enumerator.Current;
						if (current.MapId != MapId)
						{
							continue;
						}
						quickstartModel = current;
						return quickstartModel;
					}
					return null;
				}
				finally
				{
					((IDisposable)enumerator).Dispose();
				}
			}
			return quickstartModel;
		}
	}
}