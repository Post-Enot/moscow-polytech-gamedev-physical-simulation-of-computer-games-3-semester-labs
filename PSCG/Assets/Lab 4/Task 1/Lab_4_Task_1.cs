using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_4_Task_1 : TaskBase
    {
        public Vector3 StartPosition { get; set; }
        public Vector2 SpeedUnitInSecond { get; set; }
        public float TimeOffsetInSecond { get; set; }
        public float RadiusInUnit { get; set; }

        public Vector3 CurrentPosition { get; private set; }
        public float TimeInSecond { get; private set; }
        public float PathInUnit { get; private set; }

        private float _angularVelocity;
        private float _rotationFrequencyInSecond;

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            TimeInSecond = 0;
            float startTime = Time.time;
            float circumference = 2 * Mathf.PI * RadiusInUnit;
            _rotationFrequencyInSecond = 1 / (circumference / SpeedUnitInSecond.x);
            _angularVelocity = 2 * Mathf.PI * _rotationFrequencyInSecond;
            yield return new WaitForSeconds(TimeOffsetInSecond);
            while (true)
            {
                yield return null;
                TimeInSecond = Time.time - startTime;
                float rotationAngle = _angularVelocity * TimeInSecond;
                CurrentPosition = new Vector3(
                    x: StartPosition.x + RadiusInUnit * Mathf.Cos(rotationAngle),
                    z: StartPosition.z + RadiusInUnit * Mathf.Sin(rotationAngle),
                    y: StartPosition.y + SpeedUnitInSecond.y * TimeInSecond);
                PathInUnit = (TimeInSecond * _rotationFrequencyInSecond) * 2 * Mathf.PI * RadiusInUnit;
            }
        }
    }
}
