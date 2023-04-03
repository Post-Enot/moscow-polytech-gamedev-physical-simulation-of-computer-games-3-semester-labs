using UnityEngine;
using IUP.Toolkits.CoroutineShells;
using System.Collections;

namespace PSCG.Labs
{
    public sealed class Lab_1_Task_1 : TaskBase
    {
        #region Input
        public Vector2 SpeedUnitPerSecond { get; set; }
        #endregion

        #region Output
        public Vector2 CurrentPosition { get; private set; }
        public float PathInUnit { get; private set; }
        public float TimeInSecond { get; private set; }
        #endregion

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            TimeInSecond = 0;
            PathInUnit = 0;
            CurrentPosition = Vector2.zero;

            float startTime = Time.time;
            
            while (true)
            {
                yield return null;
                TimeInSecond = Time.time - startTime;
                CurrentPosition = SpeedUnitPerSecond * TimeInSecond;
                PathInUnit = SpeedUnitPerSecond.magnitude * TimeInSecond;
            }
        }
    }
}
