using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_3 : TaskBase
    {
        #region Input
        public Vector2 Acceleration { get; set; }
        public Vector2 StartSpeedUnitPerSecond { get; set; }
        public Vector2 StartPosition { get; set; }
        #endregion

        #region Output
        public Vector2 CurrentPosition { get; private set; }
        public Vector2 CurrentSpeedUnitPerSecond { get; private set; }
        public float TimeInSecond { get; private set; }
        public float PathInUnit { get; private set; }
        #endregion

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            CurrentPosition = StartPosition;
            CurrentSpeedUnitPerSecond = StartSpeedUnitPerSecond;
            TimeInSecond = 0;
            PathInUnit = 0;

            Vector2 previousPosition = CurrentPosition;
            float startTime = Time.time;

            while (true)
            {
                yield return null;
                TimeInSecond = Time.time - startTime;
                CurrentSpeedUnitPerSecond = StartSpeedUnitPerSecond + (Acceleration * TimeInSecond);
                CurrentPosition = StartPosition +
                    (StartSpeedUnitPerSecond * TimeInSecond) +
                    (Mathf.Pow(TimeInSecond, 2) * Acceleration / 2);
                PathInUnit += (CurrentPosition - previousPosition).magnitude;
                previousPosition = CurrentPosition;
            }
        }
    }
}
