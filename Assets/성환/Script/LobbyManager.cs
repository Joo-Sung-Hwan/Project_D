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
    [SerializeField] private Image roomCreate_BG;
    // Start is called before the first frame update
    void Start()
    {
        photonview = GetComponent<PhotonView>();
        roomCreate_BG.transform.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCreateStartBtn()
    {
        roomCreate_BG.transform.gameObject.SetActive(true);
    }
    public void OnCreateStartBtn_OFF()
    {
        roomCreate_BG.transform.gameObject.SetActive(false);
    }

}
