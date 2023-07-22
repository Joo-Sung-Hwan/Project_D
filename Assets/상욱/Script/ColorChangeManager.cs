using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChangeManager : MonoBehaviour
{    
    [SerializeField] private Image severInimage1;
    [SerializeField] private Color color;

    public void OnColorChange()
    {
        severInimage1.color = color;
    }
    
}
