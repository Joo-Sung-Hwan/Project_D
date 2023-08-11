using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleController : MonoBehaviour
{
    #region 변수 / 구조체선언
    #region 선언 - 데이터
    public struct Particle_Data
    {
        public Element_Type element_type;
        public Attack_Type atk_type;
        public float damage;
    }
    public Particle_Data pd;
    #endregion
    ParticleSystem ps;
    public MonsterManager mm;

    #endregion

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

    private void OnParticleCollision(GameObject other)
    {
        Monster monster = other.GetComponent<Monster>();
        if (monster != null && monster.gameObject.layer == 10)
        {
            monster.Damaged(pd.damage, Damage_Type.magic, 0.75f,Debuff_Type.slow, 2);
        }
    }
}
