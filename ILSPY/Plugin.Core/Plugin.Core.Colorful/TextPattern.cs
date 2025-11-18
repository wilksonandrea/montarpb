using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Plugin.Core.Colorful;

public sealed class TextPattern : Pattern<string>
{
	[CompilerGenerated]
	private sealed class Class33 : IEnumerable<MatchLocation>, IEnumerable, IEnumerator<MatchLocation>, IDisposable, IEnumerator
	{
		private int int_0;

		private MatchLocation matchLocation_0;

		private int int_1;

		public TextPattern textPattern_0;

		private string string_0;

		public string string_1;

		private IEnumerator ienumerator_0;

		MatchLocation IEnumerator<MatchLocation>.Current
		{
			[DebuggerHidden]
			get
			{
				return matchLocation_0;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return matchLocation_0;
			}
		}

		[DebuggerHidden]
		public Class33(int int_2)
		{
			int_0 = int_2;
			int_1 = Environment.CurrentManagedThreadId;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = int_0;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					method_0();
				}
			}
			ienumerator_0 = null;
			int_0 = -2;
		}

		private bool MoveNext()
		{
			try
			{
				int num = int_0;
				TextPattern textPattern = textPattern_0;
				switch (num)
				{
				default:
					return false;
				case 1:
					int_0 = -3;
					break;
				case 0:
				{
					int_0 = -1;
					MatchCollection matchCollection = textPattern.regex_0.Matches(string_0);
					if (matchCollection.Count == 0)
					{
						return false;
					}
					ienumerator_0 = matchCollection.GetEnumerator();
					int_0 = -3;
					break;
				}
				}
				if (!ienumerator_0.MoveNext())
				{
					method_0();
					ienumerator_0 = null;
					return false;
				}
				Match match = (Match)ienumerator_0.Current;
				int ındex = match.Index;
				int int_ = ındex + match.Length;
				MatchLocation matchLocation = new MatchLocation(ındex, int_);
				matchLocation_0 = matchLocation;
				int_0 = 1;
				return true;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void method_0()
		{
			int_0 = -1;
			if (ienumerator_0 is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		IEnumerator<MatchLocation> IEnumerable<MatchLocation>.GetEnumerator()
		{
			Class33 @class;
			if (int_0 == -2 && int_1 == Environment.CurrentManagedThreadId)
			{
				int_0 = 0;
				@class = this;
			}
			else
			{
				@class = new Class33(0)
				{
					textPattern_0 = textPattern_0
				};
			}
			@class.string_0 = string_1;
			return @class;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<MatchLocation>)this).GetEnumerator();
		}
	}

	[CompilerGenerated]
	private sealed class Class34 : IEnumerable<string>, IEnumerable, IEnumerator<string>, IDisposable, IEnumerator
	{
		private int int_0;

		private string string_0;

		private int int_1;

		public TextPattern textPattern_0;

		private string string_1;

		public string string_2;

		private IEnumerator ienumerator_0;

		string IEnumerator<string>.Current
		{
			[DebuggerHidden]
			get
			{
				return string_0;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return string_0;
			}
		}

		[DebuggerHidden]
		public Class34(int int_2)
		{
			int_0 = int_2;
			int_1 = Environment.CurrentManagedThreadId;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = int_0;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					method_0();
				}
			}
			ienumerator_0 = null;
			int_0 = -2;
		}

		private bool MoveNext()
		{
			try
			{
				int num = int_0;
				TextPattern textPattern = textPattern_0;
				switch (num)
				{
				default:
					return false;
				case 1:
					int_0 = -3;
					break;
				case 0:
				{
					int_0 = -1;
					MatchCollection matchCollection = textPattern.regex_0.Matches(string_1);
					if (matchCollection.Count == 0)
					{
						return false;
					}
					ienumerator_0 = matchCollection.GetEnumerator();
					int_0 = -3;
					break;
				}
				}
				if (!ienumerator_0.MoveNext())
				{
					method_0();
					ienumerator_0 = null;
					return false;
				}
				Match match = (Match)ienumerator_0.Current;
				string_0 = match.Value;
				int_0 = 1;
				return true;
			}
			catch
			{
				//try-fault
				((IDisposable)this).Dispose();
				throw;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void method_0()
		{
			int_0 = -1;
			if (ienumerator_0 is IDisposable disposable)
			{
				disposable.Dispose();
			}
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		[DebuggerHidden]
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			Class34 @class;
			if (int_0 == -2 && int_1 == Environment.CurrentManagedThreadId)
			{
				int_0 = 0;
				@class = this;
			}
			else
			{
				@class = new Class34(0)
				{
					textPattern_0 = textPattern_0
				};
			}
			@class.string_1 = string_2;
			return @class;
		}

		[DebuggerHidden]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<string>)this).GetEnumerator();
		}
	}

	private Regex regex_0;

	public TextPattern(string string_0)
		: base(string_0)
	{
		regex_0 = new Regex(string_0);
	}

	[IteratorStateMachine(typeof(Class33))]
	public override IEnumerable<MatchLocation> GetMatchLocations(string input)
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new Class33(-2)
		{
			textPattern_0 = this,
			string_1 = input
		};
	}

	[IteratorStateMachine(typeof(Class34))]
	public override IEnumerable<string> GetMatches(string input)
	{
		//yield-return decompiler failed: Unexpected instruction in Iterator.Dispose()
		return new Class34(-2)
		{
			textPattern_0 = this,
			string_2 = input
		};
	}
}
