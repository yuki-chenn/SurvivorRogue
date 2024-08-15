using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Utils;

public enum SpawnTpye
{
    None, // 没有任何阵型
    HorizentalLine, // 生成水平的一条敌人
    VerticalLine, // 生成垂直的一条敌人
    Circle, // 生成一个圆形的敌人
    Rect, // 在矩形内随机生成怪物
}

public class EnemySpawner : MonoBehaviour
{
    private const float XMIN = -18;
    private const float XMAX = 18;
    private const float YMIN = -14;
    private const float YMAX = 14;

    public int enemyId;
    public GameObject enemyPrefab;
    public int count;
    public float delaySpawn;
    public float SpawnInterval;

    public int recycleCount; // -1表示无限循环
    public float recycleInterval; // 循环之间的间隔

    public bool randomPosition;

    public SpawnTpye spawnTpye;

    // 水平间隔 HorizentalLine,
    public float horizentalInterval;

    // 垂直间隔 VerticalLine,

    public float verticalInterval;

    // 圆圈半径 Circle,
    public float radius;

    // 在指定矩形内 Rect,
    public float halfWidth;
    public float halfHeight;

    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        // 等待生成
        yield return new WaitForSeconds(delaySpawn);

        // 循环生成
        for(int i = 0; recycleCount == -1 || i < recycleCount; ++i)
        {
            Spawn();
            yield return new WaitForSeconds(recycleInterval);
        }
    }

    void Spawn()
    {
        if (count == 1)
        {
            SpawnOneEnemy();
        }
        else
        {
            switch (spawnTpye)
            {
                case SpawnTpye.HorizentalLine:
                    StartCoroutine(SpawnHorizontal(count));
                    break;
                case SpawnTpye.VerticalLine:
                    StartCoroutine(SpawnVertical(count));
                    break;
                case SpawnTpye.Circle:
                    StartCoroutine(SpawnCircle(count));
                    break;
                case SpawnTpye.Rect:
                    StartCoroutine(SpawnRect(count));
                    break;
                default:
                    StartCoroutine(SpawnSeveral(count));
                    break;
            }
        }
    }

    private void SpwanPrefab(Vector2 pos)
    {
        if (pos.x < XMIN || pos.x > XMAX || pos.y < YMIN || pos.y > YMAX)
        {
            Debug.LogError(string.Format("生成失败，{0}超出范围", pos));
            return;
        }
        var enemy = Instantiate(enemyPrefab, pos, Quaternion.identity, transform.parent);
        enemy.GetComponent<Enemy>().id = enemyId;
    }

    private void SpawnOneEnemy()
    {
        Vector2 pos = transform.position;
        if (randomPosition)
        {
            pos = RandomUtil.RandomVector2(XMIN,XMAX,YMIN,YMAX);
        }
        SpwanPrefab(pos);
    }

    IEnumerator SpawnSeveral(int count)
    {
        for (int i = 0; i < count; ++i)
        {
            Vector2 pos = randomPosition ?
                RandomUtil.RandomVector2(XMIN, XMAX, YMIN, YMAX):
                (Vector2)transform.position;
            SpwanPrefab(pos);
            
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    IEnumerator SpawnHorizontal(int count)
    {
        Vector2 centerPos = transform.position;
        float d = (count - 1) / 2.0f * horizentalInterval;
        if (randomPosition)
        {
            centerPos = RandomUtil.RandomVector2(centerPos.x - d, centerPos.x + d, YMIN, YMAX);
        }
        Vector2 leftPos = new Vector2(centerPos.x - d, centerPos.y);
        for(int i = 0; i < count; ++i)
        {
            SpwanPrefab(leftPos);
            leftPos.x += horizentalInterval;
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
    IEnumerator SpawnVertical(int count)
    {
        Vector2 centerPos = transform.position;
        float d = (count - 1) / 2.0f * verticalInterval;
        if (randomPosition)
        {
            centerPos = RandomUtil.RandomVector2(XMIN, XMAX, centerPos.y - d, centerPos.y + d);
        }
        Vector2 downPos = new Vector2(centerPos.x, centerPos.y - d);
        for (int i = 0; i < count; ++i)
        {
            SpwanPrefab(downPos);
            downPos.y += verticalInterval;
            yield return new WaitForSeconds(SpawnInterval);
        }
    }
    IEnumerator SpawnCircle(int count)
    {
        Vector2 centerPos = transform.position;
        if (randomPosition)
        {
            centerPos = RandomUtil.RandomVector2(XMIN + radius, XMAX - radius, YMIN + radius, YMAX - radius);
        }
        float theta = 0;
        for (int i = 0; i < count; ++i)
        {
            SpwanPrefab(GeometryUtil.GetAngelPosition(centerPos,theta,radius));
            theta += 360.0f / count;
            yield return new WaitForSeconds(SpawnInterval);
        }
    }

    IEnumerator SpawnRect(int count)
    {
        // 以中心点的矩形内随机生成count数量的敌人
        Vector2 centerPos = transform.position;
        if (randomPosition)
        {
            // 随机中心点
            centerPos = RandomUtil.RandomVector2(XMIN + halfWidth, XMAX - halfWidth, YMIN + halfHeight, YMAX - halfHeight);
        }
        
        for(int i = 0; i < count; ++i)
        {
            Vector2 pos = RandomUtil.RandomPosInRect(halfWidth, halfHeight, centerPos);
            SpwanPrefab(pos);
            yield return new WaitForSeconds(SpawnInterval);
        }
        
    }

}
