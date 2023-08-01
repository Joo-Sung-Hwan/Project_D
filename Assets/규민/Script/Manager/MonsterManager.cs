using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField] public Canvas canvas_Hp;

    public List<Monster> monsters;
    public Queue<Monster> pool_monster = new Queue<Monster>();
    public Dictionary<Monsters_Index, Queue<Monster>> d_monsters = new Dictionary<Monsters_Index, Queue<Monster>>();
    public bool isWave = false;
    public int killed = 0;

    private int spawned = 0;
    private bool canClear = false;
    private Coroutine c_wait;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("C_Wave_1");
    }

    // Update is called once per frame
    void Update()
    {
        Test();
        WaveClear();
    }

    IEnumerator C_Spawn(int index, int num, float delay)
    {
        for (int i = 0; i < num; i++)
        {
            Monster temp_monster;
            Monsters_Index mi = (Monsters_Index)index;

            if (!d_monsters.ContainsKey(mi))
                d_monsters.Add(mi, new Queue<Monster>());

            if(d_monsters[mi].TryDequeue(out temp_monster))
            {
                temp_monster.PoolInit(transform);
                temp_monster.gameObject.SetActive(true);
            }
            else
                Instantiate(monsters[index], transform);

            spawned++;
            yield return new WaitForSeconds(delay);
        }
    }

    void WaveClear()
    {
        if (canClear && spawned == killed)
        {
            Debug.Log("Wave Clear!");
            isWave = false;
            canClear = false;
            spawned = 0;
            killed = 0;
        }
    }

    #region �׽�Ʈ
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            isWave = !isWave;
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(C_Spawn((int)Monsters_Index.ghost, 1, 1));
        }
    }

    IEnumerator C_Wave_1()
    {
        isWave = true;
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.ghost, 5, 1f));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.dragon, 5, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.imp, 5, 1f));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.demon, 5, 1f));
        canClear = true;
    }

    #region C_Wave_1 �μ��Լ�
    IEnumerator C_WaitTime(float second)
    {
        float time = 0;
        while (time < second)
        {
            //wave ���ð� ���� ����
            if (Input.GetKey(KeyCode.F3))
                break;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
    #endregion

}
