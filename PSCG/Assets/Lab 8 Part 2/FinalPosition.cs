using UnityEngine;

namespace PSCG.Labs
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class FinalPosition : MonoBehaviour
    {
        [SerializeField] private Material _greenMaterial;
        [SerializeField] private Material _redMaterial;

        private MeshRenderer _meshRenderer;
        private bool _isActivate;

        public void Activate()
        {
            _meshRenderer.material = _greenMaterial;
            _isActivate = true;
        }

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Update()
        {
            if (_isActivate)
            {
                _isActivate = false;
                return;
            }
            else
            {
                _meshRenderer.material = _redMaterial;
            }
        }
    }
}
