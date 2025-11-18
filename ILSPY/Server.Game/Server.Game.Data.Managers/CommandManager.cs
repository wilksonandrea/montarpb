using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Server.Game.Data.Commands;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Managers;

public static class CommandManager
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class14
	{
		public static readonly Class14 _003C_003E9 = new Class14();

		internal IEnumerable<Type> method_0(Assembly assembly_0)
		{
			return assembly_0.GetTypes();
		}

		internal object method_1(Type type_0)
		{
			return Activator.CreateInstance(type_0);
		}
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class Class15<T>
	{
		public static readonly Class15<T> _003C_003E9 = new Class15<T>();

		public static Func<T, int, global::Class0<T, int>> _003C_003E9__5_0;

		public static Func<global::Class0<T, int>, T> _003C_003E9__5_3;

		public static Func<IGrouping<int, global::Class0<T, int>>, IEnumerable<T>> _003C_003E9__5_2;

		internal global::Class0<T, int> method_0(T gparam_0, int int_0)
		{
			return new global::Class0<T, int>(gparam_0, int_0);
		}

		internal IEnumerable<T> method_1(IGrouping<int, global::Class0<T, int>> igrouping_0)
		{
			return igrouping_0.Select((global::Class0<T, int> class0_0) => class0_0.item);
		}

		internal T method_2(global::Class0<T, int> class0_0)
		{
			return class0_0.item;
		}
	}

	[CompilerGenerated]
	private sealed class Class16
	{
		public Type type_0;

		internal bool method_0(Type type_1)
		{
			if (type_0.IsAssignableFrom(type_1) && !type_1.IsInterface)
			{
				return !type_1.IsAbstract;
			}
			return false;
		}
	}

	[CompilerGenerated]
	private sealed class Class17
	{
		public Account account_0;

		internal bool method_0(ICommand icommand_0)
		{
			return account_0.HavePermission(icommand_0.Permission);
		}
	}

	[CompilerGenerated]
	private sealed class Class18<T>
	{
		public int int_0;

		internal int method_0(global::Class0<T, int> class0_0)
		{
			return class0_0.inx / int_0;
		}
	}

	private static readonly Dictionary<string, ICommand> dictionary_0;

	static CommandManager()
	{
		dictionary_0 = new Dictionary<string, ICommand>();
		Type type_2 = typeof(ICommand);
		foreach (ICommand item in from type_1 in AppDomain.CurrentDomain.GetAssemblies().SelectMany((Assembly assembly_0) => assembly_0.GetTypes())
			where type_2.IsAssignableFrom(type_1) && !type_1.IsInterface && !type_1.IsAbstract
			select type_1 into type_0
			select Activator.CreateInstance(type_0))
		{
			dictionary_0.Add(item.Command, item);
		}
	}

	public static bool TryParse(string Text, Account Player)
	{
		Text = Text.Trim();
		if (Text.Length != 0 && Player != null)
		{
			if (Text.StartsWith(":"))
			{
				string text = Text.Substring(1);
				string[] string_ = new string[0];
				if (text.Contains(" "))
				{
					string[] array = text.Split(new string[1] { " " }, StringSplitOptions.None);
					text = array[0];
					string_ = array.Skip(1).ToArray();
				}
				return smethod_0(Player, dictionary_0, text, string_);
			}
			return false;
		}
		return false;
	}

	public static IEnumerable<ICommand> GetCommandsForPlayer(Account Player)
	{
		if (Player == null)
		{
			return Enumerable.Empty<ICommand>();
		}
		return dictionary_0.Values.Where((ICommand icommand_0) => Player.HavePermission(icommand_0.Permission));
	}

	private static bool smethod_0(Account account_0, Dictionary<string, ICommand> dictionary_1, string string_0, string[] string_1)
	{
		if (dictionary_1.ContainsKey(string_0))
		{
			ICommand command = dictionary_1[string_0];
			if (command != null)
			{
				if (account_0.HavePermission(command.Permission))
				{
					string string_2 = command.Execute(string_0, string_1, account_0);
					account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, bool_1: false, string_2));
					return true;
				}
				return false;
			}
			return false;
		}
		return false;
	}

	public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
	{
		return from class0_0 in list.Select((T gparam_0, int int_0) => new global::Class0<T, int>(gparam_0, int_0))
			group class0_0 by class0_0.inx / limit into igrouping_0
			select from class0_0 in igrouping_0
				select class0_0.item;
	}
}
