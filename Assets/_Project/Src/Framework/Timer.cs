using System;
using UnityEngine;

namespace Ransomink.Utils
{
    [Serializable]
    public class Timer
    {
        [SerializeField] protected bool  useUnscaledTime;
        [SerializeField] protected float startTime;
        [SerializeField] protected float duration;
        [SerializeField] protected float endTime;

        public Timer() {}

        public Timer(float time) => NewDuration(time);

        public bool UnscaledTime { get => useUnscaledTime; set => value = useUnscaledTime; }

        /// <summary>
        /// See if the current time is greater than or equal to the end time.
        /// </summary>
        public bool IsDone => this.Time >= endTime;

        /// <summary>
        /// Get the current time.
        /// </summary>
        private float Time => useUnscaledTime ? UnityEngine.Time.unscaledTime : UnityEngine.Time.time;

        /// <summary>
        /// Get the percentage done (helpful for Lerp methods).
        /// </summary>
        public float PercentageDone => Mathf.InverseLerp(startTime, endTime, Time);

        /// <summary>
        /// Get the percentage done with a SmoothStep applied.
        /// </summary>
        public float PercentageDoneSmoothStep => Mathf.SmoothStep(0f, 1f, PercentageDone);

        /// <summary>
        /// Set a new duration, effectively resetting the timer.
        /// </summary>
        /// <param name="newDuration">New timer duration.</param>
        public void NewDuration(float newDuration)
        {
            startTime = Time;
            duration  = newDuration;
            endTime   = startTime + duration;
        }

        /// <summary>
        /// Extend the timer if needed.
        /// </summary>
        /// <param name="addDuration">Amount to extend.</param>
        public void ExtendDuration(float addDuration)
        {
            startTime += addDuration;
            endTime   += addDuration;
        }

        /* public void GlobalTimerUpdate()
        {
            var now = Time.time;
            var i   = 0;

            while (i < delays.count)
            {
                var cur = struct[i];
                if (cur.start + cur.delay < now)
                {   
                    curr.action?.Invoke();
                    delays.RemoveAt(i);
                    continue;
                }  
                i++;
            }
        } */
    }
}
