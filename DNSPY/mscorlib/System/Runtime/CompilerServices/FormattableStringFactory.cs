using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008FD RID: 2301
	[__DynamicallyInvokable]
	public static class FormattableStringFactory
	{
		// Token: 0x06005E56 RID: 24150 RVA: 0x0014B31A File Offset: 0x0014951A
		[__DynamicallyInvokable]
		public static FormattableString Create(string format, params object[] arguments)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (arguments == null)
			{
				throw new ArgumentNullException("arguments");
			}
			return new FormattableStringFactory.ConcreteFormattableString(format, arguments);
		}

		// Token: 0x02000C99 RID: 3225
		private sealed class ConcreteFormattableString : FormattableString
		{
			// Token: 0x06007117 RID: 28951 RVA: 0x00185140 File Offset: 0x00183340
			internal ConcreteFormattableString(string format, object[] arguments)
			{
				this._format = format;
				this._arguments = arguments;
			}

			// Token: 0x17001363 RID: 4963
			// (get) Token: 0x06007118 RID: 28952 RVA: 0x00185156 File Offset: 0x00183356
			public override string Format
			{
				get
				{
					return this._format;
				}
			}

			// Token: 0x06007119 RID: 28953 RVA: 0x0018515E File Offset: 0x0018335E
			public override object[] GetArguments()
			{
				return this._arguments;
			}

			// Token: 0x17001364 RID: 4964
			// (get) Token: 0x0600711A RID: 28954 RVA: 0x00185166 File Offset: 0x00183366
			public override int ArgumentCount
			{
				get
				{
					return this._arguments.Length;
				}
			}

			// Token: 0x0600711B RID: 28955 RVA: 0x00185170 File Offset: 0x00183370
			public override object GetArgument(int index)
			{
				return this._arguments[index];
			}

			// Token: 0x0600711C RID: 28956 RVA: 0x0018517A File Offset: 0x0018337A
			public override string ToString(IFormatProvider formatProvider)
			{
				return string.Format(formatProvider, this._format, this._arguments);
			}

			// Token: 0x0400385C RID: 14428
			private readonly string _format;

			// Token: 0x0400385D RID: 14429
			private readonly object[] _arguments;
		}
	}
}
