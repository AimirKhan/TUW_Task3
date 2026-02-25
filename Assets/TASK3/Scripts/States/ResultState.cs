using AxGrid;
using AxGrid.FSM;

namespace TASK3.Scripts.States
{
    [State("ResultState")]
    public class ResultState : FSMState
    {
        [Enter]
        public void Enter()
        {
            Log.Debug("Enter result state");
        }
    
        
        [One(3)]
        public void AnimationTimer()
        {
            Log.Debug("Show result");
            Parent.Change("ReadyState");
        }
        
        [Exit]
        public void Exit()
        {
            Log.Debug("Exit result state");
        }
    }
}
