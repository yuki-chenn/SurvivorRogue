using System;


// 每拥有5金币，在战斗开始时在附近生成1金币（最多生成50个）
[Serializable]
public class 金盏花之力 : BaseBuff
{
    public override int ID => 12;

    public override void OnWaveStart()
    {
        base.OnWaveStart();
        int generateCount = Math.Min(GameManager.Instance.gameData.money / 5, 50);
        for(int i = 0; i < generateCount; ++i)
        {
            GameManager.Instance.GenerateDrop(DropEnum.Money, GameManager.Instance.playerGo.transform.position);
        }
    }

}
