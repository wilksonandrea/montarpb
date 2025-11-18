using System;

namespace System
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	public struct ConsoleKeyInfo
	{
		// Token: 0x06000B9C RID: 2972 RVA: 0x00025060 File Offset: 0x00023260
		public ConsoleKeyInfo(char keyChar, ConsoleKey key, bool shift, bool alt, bool control)
		{
			if (key < (ConsoleKey)0 || key > (ConsoleKey)255)
			{
				throw new ArgumentOutOfRangeException("key", Environment.GetResourceString("ArgumentOutOfRange_ConsoleKey"));
			}
			this._keyChar = keyChar;
			this._key = key;
			this._mods = (ConsoleModifiers)0;
			if (shift)
			{
				this._mods |= ConsoleModifiers.Shift;
			}
			if (alt)
			{
				this._mods |= ConsoleModifiers.Alt;
			}
			if (control)
			{
				this._mods |= ConsoleModifiers.Control;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x000250D8 File Offset: 0x000232D8
		public char KeyChar
		{
			get
			{
				return this._keyChar;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x000250E0 File Offset: 0x000232E0
		public ConsoleKey Key
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x000250E8 File Offset: 0x000232E8
		public ConsoleModifiers Modifiers
		{
			get
			{
				return this._mods;
			}
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000250F0 File Offset: 0x000232F0
		public override bool Equals(object value)
		{
			return value is ConsoleKeyInfo && this.Equals((ConsoleKeyInfo)value);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00025108 File Offset: 0x00023308
		public bool Equals(ConsoleKeyInfo obj)
		{
			return obj._keyChar == this._keyChar && obj._key == this._key && obj._mods == this._mods;
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00025136 File Offset: 0x00023336
		public static bool operator ==(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00025140 File Offset: 0x00023340
		public static bool operator !=(ConsoleKeyInfo a, ConsoleKeyInfo b)
		{
			return !(a == b);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002514C File Offset: 0x0002334C
		public override int GetHashCode()
		{
			return (int)((ConsoleModifiers)this._keyChar | this._mods);
		}

		// Token: 0x04000527 RID: 1319
		private char _keyChar;

		// Token: 0x04000528 RID: 1320
		private ConsoleKey _key;

		// Token: 0x04000529 RID: 1321
		private ConsoleModifiers _mods;
	}
}
