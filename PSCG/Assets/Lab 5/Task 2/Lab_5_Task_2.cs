using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_5_Task_2 : TaskBase
    {
        public float Height { get; set; }
        public float TimeOffset { get; set; }
        public float SingleAcceleration { get; set; }
        public float AngleToTheHorizon { get; set; }

        public float CurrentTime { get; private set; }
        public float FlightTime { get; set; }
        public float AverageSpeed { get; set; }
        public float LandingSpeed { get; set; }
        public float Distance { get; set; }

        public Vector2 CurrentPosition { get; private set; }

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            CurrentTime = 0;
            float startTime = Time.time;
            CurrentPosition = new Vector2(0, Height);

            float startSpeed = SingleAcceleration;
            float g = Mathf.Abs(Physics.gravity.y);

            yield return new WaitForSeconds(TimeOffset);

            while (CurrentPosition.y > 0)
            {
                yield return null;
                CurrentTime = Time.time - startTime;
                float x = startSpeed * Mathf.Cos(AngleToTheHorizon) * CurrentTime;
                float y = -g * Mathf.Pow(CurrentTime, 2) / 2;
                //float doubleSin = (1 - Mathf.Cos(2 * AngleToTheHorizon)) / 2;
                //float y = x * Mathf.Tan(AngleToTheHorizon) - (g * Mathf.Pow(x, 2) / 2 * Mathf.Pow(startSpeed, 2) * doubleSin);
                CurrentPosition = new Vector2(x, y);
                Distance = Mathf.Pow(startSpeed, 2) * Mathf.Sin(2 * AngleToTheHorizon) / g;
            }

            CurrentTime = 2 * startSpeed * Mathf.Sin(AngleToTheHorizon) / g;
            CurrentPosition = new Vector2(CurrentPosition.x, 0);
        }
    }
}
