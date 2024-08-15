
public class GameClearState : FSMState
{

    public GameClearState()
    {
        stateID = StateID.GameClearState;
        AddTransition(Transition.Confirm, StateID.ReadyState);
        AddTransition(Transition.Endless, StateID.BuyState);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        EventCenter.Broadcast(EventDefine.ShowGameClearPanel);
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
    }

}

