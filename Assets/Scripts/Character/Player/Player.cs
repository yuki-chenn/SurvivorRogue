using Survivor.Utils;
using UnityEngine;

public class Player : BaseCharacter
{
    protected Vector2 moveDir = Vector2.zero;
    private Transform wepaonParent;

    public PlayerAttribute attr;

    public float 技能冷却时间;
    private float 技能clk;

    // 记录前一次的位置
    private Vector2 pre = Vector2.zero;

    protected override void Start()
    {
        base.Start();
        wepaonParent = transform.Find("Weapons");
    }

    protected override void Update()
    {
        base.Update();
        // 更新技能
        if (技能clk > 0) 技能clk -= Time.deltaTime;
        GameUIManager.Instance.UpdateSkillCountdown(技能clk, 技能clk / 技能冷却时间);
        if (moveDir != Vector2.zero)
        {
            移动();
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        
    }

    private void OnEnable()
    {
        pre = Vector2.zero;
    }

    private void FixedUpdate()
    {
        计算移动距离();
    }

    private void 计算移动距离()
    {
        float dis = Vector2.Distance(transform.position, pre);
        pre = transform.position;

        #region 70-生命的旅程
        if (GameManager.Instance.HasItem(ItemEnum.生命的旅程))
        {
            GameManager.Instance.gameData.生命的旅程distance += dis;

            int currentDistance = (int)GameManager.Instance.gameData.生命的旅程distance;
            int previousDistance = (int)(GameManager.Instance.gameData.生命的旅程distance - dis);

            // Check if the player has crossed a 100-unit boundary
            if (currentDistance / 100 > previousDistance / 100)
            {
                GameManager.Instance.RestorePercentageHP(0.02f * GameManager.Instance.HasItemNum(ItemEnum.生命的旅程));
            }
        }
        #endregion

        GameManager.Instance.gameData.totalDistance += dis;
    }


    public void 重置技能冷却()
    {
        技能clk = -1;
    }

    public void SetMoveDir(Vector2 dir)
    {
        moveDir = dir.normalized;
    }

    protected override void OnHPChanged(float oldHp, float newHp)
    {
        base.OnHPChanged(oldHp, newHp);
        #region 84-破釜沉舟
        if (GameManager.Instance.HasItem(ItemEnum.破釜沉舟))
        {
            int 破釜沉舟_buff_id = 20;
            if (newHp / maxHp > 0.5f && oldHp / maxHp <= 0.5f)
            {
                buffList.RemoveBuff(破釜沉舟_buff_id);
            }
            else if(newHp / maxHp <= 0.5f && oldHp / maxHp > 0.5f)
            {
                buffList.AddBuff(破釜沉舟_buff_id, null, GameManager.Instance.playerGo, GameManager.Instance.HasItemNum(ItemEnum.破釜沉舟));
            }
        }
        #endregion

    }

    public virtual void OnPlayerReset(){}

    public virtual void 移动()
    {
        animator.SetBool("isMoving", true);
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        wepaonParent.localScale = GeometryUtil.GetDirectionScale(wepaonParent.localScale, moveDir, "x");
        wepaonParent.GetComponent<WeaponAutoRotate>().clockwise = wepaonParent.localScale.x < 0;
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.移动速度 * Time.deltaTime * 20);
    }

    public virtual void 使用技能()
    {
        if (技能clk > 0) return;
        animator.SetTrigger("useSkill");
        技能clk = 技能冷却时间;

        #region 拥有道具45-时间之瓶
        // 攻击速度会影响技能的冷却时间，攻击速度越快，冷却时间越短。（冷却时间不会大于原有的时间）
        if (GameManager.Instance.HasItem(ItemEnum.时间之瓶))
        {
            if(attr.attackSpeed > 100) 技能clk = 技能冷却时间 / (attr.attackSpeed / 100);
        }
        #endregion

        #region 59-破敌战斧 60-陨铁鸟 89-愈术符咒
        // 释放主动技能后，5s内造成的伤害额外提高4%
        if (GameManager.Instance.HasItem(ItemEnum.破敌战斧))
        {
            int 破敌战斧之力_buff_id = 9;
            buffList.AddBuff(破敌战斧之力_buff_id, null, gameObject, GameManager.Instance.HasItemNum(ItemEnum.破敌战斧));
        }
        // 释放主动技能后，5s内移动速度增加10%
        if (GameManager.Instance.HasItem(ItemEnum.陨铁鸟))
        {
            int 陨铁鸟之力_buff_id = 10;
            buffList.AddBuff(陨铁鸟之力_buff_id, null, gameObject, GameManager.Instance.HasItemNum(ItemEnum.陨铁鸟));
        }
        // 释放主动技能后，恢复（英雄等级/2）%最大生命的生命值
        if (GameManager.Instance.HasItem(ItemEnum.愈术符咒))
        {
            float value = GameManager.Instance.gameData.level / 2.0f * GameManager.Instance.HasItemNum(ItemEnum.愈术符咒);
            GameManager.Instance.RestorePercentageHP(value / 100.0f);
        }
        #endregion


    }

    public override void TakeDamage(DamageInfo damage)
    {
        base.TakeDamage(damage);

        #region 67-复仇护手
        // 每次受到攻击后，5s内增加2%的暴击率，最多增加20%
        if (GameManager.Instance.HasItem(ItemEnum.复仇护手))
        {
            int 复仇护手之力_buff_id = 15;
            buffList.AddBuff(复仇护手之力_buff_id, null, gameObject);
        }
        #endregion

        if (curHp <= 0) Die(5);
    }

    public override void InitAttribute()
    {
        base.InitAttribute();
        // 吸取道具范围设置
        transform.Find("PickupCollider").GetComponent<CircleCollider2D>().radius = GameManager.Instance.gameData.pickDis;
        // 属性设置
        attr = GameManager.Instance.gameData.playerAttr.DeepCopy();
        maxHp = attr.最大生命;
        curHp = maxHp;
        //if(GameManager.Instance.gameData.playerBuffs.buffs.Count == 0)
        //{
        //    // 说明不是load进来的
        //    GameManager.Instance.gameData.playerBuffs = buffList;
        //}
        //else
        //{
        //    // 说明是load进来的
        //    buffList = GameManager.Instance.gameData.playerBuffs;
        //}
        
    }

}