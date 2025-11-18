using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000423 RID: 1059
	[__DynamicallyInvokable]
	public class EventCommandEventArgs : EventArgs
	{
		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x0600350B RID: 13579 RVA: 0x000CE0B6 File Offset: 0x000CC2B6
		// (set) Token: 0x0600350C RID: 13580 RVA: 0x000CE0BE File Offset: 0x000CC2BE
		[__DynamicallyInvokable]
		public EventCommand Command
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Command>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Command>k__BackingField = value;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x0600350D RID: 13581 RVA: 0x000CE0C7 File Offset: 0x000CC2C7
		// (set) Token: 0x0600350E RID: 13582 RVA: 0x000CE0CF File Offset: 0x000CC2CF
		[__DynamicallyInvokable]
		public IDictionary<string, string> Arguments
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Arguments>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Arguments>k__BackingField = value;
			}
		}

		// Token: 0x0600350F RID: 13583 RVA: 0x000CE0D8 File Offset: 0x000CC2D8
		[__DynamicallyInvokable]
		public bool EnableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
		}

		// Token: 0x06003510 RID: 13584 RVA: 0x000CE107 File Offset: 0x000CC307
		[__DynamicallyInvokable]
		public bool DisableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
		}

		// Token: 0x06003511 RID: 13585 RVA: 0x000CE138 File Offset: 0x000CC338
		internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.Command = command;
			this.Arguments = arguments;
			this.eventSource = eventSource;
			this.listener = listener;
			this.perEventSourceSessionId = perEventSourceSessionId;
			this.etwSessionId = etwSessionId;
			this.enable = enable;
			this.level = level;
			this.matchAnyKeyword = matchAnyKeyword;
		}

		// Token: 0x0400177F RID: 6015
		[CompilerGenerated]
		private EventCommand <Command>k__BackingField;

		// Token: 0x04001780 RID: 6016
		[CompilerGenerated]
		private IDictionary<string, string> <Arguments>k__BackingField;

		// Token: 0x04001781 RID: 6017
		internal EventSource eventSource;

		// Token: 0x04001782 RID: 6018
		internal EventDispatcher dispatcher;

		// Token: 0x04001783 RID: 6019
		internal EventListener listener;

		// Token: 0x04001784 RID: 6020
		internal int perEventSourceSessionId;

		// Token: 0x04001785 RID: 6021
		internal int etwSessionId;

		// Token: 0x04001786 RID: 6022
		internal bool enable;

		// Token: 0x04001787 RID: 6023
		internal EventLevel level;

		// Token: 0x04001788 RID: 6024
		internal EventKeywords matchAnyKeyword;

		// Token: 0x04001789 RID: 6025
		internal EventCommandEventArgs nextCommand;
	}
}
