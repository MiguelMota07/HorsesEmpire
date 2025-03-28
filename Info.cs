using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorsesEmpire
{
    internal class Info
    {
        private static int money;
        private static int moneyPerClick;
        private static int moneyPerSecond;
        private static int clickUpgradeCost;
        private static int allMoney;
        private static int clickNumber;

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
    }
}
