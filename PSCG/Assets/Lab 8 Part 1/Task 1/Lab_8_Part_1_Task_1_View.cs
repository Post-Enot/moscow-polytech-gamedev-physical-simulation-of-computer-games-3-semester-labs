using System.Collections;
using IUP.Toolkits.CoroutineShells;
using UnityEngine;
using UnityEngine.UIElements;

namespace PSCG.Labs
{
    [RequireComponent(typeof(Lab_8_Part_1_Task_1), typeof(Lab_8_Part_1_Task_1_UI), typeof(MaterialSpawner))]
    public class Lab_8_Part_1_Task_1_View : TaskViewBase
    {
        private Lab_8_Part_1_Task_1 _task;
        private Lab_8_Part_1_Task_1_UI _ui;
        private MaterialSpawner _materialSpawner;

        private void Awake()
        {
            _task = GetComponent<Lab_8_Part_1_Task_1>();
            _ui = GetComponent<Lab_8_Part_1_Task_1_UI>();
            _materialSpawner = GetComponent<MaterialSpawner>();
        }

        private void Start()
        {
            _simulation = new CoroutineShell(this, DisplaySimulation);
            _task.SimulationStarted += StartSimulationDisplay;
            _task.SimulationEnded += StopSimulationDisplay;

            _ui.StartButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
                _task.StartSimulation();
                _materialSpawner.DeleteMaterials();
                _materialSpawner.SpawnMaterials(_task.Materials);
            };
            _ui.StopButton.clicked += () =>
            {
                if (_task.IsSimulationStarted)
                {
                    _task.StopSimulation();
                }
                _materialSpawner.DeleteMaterials();
            };

            _ui.RayIncidenceAngleField.RegisterValueChangedCallback(
                context => _task.RayAngle = context.newValue);
            _ui.RefractiveIndexField.RegisterValueChangedCallback(
                context => _task.RefractiveIndex = context.newValue);
            _task.RefractiveIndex = _ui.RefractiveIndexField.value;
            
            _ui.MaterialsListView.itemsSource = _task.Materials;
            _ui.MaterialsListView.makeItem = () => new MaterialDataView();
            _ui.MaterialsListView.bindItem = (VisualElement view, int materialIndex) =>
            {
                _task.Materials[materialIndex] = new MaterialData();
                (view as MaterialDataView).BindWith(_task.Materials[materialIndex]);
            };
            _ui.MaterialsListView.unbindItem = (VisualElement view, int materialIndex) =>
            (view as MaterialDataView).Unbind();
        }

        protected override IEnumerator DisplaySimulation()
        {
            yield return null;
        }
    }
}

