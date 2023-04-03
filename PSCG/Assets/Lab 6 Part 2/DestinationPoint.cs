using UnityEngine;

namespace PSCG.Labs
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class DestinationPoint : MonoBehaviour
    {
        [SerializeField] private Material _redMaterial;
        [SerializeField] private Material _greenMaterial;

        private MeshRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            _renderer.material = _greenMaterial;
        }

        public void ResetMaterial()
        {
            _renderer.material = _redMaterial;
        }
    }
}
