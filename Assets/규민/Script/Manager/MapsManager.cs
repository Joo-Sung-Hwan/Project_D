using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MapsManager : MonoBehaviour
{
    [SerializeField] List<PhotonView> pvs;
    [SerializeField] GameObject map;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Map_instantiate()
    {
        switch (PhotonNetwork.LocalPlayer.ActorNumber)
        {
            case 1:
                PhotonNetwork.Instantiate("Map", new Vector3(0, 0, 0), transform.rotation);
                break;
            case 2:
                PhotonNetwork.Instantiate("Map", new Vector3(0, 0, 17), transform.rotation);
                break;
            case 3:
                PhotonNetwork.Instantiate("Map", new Vector3(17, 0, 0), transform.rotation);
                break;
            case 4:
                PhotonNetwork.Instantiate("Map", new Vector3(17, 0, 17), transform.rotation);
                break;
            default:
                break;
        }
    }
}
