using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseWeapon : MonoBehaviour
{
    public bool controlled = true;


    public int weaponId = -1;

    protected RankType rankType;

    protected WeaponTplInfo weaponInfo
    {
        get
        {
            return TplUtil.GetWeaponMap()[weaponId];
        }
    }

    protected float ������Χ;
    public virtual float ������ { get; }
    public float �����ٶ�
    {
        get
        {
            if (GameManager.Instance.Player == null) return 1;
            return weaponInfo.AttckSpeed * GameManager.Instance.Player.attr.�����ٶ� / 100.0f;
        }
    }

    public float ������� { get { return 1.0f / �����ٶ�; } }
    protected float �������clk;

    protected CircleCollider2D circleCollider;
    public List<Transform> enemyList;

    protected void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    protected virtual void Start()
    {
        enemyList = new List<Transform>();
    }

    public virtual void InitAttribute(int id)
    {
        this.weaponId = id;
        rankType = (RankType)weaponInfo.Rank;
        ������Χ = weaponInfo.AttckRange;
        circleCollider.radius = ������Χ;
    }

    protected virtual void Update()
    {
        if (�������clk > 0) �������clk -= Time.deltaTime;
        else ����();
    }

    protected virtual void ����() {}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            enemyList.Add(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemyList.Remove(collision.transform);
        }
    }

    
}
