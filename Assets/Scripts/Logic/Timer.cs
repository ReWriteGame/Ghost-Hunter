using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Logic.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private float currentTime = 0;
        [SerializeField] private float minTime = 0;
        [SerializeField] private float maxTime = 10;

        //[SerializeField] private float timeBetweenCallsChangedTimeEvent = 1;
        [SerializeField] private bool startOnAwake = true;
        [SerializeField] private bool countdown = true;

        public UnityEvent startTimeEvent;
        public UnityEvent stopTimeEvent;
        public UnityEvent isMinTimeEvent;

        public UnityEvent isMaxTimeEvent;
        //public UnityEvent pauseTimeEvent;
        //public UnityEvent changedTimeEvent;


        private Coroutine activeTimerLinkCor = null;
        private float startTime;

        public float CurrentTime
        {
            get => currentTime;
            private set => currentTime = value;
        }

        public float MinTime
        {
            get => minTime;
            private set => minTime = value;
        }

        public float MaxTime
        {
            get => maxTime;
            private set => maxTime = value;
        }

        public float StartTime
        {
            get => startTime;
            private set => startTime = value;
        }


        private void Start()
        {
            startTime = currentTime;
            if (startOnAwake) StartTimer();
        }

        public void StartTimer()
        {
            StopTimer();
            activeTimerLinkCor = StartCoroutine(TimerCor());
            startTimeEvent?.Invoke();
        }

        public void StartTimer(int currentTime)
        {
            StopTimer();
            this.currentTime = currentTime;
            activeTimerLinkCor = StartCoroutine(TimerCor());
            startTimeEvent?.Invoke();
        }

        public void StopTimer()
        {
            if (activeTimerLinkCor != null)
                StopCoroutine(activeTimerLinkCor);
            stopTimeEvent?.Invoke();
        }

        public void AddTime(float value)
        {
            if (value < 0) return;
            currentTime = (currentTime + value) >= maxTime ? maxTime : (currentTime + value);
            TimeIsMax();
        }

        public void TakeAwayTime(float value)
        {
            if (value < 0) return;
            currentTime = (currentTime - value) <= minTime ? minTime : (currentTime - value);
            TimeIsMin();
        }

        public bool TimeIsMin()
        {
            if (currentTime == minTime) isMinTimeEvent?.Invoke();
            return currentTime == minTime;
        }

        public bool TimeIsMax()
        {
            if (currentTime == maxTime) isMaxTimeEvent?.Invoke();
            return currentTime == maxTime;
        }

        private IEnumerator TimerCor()
        {
            while (true)
            {
                if (countdown)
                {
                    currentTime = currentTime >= minTime ? (currentTime - Time.deltaTime) : minTime;
                    if (TimeIsMin()) yield break;
                }
                else
                {
                    currentTime = currentTime <= maxTime ? (currentTime + Time.deltaTime) : maxTime;
                    if (TimeIsMax()) yield break;
                }

                yield return null; // pause on 1 frame
            }

            yield break;
        }
    }
}