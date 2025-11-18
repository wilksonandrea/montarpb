namespace Plugin.Core.Settings
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ConfigEngine
    {
        private readonly FileInfo fileInfo_0;
        private readonly FileAccess fileAccess_0;
        private readonly string string_0 = Assembly.GetExecutingAssembly().GetName().Name;

        public ConfigEngine(string string_1 = null, FileAccess fileAccess_1 = 3)
        {
            this.fileAccess_0 = fileAccess_1;
            string fileName = string_1;
            if (string_1 == null)
            {
                string local1 = string_1;
                fileName = this.string_0;
            }
            this.fileInfo_0 = new FileInfo(fileName);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            string text1 = Section;
            if (Section == null)
            {
                string local1 = Section;
                text1 = this.string_0;
            }
            this.method_1(Key, null, text1);
        }

        public void DeleteSection(string Section = null)
        {
            string text1 = Section;
            if (Section == null)
            {
                string local1 = Section;
                text1 = this.string_0;
            }
            this.method_1(null, null, text1);
        }

        [DllImport("kernel32", CharSet=CharSet.Unicode)]
        private static extern int GetPrivateProfileString(string string_1, string string_2, string string_3, StringBuilder stringBuilder_0, int int_0, string string_4);
        public bool KeyExists(string Key, string Section = null) => 
            this.method_0(Key, Section).Length > 0;

        private string method_0(string string_1, string string_2 = null)
        {
            StringBuilder builder = new StringBuilder(0xfe01);
            if (this.fileAccess_0 == FileAccess.Write)
            {
                throw new Exception("Can`t read the file! No access!");
            }
            GetPrivateProfileString(string_2 ?? this.string_0, string_1, "", builder, 0xfe01, this.fileInfo_0.FullName);
            return builder.ToString();
        }

        private void method_1(string string_1, string string_2, string string_3 = null)
        {
            if (this.fileAccess_0 == FileAccess.Read)
            {
                throw new Exception("Can`t write to file! No access!");
            }
            WritePrivateProfileString(string_3 ?? this.string_0, string_1, " " + string_2, this.fileInfo_0.FullName);
        }

        public byte ReadC(string Key, byte Defaultprop, string Section = null)
        {
            byte num;
            try
            {
                num = byte.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public int ReadD(string Key, int Defaultprop, string Section = null)
        {
            int num;
            try
            {
                num = int.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public double ReadF(string Key, double Defaultprop, string Section = null)
        {
            double num;
            try
            {
                num = double.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public short ReadH(string Key, short Defaultprop, string Section = null)
        {
            short num;
            try
            {
                num = short.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public long ReadQ(string Key, long Defaultprop, string Section = null)
        {
            long num;
            try
            {
                num = long.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public string ReadS(string Key, string Defaultprop, string Section = null)
        {
            string str;
            try
            {
                str = this.method_0(Key, Section);
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return str;
        }

        public float ReadT(string Key, float Defaultprop, string Section = null)
        {
            float num;
            try
            {
                num = float.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public uint ReadUD(string Key, uint Defaultprop, string Section = null)
        {
            uint num;
            try
            {
                num = uint.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public ushort ReadUH(string Key, ushort Defaultprop, string Section = null)
        {
            ushort num;
            try
            {
                num = ushort.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public ulong ReadUQ(string Key, ulong Defaultprop, string Section = null)
        {
            ulong num;
            try
            {
                num = ulong.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return num;
        }

        public bool ReadX(string Key, bool Defaultprop, string Section = null)
        {
            bool flag;
            try
            {
                flag = bool.Parse(this.method_0(Key, Section));
            }
            catch
            {
                CLogger.Print("Read Parameter Failure: " + Key, LoggerType.Warning, null);
                return Defaultprop;
            }
            return flag;
        }

        public void WriteC(string Key, byte Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteD(string Key, int Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteD(string Key, uint Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteF(string Key, double Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteH(string Key, short Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteH(string Key, ushort Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        [DllImport("kernel32", CharSet=CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string string_1, string string_2, string string_3, string string_4);
        public void WriteQ(string Key, long Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteQ(string Key, ulong Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteS(string Key, string Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value, Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteT(string Key, float Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }

        public void WriteX(string Key, bool Value, string Section = null)
        {
            try
            {
                this.method_1(Key, Value.ToString(), Section);
            }
            catch
            {
                CLogger.Print("Write Parameter Failure: " + Key, LoggerType.Warning, null);
            }
        }
    }
}

