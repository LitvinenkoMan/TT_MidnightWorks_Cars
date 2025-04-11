using System;
using System.Collections.Generic;
using System.IO;
using Interfaces;
using UnityEngine;

namespace Core
{
    public class SaveManager
    {
        private List<ISavable> _saves;
        private string _savePath;
        public SaveManager()
        {
            _savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
            _saves = new List<ISavable>();
        }

        public void SaveGame()
        {
            SaveData data = new SaveData();
            foreach (var temporalData in _saves)
            {
                data += temporalData.Save();
            }
            File.WriteAllText(_savePath, JsonUtility.ToJson(data, true));
        }

        public void LoadGame()
        {
            if (!File.Exists(_savePath)) return;

            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(_savePath));
            
            GameManager.Instance.Wallet.Load(data);
            GameManager.Instance.InventorySystem.Load(data);
        }

        public void AddSavableObject(ISavable savable)
        {
            _saves.Add(savable);
        }
    }
    
    [Serializable]
    public class SaveData
    {
        public int money;
        public int scraps;
        public List<CarPart> ownedParts;
        
        
        public SaveData()
        {
            money = 0;
            scraps = 0;
            ownedParts = new List<CarPart>();
        }

        public SaveData(int money, int scraps)
        {
            this.money = money;
            this.scraps = scraps;
            ownedParts = new List<CarPart>();
        }
        
        public static SaveData operator+(SaveData data1, SaveData data2)
        {
            var data = new SaveData();
            data.money = data1.money + data2.money;
            data.scraps = data1.scraps + data2.scraps;
            data.ownedParts.AddRange(data1.ownedParts);
            data.ownedParts.AddRange(data2.ownedParts);
            return data;
        }
    }
}