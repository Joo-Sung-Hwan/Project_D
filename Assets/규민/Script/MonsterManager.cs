using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public enum Monsters_Index
    {
        skeleton,
        archer
    }

    public List<Monster> monsters;
    //pool[index] = [(Monsters_Index)index]종류의 몬스터 List 
    public Queue<Monster> pool_monster = new Queue<Monster>();
    public Dictionary<Monsters_Index, Queue<Monster>> d_monsters = new Dictionary<Monsters_Index, Queue<Monster>>();

    private Coroutine c_wait;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("C_Wave_1");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
    //2타입 이상 연속소환 템플릿
    IEnumerator C_Multi_Spawn()
    {
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.archer, 3, 0.5f));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.skeleton, 3, 1f));
    }
    */

    void PoolInit(Monster monster)
    {
        monster.gameObject.SetActive(true);
        monster.transform.position = transform.position;
    }

    IEnumerator C_Spawn(int index, int num, float delay)
    {
        for (int i = 0; i < num; i++)
        {
            //Instantiate(monsters[index], transform);
            Monster temp_monster;

            if (!d_monsters.ContainsKey((Monsters_Index)index))
                d_monsters.Add((Monsters_Index)index, new Queue<Monster>());

            if(d_monsters[(Monsters_Index)index].TryDequeue(out temp_monster))
            {
                PoolInit(temp_monster);
            }
            else
            {
                //d_monsters[(Monsters_Index)index].Enqueue(Instantiate(monsters[index], transform));
                Instantiate(monsters[index], transform);
            }




            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator C_Wave_1()
    {
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.skeleton, 5, 1f));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.archer, 5, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.skeleton, 5, 1f));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.archer, 5, 1f));
    }

    IEnumerator C_WaitTime(float second)
    {
        float time = 0;
        while (time < second)
        {
            //wave 대기시간 강제 종료
            if (Input.GetKey(KeyCode.F1))
                break;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
