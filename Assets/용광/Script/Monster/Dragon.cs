using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unit
{
    [SerializeField] ParticleController flameThrower;

    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.fire;
        ud.atk_type = Attack_Type.normal;
        ud.mana_type = Mana_Type.auto;
        ud.atkDelay = 1f;
        ud.attack = 30f;
        ud.maxMana = 1f;
        ud.curMana = 0f;
    }
    void Start()
    {
        Init();
    }

    protected override void Attack()
    {
        
    }  

    public override IEnumerator Skill()
    {
        Monster target = FindTarget();
        if (!target)
            yield break;
        transform.LookAt(FindTarget().transform);
        flameThrower.gameObject.SetActive(true);
        flameThrower.EffStart(1,1,null);
        Debug.Log("SKILL");
        ud.curMana = 0;
        yield break;
    }
}
