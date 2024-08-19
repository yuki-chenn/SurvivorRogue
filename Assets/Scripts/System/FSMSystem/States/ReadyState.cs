public class ReadyState : FSMState
{

    public ReadyState()
    {
        stateID = StateID.ReadyState;
        AddTransition(Transition.StartGame, StateID.FightState);
        AddTransition(Transition.LoadGame, StateID.LoadState);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        GameManager.Instance.ClearData();
        AudioManager.Instance.PlayMainMenuBGM();
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        // 设置玩家的数据
    }

}

