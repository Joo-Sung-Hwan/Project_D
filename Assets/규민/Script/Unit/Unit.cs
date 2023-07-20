using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] MovableObj movable;

    bool canAttack = true;
    float first = 0;
    float atk = 30;
    float atkDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (movable.block.isWaiting || !MapManager.instance.monsterManager.isWave)
            return;

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
                mob.Damaged(atk);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
