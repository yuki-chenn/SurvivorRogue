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

    protected float ¹¥»÷·¶Î§;
    public virtual float ¹¥»÷Á¦ { get; }
    public float ¹¥»÷ËÙ¶È
    {
        get
        {
            if (GameManager.Instance.Player == null) return 1;
            return weaponInfo.AttckSpeed * GameManager.Instance.Player.attr.¹¥»÷ËÙ¶È / 100.0f;
        }
    }

    public float ¹¥»÷¼ä¸ô { get { return 1.0f / ¹¥»÷ËÙ¶È; } }
    protected float ¹¥»÷¼ä¸ôclk;

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
        ¹¥»÷·¶Î§ = weaponInfo.AttckRange;
        circleCollider.radius = ¹¥»÷·¶Î§;
    }

    protected virtual void Update()
    {
        if (¹¥»÷¼ä¸ôclk > 0) ¹¥»÷¼ä¸ôclk -= Time.deltaTime;
        else ¹¥»÷();
    }

    protected virtual void ¹¥»÷() {}

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
