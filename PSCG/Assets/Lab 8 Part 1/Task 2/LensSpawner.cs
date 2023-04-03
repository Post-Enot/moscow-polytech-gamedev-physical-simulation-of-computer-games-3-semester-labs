using UnityEngine;

namespace PSCG.Labs
{
    public sealed class LensSpawner : MonoBehaviour
    {
        [SerializeField] private float _thicknessInUnit;
        [SerializeField] private float _heightInUnit;
        [SerializeField] private float _saggitalSize;
        [SerializeField] private GameObject _lensPrefab;
        [SerializeField] private Transform _lensPosition;

        private LensPresenter _lensPreseter;

        public void DeleteLens()
        {
            if (_lensPreseter != null)
            {
                Destroy(_lensPreseter.gameObject);
            }
        }

        public void SpawnLens(
            LensType inputLensType,
            LensType outputLensType,
            float lensRefractiveIndex)
        {
            Debug.Log($"{nameof(inputLensType)}: {inputLensType}; {nameof(outputLensType)}: {outputLensType}");
            LensData lensData = new LensData()
            {
                InputLensType = inputLensType,
                OutputLensType = outputLensType,
                ThicknessInUnit = _thicknessInUnit,
                HeightInUnit = _heightInUnit,
                RefractiveIndex = lensRefractiveIndex,
                SagittalSize = _saggitalSize
            };
            GameObject lensObject = Instantiate(_lensPrefab, _lensPosition);
            _lensPreseter = lensObject.GetComponent<LensPresenter>();
            _lensPreseter.Init(lensData);
        }
    }
}
