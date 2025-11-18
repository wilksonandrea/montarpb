using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Plugin.Core.Colorful
{
	// Token: 0x0200011A RID: 282
	public sealed class TextPattern : Pattern<string>
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x00007F50 File Offset: 0x00006150
		public TextPattern(string string_0)
			: base(string_0)
		{
			this.regex_0 = new Regex(string_0);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00007F65 File Offset: 0x00006165
		public override IEnumerable<MatchLocation> GetMatchLocations(string input)
		{
			TextPattern.Class33 @class = new TextPattern.Class33(-2);
			@class.textPattern_0 = this;
			@class.string_1 = input;
			return @class;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00007F7C File Offset: 0x0000617C
		public override IEnumerable<string> GetMatches(string input)
		{
			TextPattern.Class34 @class = new TextPattern.Class34(-2);
			@class.textPattern_0 = this;
			@class.string_2 = input;
			return @class;
		}

		// Token: 0x0400073C RID: 1852
		private Regex regex_0;

		// Token: 0x0200011B RID: 283
		[CompilerGenerated]
		private sealed class Class33 : IEnumerable<MatchLocation>, IEnumerable, IEnumerator<MatchLocation>, IDisposable, IEnumerator
		{
			// Token: 0x06000A0E RID: 2574 RVA: 0x00007F93 File Offset: 0x00006193
			[DebuggerHidden]
			public Class33(int int_2)
			{
				this.int_0 = int_2;
				this.int_1 = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000A0F RID: 2575 RVA: 0x000229AC File Offset: 0x00020BAC
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.int_0;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.method_0();
					}
				}
				this.ienumerator_0 = null;
				this.int_0 = -2;
			}

			// Token: 0x06000A10 RID: 2576 RVA: 0x000229F4 File Offset: 0x00020BF4
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.int_0;
					TextPattern textPattern = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.int_0 = -3;
					}
					else
					{
						this.int_0 = -1;
						MatchCollection matchCollection = textPattern.regex_0.Matches(input);
						if (matchCollection.Count == 0)
						{
							return false;
						}
						this.ienumerator_0 = matchCollection.GetEnumerator();
						this.int_0 = -3;
					}
					if (!this.ienumerator_0.MoveNext())
					{
						this.method_0();
						this.ienumerator_0 = null;
						flag = false;
					}
					else
					{
						Match match = (Match)this.ienumerator_0.Current;
						int index = match.Index;
						int num2 = index + match.Length;
						MatchLocation matchLocation = new MatchLocation(index, num2);
						this.matchLocation_0 = matchLocation;
						this.int_0 = 1;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000A11 RID: 2577 RVA: 0x00022AD8 File Offset: 0x00020CD8
			private void method_0()
			{
				this.int_0 = -1;
				IDisposable disposable = this.ienumerator_0 as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x17000269 RID: 617
			// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00007FAD File Offset: 0x000061AD
			MatchLocation IEnumerator<MatchLocation>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.matchLocation_0;
				}
			}

			// Token: 0x06000A13 RID: 2579 RVA: 0x00007907 File Offset: 0x00005B07
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700026A RID: 618
			// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00007FAD File Offset: 0x000061AD
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.matchLocation_0;
				}
			}

			// Token: 0x06000A15 RID: 2581 RVA: 0x00022B04 File Offset: 0x00020D04
			[DebuggerHidden]
			IEnumerator<MatchLocation> IEnumerable<MatchLocation>.GetEnumerator()
			{
				TextPattern.Class33 @class;
				if (this.int_0 == -2 && this.int_1 == Environment.CurrentManagedThreadId)
				{
					this.int_0 = 0;
					@class = this;
				}
				else
				{
					@class = new TextPattern.Class33(0);
					@class.textPattern_0 = this;
				}
				@class.string_0 = input;
				return @class;
			}

			// Token: 0x06000A16 RID: 2582 RVA: 0x00007FB5 File Offset: 0x000061B5
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<Plugin.Core.Colorful.MatchLocation>.GetEnumerator();
			}

			// Token: 0x0400073D RID: 1853
			private int int_0;

			// Token: 0x0400073E RID: 1854
			private MatchLocation matchLocation_0;

			// Token: 0x0400073F RID: 1855
			private int int_1;

			// Token: 0x04000740 RID: 1856
			public TextPattern textPattern_0;

			// Token: 0x04000741 RID: 1857
			private string string_0;

			// Token: 0x04000742 RID: 1858
			public string string_1;

			// Token: 0x04000743 RID: 1859
			private IEnumerator ienumerator_0;
		}

		// Token: 0x0200011C RID: 284
		[CompilerGenerated]
		private sealed class Class34 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x06000A17 RID: 2583 RVA: 0x00007FBD File Offset: 0x000061BD
			[DebuggerHidden]
			public Class34(int int_2)
			{
				this.int_0 = int_2;
				this.int_1 = Environment.CurrentManagedThreadId;
			}

			// Token: 0x06000A18 RID: 2584 RVA: 0x00022B54 File Offset: 0x00020D54
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.int_0;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.method_0();
					}
				}
				this.ienumerator_0 = null;
				this.int_0 = -2;
			}

			// Token: 0x06000A19 RID: 2585 RVA: 0x00022B9C File Offset: 0x00020D9C
			bool IEnumerator.MoveNext()
			{
				bool flag;
				try
				{
					int num = this.int_0;
					TextPattern textPattern = this;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.int_0 = -3;
					}
					else
					{
						this.int_0 = -1;
						MatchCollection matchCollection = textPattern.regex_0.Matches(input);
						if (matchCollection.Count == 0)
						{
							return false;
						}
						this.ienumerator_0 = matchCollection.GetEnumerator();
						this.int_0 = -3;
					}
					if (!this.ienumerator_0.MoveNext())
					{
						this.method_0();
						this.ienumerator_0 = null;
						flag = false;
					}
					else
					{
						Match match = (Match)this.ienumerator_0.Current;
						this.string_0 = match.Value;
						this.int_0 = 1;
						flag = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return flag;
			}

			// Token: 0x06000A1A RID: 2586 RVA: 0x00022C68 File Offset: 0x00020E68
			private void method_0()
			{
				this.int_0 = -1;
				IDisposable disposable = this.ienumerator_0 as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}

			// Token: 0x1700026B RID: 619
			// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00007FD7 File Offset: 0x000061D7
			string IEnumerator<string>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.string_0;
				}
			}

			// Token: 0x06000A1C RID: 2588 RVA: 0x00007907 File Offset: 0x00005B07
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x1700026C RID: 620
			// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00007FD7 File Offset: 0x000061D7
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.string_0;
				}
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x00022C94 File Offset: 0x00020E94
			[DebuggerHidden]
			IEnumerator<string> IEnumerable<string>.GetEnumerator()
			{
				TextPattern.Class34 @class;
				if (this.int_0 == -2 && this.int_1 == Environment.CurrentManagedThreadId)
				{
					this.int_0 = 0;
					@class = this;
				}
				else
				{
					@class = new TextPattern.Class34(0);
					@class.textPattern_0 = this;
				}
				@class.string_1 = input;
				return @class;
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00007FDF File Offset: 0x000061DF
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<System.String>.GetEnumerator();
			}

			// Token: 0x04000744 RID: 1860
			private int int_0;

			// Token: 0x04000745 RID: 1861
			private string string_0;

			// Token: 0x04000746 RID: 1862
			private int int_1;

			// Token: 0x04000747 RID: 1863
			public TextPattern textPattern_0;

			// Token: 0x04000748 RID: 1864
			private string string_1;

			// Token: 0x04000749 RID: 1865
			public string string_2;

			// Token: 0x0400074A RID: 1866
			private IEnumerator ienumerator_0;
		}
	}
}
