using AxGrid;
using AxGrid.Base;
using AxGrid.FSM;
using AxGrid.Model;
using UnityEngine;

namespace TASK3.Scripts.States
{
    public class InitFsm : MonoBehaviourExt
    {
        [OnAwake]
        private void AwakeThis()
        {
            Log.Debug("Init Awake");
        }

        [OnStart]
        private void StartThis()
        {
            Log.Debug("Init Start");
            Settings.Fsm = new FSM();
            Settings.Fsm.Add(new InitMachineState());
            Settings.Fsm.Add(new ReadyState());
            Settings.Fsm.Add(new RollingState());
            Settings.Fsm.Add(new StoppingState());
            Settings.Fsm.Add(new ResultState());
            
            Settings.Fsm.Start("InitMachineState");
        }

        [OnUpdate]
        private void UpdateThis()
        {
            Settings.Fsm.Update(Time.deltaTime);
        }
    }
}
