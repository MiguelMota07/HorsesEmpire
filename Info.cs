using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorsesEmpire
{
	public class Equipment{
		public int Id { get; set; }
		public string Name { get; set; }
		public double Multiplier { get; set; }
		public int Price { get; set; }
		public int SoldAmount { get; set; }
		public int InUse { get; set; }

		public Equipment(int id, string name, double multiplier, int price, int soldAmount, int inUse)
		{
			Id = id;
			Name = name;
			Multiplier = multiplier;
			Price = price;
			SoldAmount = soldAmount;
			InUse = inUse;
		}
	}
	public class Horse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int BaseProduction { get; set; }
		public int BuyPrice { get; set; }
		public int SellPrice { get; set; }
		public bool IsSold { get; set; }
		public List<Equipment> Equipments { get; set; } = new List<Equipment>();

		public Horse(int id, string name, int baseProduction, int buyPrice, int sellPrice, bool isSold, List<Equipment> equipments = null)
		{
			Id = id;
			Name = name;
			BaseProduction = baseProduction;
			BuyPrice = buyPrice;
			SellPrice = sellPrice;
			IsSold = isSold;
			Equipments = equipments;
		}
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
		private static int horsesSpacesPrice;
		private static long lastSave;
		private static List<Horse> horses = new List<Horse>();
        private static List<Equipment> equipments = new List<Equipment>();
		
		public static int HorsesSpacesPrice
		{
			get => horsesSpacesPrice;
			set => horsesSpacesPrice = value >=0 ? value : 0; 
		}
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
        public static List<Equipment> Equipments
        {
            get => equipments;
        }
        public static long GetCurrentDateTime()
		{
			return DateTimeOffset.Now.ToUnixTimeSeconds();
		}
	}
}
