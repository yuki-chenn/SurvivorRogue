using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameoverState : FSMState
{

    public GameoverState()
    {
        stateID = StateID.GameoverState;
        AddTransition(Transition.Confirm, StateID.ReadyState);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        EventCenter.Broadcast(EventDefine.ShowGameOverPanel);
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        // TODO:按道理来说这边应该清除上把的游戏数据
        GameManager.Instance.ClearData();
    }

}

