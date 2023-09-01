using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaTitan : Unit
{
    [SerializeField] Transform head_Trans;
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.water;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 1f;
        ud.mana_type = Mana_Type.auto;
        ud.maxMana = 5f;
        ud.curMana = 0f;
        ud.isBuy = false;
    }
    private void Awake()
    {
        Init();
    }
    protected override void Attack()
    {
        StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic));
    }

    //스킬 애니메이션 시작할 때 호출
    public void ESkill_Start()
    {
        isSkill = true;
        transform.LookAt(target.transform);

        if (ptc == null)
            ptc = Instantiate(particle_Prf, head_Trans);
        else
            ptc.transform.position = head_Trans.position;

        ptc.Init();
        ptc.transform.SetParent(MapManager.instance.particle_parent);
        ptc.transform.localScale = Vector3.one;
        ptc.EffStart(1, 1, null);

        ptc.transform.position = head_Trans.position;
        ptc.transform.LookAt(target.transform);
        ud.curMana = 0;
        canManaRestore = false;
    }

    public override IEnumerator Skill()
    {
        anim.SetTrigger("skill");
        yield break;
    }
}
