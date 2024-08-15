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

        #region 46-���˱��� 88-�ƽ������
        // ÿʰȡ300��ң����һ�����Ʒ�ʵı��䡣��95%ϡ�У�4%ʷʫ��1%��˵��
        if (GameManager.Instance.HasItem(ItemEnum.���˱���))
        {
            if (GameManager.Instance.gameData.collectMoney > 0 && 
                GameManager.Instance.gameData.collectMoney % 300 == 0)
            {
                DropEnum drop = (DropEnum)(2 + RandomUtil.RandomIndexWithProbablity(Constants.���˱�������));
                GameManager.Instance.GenerateDrop(drop, (Vector3)transform.position);
                //Instantiate(AssetManager.Instance.DropPrefab[chestIndex], transform.position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer);
            }
        }
        // ʰȡ���ʱ����20 % �ĸ��ʻָ���10 + 10 % ���룩������ֵ
        if (GameManager.Instance.HasItem(ItemEnum.�ƽ������))
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
