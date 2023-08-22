using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Information : MonoBehaviour
{
    [SerializeField] TMP_Text elemental_Type;
    [SerializeField] TMP_Text attack_Type;
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text mana;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInformText(Unit unit)
    {
        elemental_Type.text = $"속성 : {Translate_Elemental_Type(unit.ud.element_type)}";
        attack_Type.text = $"공격 대상 : {Translate_Atk_Type(unit.ud.atk_type)}";
        attack.text = $"공격력 : {unit.ud.attack}";
        mana.text = $"마나 : {unit.ud.curMana} / {unit.ud.maxMana}";
    }

    string Translate_Elemental_Type(Element_Type et)
    {
        switch (et)
        {
            case Element_Type.water:
                return "수";
            case Element_Type.wind:
                return "풍";
            case Element_Type.earth:
                return "지";
            case Element_Type.fire:
                return "화";
            case Element_Type.light:
                return "광";
            case Element_Type.dark:
                return "암";
            default:
                return "무";
        }
    }
    string Translate_Atk_Type(Attack_Type at)
    {
        switch (at)
        {
            case Attack_Type.normal:
                return "단일";
            case Attack_Type.splash:
                return "범위";
            default:
                return "Atk_type 확인";
        }
    }
}
