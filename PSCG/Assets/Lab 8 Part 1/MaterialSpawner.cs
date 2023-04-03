using System.Collections.Generic;
using UnityEngine;

namespace PSCG.Labs
{
    public sealed class MaterialSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _materialPrefab;
        [SerializeField] private float _xStartPosition;
        [SerializeField] private float _ySpawnOffset;

        private List<GameObject> _materials = new();

        public void SpawnMaterials(List<MaterialData> materialsData)
        {
            float xStartPosition = _xStartPosition;
            foreach (MaterialData materialData in materialsData)
            {
                float xSpawnPosition = xStartPosition + (materialData.ThicknessInUnit / 2);
                xStartPosition += materialData.ThicknessInUnit;
                SpawnMaterial(xSpawnPosition, materialData);
            }
        }

        public void DeleteMaterials()
        {
            foreach (GameObject material in _materials)
            {
                Destroy(material);
            }
            _materials.Clear();
        }

        private void SpawnMaterial(float xSpawnPosition, MaterialData materialData)
        {
            Vector3 spawnPoint = new(xSpawnPosition, _ySpawnOffset, 0);
            GameObject material = Instantiate(_materialPrefab, spawnPoint, Quaternion.identity);
            MaterialPresenter materialView = material.GetComponent<MaterialPresenter>();
            materialView.Init(materialData);
            _materials.Add(material);
        }
    }
}
