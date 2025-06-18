#if ENABLE_VCONTAINER
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class DataSaver : MonoBehaviour
    {
        IDataRepository _repository;
        IEnumerable<IPreSave> _savables;

        [Inject]
        public void Inject(
            IDataRepository repository,
            IEnumerable<IPreSave> savables
        )
        {
            _repository = repository;
            _savables = savables;
        }

        void Save()
        {
            foreach (var savable in _savables)
            {
                savable.OnBeforeSave();
            }
            _repository.Save();
        }

        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                Save();
            }
        }

        void OnApplicationQuit()
        {
            Save();
        }
    }
}
#endif
