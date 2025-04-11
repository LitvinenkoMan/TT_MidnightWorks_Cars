using System;
using Interfaces;

namespace Core
{
    public class Wallet : ISavable
    {
        private int _money;
        private int _scraps;

        public int Money => _money;
        public int Scraps => _scraps;

        public event Action<int> OnMoneyChanged;
        public event Action<int> OnScrapsChanged;

        public void AddMoney(int addAmount) => _money += addAmount;

        public void AddScraps(int addAmount) => _scraps += addAmount;
        
        public SaveData Save()
        {
            return new SaveData(_money, _scraps);
        }

        public void Load(SaveData data)
        {
            _money = data.money;
            _scraps = data.scraps;
        }
    }
}
