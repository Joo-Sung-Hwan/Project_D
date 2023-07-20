using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitUnion : MonoBehaviour
{
    float first = 0;
    float atk = 30;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
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
                return;

            first_mob.Damaged(atk);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
