using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    ParticleSystem ps;
    public MonsterManager mm;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        mm = FindObjectOfType<MonsterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {
        if (mm.isWave != false)
        {
            ps.Play();
        }

        else if (mm.isWave == false)
        {
            ps.Stop();
        }
    }
}
