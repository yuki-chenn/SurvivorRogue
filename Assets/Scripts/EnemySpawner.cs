using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Survivor.Utils;

public enum SpawnTpye
{
    None, // û���κ�����
    HorizentalLine, // ����ˮƽ��һ������
    VerticalLine, // ���ɴ�ֱ��һ������
    Circle, // ����һ��Բ�εĵ���
    Rect, // �ھ�����������ɹ���
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

    public int recycleCount; // -1��ʾ����ѭ��
    public float recycleInterval; // ѭ��֮��ļ��

    public bool randomPosition;

    public SpawnTpye spawnTpye;

    // ˮƽ��� HorizentalLine,
    public float horizentalInterval;

    // ��ֱ��� VerticalLine,

    public float verticalInterval;

    // ԲȦ�뾶 Circle,
    public float radius;

    // ��ָ�������� Rect,
    public float halfWidth;
    public float halfHeight;

    void Start()
    {
        StartCoroutine(StartSpawn());
    }

    IEnumerator StartSpawn()
    {
        // �ȴ�����
        yield return new WaitForSeconds(delaySpawn);

        // ѭ������
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
            Debug.LogError(string.Format("����ʧ�ܣ�{0}������Χ", pos));
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
        // �����ĵ�ľ������������count�����ĵ���
        Vector2 centerPos = transform.position;
        if (randomPosition)
        {
            // ������ĵ�
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
