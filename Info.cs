using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorsesEmpire
{
	public class Horse
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		public decimal BaseProduction { get; set; }
		public decimal BuyPrice { get; set; }
		public decimal SellPrice { get; set; }
		public bool IsSold { get; set; }
		public List<int> Upgrades { get; set; } = new List<int>();
	}

	internal class Info
	{
		private static int money;
		private static int moneyPerClick;
		private static int moneyPerSecond;
		private static int clickUpgradeCost;
		private static int allMoney;
		private static int clickNumber;
		private static int horsesSpaces;
		private static long lastSave;
		private static List<Horse> horses = new List<Horse>();

		public static int Money
		{
			get => money;
			set => money = value >= 0 ? value : 0; // Example validation
		}

		public static int MoneyPerClick
		{
			get => moneyPerClick;
			set => moneyPerClick = value >= 0 ? value : 0; // Example validation
		}

		public static int MoneyPerSecond
		{
			get => moneyPerSecond;
			set => moneyPerSecond = value >= 0 ? value : 0; // Example validation
		}

		public static int ClickUpgradeCost
		{
			get => clickUpgradeCost;
			set => clickUpgradeCost = value >= 0 ? value : 0; // Example validation
		}

		public static int AllMoney
		{
			get => allMoney;
			set => allMoney = value >= 0 ? value : 0; // Example validation
		}

		public static int ClickNumber
		{
			get => clickNumber;
			set => clickNumber = value >= 0 ? value : 0; // Example validation
		}

        public static int HorsesSpaces
        {
            get => horsesSpaces;
            set => horsesSpaces = value >= 0 ? value : 0;
        }

		public static long LastSave
		{
			get => lastSave;
			set => lastSave = value >= 0 ? value : 0;
		}

        public static List<Horse> Horses
		{
			get => horses;
		}

		public static long GetCurrentDateTime()
		{
			return DateTimeOffset.Now.ToUnixTimeSeconds();
		}
	}
}
