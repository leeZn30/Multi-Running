using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{

    [Header("Prefabs")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject myPlayer;

    [Header("Objects")]
    [SerializeField] GameObject panel;

    [Header("Variables")]
    public string winner;
    [SerializeField] public bool isStart = true;

    private void Awake()
    {
        myPlayer = PhotonNetwork.Instantiate(player.name, new Vector3(0, 0, -1), Quaternion.identity);

        panel.GetComponentInChildren<Button>().onClick.AddListener(leaveRoom);
    }

    [PunRPC]
    private void showPanel()
    {
        isStart = false;
        panel.GetComponentInChildren<TextMeshProUGUI>().text = "Winner is " + winner;
        panel.SetActive(true);
    }

    [PunRPC]
    private void changeWinner(string name)
    {
        Debug.Log(name);
        winner = name;
    }

    public void callWinner(string name)
    {
        photonView.RPC("changeWinner", RpcTarget.All, name);
    }

    public void finishGame()
    {
        photonView.RPC("showPanel", RpcTarget.All);
    }

    private void leaveRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.DestroyAll();
        }
        PhotonNetwork.LeaveRoom();

    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

}
