using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Synergy : MonoBehaviour
{
    public Element_Type s_type;
    [SerializeField] private Image active_image;
    [HideInInspector] public int count = 0;
    public TMP_Text count_text;


    public void Update()
    {
        if(s_type == Element_Type.dark || s_type == Element_Type.light)
        {
            count_text.text = $"{count} / 2";
            if (count >= 2)
            {
                active_image.gameObject.SetActive(true);
                //MapManager.instance.unitManager.ActiveSynergy(s_type);
            }
            else
            {
                active_image.gameObject.SetActive(false);
            }
        }
        else
        {
            count_text.text = $"{count} / 3";
            if (count >= 3)
            {
                active_image.gameObject.SetActive(true);
                //MapManager.instance.unitManager.ActiveSynergy(s_type);
            }
            else
            {
                active_image.gameObject.SetActive(false);
            }
        }
    }
}
