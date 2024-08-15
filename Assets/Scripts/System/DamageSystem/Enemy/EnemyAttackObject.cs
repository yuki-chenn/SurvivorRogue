using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������������еĹ������壬ֻ����ҽ�ɫ��ײ
public abstract class EnemyAttackObject : BaseAttackObject 
{

    protected Enemy _enemy;
    protected Enemy Enemy
    {
        get
        {
            if (_enemy == null) _enemy = source.GetComponent<Enemy>();
            return _enemy;
        }
    }
    protected EnemyAttribute EnemyAttr
    {
        get
        {
            if (_enemy == null) _enemy = source.GetComponent<Enemy>();
            return _enemy.attr;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.tag == "Player")
        {
            var amount = GetBaseDamageAmount();
            var player = collision.GetComponentInParent<Player>();
            var playerAttr = player.attr;

            DamageInfo dmgInfo = new DamageInfo(source, collision.transform.parent.gameObject, amount, 
                playerAttr.����, playerAttr.����, 0);

            OnBeforeEnemyGiveDamage(dmgInfo);
            OnBeforePlayerTakeDamage(player, dmgInfo);

            // �ж�dmg�Ƿ��ɱ��
            if (dmgInfo.Value >= player.curHp)
            {
                OnBeforePlayerKilled(player, dmgInfo);
            }

            if (dmgInfo.Value >= player.curHp)
            {
                OnEnemyKill();
                OnPlayerKilled(player);
            }

            player.TakeDamage(dmgInfo);

            if (!dmgInfo.isMiss)
            {
                if (dmgInfo.isCritical) OnEnemyCritical();
                OnEnemyHurt(dmgInfo);
                OnPlayerHurted(player,dmgInfo);
            }
            else
            {
                OnPlayerDodge(player);
                #region 91-��������
                if (GameManager.Instance.HasItem(ItemEnum.��������))
                {
                    int ��������_buff_id = 25;
                    player.buffList.AddBuff(��������_buff_id, null, GameManager.Instance.playerGo);
                }
                #endregion
            }



            if (autoDestory) Destroy(gameObject);
        }

    }

    private void OnBeforeEnemyGiveDamage(DamageInfo info)
    {
        var buffList = Enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnBeforeGiveDamage(info);
        }
    }

    private void OnBeforePlayerTakeDamage(Player player, DamageInfo info)
    {
        var buffList = player.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnBeforeTakeDamage(info);
        }
    }

    private void OnBeforePlayerKilled(Player player, DamageInfo info)
    {
        var buffList = player.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnBeforeKilled(info);
        }
    }

    private void OnEnemyKill()
    {
        var buffList = Enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnKill();
        }
    }

    private void OnEnemyCritical()
    {
        var buffList = Enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnCritical();
        }
    }

    private void OnPlayerKilled(Player player)
    {
        var buffList = player.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnKilled();
        }
    }

    private void OnPlayerDodge(Player player)
    {
        var buffList = player.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnDodge();
        }
    }

    private void OnEnemyHurt(DamageInfo info)
    {
        var buffList = Enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnHurt(info);
        }
    }

    private void OnPlayerHurted(Player player, DamageInfo info)
    {
        var buffList = player.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnHurted(info);
        }
    }

    protected abstract float GetBaseDamageAmount();
}
