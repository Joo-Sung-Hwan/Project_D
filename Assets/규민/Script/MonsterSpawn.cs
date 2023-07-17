using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    [SerializeField] List<Monster> monsters;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", 1 , 1f);

    }

    // Update is called once per frame
    void Update()
    {
    }

    void Spawn()
    {
        Instantiate(monsters[0], transform);
    }
}
