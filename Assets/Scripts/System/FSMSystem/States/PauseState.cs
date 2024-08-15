public class PauseState : FSMState
{

    public PauseState()
    {
        stateID = StateID.PauseState;
        AddTransition(Transition.Restart, StateID.FightState);
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

