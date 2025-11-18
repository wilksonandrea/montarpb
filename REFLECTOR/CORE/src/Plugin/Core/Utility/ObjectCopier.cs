namespace Plugin.Core.Utility
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class ObjectCopier
    {
        private static readonly MethodInfo methodInfo_0 = typeof(object).GetMethod("MemberwiseClone", BindingFlags.NonPublic | BindingFlags.Instance);

        public static T Clone<T>(T Source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }
            if (Source == null)
            {
                return default(T);
            }
            IFormatter formatter = new BinaryFormatter();
            Stream serializationStream = new MemoryStream();
            using (serializationStream)
            {
                formatter.Serialize(serializationStream, Source);
                serializationStream.Seek(0L, SeekOrigin.Begin);
                return (T) formatter.Deserialize(serializationStream);
            }
        }

        public static object Copy(this object originalObject) => 
            smethod_0(originalObject, new Dictionary<object, object>(new REComparer()));

        public static T Copy<T>(this T original) => 
            (T) original.Copy();

        public static T DeepCopy<T>(T Source)
        {
            if (Source == null)
            {
                throw new ArgumentNullException("Object cannot be null");
            }
            return (T) smethod_3(Source);
        }

        public static bool IsPrimitive(this Type type) => 
            !(type == typeof(string)) ? (type.IsValueType & type.IsPrimitive) : true;

        private static object smethod_0(object object_0, IDictionary<object, object> idictionary_0)
        {
            Class7 class2 = new Class7 {
                idictionary_0 = idictionary_0
            };
            if (object_0 == null)
            {
                return null;
            }
            Type type = object_0.GetType();
            if (type.IsPrimitive())
            {
                return object_0;
            }
            if (class2.idictionary_0.ContainsKey(object_0))
            {
                return class2.idictionary_0[object_0];
            }
            if (typeof(Delegate).IsAssignableFrom(type))
            {
                return null;
            }
            object obj2 = methodInfo_0.Invoke(object_0, null);
            if (type.IsArray && !type.GetElementType().IsPrimitive())
            {
                class2.array_0 = (Array) obj2;
                class2.array_0.ForEach(new Action<Array, int[]>(class2.method_0));
            }
            class2.idictionary_0.Add(object_0, obj2);
            smethod_2(object_0, class2.idictionary_0, obj2, type, BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null);
            smethod_1(object_0, class2.idictionary_0, obj2, type);
            return obj2;
        }

        private static void smethod_1(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0)
        {
            if (type_0.BaseType != null)
            {
                smethod_1(object_0, idictionary_0, object_1, type_0.BaseType);
                Func<FieldInfo, bool> func1 = Class6.<>9__4_0;
                if (Class6.<>9__4_0 == null)
                {
                    Func<FieldInfo, bool> local1 = Class6.<>9__4_0;
                    func1 = Class6.<>9__4_0 = new Func<FieldInfo, bool>(Class6.<>9.method_0);
                }
                smethod_2(object_0, idictionary_0, object_1, type_0.BaseType, BindingFlags.NonPublic | BindingFlags.Instance, func1);
            }
        }

        private static void smethod_2(object object_0, IDictionary<object, object> idictionary_0, object object_1, Type type_0, BindingFlags bindingFlags_0 = 0x74, Func<FieldInfo, bool> func_0 = null)
        {
            foreach (FieldInfo info in type_0.GetFields(bindingFlags_0))
            {
                if (((func_0 == null) || func_0(info)) && !info.FieldType.IsPrimitive())
                {
                    object obj2 = smethod_0(info.GetValue(object_0), idictionary_0);
                    info.SetValue(object_1, obj2);
                }
            }
        }

        private static object smethod_3(object object_0)
        {
            if (object_0 == null)
            {
                return null;
            }
            Type type = object_0.GetType();
            if (type.IsValueType || (type == typeof(string)))
            {
                return object_0;
            }
            if (type.IsArray)
            {
                Array array = object_0 as Array;
                Array array2 = Array.CreateInstance(Type.GetType(type.FullName.Replace("[]", string.Empty)), array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    array2.SetValue(smethod_3(array.GetValue(i)), i);
                }
                return Convert.ChangeType(array2, object_0.GetType());
            }
            if (!type.IsClass)
            {
                throw new ArgumentException("Unknown type");
            }
            object obj2 = Activator.CreateInstance(object_0.GetType());
            foreach (FieldInfo info in type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance))
            {
                object obj3 = info.GetValue(object_0);
                if (obj3 != null)
                {
                    info.SetValue(obj2, smethod_3(obj3));
                }
            }
            return obj2;
        }

        [Serializable, CompilerGenerated]
        private sealed class Class6
        {
            public static readonly ObjectCopier.Class6 <>9 = new ObjectCopier.Class6();
            public static Func<FieldInfo, bool> <>9__4_0;

            internal bool method_0(FieldInfo fieldInfo_0) => 
                fieldInfo_0.IsPrivate;
        }

        [CompilerGenerated]
        private sealed class Class7
        {
            public IDictionary<object, object> idictionary_0;
            public Array array_0;

            internal void method_0(Array array_1, int[] int_0)
            {
                array_1.SetValue(ObjectCopier.smethod_0(this.array_0.GetValue(int_0), this.idictionary_0), int_0);
            }
        }
    }
}

