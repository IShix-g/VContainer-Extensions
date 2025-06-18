
using System;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class OpenAppTimeTracker : IPreSave
    {
        public DateTime SessionStartTime { get; private set; }

        readonly IDataRepository _dataRepository;

        public OpenAppTimeTracker(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            SessionStartTime = DateTime.Now;
        }

        public double SessionDuration()
        {
            var sessionDuration = DateTime.Now - SessionStartTime;
            return sessionDuration.TotalSeconds;
        }

        public double SessionTotalDuration()
        {
            var model = _dataRepository.Get();
            var duration = SessionDuration();
            return model.OpenAppTime + duration;
        }

        void IPreSave.OnBeforeSave()
        {
            var model = _dataRepository.Get();
            var totalDuration = SessionTotalDuration();
            model.OpenAppTime = totalDuration;
            SessionStartTime = DateTime.Now;
        }
    }
}
