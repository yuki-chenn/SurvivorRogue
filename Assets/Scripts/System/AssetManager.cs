using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "������Դ�������")]
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

    public Sprite[] �̵��������߹���ȼ��߿�;
    public Sprite[] �������ߵȼ��߿�;
    public Sprite[] �������߱���;

    public GameObject Ӣ��ѡ��ͷ��ģ��;
    public GameObject ��������ѡ��ģ��;
    public GameObject �̵�������ʾģ��;
    public GameObject �������߹���ģ��;
    public GameObject ���ݴ浵ģ��;
    public GameObject ��ͣ����ģ��;
    public GameObject ��ͣ����ģ��;


    public GameObject ����ʥ�Թ�������;

    public GameObject EnemySpawnerPrefab;

    public List<GameObject> HeroPrefab;

    public List<GameObject> weaponPrefab;

    public List<GameObject> EnemyPrefab;

    public List<GameObject> DropPrefab;

}
