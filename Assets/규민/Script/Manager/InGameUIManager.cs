using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour
{
    public Canvas canvas_hp;
    public Transform bar_Parent;

    public Image Mask;
    public Image information_Prf;
    [HideInInspector] public Image information;
    // Start is called before the first frame update
    void Start()
    {
        Mask.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveInform(Transform trans, Unit unit)
    {
        Vector2 wtsTrans = Camera.main.WorldToScreenPoint(trans.position) + new Vector3(200, 200);

        if (!information)
        {
            information = Instantiate(information_Prf, canvas_hp.transform);
            information.rectTransform.anchoredPosition = wtsTrans;
        }
        else
        {
            information.gameObject.SetActive(true);
            information.rectTransform.anchoredPosition = wtsTrans;
        }
        information.GetComponent<Information>().SetInformText(unit);
        Mask.gameObject.SetActive(true);
    }

    public void OnSetActiveInform()
    {
        information.gameObject.SetActive(false);
    }
    
}
