using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Databases/PartSpriteDatabase")]
    public class PartSpriteDatabase : ScriptableObject
    {
        public List<PartIconEntry> entries;

        [System.Serializable]
        public class PartIconEntry
        {
            public CarPartType type;
            public Sprite icon;
        }

        public Sprite GetSpriteForPart(CarPartType type)
        {
            return entries.Find(e => e.type == type)?.icon;
        }
    }
}
