using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_5_Task_1 : TaskBase
    {
        public float SpeedUnitPerSecond { get; set; }
        public float HeightInUnit { get; set; }

        public Vector2 CurrentPosition { get; private set; }
        public float TimeInSecond { get; private set; }
        public float DistanceInUnit { get; private set; }

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            TimeInSecond = 0;
            CurrentPosition = new Vector2(x: 0, y: HeightInUnit);
            float startTime = Time.time;
            while (CurrentPosition.y > 0)
            {
                yield return null;
                TimeInSecond = Time.time - startTime;
                CurrentPosition = new Vector2(
                    x: SpeedUnitPerSecond * TimeInSecond,
                    y: HeightInUnit + (Physics.gravity.y * TimeInSecond * TimeInSecond) / 2);
                DistanceInUnit = SpeedUnitPerSecond * Mathf.Sqrt(2 * HeightInUnit / Mathf.Abs(Physics.gravity.y));
            }
            TimeInSecond = Mathf.Sqrt(2 * HeightInUnit / Mathf.Abs(Physics.gravity.y));
            CurrentPosition = new Vector2(CurrentPosition.x, 0);
        }
    }
}
