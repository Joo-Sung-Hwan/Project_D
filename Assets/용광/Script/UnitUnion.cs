using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitTpye
{
    None,
    Fire,
    Wind,
    Earth,
    Water
}

public class UnitUnion : MonoBehaviour
{
    public UnitTpye uType = UnitTpye.None;
    [SerializeField] MovableObj movable;

    bool canAttack = true;
    bool union;
    public int level = 0;
    float first = 0;
    float atk = 30;
    float atkDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        uType = UnitTpye.Fire;
    }

    // Update is called once per frame
    void Update()
    {
        //if (movable.block.isWaiting || !MapManager.instance.monsterManager.isWave)
        //return;

        if (canAttack)
            StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        first = 0;
        Collider[] monsters = Physics.OverlapSphere(transform.position, 2);
        Monster first_mob = null;
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
            {
                if (first <= mob.moved)
                {
                    first = mob.moved;
                    first_mob = mob;
                }
            }
        }
        if (first_mob == null)
            yield break;

        //first_mob.Damaged(atk);
        SplashAttack(first_mob);

        canAttack = false;
        StartCoroutine(ColorChange());
        yield return new WaitForSeconds(atkDelay);
        canAttack = true;
    }

    IEnumerator ColorChange()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void SplashAttack(Monster target)
    {
        Collider[] monsters = Physics.OverlapSphere(target.transform.position, 1);
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
                mob.Damaged(atk, Damage_Type.physic);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }

    private void OnMouseDown()
    {
        union = false;
    }
    private void OnMouseUp()
    {
        union = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (union == true)
        {
            switch (uType)
            {
                case UnitTpye.Fire:
                    if (other.gameObject.GetComponent<UnitUnion>().uType == UnitTpye.Fire && level == 1)
                    {
                        level = 2;
                        Destroy(other.gameObject);
                    }
                    break;
                case UnitTpye.Wind:
                    break;
                case UnitTpye.Earth:
                    break;
                case UnitTpye.Water:
                    break;
            }
        }
    }
}

