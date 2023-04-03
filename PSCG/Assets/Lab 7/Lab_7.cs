using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_7 : TaskBase
    {
        [field: Header("References:")]
        [field: SerializeField] public Rigidbody SphereRigidbody { get; private set; }

        [field: Header("Physic materials:")]
        [field: SerializeField] public PhysicMaterial Floor1_PhysicMaterial { get; private set; }
        [field: SerializeField] public PhysicMaterial Floor2_PhysicMaterial { get; private set; }
        [field: SerializeField] public PhysicMaterial Floor3_PhysicMaterial { get; private set; }

        [field: Header("Start positions:")]
        [field: SerializeField] public Transform StartPositionOnFloor1 { get; private set; }
        [field: SerializeField] public Transform StartPositionOnFloor2 { get; private set; }
        [field: SerializeField] public Transform StartPositionOnFloor3 { get; private set; }

        public float StartVelocity { get; set; }
        public float A { get; set; }
        public Transform StartPosition { get; set; }

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            SphereRigidbody.Sleep();
            yield return null;
            SphereRigidbody.transform.SetPositionAndRotation(StartPosition.position, Quaternion.identity);

            float currentTime;
            float startTime = Time.time;

            int velocityFactor;
            if ((StartPosition == StartPositionOnFloor1 || StartPosition == StartPositionOnFloor2))
            {
                velocityFactor = 1;
            }
            else
            {
                velocityFactor = -1;
            }

            Vector3 startVelocity = new(velocityFactor * StartVelocity, 0, 0);
            SphereRigidbody.velocity = startVelocity;

            while (true)
            {
                yield return null;
                currentTime = Time.time - startTime;
                SphereRigidbody.velocity = new Vector3(
                    x: SphereRigidbody.velocity.x + A * currentTime * velocityFactor,
                    y: 0,
                    z: 0);
            }
        }
    }
}
