using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace TASK3.Scripts.States
{
    [State("ReadyState")]
    public class ReadyState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("Enter Ready state");
            Model.Set("EnableStartButton", true);
            Model.Set("EnableStopButton", false);
        }

        [Bind("OnBtn")]
        public void OnStartButtonClicked(string btnName)
        {
            if (btnName == "StartMachine")
            {
                Log.Debug("Start Roll");
                Parent.Change("RollingState");
            }
        }
        
        [Exit]
        public void Exit()
        {
            Log.Debug("Exit Ready state");
        }
    }
}