using Survivor.Base;
using Survivor.Template;
using Survivor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : PersistentSingleton<GameManager>
{
    public GameObject playerGo;
    private Player _player;
    public Player Player
    {
        get
        {
            if (_player == null)
            {
                if (playerGo != null) _player = playerGo.GetComponent<Player>();
            }
            return _player;
        }
    }

    public FSMSystem fsm;

    // 游戏数据
    public GameData gameData;

    private Dictionary<int, List<WaveTplInfo>> _waveDic;
    public Dictionary<int, List<WaveTplInfo>> WaveDic
    {
        get
        {
            if(_waveDic == null)
            {
                _waveDic = TplUtil.GetWaveDictionaryWithWaveKey();
            }
            return _waveDic;
        }
    }

    protected override void Awake()
    {
        base.Awake();
#if UNITY_ANDROID && !UNITY_EDITOR
        Application.targetFrameRate = 60;
#else
        Application.targetFrameRate = -1;
#endif
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        InitFSM();
        gameData = new GameData();
    }

    private void Update()
    {
        fsm.Update();
        // 记录游玩时间
        if (gameData.saveIndex != -1)
        {
            gameData.playTime += Time.deltaTime;
        }
    }

    private void InitFSM()
    {
        // 创建一个fsm
        fsm = new FSMSystem();

        // 添加state
        fsm.AddState(new ReadyState());
        fsm.AddState(new FightState());
        fsm.AddState(new PauseState());
        fsm.AddState(new BuyState());
        fsm.AddState(new GameoverState());
        fsm.AddState(new GameClearState());
        fsm.AddState(new LoadState());

        // 设置初始状态
        fsm.SetCurrentState(StateID.ReadyState);
    }

    public void ClearData()
    {
        playerGo = null;
        _player = null;

        gameData = new GameData();
    }

    public void SetMoveDir(Vector2 dir)
    {
        if (playerGo == null) return;
        Player move = playerGo.GetComponent<Player>();
        move.SetMoveDir(dir);
    }

    public Vector2? GetPlayerPosition()
    {
        if (playerGo != null && playerGo.activeSelf) return playerGo.transform.position;
        else {
            //Debug.LogError("playerGo is Null");
            return null;
        }
    }

    public void GainExp(int exp)
    {
        gameData.curExp += exp;
        while(gameData.nextLevelExp <= gameData.curExp)
        {
            gameData.curExp -= gameData.nextLevelExp;
            LevelUp();
        }
        GameUIManager.Instance.UpdateLevelExp(gameData.level,gameData.curExp,gameData.nextLevelExp);
    }

    public void LevelUp()
    {
        AudioManager.Instance.PlayLevelUpEffect();
        gameData.level += 1;
        gameData.nextLevelExp = (int)(1000 * Mathf.Exp(0.2f * (gameData.level - 1)));
        // 属性增加
        gameData.LevelUpAddAttr();
        Player.InitAttribute();
    }

    public void PickUpMoney(int num)
    {
        gameData.money += num;
        gameData.collectMoney += num;
        GameUIManager.Instance.UpdateMoney(gameData.money);
    }
    public void PickUpChest(RankType rankType)
    {
        gameData.chestCount[(int)rankType - 2]++;
    }
    public void RestoreHP(float num)
    {
        Player.curHp += num;
        if (Player.curHp >= Player.maxHp)
        {
            Player.curHp = Player.maxHp;
        }
        // 恢复显示
        DamagePopupManager.Instance.CreateRestoreHint(playerGo.transform.position, num);
    }
    public void RestorePercentageHP(float percentage)
    {
        if (percentage > 1) percentage = 1;
        float val = Player.attr.maxhp * percentage;
        RestoreHP(val);
    }


    public bool HasItem(ItemEnum itemEnum)
    {
        return gameData.itemIDs.ContainsKey((int)itemEnum) && gameData.itemIDs[(int)itemEnum] > 0;
    }

    public int HasItemNum(ItemEnum itemEnum)
    {
        return gameData.itemIDs.GetValueOrDefault((int)itemEnum, 0);
    }

    #region 商店
    // 波次结束进入商店
    public void BeforeEnterShop()
    {
        gameData.costX = 0;
        #region 94-商贾之印
        // 每次商店的第一次刷新免费
        if (GameManager.Instance.HasItem(ItemEnum.商贾之印))
        {
            gameData.isNextFree = true;
        }
        #endregion
        bool[] isLock = new bool[gameData.shopSlot];
        RefreshShopSelections(isLock, ref gameData.selectionsType, ref gameData.selectionsId, ref gameData.isFree);
    }

    // 刷新商店商品
    public void RefreshShopSelections(bool[] isLock,ref int[] type,ref int[] ids,ref bool[] isFree)
    {
        int n = isLock.Length;

        var probArrayOri = Constants.GetRankTypeProbabilityByLevel(gameData.level);
        float[] probArray = probArrayOri.ToArray();
        // 幸运补偿
        float luck = Math.Max(0, gameData.playerAttr.幸运);
        float offsetluck03 = Mathf.Min(probArray[0], 100 - probArray[3], luck / 500f);
        probArray[0] -= offsetluck03;
        probArray[3] += offsetluck03;
        float offsetluck12 = Mathf.Min(probArray[1], 100 - probArray[2], luck / 250f);
        probArray[1] -= offsetluck12;
        probArray[2] += offsetluck12;

        //Debug.LogError(string.Format("probability:{0},{1},{2},{3}", probArray[0], probArray[1], probArray[2], probArray[3]));

        // fix-bug : 修复有概率刷出两个相同的物品，会超过最大持有数
        Dictionary<int, int> itemHasAppear = new Dictionary<int, int>();

        for (int i = 0; i < n; ++i)
        {
            if (isLock[i]) continue;
            // 先确定是什么rank
            int rankType = 1 + RandomUtil.RandomIndexWithProbablity(probArray);
            // 确定是武器还是道具
            int isItem = RandomUtil.RandomIndexWithProbablity(Constants.GetWeapaonItemProbabilityByLevel(gameData.level));

            // 如果是道具，我要先看看有没有这个rank的道具剩余了
            List<int> itemList = null;
            List<int> weaponList = null;
            if(isItem == 1)
            {
                itemList = TplUtil.GetItemMap().Values
                    .Where(item => item.Rank == rankType && (item.MaxOwnCount == -1 || item.MaxOwnCount > itemHasAppear.GetValueOrDefault(item.ID, 0) + gameData.itemIDs.GetValueOrDefault(item.ID, 0)))
                    .Select(item => item.ID)
                    .ToList();
                // 如果道具列表里什么都没有，那就是武器
                if (itemList.Count == 0) isItem = 0;
            }
            type[i] = isItem;
            if (isItem == 0)
            {
                // 是武器
                weaponList = TplUtil.GetWeaponMap().Values
                    .Where(item => item.Rank == rankType)
                    .Select(item => item.ID)
                    .ToList();
                ids[i] = RandomUtil.GetRandomValueInList<int>(weaponList, ids[i]);
            }
            else
            {
                // 是道具
                ids[i] = RandomUtil.GetRandomValueInList<int>(itemList, ids[i]);
                itemHasAppear[ids[i]] = itemHasAppear.GetValueOrDefault(ids[i],0) + 1;
            }
        }

        #region 97-魔术牌
        // 每次刷新商店，有1%的概率本次所有道具或武器免费购买。
        for (int i = 0; i < n; ++i) isFree[i] = false;
        if (HasItem(ItemEnum.魔术牌))
        {
            if (RandomUtil.IsProbabilityMet(0.05f))
            {
                for (int i = 0; i < n; ++i) isFree[i] = true;
            }
        }
        #endregion

    }

    // 随机宝箱物品
    public int GetRandomItem(RankType rankType)
    {
        var itemList = TplUtil.GetItemMap().Values
                    .Where(item => item.Rank == (int)rankType && (item.MaxOwnCount == -1 || item.MaxOwnCount > gameData.itemIDs.GetValueOrDefault(item.ID, 0)))
                    .Select(item => item.ID)
                    .ToList();
        if (itemList.Count == 0) return -1;
        return RandomUtil.GetRandomValueInList<int>(itemList);

    }

    public void BuyWeapon(WeaponTplInfo info, float discount, bool isFree = false)
    {
        int price = isFree ? 0 : (int)(info.Price * discount);
        // 没钱
        if (gameData.money < price) return;

        // 满了
        int emptyIndex = CanBuyWeapon();
        if (emptyIndex == -1) return;
        AudioManager.Instance.PlayBuySelectionsEffect();
        gameData.money -= price;

        Debug.Log("buy weapon : " + info.Name);
        gameData.weaponIDs[emptyIndex] = info.ID;
    }

    public void BuyWeaponAndRankUp(WeaponTplInfo info,int rankupIndex, float discount, bool isFree = false)
    {
        int price = isFree ? 0 : (int)(info.Price * discount);
        // 不能合成
        if (info.RankupWeaponID == -1) return;
        // 没钱
        if (gameData.money < price) return;
        // 两把武器不一样
        if (gameData.weaponIDs[rankupIndex] != info.ID) return;
        gameData.money -= price;
        AudioManager.Instance.PlayLevelUpWeaponEffect();
        Debug.Log("buy weapon and rank up : " + info.Name);
        gameData.weaponIDs[rankupIndex] = info.RankupWeaponID;
        #region 77-女巫坩埚 78-半满坩埚
        // 进阶武器后，智力+1，力量+1
        if (HasItem(ItemEnum.女巫坩埚))
        {
            int val = HasItemNum(ItemEnum.女巫坩埚);
            gameData.playerAttr.智力 += val;
            gameData.playerAttr.力量 += val;
        }
        // 进阶武器后，获得200经验值
        if (HasItem(ItemEnum.半满坩埚))
        {
            int val = HasItemNum(ItemEnum.半满坩埚);
            GainExp(200 * val);
        }
        #endregion
    }

    public void BuyItem(ItemTplInfo info, float discount,bool isFree = false)
    {
        int price = isFree ? 0 : (int)(info.Price * discount);

        if (gameData.itemIDs.ContainsKey(info.ID) && (info.MaxOwnCount != -1 && gameData.itemIDs[info.ID] >= info.MaxOwnCount))
        {
            Debug.LogError(info.Name + "持有数量达到上限：" + info.MaxOwnCount);
            return;
        }
        if (gameData.money < price)
        {
            Debug.LogError("金币不足");
            return;
        }
        AudioManager.Instance.PlayBuySelectionsEffect();
        gameData.money -= price;
        gameData.itemIDs[info.ID] = gameData.itemIDs.TryGetValue(info.ID, out int count) ? count + 1 : 1;
        var item = ItemFactory.GetItemByID(info.ID);
        if (item != null) item.OnGet();
        Debug.Log("buy item : " + info.Name);
    }

    // 开宝箱时获取Item
    public void GetItem(ItemTplInfo info)
    {
        gameData.itemIDs[info.ID] = gameData.itemIDs.TryGetValue(info.ID, out int count) ? count + 1 : 1;
        var item = ItemFactory.GetItemByID(info.ID);
        if (item != null) item.OnGet();
        Debug.Log("get item : " + info.Name);
    }

    public void SaleItem(int itemID)
    {
        if (!gameData.itemIDs.ContainsKey(itemID)) return;
        var info = TplUtil.GetItemMap()[itemID];
        int money = (int)(info.Price * gameData.saleDiscount);

        #region 55-大金牙 79-满满的坩埚
        // 出售道具或者武器时，额外获得 2+当前波次/4 个金币
        if (HasItem(ItemEnum.大金牙))
        {
            money += (2 + gameData.curWave / 4) * HasItemNum(ItemEnum.大金牙);
        }
        // 出售武器或道具后，防御+1
        if (HasItem(ItemEnum.满满的坩埚))
        {
            gameData.playerAttr.防御 += HasItemNum(ItemEnum.满满的坩埚);
        }
        #endregion

        // 加钱
        gameData.money += money;

        // 清空map
        gameData.itemIDs[itemID] = gameData.itemIDs.GetValueOrDefault(itemID, 0) - 1;
        if (gameData.itemIDs[itemID] <= 0) gameData.itemIDs.Remove(itemID);

        var item = ItemFactory.GetItemByID(itemID);
        if (item != null) item.OnDiscard();
        Debug.Log("sale item : " + info.Name);
    }

    public void SaleWeapon(int index)
    {
        int money = TplUtil.GetWeaponMap()[gameData.weaponIDs[index]].Price;
        money = (int)(money * gameData.saleDiscount);

        #region 55-大金牙 76-坩埚 79-满满的坩埚
        // 出售道具或者武器时，额外获得 2 + 当前波次 / 4 个金币
        if (HasItem(ItemEnum.大金牙))
        {
            money += (2 + gameData.curWave / 4) * HasItemNum(ItemEnum.大金牙);
        }
        // 出售武器后，可以获得一次免费的刷新次数（次数不会保留）
        if (HasItem(ItemEnum.坩埚))
        {
            gameData.isNextFree = true;
        }
        // 出售武器或道具后，防御+1
        if (HasItem(ItemEnum.满满的坩埚))
        {
            gameData.playerAttr.防御 += HasItemNum(ItemEnum.满满的坩埚);
        }
        #endregion

        gameData.weaponIDs[index] = -1 ;
        gameData.money += money;
    }

    public void RankUpWeapon(int index)
    {
        if (!CanRankUp(index)) return;
        WeaponTplInfo info = TplUtil.GetWeaponMap()[gameData.weaponIDs[index]];
        int removeIndex = -1;
        for (int i = 0; i < gameData.weaponSlot; ++i)
        {
            if (i == index) continue;
            if (gameData.weaponIDs[i] == info.ID)
            {
                removeIndex = i;
                break;
            }
        }
        if(removeIndex != -1)
        {
            gameData.weaponIDs[removeIndex] = -1;
            gameData.weaponIDs[index]= info.RankupWeaponID;
        }
        #region 77-女巫坩埚 78-半满坩埚
        // 进阶武器后，智力+1，力量+1
        if (HasItem(ItemEnum.女巫坩埚))
        {
            int val = HasItemNum(ItemEnum.女巫坩埚);
            gameData.playerAttr.智力 += val;
            gameData.playerAttr.力量 += val;
        }
        // 进阶武器后，获得200经验值
        if (HasItem(ItemEnum.半满坩埚))
        {
            int val = HasItemNum(ItemEnum.半满坩埚);
            GainExp(200 * val);
        }
        #endregion
    }

    public bool CanRankUp(int index)
    {
        WeaponTplInfo info = TplUtil.GetWeaponMap()[gameData.weaponIDs[index]];

        // 没有升级的武器了
        if (info.RankupWeaponID == -1) return false;
        // 背包中没有和他相同的武器
        for (int i = 0; i < gameData.weaponSlot; ++i)
        {
            if (i == index) continue;
            if (gameData.weaponIDs[i] == info.ID) return true;
        }
        return false;
    }

    public int CanBuyWeapon()
    {
        for(int i = 0; i < gameData.weaponSlot; ++i)
        {
            if (gameData.weaponIDs[i] == -1) return i;
        }
        return -1;
    }

    #endregion

    #region Scene

    public void GenerateDrop(DropEnum dropEnum, Vector3 position)
    {
        Instantiate(AssetManager.Instance.DropPrefab[(int)dropEnum], position, Quaternion.identity, ContainerManager.Instance.dropObjectContainer);
    }

    public void GenerateEnemy(WaveTplInfo info)
    {
        var go = Instantiate(AssetManager.Instance.EnemySpawnerPrefab,
                new Vector2(info.GeneratePositionX, info.GeneratePositionY),
                Quaternion.identity, ContainerManager.Instance.enemyContainer);
        EnemySpawner spawner = go.GetComponent<EnemySpawner>();
        spawner.enemyId = info.EnemyID;
        spawner.count = info.Count;
        spawner.recycleCount = info.RecycleCount;
        spawner.recycleInterval = info.RecycleInterval;
        spawner.delaySpawn = info.DelaySpawnTime;
        spawner.enemyPrefab = AssetManager.Instance.EnemyPrefab[TplUtil.GetEnemyMap()[info.EnemyID].Index];
        spawner.SpawnInterval = info.SpawnInterval;
        spawner.randomPosition = info.RandomPosition == 1;
        spawner.spawnTpye = (SpawnTpye)info.SpawnType;
        spawner.horizentalInterval = info.HorizentalInterval;
        spawner.verticalInterval = info.VerticalInterval;
        spawner.radius = info.Radius;
        spawner.halfHeight = info.HalfHeight;
        spawner.halfWidth = info.HalfWidth;
    }

    public void GenerateEnemySpawner(int waveCount)
    {
        // 当 wave > 20 时处于无尽模式
        if (waveCount > Constants.ENDLESS_WAVE)
        {
            if(waveCount % 10 == 0)
            {
                // 选择双boss wave
                waveCount = Constants.TWO_BOSS_WAVE;
            }
            else if(waveCount % 5 == 0)
            {
                // 单boss wave
                waveCount = Constants.ONE_BOSS_WAVE;
            }
            else
            {
                // 普通 三选一
                waveCount = RandomUtil.GetRandomValueInList(Constants.NORMAL_WAVE);
            }
        }

        if (!WaveDic.ContainsKey(waveCount)) return;
        List<WaveTplInfo> enemySpawnerList = WaveDic[waveCount];

        foreach (var info in enemySpawnerList)
        {
            GenerateEnemy(info);
        }
    }

    public void ResetPlayer()
    {
        if (playerGo == null) playerGo = Instantiate(gameData.playerPrefab, Vector3.zero, Quaternion.identity);
        playerGo.SetActive(true);
        playerGo.transform.position = Vector3.zero;

        // 设置玩家属性
        Player.InitAttribute();

        // 根据weaponsID设置武器
        GenerateWeapons();

        // 重置冷却
        Player.重置技能冷却();

        
    }

    public void DisablePlayer()
    {
        Player.OnPlayerReset();
        playerGo.SetActive(false);
    }

    public void PlayerDie()
    {
        GameUIManager.Instance.ShowLoadingPanel();
        StartCoroutine(DieTransition());
    }

    IEnumerator DieTransition()
    {
        yield return new WaitForSeconds(3);
        GameUIManager.Instance.CloseLoadingPanel();
        fsm.PerformTransition(Transition.PlayerDie);
    }

    public void GenerateWeapons()
    {
        var par = playerGo.transform.Find("Weapons");
        par.GetComponent<WeaponAutoRotate>().StopRotate();
        // 清空所有的weapon
        foreach (Transform child in par)
        {
            Destroy(child.gameObject);
        }

        var weaponMap = TplUtil.GetWeaponMap();
        foreach(int id in gameData.weaponIDs)
        {
            if (id == -1) continue;
            var weaponInfo = weaponMap[id];
            var weaponGo = Instantiate(AssetManager.Instance.weaponPrefab[weaponInfo.Index],Vector3.zero,Quaternion.identity,par);
            var weapon = weaponGo.GetComponent<BaseWeapon>();
            weapon.InitAttribute(weaponInfo.ID);
        }
        par.GetComponent<WeaponAutoRotate>().StartRotate();
    }

    public void RemoveObjectInScene()
    {
        // 禁用玩家
        DisablePlayer();

        // 禁用所有enemySpawner脚本
        DisableSpawners(ContainerManager.Instance.enemyContainer);

        // 清空敌人
        RemoveAllInContainer(ContainerManager.Instance.enemyContainer);

        // 清空掉落物
        RemoveAllInContainer(ContainerManager.Instance.dropObjectContainer);

        // 清空伤害显示
        RemoveAllInContainer(ContainerManager.Instance.damageHintContainer);

        // 清空武器飞行物
        RemoveAllInContainer(ContainerManager.Instance.weaponObjectContainer);
    }

    private void DisableSpawners(Transform parent)
    {
        foreach (Transform child in parent)
        {
            var spawner = child.GetComponent<EnemySpawner>();
            if (spawner != null)
            {
                spawner.enabled = false;
            }
        }
    }

    public void RemoveAllInContainer(Transform container)
    {
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the gameScene
        if (scene.name == "GameScene")
        {
            // 先把玩家创建出来
            playerGo = Instantiate(gameData.playerPrefab, Vector3.zero, Quaternion.identity);

            // 新游戏
            if (fsm.CurrentStateID == StateID.ReadyState)
            {
                // 触发一下初始选择的item的获取方法
                ItemFactory.GetItemByID(gameData.initialItemId)?.OnGet();

                if (fsm != null) fsm.PerformTransition(Transition.StartGame);
            }

            if (fsm.CurrentStateID == StateID.LoadState)
            {
                // 读取一下所有的buff
                Player.buffList = gameData.playerBuffs;
                if (fsm != null)
                {
                    if (gameData.curWave != 0)
                    {
                        DisablePlayer();
                        fsm.PerformTransition(Transition.ContinueGame);
                    }
                    else
                    {
                        // 触发一下初始选择的item的获取方法
                        ItemFactory.GetItemByID(gameData.initialItemId)?.OnGet();
                        fsm.PerformTransition(Transition.StartGame);
                    }
                }
            }



        }
    }



    #endregion

    #region 存档
    /// <summary>
    /// 保存数据
    /// </summary>
    public void SaveGameData(int dataIndex)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + string.Format("/SurvivorData{0}.data",dataIndex)))
            {
                FileData data = new FileData();
                bf.Serialize(fs, data);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public void SavePlayerPrefs(int dataIndex)
    {
        FileShowData playerprefsData = new FileShowData(gameData);
        string jsonData = JsonUtility.ToJson(playerprefsData);
        PlayerPrefs.SetString(string.Format(Constants.PLAYERPREFS_DATA, dataIndex), jsonData);
        PlayerPrefs.Save();
    }

    public void OverridePlayerPrefs(int dataIndex)
    {
        FileShowData playerprefsData = LoadPlayerPrefs(dataIndex);
        playerprefsData.SetNewData(gameData);
        string jsonData = JsonUtility.ToJson(playerprefsData);
        PlayerPrefs.SetString(string.Format(Constants.PLAYERPREFS_DATA, dataIndex), jsonData);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public void LoadGameData(int dataIndex)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + string.Format("/SurvivorData{0}.data", dataIndex), FileMode.Open))
            {
                var data = (FileData)bf.Deserialize(fs);
                gameData = data.gameData;
                gameData.playerBuffs = data.playerBuffs;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    public FileShowData LoadPlayerPrefs(int dataIndex)
    {
        string key = string.Format(Constants.PLAYERPREFS_DATA, dataIndex);
        if (PlayerPrefs.HasKey(key))
        {
            string jsonData = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<FileShowData>(jsonData);
        }
        else return null;
    }

    public void ClearGameDataAndPlayerPrefs(int dataIndex)
    {
        try
        {
            // 删除游戏数据文件
            string filePath = Application.persistentDataPath + string.Format("/SurvivorData{0}.data", dataIndex);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("Game data file deleted successfully.");
            }
            else
            {
                Debug.LogWarning("Game data file not found.");
            }

            // 删除PlayerPrefs中的数据
            string playerPrefsKey = string.Format(Constants.PLAYERPREFS_DATA, dataIndex);
            if (PlayerPrefs.HasKey(playerPrefsKey))
            {
                PlayerPrefs.DeleteKey(playerPrefsKey);
                PlayerPrefs.Save();
                Debug.Log("PlayerPrefs data deleted successfully.");
            }
            else
            {
                Debug.LogWarning("PlayerPrefs key not found.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error clearing data: " + e.Message);
        }
    }

    #endregion

    public bool isEndlessMode(bool isFight=true)
    {
        if(isFight) return gameData.curWave > Constants.ENDLESS_WAVE;
        else return gameData.curWave >= Constants.ENDLESS_WAVE;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    
}
