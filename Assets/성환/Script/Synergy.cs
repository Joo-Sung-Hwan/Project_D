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
        count_text.text = $"{count} / 3";
        if(count >= 3)
        {
            active_image.gameObject.SetActive(true);
        }
        else
        {
            active_image.gameObject.SetActive(false);
        }
    }
}
