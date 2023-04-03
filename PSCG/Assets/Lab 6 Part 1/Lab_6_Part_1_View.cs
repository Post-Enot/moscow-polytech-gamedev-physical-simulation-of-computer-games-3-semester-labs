using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    [RequireComponent(typeof(Lab_6_Part_1), typeof(Lab_6_Part_1_UI))]
    public class Lab_6_Part_1_View : TaskViewBase
    {
        [SerializeField] private Vector3 _sphere1_StartPosition;
        [SerializeField] private Vector3 _sphere2_StartPosition;
        [SerializeField] private GameObject _sphere1;
        [SerializeField] private GameObject _sphere2;

        private Lab_6_Part_1 _task;
        private Lab_6_Part_1_UI _ui;

        private void Awake()
        {
            _task = GetComponent<Lab_6_Part_1>();
            _ui = GetComponent<Lab_6_Part_1_UI>();
        }

        private void Start()
        {
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.Sphere1_VelocityField.RegisterValueChangedCallback(context => _task.Sphere1_Velocity = context.newValue);
            _ui.Sphere1_MassField.RegisterValueChangedCallback(context => _task.Sphere1_Mass = context.newValue);
            _ui.Sphere2_VelocityField.RegisterValueChangedCallback(context => _task.Sphere2_Velocity= context.newValue);
            _ui.Sphere2_MassField.RegisterValueChangedCallback(context => _task.Sphere2_Mass = context.newValue);
            _ui.DiflectionField.RegisterValueChangedCallback(context => _task.Deflection = context.newValue);

            _ui.StartButton.clicked += () =>
            {
                if (!_task.IsSimulationStarted)
                {
                    _task.StartSimulation();
                }
            };
            _ui.StopButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
            };
        }

        protected override IEnumerator DisplaySimulation()
        {
            _sphere1.transform.position = _sphere1_StartPosition;
            _sphere2.transform.position = _sphere2_StartPosition;
            yield return null;
        }
    }
}
