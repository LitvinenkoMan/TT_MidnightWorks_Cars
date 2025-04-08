using System.IO;
using UnityEngine;

namespace Core
{
    public class SaveManager : MonoBehaviour
    {
        private string savePath => Path.Combine(Application.persistentDataPath, "savegame.json");

        public void SaveGame()
        {
            SaveData data = new SaveData();
            data += GameManager.Instance.resourceHolder.Save();
            File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        }

        public void LoadGame()
        {
            if (!File.Exists(savePath)) return;

            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(savePath));
            
            GameManager.Instance.resourceHolder.Load(data);
        }
    }
    
    public class SaveData
    {
        public SaveData()
        {
            money = 0;
            scraps = 0;
        }

        public SaveData(int money, int scraps)
        {
            this.money = money;
            this.scraps = scraps;
        }
        
        public static SaveData operator+(SaveData data1, SaveData data2)
        {
            return new SaveData(data1.money + data2.money, data1.scraps + data2.scraps);
        }

        public int money;
        public int scraps;
        //public List<string> ownedParts;
    }
}