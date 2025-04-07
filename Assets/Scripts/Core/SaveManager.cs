using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core
{
    public class SaveManager : MonoBehaviour
    {
        private string savePath => Path.Combine(Application.persistentDataPath, "savegame.json");

        public void SaveGame()
        {
            SaveData data = new SaveData
            {
                //money = ....
                //parts = .... 
            };
            File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        }

        public void LoadGame()
        {
            if (!File.Exists(savePath)) return;

            SaveData data = JsonUtility.FromJson<SaveData>(File.ReadAllText(savePath));
        }
    }
    
    public class SaveData
    {
        public int money;
        public List<string> ownedParts;
    }
}