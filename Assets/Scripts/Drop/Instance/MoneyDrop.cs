using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoneyDrop : BaseDrop
{
    public int money = 1;

    protected override void OnPickedUp()
    {
        base.OnPickedUp();
        GameManager.Instance.PickUpMoney(money);

        #region 46-财运背包 88-黄金奇异果
        // 每拾取300金币，获得一个随机品质的宝箱。（95%稀有，4%史诗，1%传说）
        if (GameManager.Instance.HasItem(ItemEnum.财运背包))
        {
            if (GameManager.Instance.gameData.collectMoney > 0 && 
                GameManager.Instance.gameData.collectMoney % 300 == 0)
            {
                DropEnum drop = (DropEnum)(2 + RandomUtil.RandomIndexWithProbablity(Constants.财运背包概率));
                GameManager.Instance.GenerateDrop(drop, (Vector3)transform.position);
                //Instantiate(AssetManager.Instance.DropPrefab[chestIndex], transform.position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer);
            }
        }
        // 拾取金币时，有20 % 的概率恢复（10 + 10 % 收入）的生命值
        if (GameManager.Instance.HasItem(ItemEnum.黄金奇异果))
        {
            if (RandomUtil.IsProbabilityMet(0.2f))
            {
                float value = 10 + 0.1f * GameManager.Instance.gameData.salary;
                GameManager.Instance.RestoreHP(value);
            }
        }
        #endregion
    }

}
