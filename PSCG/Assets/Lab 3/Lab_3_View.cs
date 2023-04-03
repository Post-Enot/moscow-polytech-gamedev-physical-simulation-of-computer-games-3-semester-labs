using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    public sealed class Lab_3_View : TaskViewBase
    {
        [SerializeField] private GameObject _sphere;
        [SerializeField] private Lab_3 _task;
        [SerializeField] private Lab_3_UI _ui;

        private Vector3 _positionOffset;

        private void Start()
        {
            _positionOffset = _sphere.transform.position;
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.T1Field.RegisterValueChangedCallback(T1VCC);
            _ui.T2Field.RegisterValueChangedCallback(T2VCC);
            _ui.A_Field.RegisterValueChangedCallback(A_VCC);
            _ui.B_Field.RegisterValueChangedCallback(B_VCC);
            _ui.StartPositionField.RegisterValueChangedCallback(StartPositionVCC);

            _ui.StartButton.clicked += () =>
            {
                if (!_task.IsSimulationStarted)
                {
                    _task.StartSimulation();
                }
            };
            _task.SimulationEnded += _task_SimulationEnded;
            _ui.StopButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
            };
        }

        private void _task_SimulationEnded()
        {
            _sphere.transform.position = _positionOffset +
                new Vector3(_task.CurrentPosition.x, _task.CurrentPosition.y, 0);
            _ui.TimeField.value = _task.TimeInSecond;
            _ui.PathField.value = _task.PathInUnit;
            _ui.SpeedField.value = _task.CurrentSpeed;
            _ui.AccelerationField.value = _task.CurrentAcceleration;
        }

        private void StartPositionVCC(ChangeEvent<Vector2> context)
        {
            _task.StartPosition = context.newValue;
        }

        private void A_VCC(ChangeEvent<Vector2> context)
        {
            _task.A = context.newValue;
        }

        private void B_VCC(ChangeEvent<Vector2> context)
        {
            _task.B = context.newValue;
        }

        private void T1VCC(ChangeEvent<float> context)
        {
            _task.Timestamp1 = context.newValue;
        }

        private void T2VCC(ChangeEvent<float> context)
        {
            _task.Timestamp2 = context.newValue;
        }

        protected override IEnumerator DisplaySimulation()
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                _sphere.transform.position = _positionOffset +
                    new Vector3(_task.CurrentPosition.x, _task.CurrentPosition.y, 0);
                _ui.TimeField.value = _task.TimeInSecond;
                _ui.PathField.value = _task.PathInUnit;
                _ui.SpeedField.value = _task.CurrentSpeed;
                _ui.AccelerationField.value = _task.CurrentAcceleration;
            }
        }
    }
}
