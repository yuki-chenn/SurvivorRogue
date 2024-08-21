using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "创建资源管理对象")]
public class AssetManager : ScriptableObject
{
    private static AssetManager _instance;
    public static AssetManager Instance
    {
        get
        {
            if(_instance == null) _instance = Resources.Load<AssetManager>("AssetContainer");
            return _instance;
        }
    }

    public List<Sprite> heroSprite;
    public List<Sprite> weaponSprite;
    public List<Sprite> itemSprite;
    public List<Sprite> skillSprite;
    public List<Sprite> passiveSprite;

    public Sprite shopItemLockSprite;
    public Sprite shopItemUnLockSprite;

    public Sprite[] 商店武器道具购买等级边框;
    public Sprite[] 武器道具等级边框;
    public Sprite[] 武器道具背景;

    public GameObject 英雄选择头像模板;
    public GameObject 武器道具选择模板;
    public GameObject 商店武器显示模板;
    public GameObject 武器道具购买模板;
    public GameObject 数据存档模板;
    public GameObject 暂停道具模板;
    public GameObject 暂停增益模板;


    public GameObject 不灭圣辉攻击物体;

    public GameObject EnemySpawnerPrefab;

    public List<GameObject> HeroPrefab;

    public List<GameObject> weaponPrefab;

    public List<GameObject> EnemyPrefab;

    public List<GameObject> DropPrefab;

}
