
using System;
using UnityEngine;
using UnityEngine.UI;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class OpenAppTimeText : MonoBehaviour
    {
        [SerializeField] Text _text;

        double _time;
        double _oneSecond;

        public void SetTime(double time)
        {
            var integer = Math.Truncate(time);
            var fractional = time - integer;
            _oneSecond = fractional;
            PrintTime(integer);
        }

        void PrintTime(double time)
        {
            if (Math.Abs(_time - time) == 0)
            {
                return;
            }
            _time = time;
            var dateTime = TimeSpan.FromSeconds(time);
            _text.text = dateTime.ToString(@"hh\:mm\:ss");
        }

        void Update()
        {
            _oneSecond += Time.deltaTime;
            if (_oneSecond < 1)
            {
                return;
            }
            _oneSecond = 0;
            PrintTime(_time + 1);
        }

        void Reset()
        {
            _text = GetComponent<Text>();
        }
    }
}
