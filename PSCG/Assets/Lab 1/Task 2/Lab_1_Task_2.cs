using IUP.Toolkits.CoroutineShells;
using System.Collections;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_2 : TaskBase
    {
        #region Input
        public Vector2 StartPosition { get; set; }
        public Vector2 SpeedUnitPerSecond { get; set; }
        #endregion

        #region Output
        public Vector2 CurrentPosition { get; private set; }
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
            TimeInSecond = 0;
            PathInUnit = 0;

            float startTime = Time.time;

            while (true)
            {
                yield return null;
                TimeInSecond = Time.time - startTime;
                CurrentPosition = StartPosition + (SpeedUnitPerSecond * TimeInSecond);
                PathInUnit = SpeedUnitPerSecond.magnitude * TimeInSecond;
            }
        }
    }
}
