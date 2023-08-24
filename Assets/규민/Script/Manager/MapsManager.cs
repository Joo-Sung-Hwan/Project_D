using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MapsManager : MonoBehaviour
{
    public static MapsManager instance = null;
    [HideInInspector] public GameObject map_list;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Map_instantiate()
    {
        switch (InGameUI.instance.GetBtnIndex())
        {
            case 0:
                map_list = PhotonNetwork.Instantiate("Map", new Vector3(0, 0, 0), transform.rotation);
                break;
            case 1:
                map_list = PhotonNetwork.Instantiate("Map", new Vector3(0, 0, 17), transform.rotation);
                break;
            case 2:
                map_list = PhotonNetwork.Instantiate("Map", new Vector3(17, 0, 0), transform.rotation);
                break;
            case 3:
                map_list = PhotonNetwork.Instantiate("Map", new Vector3(17, 0, 17), transform.rotation);
                break;
            default:
                break;
        }
    }
}
