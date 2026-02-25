using AxGrid;
using AxGrid.FSM;

namespace TASK3.Scripts.States
{
    [State("InitMachineState")]
    public class InitMachineState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("Enter Init state");
            Model.Set("EnableStartButton", false);
            Model.Set("EnableStopButton", false);
        }

        [One(1)]
        public void MachineReadyState()
        {
            Parent.Change("ReadyState");
        }
        
        [Exit]
        public void Exit()
        {
            Log.Debug("Exit Init State");
        }
    }
}