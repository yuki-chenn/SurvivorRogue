
public class BuyState : FSMState
{

    public BuyState()
    {
        stateID = StateID.BuyState;
        AddTransition(Transition.BuyEnd, StateID.FightState);
        AddTransition(Transition.BackMenu, StateID.ReadyState);
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        AudioManager.Instance.PlayShopBGM();

        // 展示开宝箱界面
        if (GameManager.Instance.gameData.chestCount[0] + 
           GameManager.Instance.gameData.chestCount[1] + 
           GameManager.Instance.gameData.chestCount[2] > 0)
        {
            EventCenter.Broadcast<int[]>(EventDefine.ShowOpenChestPanel,GameManager.Instance.gameData.chestCount);
        }

        // 存档
        GameManager.Instance.SaveGameData(GameManager.Instance.gameData.saveIndex);
        GameManager.Instance.OverridePlayerPrefs(GameManager.Instance.gameData.saveIndex);

        // 展示商店界面
        EventCenter.Broadcast(EventDefine.ShowShopPanel);

        GameUIManager.Instance.UpdateLevelExp(GameManager.Instance.gameData.level,
            GameManager.Instance.gameData.curExp, GameManager.Instance.gameData.nextLevelExp);
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        // 清空敌人
        GameManager.Instance.RemoveAllInContainer(ContainerManager.Instance.enemyContainer);

        // 清空掉落物
        GameManager.Instance.RemoveAllInContainer(ContainerManager.Instance.dropObjectContainer);
    }

}

