using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �������������еĹ������壬ֻ�����з���ҽ�ɫ��ײ
public abstract class PlayerAttackObject : BaseAttackObject 
{


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if(collision.tag == "Enemy")
        {
            var amount = GetBaseDamageAmount();
            var enemy = collision.GetComponentInParent<Enemy>();
            var enemyAttr = enemy.attr;
            var playerAttr = GameManager.Instance.Player.attr;

            DamageInfo dmgInfo = new DamageInfo(source, collision.transform.parent.gameObject,amount,enemyAttr.����,
                0,playerAttr.������,playerAttr.�����˺�);

            OnBeforePlayerGiveDamage(dmgInfo);
            OnBeforeEnemyTakeDamage(enemy, dmgInfo);

            // �ж�dmg�Ƿ��ɱ��
            if(dmgInfo.Value >= enemy.curHp)
            {
                OnBeforeEnemyKilled(enemy, dmgInfo);
            }

            if (dmgInfo.Value >= enemy.curHp)
            {
                OnPlayerKill();
                OnEnemyKilled(enemy);
            }

            enemy.TakeDamage(dmgInfo);

            if (!dmgInfo.isMiss)
            {
                if (dmgInfo.isCritical) OnPlayerCritical();
                OnPlayerHurt(dmgInfo);
                OnEnemyHurted(enemy,dmgInfo);
            }
            else
            {
                OnEnemyDodge(enemy);
            }

            if (autoDestory) Destroy(gameObject);
        }

    }

    private void OnBeforePlayerGiveDamage(DamageInfo info)
    {
        var buffList = source.GetComponent<Player>().buffList.buffs;
        foreach(var buff in buffList)
        {
            buff.OnBeforeGiveDamage(info);
        }
    }

    private void OnBeforeEnemyTakeDamage(Enemy enemy, DamageInfo info)
    {
        var buffList = enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnBeforeTakeDamage(info);
        }
    }

    private void OnBeforeEnemyKilled(Enemy enemy, DamageInfo info)
    {
        var buffList = enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnBeforeKilled(info);
        }
    }

    private void OnPlayerKill()
    {
        var buffList = source.GetComponent<Player>().buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnKill();
        }
    }

    private void OnPlayerCritical()
    {
        var buffList = source.GetComponent<Player>().buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnCritical();
        }
    }

    private void OnEnemyKilled(Enemy enemy)
    {
        var buffList = enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnKilled();
        }
    }

    private void OnEnemyDodge(Enemy enemy)
    {
        var buffList = enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnDodge();
        }
    }

    private void OnPlayerHurt(DamageInfo info)
    {
        var buffList = source.GetComponent<Player>().buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnHurt(info);
        }
    }

    private void OnEnemyHurted(Enemy enemy, DamageInfo info)
    {
        var buffList = enemy.buffList.buffs;
        foreach (var buff in buffList)
        {
            buff.OnHurted(info);
        }
    }

    protected abstract float GetBaseDamageAmount();
}
