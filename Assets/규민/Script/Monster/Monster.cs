using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    public struct monster_Data
    {
        public Monsters_Index type;
        public float maxHP;
        public float curHP;
        public float speed;
    }
    public monster_Data md;

    int index = 0;
    int preIndex = 0;
    
    float rotated = 0;
    public float moved = 0;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public abstract void Init();

    private void Move()
    {
        //index - 왼쪽:0, 아래:1, 오른쪽:2, 위:3;
        if (index != -1)
            moved += md.speed * Time.deltaTime;

        switch (index)
        {
            case 0:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.position.x >= 7)
                    index = -1;
                break;
            case 1:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.position.z >= 7)
                {
                    index = -1;
                    preIndex = 1;
                }
                break;
            case 2:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.position.x <= 1)
                {
                    index = -1;
                    preIndex = 2;
                }
                break;
            case 3:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.position.z <= 2)
                    Dead();
                break;
            default:
                transform.Rotate(new Vector3(0, -1, 0) * 120 * Time.deltaTime) ;
                rotated += 120 * Time.deltaTime;
                if (rotated >= 90)
                {
                    index = preIndex + 1;
                    rotated = 0;
                }
                break;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        Dictionary<Monsters_Index, Queue<Monster>> dm = MapManager.instance.monsterManager.d_monsters;
        if (!dm.ContainsKey(md.type))
            dm.Add(md.type, new Queue<Monster>());

        dm[md.type].Enqueue(this);
    }

    public void PoolInit(Transform mm_trans)
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        transform.position = mm_trans.position;
        Init();
        index = 0;
        preIndex = 0;
        rotated = 0;
        moved = 0;
    }

    public void Damaged(float damage)
    {
        md.curHP -= damage;
        if (md.curHP<=0)
            Dead();
    }

    private void OnMouseDown()
    {
        Dead();
    }
}
