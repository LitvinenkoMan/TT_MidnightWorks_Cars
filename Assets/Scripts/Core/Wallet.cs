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

        public Wallet()
        {
            GameManager.Instance.SaveManager.AddSavableObject(this);
        }

        public void AddMoney(int addAmount)
        {
            _money += addAmount;
            OnMoneyChanged?.Invoke(_money);
        }

        public void AddScraps(int addAmount)
        {
            _scraps += addAmount;
            OnScrapsChanged?.Invoke(_scraps);
        }

        public SaveData Save()
        {
            return new SaveData(_money, _scraps);
        }

        public void Load(SaveData data)
        {
            _money = data.money;
            OnMoneyChanged?.Invoke(_money);
            _scraps = data.scraps;
            OnScrapsChanged?.Invoke(_scraps);
        }
    }
}
