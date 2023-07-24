using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    [SerializeField] Image hpbar_BG;
    public Image hpbar;
    public Monster monster;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        hpbar_BG.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(monster.transform.position)+ new Vector3(0,-10);
    }
}
