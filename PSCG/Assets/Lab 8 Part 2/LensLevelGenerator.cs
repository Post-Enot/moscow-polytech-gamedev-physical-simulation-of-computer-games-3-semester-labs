using System;
using System.Collections.Generic;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class LensLevelGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _leftUpCorner;
        [SerializeField] private Transform _rightDownCorner;

        [SerializeField] private GameObject _mirrorPrefab;
        [SerializeField] private GameObject _lensPrefab;

        private List<GameObject> _objects = new();

        public void ClearLevel()
        {
            foreach (GameObject gObject in _objects)
            {
                Destroy(gObject);
            }
            _objects.Clear();
        }

        public void GenerateLevel()
        {
            ClearLevel();
            int n = UnityEngine.Random.Range(2, 11);
            for (int i = 0; i < n; i += 1)
            {
                Vector3 position = new(
                    x: UnityEngine.Random.Range(_rightDownCorner.position.x, _leftUpCorner.position.x),
                    y: UnityEngine.Random.Range(_leftUpCorner.position.y, _rightDownCorner.position.y),
                    z: 0);
                float angle = UnityEngine.Random.Range(0, 180);

                if (UnityEngine.Random.value < 0.5f)
                {
                    SpawnMirror(position, angle);
                }
                else
                {
                    SpawnLens(position, angle);
                }
            }
        }

        private void SpawnLens(Vector3 position, float angleInDegrees)
        {
            LensData lensData = new LensData()
            {
                InputLensType = GetRandomLensType(),
                OutputLensType = GetRandomLensType(),
                ThicknessInUnit = 0.25f,
                HeightInUnit = 2f,
                RefractiveIndex = UnityEngine.Random.Range(1f, 2f),
                SagittalSize = 0.5f
            };
            GameObject prefabObject = Instantiate(
                _lensPrefab,
                position,
                Quaternion.Euler(0, 0, angleInDegrees));
            _objects.Add(prefabObject);
            LensPresenter lensPresenter = prefabObject.GetComponent<LensPresenter>();
            lensPresenter.Init(lensData);
        }

        private LensType GetRandomLensType()
        {
            int randomValue = UnityEngine.Random.Range(0, 3);
            return randomValue switch
            {
                0 => LensType.Flat,
                1 => LensType.Concave,
                2 => LensType.Convex,
                _ => throw new ArgumentOutOfRangeException(nameof(randomValue))
            };
        }

        private void SpawnMirror(Vector3 position, float angleInDegrees)
        {
            GameObject prefabObject = Instantiate(
                _mirrorPrefab,
                position,
                Quaternion.Euler(0, 0, angleInDegrees));
            _objects.Add(prefabObject);
        }
    }
}
