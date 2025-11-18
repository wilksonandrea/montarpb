using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000394 RID: 916
	internal class ResourceFallbackManager : IEnumerable<CultureInfo>, IEnumerable
	{
		// Token: 0x06002D15 RID: 11541 RVA: 0x000AA0F9 File Offset: 0x000A82F9
		internal ResourceFallbackManager(CultureInfo startingCulture, CultureInfo neutralResourcesCulture, bool useParents)
		{
			if (startingCulture != null)
			{
				this.m_startingCulture = startingCulture;
			}
			else
			{
				this.m_startingCulture = CultureInfo.CurrentUICulture;
			}
			this.m_neutralResourcesCulture = neutralResourcesCulture;
			this.m_useParents = useParents;
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000AA126 File Offset: 0x000A8326
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002D17 RID: 11543 RVA: 0x000AA12E File Offset: 0x000A832E
		public IEnumerator<CultureInfo> GetEnumerator()
		{
			bool reachedNeutralResourcesCulture = false;
			CultureInfo currentCulture = this.m_startingCulture;
			while (this.m_neutralResourcesCulture == null || !(currentCulture.Name == this.m_neutralResourcesCulture.Name))
			{
				yield return currentCulture;
				currentCulture = currentCulture.Parent;
				if (!this.m_useParents || currentCulture.HasInvariantCultureName)
				{
					IL_CE:
					if (!this.m_useParents || this.m_startingCulture.HasInvariantCultureName)
					{
						yield break;
					}
					if (reachedNeutralResourcesCulture)
					{
						yield break;
					}
					yield return CultureInfo.InvariantCulture;
					yield break;
				}
			}
			yield return CultureInfo.InvariantCulture;
			reachedNeutralResourcesCulture = true;
			goto IL_CE;
		}

		// Token: 0x04001231 RID: 4657
		private CultureInfo m_startingCulture;

		// Token: 0x04001232 RID: 4658
		private CultureInfo m_neutralResourcesCulture;

		// Token: 0x04001233 RID: 4659
		private bool m_useParents;

		// Token: 0x02000B66 RID: 2918
		[CompilerGenerated]
		private sealed class <GetEnumerator>d__5 : IEnumerator<CultureInfo>, IDisposable, IEnumerator
		{
			// Token: 0x06006C08 RID: 27656 RVA: 0x00175BA0 File Offset: 0x00173DA0
			[DebuggerHidden]
			public <GetEnumerator>d__5(int <>1__state)
			{
				this.<>1__state = <>1__state;
			}

			// Token: 0x06006C09 RID: 27657 RVA: 0x00175BAF File Offset: 0x00173DAF
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x06006C0A RID: 27658 RVA: 0x00175BB4 File Offset: 0x00173DB4
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				ResourceFallbackManager resourceFallbackManager = this;
				switch (num)
				{
				case 0:
					this.<>1__state = -1;
					reachedNeutralResourcesCulture = false;
					currentCulture = resourceFallbackManager.m_startingCulture;
					break;
				case 1:
					this.<>1__state = -1;
					reachedNeutralResourcesCulture = true;
					goto IL_CE;
				case 2:
					this.<>1__state = -1;
					currentCulture = currentCulture.Parent;
					if (!resourceFallbackManager.m_useParents || currentCulture.HasInvariantCultureName)
					{
						goto IL_CE;
					}
					break;
				case 3:
					this.<>1__state = -1;
					return false;
				default:
					return false;
				}
				if (resourceFallbackManager.m_neutralResourcesCulture != null && currentCulture.Name == resourceFallbackManager.m_neutralResourcesCulture.Name)
				{
					this.<>2__current = CultureInfo.InvariantCulture;
					this.<>1__state = 1;
					return true;
				}
				this.<>2__current = currentCulture;
				this.<>1__state = 2;
				return true;
				IL_CE:
				if (!resourceFallbackManager.m_useParents || resourceFallbackManager.m_startingCulture.HasInvariantCultureName)
				{
					return false;
				}
				if (reachedNeutralResourcesCulture)
				{
					return false;
				}
				this.<>2__current = CultureInfo.InvariantCulture;
				this.<>1__state = 3;
				return true;
			}

			// Token: 0x1700123B RID: 4667
			// (get) Token: 0x06006C0B RID: 27659 RVA: 0x00175CCC File Offset: 0x00173ECC
			CultureInfo IEnumerator<CultureInfo>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x06006C0C RID: 27660 RVA: 0x00175CD4 File Offset: 0x00173ED4
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700123C RID: 4668
			// (get) Token: 0x06006C0D RID: 27661 RVA: 0x00175CDB File Offset: 0x00173EDB
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x0400344B RID: 13387
			private int <>1__state;

			// Token: 0x0400344C RID: 13388
			private CultureInfo <>2__current;

			// Token: 0x0400344D RID: 13389
			public ResourceFallbackManager <>4__this;

			// Token: 0x0400344E RID: 13390
			private bool <reachedNeutralResourcesCulture>5__2;

			// Token: 0x0400344F RID: 13391
			private CultureInfo <currentCulture>5__3;
		}
	}
}
