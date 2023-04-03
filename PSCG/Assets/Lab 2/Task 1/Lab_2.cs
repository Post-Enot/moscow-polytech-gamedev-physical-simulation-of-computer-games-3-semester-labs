using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_2 : TaskBase
    {
        public float Radius { get; set; }
        public float RotationFrequencyInSecond { get; set; }

        public float TimeInSecond { get; private set; }
        public float AngularVelocity { get; private set; }
        public float LinearVelocity { get; private set; }
        public float PathInUnit { get; private set; }
        public float RotationAngle { get; private set; }
        public Vector2 CurrentPosition { get; private set; }
        public int RevolutionCount { get; private set; }

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            TimeInSecond = 0;
            LinearVelocity = 2 * Mathf.PI * Radius * RotationFrequencyInSecond;
            float startTime = Time.time;
            while (true)
            {
                yield return null;
                AngularVelocity = 2 * Mathf.PI * RotationFrequencyInSecond;
                TimeInSecond = Time.time - startTime;
                RevolutionCount = (int)(RotationFrequencyInSecond * TimeInSecond);
                RotationAngle = AngularVelocity * TimeInSecond;
                CurrentPosition = new Vector2(
                    x: Radius * Mathf.Cos(RotationAngle),
                    y: Radius * Mathf.Sin(RotationAngle));
                PathInUnit = (TimeInSecond * RotationFrequencyInSecond) * 2 * Mathf.PI * Radius;

                AngularVelocity *= Mathf.Rad2Deg;
                RotationAngle = AngularVelocity * Mathf.Rad2Deg * TimeInSecond % 360.0f;
            }
        }
    }
}
