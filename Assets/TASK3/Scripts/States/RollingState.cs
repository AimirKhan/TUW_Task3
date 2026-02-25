using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace TASK3.Scripts.States
{
    [State("RollingState")]
    public class RollingState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("Enter Rolling state");
            Model.EventManager.Invoke("RollItems");
            Model.Set("EnableStartButton", false);
            Model.Set("EnableStopButton", false);
        }

        [One(3)]
        public void BlockTimer()
        {
            Log.Debug("Stop button available");
            Model.Set("EnableStopButton", true);
        }

        [Bind("OnBtn")]
        public void OnStopButtonClicked(string btnName)
        {
            if (btnName == "StopMachine")
            {
                Log.Debug("Stop Roll");
                Parent.Change("StoppingState");
            }
        }
        
        [Exit]
        public void Exit()
        {
            Log.Debug("Exit Rolling state");
        }
    }
}
