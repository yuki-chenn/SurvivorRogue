using Survivor.Utils;
using UnityEngine;

public class Player : BaseCharacter
{
    protected Vector2 moveDir = Vector2.zero;
    private Transform wepaonParent;

    public PlayerAttribute attr;

    public float ������ȴʱ��;
    private float ����clk;

    // ��¼ǰһ�ε�λ��
    private Vector2 pre = Vector2.zero;

    protected override void Start()
    {
        base.Start();
        wepaonParent = transform.Find("Weapons");
    }

    protected override void Update()
    {
        base.Update();
        // ���¼���
        if (����clk > 0) ����clk -= Time.deltaTime;
        GameUIManager.Instance.UpdateSkillCountdown(����clk, ����clk / ������ȴʱ��);
        if (moveDir != Vector2.zero)
        {
            �ƶ�();
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
        �����ƶ�����();
    }

    private void �����ƶ�����()
    {
        float dis = Vector2.Distance(transform.position, pre);
        pre = transform.position;

        #region 70-�������ó�
        if (GameManager.Instance.HasItem(ItemEnum.�������ó�))
        {
            GameManager.Instance.gameData.�������ó�distance += dis;

            int currentDistance = (int)GameManager.Instance.gameData.�������ó�distance;
            int previousDistance = (int)(GameManager.Instance.gameData.�������ó�distance - dis);

            // Check if the player has crossed a 100-unit boundary
            if (currentDistance / 100 > previousDistance / 100)
            {
                GameManager.Instance.RestorePercentageHP(0.02f * GameManager.Instance.HasItemNum(ItemEnum.�������ó�));
            }
        }
        #endregion

        GameManager.Instance.gameData.totalDistance += dis;
    }


    public void ���ü�����ȴ()
    {
        ����clk = -1;
    }

    public void SetMoveDir(Vector2 dir)
    {
        moveDir = dir.normalized;
    }

    protected override void OnHPChanged(float oldHp, float newHp)
    {
        base.OnHPChanged(oldHp, newHp);
        #region 84-�Ƹ�����
        if (GameManager.Instance.HasItem(ItemEnum.�Ƹ�����))
        {
            int �Ƹ�����_buff_id = 20;
            if (newHp / maxHp > 0.5f && oldHp / maxHp <= 0.5f)
            {
                buffList.RemoveBuff(�Ƹ�����_buff_id);
            }
            else if(newHp / maxHp <= 0.5f && oldHp / maxHp > 0.5f)
            {
                buffList.AddBuff(�Ƹ�����_buff_id, null, GameManager.Instance.playerGo, GameManager.Instance.HasItemNum(ItemEnum.�Ƹ�����));
            }
        }
        #endregion

    }

    public virtual void OnPlayerReset(){}

    public virtual void �ƶ�()
    {
        animator.SetBool("isMoving", true);
        transform.localScale = GeometryUtil.GetDirectionScale(transform.localScale, moveDir, "x");
        wepaonParent.localScale = GeometryUtil.GetDirectionScale(wepaonParent.localScale, moveDir, "x");
        wepaonParent.GetComponent<WeaponAutoRotate>().clockwise = wepaonParent.localScale.x < 0;
        UItrans.localScale = GeometryUtil.GetDirectionScale(UItrans.localScale, moveDir, "x");
        rigidBody.AddForce(moveDir * attr.�ƶ��ٶ� * Time.deltaTime * 20);
    }

    public virtual void ʹ�ü���()
    {
        if (����clk > 0) return;
        animator.SetTrigger("useSkill");
        ����clk = ������ȴʱ��;

        #region ӵ�е���45-ʱ��֮ƿ
        // �����ٶȻ�Ӱ�켼�ܵ���ȴʱ�䣬�����ٶ�Խ�죬��ȴʱ��Խ�̡�����ȴʱ�䲻�����ԭ�е�ʱ�䣩
        if (GameManager.Instance.HasItem(ItemEnum.ʱ��֮ƿ))
        {
            if(attr.attackSpeed > 100) ����clk = ������ȴʱ�� / (attr.attackSpeed / 100);
        }
        #endregion

        #region 59-�Ƶ�ս�� 60-������ 89-��������
        // �ͷ��������ܺ�5s����ɵ��˺��������4%
        if (GameManager.Instance.HasItem(ItemEnum.�Ƶ�ս��))
        {
            int �Ƶ�ս��֮��_buff_id = 9;
            buffList.AddBuff(�Ƶ�ս��֮��_buff_id, null, gameObject, GameManager.Instance.HasItemNum(ItemEnum.�Ƶ�ս��));
        }
        // �ͷ��������ܺ�5s���ƶ��ٶ�����10%
        if (GameManager.Instance.HasItem(ItemEnum.������))
        {
            int ������֮��_buff_id = 10;
            buffList.AddBuff(������֮��_buff_id, null, gameObject, GameManager.Instance.HasItemNum(ItemEnum.������));
        }
        // �ͷ��������ܺ󣬻ָ���Ӣ�۵ȼ�/2��%�������������ֵ
        if (GameManager.Instance.HasItem(ItemEnum.��������))
        {
            float value = GameManager.Instance.gameData.level / 2.0f * GameManager.Instance.HasItemNum(ItemEnum.��������);
            GameManager.Instance.RestorePercentageHP(value / 100.0f);
        }
        #endregion


    }

    public override void TakeDamage(DamageInfo damage)
    {
        base.TakeDamage(damage);

        #region 67-������
        // ÿ���ܵ�������5s������2%�ı����ʣ��������20%
        if (GameManager.Instance.HasItem(ItemEnum.������))
        {
            int ������֮��_buff_id = 15;
            buffList.AddBuff(������֮��_buff_id, null, gameObject);
        }
        #endregion

        if (curHp <= 0) Die(5);
    }

    public override void InitAttribute()
    {
        base.InitAttribute();
        // ��ȡ���߷�Χ����
        transform.Find("PickupCollider").GetComponent<CircleCollider2D>().radius = GameManager.Instance.gameData.pickDis;
        // ��������
        attr = GameManager.Instance.gameData.playerAttr.DeepCopy();
        maxHp = attr.�������;
        curHp = maxHp;
        //if(GameManager.Instance.gameData.playerBuffs.buffs.Count == 0)
        //{
        //    // ˵������load������
        //    GameManager.Instance.gameData.playerBuffs = buffList;
        //}
        //else
        //{
        //    // ˵����load������
        //    buffList = GameManager.Instance.gameData.playerBuffs;
        //}
        
    }

}