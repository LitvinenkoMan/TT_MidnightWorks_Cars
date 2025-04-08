using Core;

namespace Interfaces
{
    public interface ISavable
    {
        public SaveData Save();

        public void Load(SaveData data);
    }
}
