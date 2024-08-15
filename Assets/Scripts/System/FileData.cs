using Survivor.Utils;
using System;
using UnityEngine;

// 存档中需要记录的数据
[Serializable]
public class FileData
{
    public GameData gameData;

    // 玩家拥有的buff
    public BuffList playerBuffs;

    public FileData()
    {
        gameData = GameManager.Instance.gameData;
        if (GameManager.Instance.Player != null) playerBuffs = GameManager.Instance.Player.buffList;
        else playerBuffs = new BuffList();
    }

}

// 存档界面展示的一些数据
[Serializable]
public class FileShowData
{
    public int selectHeroSpriteIndex;

    public string saveTime;

    public int level;

    public int waveCount;

    public bool isEndless;

    public int money;

    // 单位是秒
    public int playTime;


    public FileShowData(GameData gameData)
    {
        saveTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        selectHeroSpriteIndex = TplUtil.GetHeroMap()[gameData.selectHeroId].Index;
        level = gameData.level;
        waveCount = gameData.curWave;
        isEndless = gameData.curWave >= Constants.ENDLESS_WAVE;
        money = gameData.money;

        playTime = 0;
    }

    public void SetNewData(GameData gameData)
    {
        level = gameData.level;
        waveCount = gameData.curWave;
        isEndless = gameData.curWave >= Constants.ENDLESS_WAVE;
        money = gameData.money;
        playTime = (int)gameData.playTime;
    }

}

