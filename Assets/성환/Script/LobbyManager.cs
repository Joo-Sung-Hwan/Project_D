using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviour
{
    PhotonView photonview;
    [SerializeField] private Image userList;
    [SerializeField] private Image optionsSettingBG;
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnStartBtn()
    {

    }

}
