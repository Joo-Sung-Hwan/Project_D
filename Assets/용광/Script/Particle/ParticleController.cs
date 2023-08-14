using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleController : MonoBehaviour
{
    #region 변수 / 구조체선언
    #region 선언 - 데이터
    public struct Particle_Data
    {
        public Debuff_Type debuff_type;
        public Damage_Type damage_type;
        public Element_Type element_type;
        public Attack_Type atk_type;
        public float damage;
        public float element_const;
        public float debufftime;
    }
    public Particle_Data pd;
    #endregion
    ParticleSystem ps;
    public MonsterManager mm;

    #endregion

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public virtual void Init()
    {
        ps = GetComponent<ParticleSystem>();
        mm = FindObjectOfType<MonsterManager>();
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
            monster.Damaged(pd.damage, pd.damage_type, pd.element_const ,pd.debuff_type, pd.debufftime);
        }
    }
}
