using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    UnityAction action;

    #endregion

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Init()
    {
        ps = GetComponent<ParticleSystem>();
    }
    public void EffTest(float dis)
    {
        EffStart(dis, -1, null);
    }

    public void EffStart(float attDistance, float attNextDelay, UnityAction action)
    {
        ps.startLifetime = attDistance;
        ps.Play();

        this.action = action;
        if (attNextDelay != -1)
            Invoke("NextAction", attNextDelay);
    }

    public void EffStop()
    {
    }

    void NextAction()
    {
        if (action != null)
        {
            action();
            action = null;
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
