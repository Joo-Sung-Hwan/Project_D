using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EffPartcle : MonoBehaviour
{
    ParticleSystem ps;

    UnityAction action;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void EffTest(float dis)
    {
        EffStart(dis, -1, null);
    }

    public void EffStart(float attDistance, float attNextDelay, UnityAction action)
    {
        ps.startLifetime = attDistance;
        ps.Play();
        
        this.action = action;
        if(attNextDelay != -1)
            Invoke("NextAction", attNextDelay);
    }

    public void EffStop()
    {
    }

    void NextAction()
    {
        if(action != null)
        {
            action();
            action = null;
        }
    }

}
