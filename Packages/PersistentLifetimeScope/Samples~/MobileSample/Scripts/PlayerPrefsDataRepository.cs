
using UnityEngine;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class PlayerPrefsDataRepository : IDataRepository
    {
        static readonly string s_key = "PlayerPrefsDataRepository_dataModel";

        readonly DataModel _dataModel;

        public PlayerPrefsDataRepository()
        {
            if (PlayerPrefs.HasKey(s_key))
            {
                var json = PlayerPrefs.GetString(s_key);
                if (!string.IsNullOrEmpty(json)
                    && json != "[]")
                {
                    _dataModel = JsonUtility.FromJson<DataModel>(json);
                }
            }

            _dataModel ??= new();

            Debug.Log("PlayerPrefs is not suitable for saving game data. Do not use it in production.");
        }

        DataModel IDataRepository.Get() => _dataModel;

        void IDataRepository.Save()
        {
            var json = JsonUtility.ToJson(_dataModel);
            PlayerPrefs.SetString(s_key, json);
            PlayerPrefs.Save();
        }
    }
}
