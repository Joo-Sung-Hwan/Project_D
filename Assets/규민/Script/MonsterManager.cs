using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monsters_Index
{
    skeleton,
    archer
}

public class MonsterManager : MonoBehaviour
{
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

    void PoolInit(Monster monster)
    {
        monster.gameObject.SetActive(true);
        monster.transform.rotation = Quaternion.Euler(0, 90, 0);
        monster.transform.position = transform.position;
    }

    IEnumerator C_Spawn(int index, int num, float delay)
    {
        for (int i = 0; i < num; i++)
        {
            //Instantiate(monsters[index], transform);
            Monster temp_monster;
            Monsters_Index mi = (Monsters_Index)index;

            if (!d_monsters.ContainsKey(mi))
                d_monsters.Add(mi, new Queue<Monster>());

            if(d_monsters[mi].TryDequeue(out temp_monster))
            {
                temp_monster.gameObject.SetActive(true);
                temp_monster.PoolInit(transform);
            }
            else
            {
                //d_monsters[mi].Enqueue(Instantiate(monsters[index], transform));
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
