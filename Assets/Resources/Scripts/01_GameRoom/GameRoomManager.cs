using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class GameRoomManager : MonoBehaviourPunCallbacks
{
    [Header("UI Objects")]
    [SerializeField] Button readyStartBtn;
    [SerializeField] TextMeshProUGUI readyCountText;

    [Header("Variables")]
    [SerializeField] int playerCnt;
    [SerializeField] int readyCnt;
    [SerializeField] bool isReady = false;

    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            readyStartBtn.GetComponentInChildren<TextMeshProUGUI>().text = "Start";
            readyStartBtn.onClick.AddListener(startGame);
        }
        else
        {
            readyStartBtn.onClick.AddListener(ready);
        }

        playerCnt = PhotonNetwork.PlayerList.Length;
        readyCountText.text = "ready Players: " + readyCnt + " / " + (playerCnt - 1);
    }

    private void Update()
    {
        playerCnt = PhotonNetwork.PlayerList.Length;
        readyCountText.text = "ready Players: " + readyCnt + " / " + (playerCnt - 1);
    }

    private void startGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (readyCnt == playerCnt - 1)
            {
                PhotonNetwork.LoadLevel("02_Game");
            }
        }
    }

    private void ready()
    {
        if (!isReady)
        {
            isReady = true;
            photonView.RPC("changeReadyCnt", RpcTarget.All, 0);
        }
        else
        {
            isReady = false;
            photonView.RPC("changeReadyCnt", RpcTarget.All, 1);
        }
    }

    [PunRPC]
    private void changeReadyCnt(int mode)
    {
        if (mode == 0)
        {
            readyCnt++;
            //readyCountText.text = "ready Players: " + readyCnt + " / " + (playerCnt - 1);
        }
        else
        {
            readyCnt--;
            //readyCountText.text = "ready Players: " + readyCnt + " / " + (playerCnt - 1);
        }
    }

}
