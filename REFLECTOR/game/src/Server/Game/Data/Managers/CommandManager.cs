namespace Server.Game.Data.Managers
{
    using Server.Game.Data.Commands;
    using Server.Game.Data.Models;
    using Server.Game.Network.ServerPacket;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class CommandManager
    {
        private static readonly Dictionary<string, ICommand> dictionary_0 = new Dictionary<string, ICommand>();

        static CommandManager()
        {
            Class16 class2 = new Class16 {
                type_0 = typeof(ICommand)
            };
            foreach (ICommand command in AppDomain.CurrentDomain.GetAssemblies().SelectMany<Assembly, Type>(new Func<Assembly, IEnumerable<Type>>(Class14.<>9.method_0)).Where<Type>(new Func<Type, bool>(class2.method_0)).Select<Type, object>(new Func<Type, object>(Class14.<>9.method_1)))
            {
                dictionary_0.Add(command.Command, command);
            }
        }

        public static IEnumerable<ICommand> GetCommandsForPlayer(Account Player)
        {
            Class17 class2 = new Class17 {
                account_0 = Player
            };
            return ((class2.account_0 != null) ? dictionary_0.Values.Where<ICommand>(new Func<ICommand, bool>(class2.method_0)) : Enumerable.Empty<ICommand>());
        }

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
            if (!account_0.HavePermission(command.Permission))
            {
                return false;
            }
            string str = command.Execute(string_0, string_1, account_0);
            account_0.SendPacket(new PROTOCOL_LOBBY_CHATTING_ACK("Server", 0, 5, false, str));
            return true;
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> list, int limit)
        {
            Class18<T> class2 = new Class18<T> {
                int_0 = limit
            };
            Func<T, int, Class0<T, int>> selector = Class15<T>.<>9__5_0;
            if (Class15<T>.<>9__5_0 == null)
            {
                Func<T, int, Class0<T, int>> local1 = Class15<T>.<>9__5_0;
                selector = Class15<T>.<>9__5_0 = new Func<T, int, Class0<T, int>>(Class15<T>.<>9.method_0);
            }
            Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> func2 = Class15<T>.<>9__5_2;
            if (Class15<T>.<>9__5_2 == null)
            {
                Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> local2 = Class15<T>.<>9__5_2;
                func2 = Class15<T>.<>9__5_2 = new Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(Class15<T>.<>9.method_1);
            }
            return list.Select<T, Class0<T, int>>(selector).GroupBy<Class0<T, int>, int>(new Func<Class0<T, int>, int>(class2.method_0)).Select<IGrouping<int, Class0<T, int>>, IEnumerable<T>>(func2);
        }

        public static bool TryParse(string Text, Account Player)
        {
            Text = Text.Trim();
            if ((Text.Length == 0) || (Player == null))
            {
                return false;
            }
            if (!Text.StartsWith(":"))
            {
                return false;
            }
            string str = Text.Substring(1);
            string[] strArray = new string[0];
            if (str.Contains(" "))
            {
                string[] separator = new string[] { " " };
                string[] source = str.Split(separator, StringSplitOptions.None);
                str = source[0];
                strArray = source.Skip<string>(1).ToArray<string>();
            }
            return smethod_0(Player, dictionary_0, str, strArray);
        }

        [Serializable, CompilerGenerated]
        private sealed class Class14
        {
            public static readonly CommandManager.Class14 <>9 = new CommandManager.Class14();

            internal IEnumerable<Type> method_0(Assembly assembly_0) => 
                assembly_0.GetTypes();

            internal object method_1(Type type_0) => 
                Activator.CreateInstance(type_0);
        }

        [Serializable, CompilerGenerated]
        private sealed class Class15<T>
        {
            public static readonly CommandManager.Class15<T> <>9;
            public static Func<T, int, Class0<T, int>> <>9__5_0;
            public static Func<Class0<T, int>, T> <>9__5_3;
            public static Func<IGrouping<int, Class0<T, int>>, IEnumerable<T>> <>9__5_2;

            static Class15()
            {
                CommandManager.Class15<T>.<>9 = new CommandManager.Class15<T>();
            }

            internal Class0<T, int> method_0(T gparam_0, int int_0) => 
                new Class0<T, int>(gparam_0, int_0);

            internal IEnumerable<T> method_1(IGrouping<int, Class0<T, int>> igrouping_0)
            {
                Func<Class0<T, int>, T> selector = CommandManager.Class15<T>.<>9__5_3;
                if (CommandManager.Class15<T>.<>9__5_3 == null)
                {
                    Func<Class0<T, int>, T> local1 = CommandManager.Class15<T>.<>9__5_3;
                    selector = CommandManager.Class15<T>.<>9__5_3 = new Func<Class0<T, int>, T>(CommandManager.Class15<T>.<>9.method_2);
                }
                return igrouping_0.Select<Class0<T, int>, T>(selector);
            }

            internal T method_2(Class0<T, int> class0_0) => 
                class0_0.item;
        }

        [CompilerGenerated]
        private sealed class Class16
        {
            public Type type_0;

            internal bool method_0(Type type_1) => 
                this.type_0.IsAssignableFrom(type_1) && (!type_1.IsInterface && !type_1.IsAbstract);
        }

        [CompilerGenerated]
        private sealed class Class17
        {
            public Account account_0;

            internal bool method_0(ICommand icommand_0) => 
                this.account_0.HavePermission(icommand_0.Permission);
        }

        [CompilerGenerated]
        private sealed class Class18<T>
        {
            public int int_0;

            internal int method_0(Class0<T, int> class0_0) => 
                class0_0.inx / this.int_0;
        }
    }
}

