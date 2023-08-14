using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MapsManager : MonoBehaviour
{
    [SerializeField] List<PhotonView> pvs;

    // Start is called before the first frame update
    void Start()
    {
        /*
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                break;
            case 2:
                
                break;
            case 3:
                
                break;
            case 4:
                
                break;
            default:
                break;
        }
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
