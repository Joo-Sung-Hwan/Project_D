using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mp_Bar : MonoBehaviour
{
    [SerializeField] Image mpbar_BG;
    public Image mpbar;
    public Unit unit;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mpbar_BG.rectTransform.anchoredPosition = Camera.main.WorldToScreenPoint(unit.transform.position) + new Vector3(0, -60);
    }
}
