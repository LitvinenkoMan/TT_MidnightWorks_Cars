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
            if (addAmount < 0) return;
            
            _money += addAmount;
            OnMoneyChanged?.Invoke(_money);
        }

        public bool SpendMoney(int spendAmount)
        {
            if (_money >= spendAmount)
            {
                _money -= spendAmount;
                OnMoneyChanged?.Invoke(_money);
                return true;
            }
            return false;
        }

        public void AddScraps(int addAmount)
        {
            _scraps += addAmount;
            OnScrapsChanged?.Invoke(_scraps);
        }

        public bool SpendScraps(int spendAmount)
        {
            if (_scraps >= spendAmount)
            {
                _scraps -= spendAmount;
                OnScrapsChanged?.Invoke(_scraps);
                return true;
            }
            return false;
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
