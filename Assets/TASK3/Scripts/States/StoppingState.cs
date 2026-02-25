using AxGrid;
using AxGrid.FSM;
using AxGrid.Model;

namespace TASK3.Scripts.States
{
    [State("StoppingState")]
    public class StoppingState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("Enter Stopping state");
            Model.EventManager.Invoke("SetWinItem");
            Model.EventManager.Invoke("StopRollItems");
            Model.Set("EnableStartButton", false);
            Model.Set("EnableStopButton", false);
        }
    
        [Bind("ItemsRollStopped")]
        public void ItemsRollStopped()
        {
            Log.Debug("Rolling stopped");
            Parent.Change("ResultState");
        }
        
        [Exit]
        public void Exit()
        {
            Log.Debug("Exit Stopping state");
        }
    }
}
