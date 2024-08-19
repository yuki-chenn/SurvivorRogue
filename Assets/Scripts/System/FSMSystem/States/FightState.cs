using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightState : FSMState
{
    private GameData gameData { get { return GameManager.Instance.gameData; } } 

    private float overClk;


    public FightState()
    {
        stateID = StateID.FightState;
        AddTransition(Transition.Pause, StateID.PauseState);
        AddTransition(Transition.WaveEnd, StateID.BuyState);
        AddTransition(Transition.PlayerDie, StateID.GameoverState);
        AddTransition(Transition.Clear20Wave, StateID.GameClearState);
        gameData.curWave = 0;
        overClk = gameData.waveTime;
    }

    public override void DoBeforeEntering()
    {
        base.DoBeforeEntering();
        AudioManager.Instance.PlayGameBGM();

        overClk = gameData.waveTime;
        gameData.curWave++;
        // 更新敌人生成
        GameManager.Instance.GenerateEnemySpawner(gameData.curWave);

        // 实例化玩家对象,更新玩家的武器道具
        GameManager.Instance.ResetPlayer();

        // 清空宝箱掉落记录
        gameData.ClearChestCount();
        
        // 调用进入wave前的buff
        BuffOnWaveStart();



        // 更新波次UI
        GameUIManager.Instance.UpdateWaveCount(gameData.curWave);
        GameUIManager.Instance.UpdateMoney(gameData.money);
        GameUIManager.Instance.UpdateLevelExp(gameData.level,gameData.curExp,gameData.nextLevelExp);

        
    }

    public override void DoBeforeLeaving()
    {
        base.DoBeforeLeaving();
        // 执行所有buff
        BuffOnWaveEnd();
        // 需要把场景中 人物、掉落物、敌人 等物体都清掉
        GameManager.Instance.RemoveObjectInScene();

        GameManager.Instance.BeforeEnterShop();

        // 增加收入
        GameManager.Instance.gameData.money += GameManager.Instance.gameData.salary;
        GameUIManager.Instance.UpdateMoney(GameManager.Instance.gameData.money);

        #region 54-士兵棋子 57-主教棋子 58皇后棋子
        // 需要在回合结束时卖掉
        while (GameManager.Instance.HasItem(ItemEnum.士兵棋子))
        {
            GameManager.Instance.SaleItem((int)ItemEnum.士兵棋子);
        }
        while (GameManager.Instance.HasItem(ItemEnum.主教棋子))
        {
            GameManager.Instance.SaleItem((int)ItemEnum.主教棋子);
        }
        while (GameManager.Instance.HasItem(ItemEnum.皇后棋子))
        {
            GameManager.Instance.SaleItem((int)ItemEnum.皇后棋子);
        }
        #endregion

    }

    public override void Act()
    {

        // 游戏结束
        if (GameManager.Instance.playerGo.GetComponent<Player>().curHp <= 0)
        {
            Debug.Log("Die");
            GameManager.Instance.fsm.PerformTransition(Transition.PlayerDie);
        }

        // 本轮波次结束
        if(overClk <= 0)
        {
            // 如果当前波次是20波，通关
            if(GameManager.Instance.gameData.curWave == Constants.ENDLESS_WAVE)
            {
                GameManager.Instance.fsm.PerformTransition(Transition.Clear20Wave);
            }
            else
            {
                GameManager.Instance.fsm.PerformTransition(Transition.WaveEnd);
            }
        }

        overClk -= Time.deltaTime;
        GameUIManager.Instance.UpdateWaveCountDown(overClk);

    }

    private void BuffOnWaveStart()
    {
        var buffList = GameManager.Instance.Player.buffList.buffs;
        var buffsToProcess = new List<BaseBuff>(buffList);

        foreach (var buff in buffsToProcess)
        {
            buff.OnWaveStart();
        }
    }

    private void BuffOnWaveEnd()
    {
        var buffList = GameManager.Instance.Player.buffList.buffs;
        var buffsToProcess = new List<BaseBuff>(buffList);

        foreach (var buff in buffsToProcess)
        {
            buff.OnWaveEnd();
        }
    }
}

