using System.Collections;
using System.Collections.Generic;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class Lab_6_Part_2 : TaskBase
    {
        [SerializeField] private Vector3 _sphereStartPosition;
        [SerializeField] private DestinationPoint _destinationPoint;
        [SerializeField] private Rigidbody _sphereRigidbody;
        [SerializeField] private Transform _aCorner;
        [SerializeField] private Transform _bCorner;

        [Header("Barrier prefabs:")]
        [SerializeField] private GameObject _staticBarrier;
        [SerializeField] private GameObject _dynamicBarrier;
        [SerializeField] private GameObject _acceleratingBarrier;

        public float Velocity { get; set; }
        public float Angle { get; set; }

        private readonly List<GameObject> _barriers = new();

        private void Awake()
        {
            _simulation = new CoroutineShell(this, Simulation);
        }

        protected override IEnumerator Simulation()
        {
            _sphereRigidbody.Sleep();
            ClearField();
            GenerateField();
            yield return null;
            _sphereRigidbody.transform.SetPositionAndRotation(_sphereStartPosition, Quaternion.Euler(0, Angle + 90, 0));
            _sphereRigidbody.velocity = Velocity * _sphereRigidbody.transform.forward;

            _destinationPoint.ResetMaterial();
            yield return null;
        }

        private void GenerateField()
        {
            int barrierCount = Random.Range(3, 10);
            for (int i = 0; i < barrierCount; i += 1)
            {
                int barrierTypeID = Random.Range(0, 3);
                GameObject prefab = barrierTypeID switch
                {
                    0 => _staticBarrier,
                    1 => _dynamicBarrier,
                    2 => _acceleratingBarrier,
                    _ => throw new System.ArgumentOutOfRangeException(nameof(barrierTypeID))
                };
                Vector3 position = new()
                {
                    x = Random.Range(_aCorner.position.x, _bCorner.position.x),
                    z = Random.Range(_aCorner.position.z, _bCorner.position.z)
                };
                Quaternion rotation = Quaternion.Euler(
                    x: 0,
                    y: Random.Range(0f, 360f),
                    z: 0);
                GameObject barrier = Instantiate(prefab, position, rotation);
                _barriers.Add(barrier);
            }
        }

        private void ClearField()
        {
            foreach (GameObject barrier in _barriers)
            {
                Destroy(barrier);
            }
            _barriers.Clear();
        }
    }
}
