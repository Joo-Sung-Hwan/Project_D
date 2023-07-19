using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    float first;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Collider[] monsters = Physics.OverlapSphere(transform.position, 2);
            Monster first_mob;
            foreach (var item in monsters)
            {
                Monster mob;
                if (mob = item.GetComponent<Monster>())
                {
                    first = first > mob.moved ? first : mob.moved;
                    first_mob = mob;
                }
            }
            //first_mob.md.curHP = 
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
