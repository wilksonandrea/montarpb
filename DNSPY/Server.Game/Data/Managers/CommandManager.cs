using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Server.Game.Data.Commands;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Managers
{
	// Token: 0x0200020A RID: 522
	public static class CommandManager
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x00038038 File Offset: 0x00036238
		static CommandManager()
		{
			CommandManager.Class16 @class = new CommandManager.Class16();
			@class.type_0 = typeof(ICommand);
			foreach (object obj in AppDomain.CurrentDomain.GetAssemblies().SelectMany(new Func<Assembly, IEnumerable<Type>>(CommandManager.Class14.<>9.method_0)).Where(new Func<Type, bool>(@class.method_0))
				.Select(new Func<Type, object>(CommandManager.Class14.<>9.method_1)))
			{
				ICommand command = (ICommand)obj;
				CommandManager.dictionary_0.Add(command.Command, command);
			}
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000380F4 File Offset: 0x000362F4
		public static bool TryParse(string Text, Account Player)
		{
			Text = Text.Trim();
			if (Text.Length == 0 || Player == null)
			{
				return false;
			}
			if (Text.StartsWith(":"))
			{
				string text = Text.Substring(1);
				string[] array = new string[0];
				if (text.Contains(" "))
				{
					string[] array2 = text.Split(new string[] { " " }, StringSplitOptions.None);
					text = array2[0];
					array = array2.Skip(1).ToArray<string>();
				}
				return CommandManager.smethod_0(Player, CommandManager.dictionary_0, text, array);
			}
			return false;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00038174 File Offset: 0x00036374
		public static IEnumerable<ICommand> GetCommandsForPlayer(Account Player)
		{
			CommandManager.Class17 @class = new CommandManager.Class17();
			@class.account_0 = Player;
			if (@class.account_0 == null)
			{
				return Enumerable.Empty<ICommand>();
			}
			return CommandManager.dictionary_0.Values.Where(new Func<ICommand, bool>(@class.method_0));
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x000381B8 File Offset: 0x000363B8
		private static bool smethod_0(Account account_0, Dictionary<string, ICommand> dictionary_1, string string_0, string[] string_1)
		{
			if (!dictionary_1.ContainsKey(string_0))
			{
				return false;
			}
			ICommand command = dictionary_1[string_0];
			if (command == null)
			{
				return false;
			}
			if (account_0.HavePermission(command.Permission))
			{
				string text = command.Execute(string_0, string_1, account_0);
				account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, text));
				return true;
			}
			return false;
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0003820C File Offset: 0x0003640C
		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
		{
			CommandManager.Class18<T> @class = new CommandManager.Class18<T>();
			@class.int_0 = limit;
			return list.Select(new Func<T, int, Class0<T, int>>(CommandManager.Class15<T>.<>9.method_0)).GroupBy(new Func<Class0<T, int>, int>(@class.method_0)).Select(new Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(CommandManager.Class15<T>.<>9.method_1));
		}

		// Token: 0x04000450 RID: 1104
		private static readonly Dictionary<string, ICommand> dictionary_0 = new Dictionary<string, ICommand>();

		// Token: 0x0200020B RID: 523
		[CompilerGenerated]
		[Serializable]
		private sealed class Class14
		{
			// Token: 0x060006EB RID: 1771 RVA: 0x00006283 File Offset: 0x00004483
			// Note: this type is marked as 'beforefieldinit'.
			static Class14()
			{
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x000025DF File Offset: 0x000007DF
			public Class14()
			{
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x0000628F File Offset: 0x0000448F
			internal IEnumerable<Type> method_0(Assembly assembly_0)
			{
				return assembly_0.GetTypes();
			}

			// Token: 0x060006EE RID: 1774 RVA: 0x00006297 File Offset: 0x00004497
			internal object method_1(Type type_0)
			{
				return Activator.CreateInstance(type_0);
			}

			// Token: 0x04000451 RID: 1105
			public static readonly CommandManager.Class14 <>9 = new CommandManager.Class14();
		}

		// Token: 0x0200020C RID: 524
		[CompilerGenerated]
		[Serializable]
		private sealed class Class15<T>
		{
			// Token: 0x060006EF RID: 1775 RVA: 0x0000629F File Offset: 0x0000449F
			// Note: this type is marked as 'beforefieldinit'.
			static Class15()
			{
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x000025DF File Offset: 0x000007DF
			public Class15()
			{
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x000062AB File Offset: 0x000044AB
			internal Class0<T, int> method_0(T gparam_0, int int_0)
			{
				return new Class0<T, int>(gparam_0, int_0);
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x000062B4 File Offset: 0x000044B4
			internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
			{
				return igrouping_0.Select(new Func<Class0<T, int>, T>(CommandManager.Class15<T>.<>9.method_2));
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x000062DB File Offset: 0x000044DB
			internal T method_2(Class0<T, int> class0_0)
			{
				return class0_0.item;
			}

			// Token: 0x04000452 RID: 1106
			public static readonly CommandManager.Class15<T> <>9 = new CommandManager.Class15<T>();

			// Token: 0x04000453 RID: 1107
			public static Func<T, int, Class0<T, int>> <>9__5_0;

			// Token: 0x04000454 RID: 1108
			public static Func<Class0<T, int>, T> <>9__5_3;

			// Token: 0x04000455 RID: 1109
			public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> <>9__5_2;
		}

		// Token: 0x0200020D RID: 525
		[CompilerGenerated]
		private sealed class Class16
		{
			// Token: 0x060006F4 RID: 1780 RVA: 0x000025DF File Offset: 0x000007DF
			public Class16()
			{
			}

			// Token: 0x060006F5 RID: 1781 RVA: 0x000062E3 File Offset: 0x000044E3
			internal bool method_0(Type type_1)
			{
				return this.type_0.IsAssignableFrom(type_1) && !type_1.IsInterface && !type_1.IsAbstract;
			}

			// Token: 0x04000456 RID: 1110
			public Type type_0;
		}

		// Token: 0x0200020E RID: 526
		[CompilerGenerated]
		private sealed class Class17
		{
			// Token: 0x060006F6 RID: 1782 RVA: 0x000025DF File Offset: 0x000007DF
			public Class17()
			{
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x00006306 File Offset: 0x00004506
			internal bool method_0(ICommand icommand_0)
			{
				return this.account_0.HavePermission(icommand_0.Permission);
			}

			// Token: 0x04000457 RID: 1111
			public Account account_0;
		}

		// Token: 0x0200020F RID: 527
		[CompilerGenerated]
		private sealed class Class18<T>
		{
			// Token: 0x060006F8 RID: 1784 RVA: 0x000025DF File Offset: 0x000007DF
			public Class18()
			{
			}

			// Token: 0x060006F9 RID: 1785 RVA: 0x00006319 File Offset: 0x00004519
			internal int method_0(Class0<T, int> class0_0)
			{
				return class0_0.inx / this.int_0;
			}

			// Token: 0x04000458 RID: 1112
			public int int_0;
		}
	}
}
