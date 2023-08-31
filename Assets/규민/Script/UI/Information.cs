using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Information : MonoBehaviour
{
    [SerializeField] TMP_Text elemental_Type;
    [SerializeField] TMP_Text attack_Type;
    [SerializeField] TMP_Text attack;
    [SerializeField] TMP_Text mana;
    public Button sellbtn;

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
        mana.text = $"마나 : {(int)unit.ud.curMana} / {unit.ud.maxMana}";
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

    public void OnSellUnit(Unit unit)
    {
        if(unit.GetComponent<MovableObj>().block.isWating == false)
        {
            InGameUI.instance.unit_dic[unit.name] -= 1;
            if (InGameUI.instance.unit_dic[unit.name] <= 0)
            {
                InGameUI.instance.unit_dic.Remove(unit.name);
                InGameUI.instance.synergy_list[unit.ud.element_type.ToString()].count -= 1;
                if (InGameUI.instance.synergy_list[unit.ud.element_type.ToString()].count <= 0)
                {
                    Destroy(InGameUI.instance.synergy_list[unit.ud.element_type.ToString()].gameObject);
                    InGameUI.instance.synergy_list.Remove(unit.ud.element_type.ToString());
                    for(int i = 0; i < InGameUI.instance.s_list.Count; i++)
                    {
                        if(InGameUI.instance.s_list[i].s_type == unit.ud.element_type)
                        {
                            InGameUI.instance.s_list.RemoveAt(i);
                        }
                    }
                    
                }
            }
            MapManager.instance.unitManager.units.Remove(unit);
            unit.GetComponent<MovableObj>().block.SetUnit(true);
            unit.DestroyUnit();
            GameManager.instance.playermanager.SetGold(1, true);
        }
        else
        {
            MapManager.instance.unitManager.units.Remove(unit);
            unit.GetComponent<MovableObj>().block.SetUnit(true);
            unit.DestroyUnit();
        }
    }
}
