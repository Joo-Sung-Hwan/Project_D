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
        elemental_Type.text = $"�Ӽ� : {Translate_Elemental_Type(unit.ud.element_type)}";
        attack_Type.text = $"���� ��� : {Translate_Atk_Type(unit.ud.atk_type)}";
        attack.text = $"���ݷ� : {unit.ud.attack}";
        mana.text = $"���� : {unit.ud.curMana} / {unit.ud.maxMana}";
    }

    string Translate_Elemental_Type(Element_Type et)
    {
        switch (et)
        {
            case Element_Type.water:
                return "��";
            case Element_Type.wind:
                return "ǳ";
            case Element_Type.earth:
                return "��";
            case Element_Type.fire:
                return "ȭ";
            case Element_Type.light:
                return "��";
            case Element_Type.dark:
                return "��";
            default:
                return "��";
        }
    }
    string Translate_Atk_Type(Attack_Type at)
    {
        switch (at)
        {
            case Attack_Type.normal:
                return "����";
            case Attack_Type.splash:
                return "����";
            default:
                return "Atk_type Ȯ��";
        }
    }
}
