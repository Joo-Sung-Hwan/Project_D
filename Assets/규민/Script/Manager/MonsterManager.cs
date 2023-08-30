using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class MonsterManager : MonoBehaviourPunCallbacks
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

    private void Awake()
    {
        //PhotonNetwork.ConnectUsingSettings();
    }

    public void OnConnectedToServer()
    {
        //PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 4 }, null);
    }

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

    #region spawn
    IEnumerator C_Spawn(int index, int num, float delay, float rotateSpeed = 0)
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
                temp_monster.rotateSpeed = rotateSpeed;
                temp_monster.gameObject.SetActive(true);
            }
            else
                Instantiate(monsters[index], transform).rotateSpeed = rotateSpeed;

            spawned++;
            GameManager.instance.playermanager.SetMonsterLeft(true);
            yield return new WaitForSeconds(delay);
        }
    }
    #endregion

    #region Wave
    void WaveClear()
    {
        if (canClear && spawned == killed)
        {
            Debug.Log("Wave Clear!");
            SetIsWave(false);
            canClear = false;
            spawned = 0;
            killed = 0;
        }
    }

    void SetIsWave(bool isWave)
    {
        this.isWave = isWave;
        MapManager.instance.unitManager.Init_IsWave(isWave);
    }
    #endregion

    #region 테스트
    private void Test()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SetIsWave(!isWave);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            StartCoroutine(C_Spawn((int)Monsters_Index.orc_grunt, 1, 1));
        }
    }

    IEnumerator C_Wave_1()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.pirate_warrior, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.pirate_warrior, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.pirate_warrior, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_2");
        }
    }
    IEnumerator C_Wave_2()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_shaman, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_shaman, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_shaman, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_3");
        }
    }
    IEnumerator C_Wave_3()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_heavy, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_heavy, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_heavy, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_4");
        }
    }
    IEnumerator C_Wave_4()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_grunt, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_grunt, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_grunt, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_5");
        }
    }
    IEnumerator C_Wave_5()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.orc_lord, 3, 15f));
        yield return c_wait = StartCoroutine(C_WaitTime(150));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_6");
        }
    }
    IEnumerator C_Wave_6()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.snowman, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.snowman, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.snowman, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_7");
        }
    }
    IEnumerator C_Wave_7()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.demon, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.demon, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.demon, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_8");
        }
    }
    IEnumerator C_Wave_8()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.imp, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.imp, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.imp, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_9");
        }
    }
    IEnumerator C_Wave_9()
    {
        yield return new WaitForSeconds(20);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.ghost, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.ghost, 10, 1f));
        yield return c_wait = StartCoroutine(C_WaitTime(5));
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.ghost, 10, 1f));
        canClear = true;
        if (canClear == true)
        {
            StartCoroutine("C_Wave_10");
        }
    }
    IEnumerator C_Wave_10()
    {
        yield return new WaitForSeconds(30);
        SetIsWave(true);
        canClear = false;
        yield return StartCoroutine(C_Spawn((int)Monsters_Index.ice_elementalboss, 3, 15f));
        yield return c_wait = StartCoroutine(C_WaitTime(150));
        canClear = true;
    }

    #region C_Wave_1 부속함수
    IEnumerator C_WaitTime(float second)
    {
        float time = 0;
        while (time < second)
        {
            //wave 대기시간 강제 종료
            if (Input.GetKey(KeyCode.F3))
                break;
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
    #endregion

}
