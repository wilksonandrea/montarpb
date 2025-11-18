using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class PlayerBonus
	{
		public int Bonuses
		{
			get;
			set;
		}

		public int CrosshairColor
		{
			get;
			set;
		}

		public string FakeNick
		{
			get;
			set;
		}

		public int FakeRank
		{
			get;
			set;
		}

		public int FreePass
		{
			get;
			set;
		}

		public int MuzzleColor
		{
			get;
			set;
		}

		public int NickBorderColor
		{
			get;
			set;
		}

		public long OwnerId
		{
			get;
			set;
		}

		public PlayerBonus()
		{
			this.CrosshairColor = 4;
			this.FakeRank = 55;
			this.FakeNick = "";
		}

		public bool AddBonuses(int ItemId)
		{
			int bonuses = this.Bonuses;
			int freePass = this.FreePass;
			if (ItemId <= 1600037)
			{
				switch (ItemId)
				{
					case 1600001:
					{
						this.method_1(1);
						break;
					}
					case 1600002:
					{
						this.method_1(2);
						break;
					}
					case 1600003:
					{
						this.method_1(4);
						break;
					}
					case 1600004:
					{
						this.method_1(16);
						break;
					}
					default:
					{
						if (ItemId == 1600011)
						{
							this.method_3(128);
							break;
						}
						else if (ItemId == 1600037)
						{
							this.method_1(8);
							break;
						}
						else
						{
							break;
						}
					}
				}
			}
			else if (ItemId == 1600038)
			{
				this.method_1(64);
			}
			else if (ItemId == 1600119)
			{
				this.method_1(32);
			}
			else
			{
				switch (ItemId)
				{
					case 1600201:
					{
						this.method_1(512);
						break;
					}
					case 1600202:
					{
						this.method_1(1024);
						break;
					}
					case 1600203:
					{
						this.method_1(2048);
						break;
					}
					case 1600204:
					{
						this.method_1(4096);
						break;
					}
				}
			}
			if (this.Bonuses != bonuses)
			{
				return true;
			}
			return this.FreePass != freePass;
		}

		private void method_0(int int_6)
		{
			this.Bonuses = this.Bonuses & ~int_6;
		}

		private void method_1(int int_6)
		{
			this.Bonuses = this.Bonuses | int_6;
		}

		private void method_2(int int_6)
		{
			this.FreePass = this.FreePass & ~int_6;
		}

		private void method_3(int int_6)
		{
			this.FreePass = this.FreePass | int_6;
		}

		public bool RemoveBonuses(int ItemId)
		{
			int bonuses = this.Bonuses;
			int freePass = this.FreePass;
			if (ItemId <= 1600037)
			{
				switch (ItemId)
				{
					case 1600001:
					{
						this.method_0(1);
						break;
					}
					case 1600002:
					{
						this.method_0(2);
						break;
					}
					case 1600003:
					{
						this.method_0(4);
						break;
					}
					case 1600004:
					{
						this.method_0(16);
						break;
					}
					default:
					{
						if (ItemId == 1600011)
						{
							this.method_2(128);
							break;
						}
						else if (ItemId == 1600037)
						{
							this.method_0(8);
							break;
						}
						else
						{
							break;
						}
					}
				}
			}
			else if (ItemId == 1600038)
			{
				this.method_0(64);
			}
			else if (ItemId == 1600119)
			{
				this.method_0(32);
			}
			else
			{
				switch (ItemId)
				{
					case 1600201:
					{
						this.method_0(512);
						break;
					}
					case 1600202:
					{
						this.method_0(1024);
						break;
					}
					case 1600203:
					{
						this.method_0(2048);
						break;
					}
					case 1600204:
					{
						this.method_0(4096);
						break;
					}
				}
			}
			if (this.Bonuses != bonuses)
			{
				return true;
			}
			return this.FreePass != freePass;
		}
	}
}