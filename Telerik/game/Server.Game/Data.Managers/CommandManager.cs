using Server.Game.Data.Commands;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Server.Game.Data.Managers
{
	public static class CommandManager
	{
		private readonly static Dictionary<string, ICommand> dictionary_0;

		static CommandManager()
		{
			CommandManager.dictionary_0 = new Dictionary<string, ICommand>();
			Type type = typeof(ICommand);
			foreach (ICommand command in AppDomain.CurrentDomain.GetAssemblies().SelectMany<Assembly, Type>((Assembly assembly_0) => assembly_0.GetTypes()).Where<Type>((Type type_1) => {
				if (!type.IsAssignableFrom(type_1) || type_1.IsInterface)
				{
					return false;
				}
				return !type_1.IsAbstract;
			}).Select<Type, object>((Type type_0) => Activator.CreateInstance(type_0)))
			{
				CommandManager.dictionary_0.Add(command.Command, command);
			}
		}

		public static IEnumerable<ICommand> GetCommandsForPlayer(Account Player)
		{
			if (Player == null)
			{
				return Enumerable.Empty<ICommand>();
			}
			return 
				from icommand_0 in CommandManager.dictionary_0.Values
				where Player.HavePermission(icommand_0.Permission)
				select icommand_0;
		}

		private static bool smethod_0(Account account_0, Dictionary<string, ICommand> dictionary_1, string string_0, string[] string_1)
		{
			if (!dictionary_1.ContainsKey(string_0))
			{
				return false;
			}
			ICommand ıtem = dictionary_1[string_0];
			if (ıtem == null)
			{
				return false;
			}
			if (!account_0.HavePermission(ıtem.Permission))
			{
				return false;
			}
			string str = ıtem.Execute(string_0, string_1, account_0);
			account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, str));
			return true;
		}

		public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
		{
			return list.Select((T gparam_0, int int_0) => new { item = gparam_0, inx = int_0 }).GroupBy((class0_0) => class0_0.inx / limit).Select((igrouping_0) => 
				from  in igrouping_0
				select class0_0.item);
		}

		public static bool TryParse(string Text, Account Player)
		{
			Text = Text.Trim();
			if (Text.Length == 0 || Player == null)
			{
				return false;
			}
			if (!Text.StartsWith(":"))
			{
				return false;
			}
			string str = Text.Substring(1);
			string[] array = new string[0];
			if (str.Contains(" "))
			{
				string[] strArrays = str.Split(new string[] { " " }, StringSplitOptions.None);
				str = strArrays[0];
				array = strArrays.Skip<string>(1).ToArray<string>();
			}
			return CommandManager.smethod_0(Player, CommandManager.dictionary_0, str, array);
		}
	}
}