public class LoadState : FSMState
{

    public LoadState()
    {
        stateID = StateID.LoadState;
        AddTransition(Transition.ContinueGame, StateID.BuyState);
        AddTransition(Transition.StartGame, StateID.FightState);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
    }
}

