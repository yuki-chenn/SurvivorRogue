using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Constants
{
    // 最大存档数
    public static int MAX_DATAFILE_SLOT = 10;
    public static string PLAYERPREFS_DATA = "SaveData-{0}";

    #region 概率

    // 武器道具刷新概率
    public static List<LevelProbability> RankTypeProbabilities = new List<LevelProbability>
    {
        new LevelProbability(1, new float[] { 100f, 0f, 0f, 0f }),
        new LevelProbability(2, new float[] { 98f, 2f, 0f, 0f }),
        new LevelProbability(3, new float[] { 95f, 5f, 0f, 0f }),
        new LevelProbability(4, new float[] { 93f, 7f, 0f, 0f }),
        new LevelProbability(5, new float[] { 90f, 10f, 0f, 0f }),
        new LevelProbability(6, new float[] { 85f, 15f, 0f, 0f }),
        new LevelProbability(8, new float[] { 80f, 19f, 1f, 0f }),
        new LevelProbability(10, new float[] { 65f, 25f, 9f, 1f }),
        new LevelProbability(13, new float[] { 55f, 30f, 13f, 2f }),
        new LevelProbability(15, new float[] { 45f, 35f, 16f, 4f }),
        new LevelProbability(18, new float[] { 30f, 44f, 20f, 6f }),
        new LevelProbability(20, new float[] { 25f, 40f, 27f, 8f }),
        new LevelProbability(21, new float[] { 20f, 35f, 35f, 10f })
    };

    public static List<LevelProbability> WeapaonItemProbabilities = new List<LevelProbability>
    {
        new LevelProbability(3, new float[] { 60f, 40f }),
        new LevelProbability(5, new float[] {50f, 50f }),
        new LevelProbability(8, new float[] { 45f, 55f }),
        new LevelProbability(10, new float[] { 40f, 60f }),
        new LevelProbability(13, new float[] { 35f, 65f }),
        new LevelProbability(15, new float[] { 30f, 70f }),
        new LevelProbability(18, new float[] { 25f, 75f }),
        new LevelProbability(20, new float[] { 20f, 80f }),
        new LevelProbability(21, new float[] { 10f, 90f }),
    };
    public static float[] GetRankTypeProbabilityByLevel(int level)
    {

        for (int i = 0; i < RankTypeProbabilities.Count; i++)
        {
            if (level <= RankTypeProbabilities[i].Level)
            {
                return RankTypeProbabilities[i].Probability;
            }
        }

        // 默认返回最高等级的概率分布
        return RankTypeProbabilities[RankTypeProbabilities.Count - 1].Probability;
    }
    public static float[] GetWeapaonItemProbabilityByLevel(int level)
    {

        for (int i = 0; i < WeapaonItemProbabilities.Count; i++)
        {
            if (level <= WeapaonItemProbabilities[i].Level)
            {
                return WeapaonItemProbabilities[i].Probability;
            }
        }

        // 默认返回最高等级的概率分布
        return WeapaonItemProbabilities[WeapaonItemProbabilities.Count - 1].Probability;
    }

    // 46-财运背包概率
    public static float[] 财运背包概率 = new float[] { 95f, 4f, 1f };


    #endregion

    public static Vector2[] DIR4 = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
    public static string[] RANK_NAME = new string[] { "未知", "普通", "稀有", "史诗", "传说" };
    public static Color[] RANK_COLOR = new Color[] 
    {
        new Color(0.0f, 0.0f, 0.0f), // 占位
        new Color(0.8f, 0.8f, 0.8f), // 普通: 灰色
        new Color(0.7f, 1.0f, 1.0f), // 稀有: 蓝色
        new Color(0.6f, 0.2f, 0.8f), // 史诗: 紫色
        new Color(1.0f, 0.5f, 0.0f)  // 传说: 橙色
    };

    public static Color[] RANK_COLOR_BG = new Color[]
    {
        new Color(0.0f, 0.0f, 0.0f, 0.1f), // 占位
        new Color(0.8f, 0.8f, 0.8f, 0.1f), // 普通: 灰色
        new Color(0.7f, 1.0f, 1.0f, 0.1f), // 稀有: 蓝色
        new Color(0.6f, 0.2f, 0.8f, 0.1f), // 史诗: 紫色
        new Color(1.0f, 0.5f, 0.0f, 0.1f)  // 传说: 橙色
    };

    public static int[] CHEST_SALE_PRICE_BY_RANK = new int[] { 0, 21, 51, 131, 291 };


    public static int ENDLESS_WAVE = 20;
    public static int TWO_BOSS_WAVE = 10005;
    public static int ONE_BOSS_WAVE = 10004;
    public static int[] NORMAL_WAVE = new int[] { 10001, 10002, 10003 };

}

public struct LevelProbability
{
    public int Level { get; set; }
    public float[] Probability { get; set; }

    public LevelProbability(int level, float[] probability)
    {
        Level = level;
        Probability = probability;
    }
}


