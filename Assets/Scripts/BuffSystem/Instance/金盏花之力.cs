using System;


// ÿӵ��5��ң���ս����ʼʱ�ڸ�������1��ң��������50����
[Serializable]
public class ��յ��֮�� : BaseBuff
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
