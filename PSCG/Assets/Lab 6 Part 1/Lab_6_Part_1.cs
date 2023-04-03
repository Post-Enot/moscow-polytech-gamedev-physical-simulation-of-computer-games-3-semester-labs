using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_6_Part_1 : TaskBase
    {
        [SerializeField] private Rigidbody _sphere1_rigidbody;
        [SerializeField] private Rigidbody _sphere2_rigidbody;

        public float Sphere1_Mass { get; set; }
        public float Sphere1_Velocity { get; set; }
        public float Sphere2_Mass { get; set; }
        public float Sphere2_Velocity { get; set; }
        public float Deflection { get; set; }

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            _sphere1_rigidbody.Sleep();
            _sphere2_rigidbody.Sleep();
            yield return null;
            _sphere1_rigidbody.mass = Sphere1_Mass;
            _sphere1_rigidbody.velocity = new Vector3(Sphere1_Velocity, 0, 0);
            _sphere1_rigidbody.angularVelocity = new Vector3(Deflection, 0, 0);
            _sphere2_rigidbody.mass = Sphere2_Mass;
            _sphere2_rigidbody.velocity = new Vector3(-Sphere2_Velocity, 0, 0);
            _sphere1_rigidbody.ResetInertiaTensor();
            _sphere2_rigidbody.ResetInertiaTensor();
            yield return null;
        }

    }
}
