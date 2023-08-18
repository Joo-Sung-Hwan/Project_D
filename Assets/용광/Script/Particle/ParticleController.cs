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
        public Effect_Type eft_type;
        public float atkdelay;
        public float damage;
        public float element_const;
        public float debufftime;
    }
    public Particle_Data pd;
    #endregion
    ParticleSystem ps;
    UnityAction action;
    Monster monster;
    [SerializeField] GameObject balltype;
    float delaytime;

    #endregion

    // Update is called once per frame
    void Update()
    {
        delaytime += Time.deltaTime;
        ps.trigger.AddCollider(monster);
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

    private void OnParticleTrigger()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        monster = other.GetComponent<Monster>();

        if (monster != null && monster.gameObject.layer == 10)
        {
            if (pd.atkdelay <= delaytime)
            {
                switch (pd.eft_type)
                {
                    case Effect_Type.ball:
                        balltype.SetActive(false);
                        if (balltype == false)
                        {
                            ps.Play();
                        }
                        break;
                    case Effect_Type.thrower:
                        P_Attack(pd.atk_type, pd.damage_type, pd.debuff_type, pd.debufftime);
                        break;
                    case Effect_Type.spawn:
                        break;
                    case Effect_Type.strike:
                        break;
                    default:
                        break;
                }
                delaytime = 0;
            }
            
        }
    }
    
    //공격타입
    void P_Attack(Attack_Type attack_Type, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuff_Time = 0f)
    {
        switch (attack_Type)
        {
            case Attack_Type.normal:
                NormalAttack(monster, damage_Type, debuff_Type, debuff_Time);
                break;
            case Attack_Type.splash:
                SplashAttack(monster, damage_Type, debuff_Type, debuff_Time);
                break;
            default:
                break;
        }
    }

    //단일, 스플래쉬 공격 코드((P_Attack)부속코드)
    void NormalAttack(Monster target, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuffTime = 0)
    {
        target.Damaged(pd.damage, damage_Type, Element_Const(target), debuff_Type, debuffTime);
    }

    void SplashAttack(Monster target, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuffTime = 0)
    {
        Collider[] monsters = Physics.OverlapSphere(target.transform.position, 1);
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
                mob.Damaged(pd.damage, damage_Type, Element_Const(mob), debuff_Type, debuffTime);
        }
    }

    // 속성값 계산((P_Attack)부속코드)
    float Element_Const(Monster monster)
    {
        Element_Type md_et = monster.md.element_Type;
        switch (pd.element_type)
        {
            case Element_Type.none:
                return 1;
            case Element_Type.water:
                return md_et == Element_Type.wind ? 0.75f
                    : md_et == Element_Type.fire ? 1.25f
                    : 1;
            case Element_Type.wind:
                return md_et == Element_Type.earth ? 0.75f
                    : md_et == Element_Type.water ? 1.25f
                    : 1;
            case Element_Type.earth:
                return md_et == Element_Type.fire ? 0.75f
                    : md_et == Element_Type.wind ? 1.25f
                    : 1;
            case Element_Type.fire:
                return md_et == Element_Type.water ? 0.75f
                    : md_et == Element_Type.earth ? 1.25f
                    : 1;
            default:
                return 1;
        }
    }

}
