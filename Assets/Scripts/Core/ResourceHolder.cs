using System;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class ResourceHolder : MonoBehaviour, ISavable
    {
        private int _money;
        private int _scraps;

        public int Money => _money;
        public int Scraps => _scraps;

        private void OnEnable()
        {
            GameManager.Instance.resourceHolder = this;
        }

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
