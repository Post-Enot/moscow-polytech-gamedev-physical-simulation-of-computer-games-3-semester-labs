using System.Collections;
using IUP.Toolkits.CoroutineShells;
using PSCG.Labs;
using UnityEngine;

namespace PSCG
{
    public sealed class Lab_3 : TaskBase
    {
        #region Input
        public float Timestamp1 { get; set; }
        public float Timestamp2 { get; set; }
        public Vector2 StartPosition { get; set; }
        public Vector2 A { get; set; }
        public Vector2 B { get; set; }
        #endregion

        #region Output
        public float TimeInSecond { get; private set; }
        public float PathInUnit { get; private set; }
        public Vector2 CurrentSpeed { get; private set; }
        public Vector2 CurrentAcceleration { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        #endregion

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            TimeInSecond = 0;
            PathInUnit = 0;
            CurrentSpeed = Vector2.zero;
            CurrentPosition = StartPosition;

            float startTime = Time.time;
            Vector2 previousPosition = StartPosition;

            while (TimeInSecond < Timestamp2)
            {
                yield return null;
                TimeInSecond = Time.time - startTime;
                if (TimeInSecond >= Timestamp1)
                {
                    if (TimeInSecond > Timestamp2)
                    {
                        TimeInSecond = Timestamp2;
                    }
                    float time = TimeInSecond - Timestamp1;
                    CurrentAcceleration = A + (B * time);
                    CurrentSpeed = CurrentAcceleration * time;
                    CurrentPosition = StartPosition + (Mathf.Pow(time, 2) * CurrentAcceleration / 2);
                    PathInUnit += (CurrentPosition - previousPosition).magnitude;
                    previousPosition = CurrentPosition;
                }
            }
            StopSimulation();
        }
    }
}
