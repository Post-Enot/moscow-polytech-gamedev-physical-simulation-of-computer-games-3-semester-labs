using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    [RequireComponent(typeof(Lab_6_Part_2), typeof(Lab_6_Part_2_UI))]
    public class Lab_6_Part_2_View : TaskViewBase
    {
        private Lab_6_Part_2 _task;
        private Lab_6_Part_2_UI _ui;

        private void Awake()
        {
            _task = GetComponent<Lab_6_Part_2>();
            _ui = GetComponent<Lab_6_Part_2_UI>();
        }

        private void Start()
        {
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.AngleField.RegisterValueChangedCallback(context => _task.Angle = context.newValue);
            _ui.VelocityField.RegisterValueChangedCallback(context => _task.Velocity = context.newValue);

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
            yield return null;
        }
    }
}
