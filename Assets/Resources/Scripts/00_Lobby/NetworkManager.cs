using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [Header("UI Objects")]
    [SerializeField] TMP_InputField nicknameFiled;
    [SerializeField] TMP_InputField gameRoomFiled;
    [SerializeField] Button createRoomBtn;
    [SerializeField] Button joinRoomBtn;
    [SerializeField] private GameObject panel;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        // 임시
        Screen.SetResolution(720, 480, false);
    }

    void Start()
    {
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();

        createRoomBtn.onClick.AddListener(createRoom);
        joinRoomBtn.onClick.AddListener(joinRoom);
    }

    void Connect() => PhotonNetwork.ConnectUsingSettings();

    public override void OnConnectedToMaster()
    {
        Debug.Log("Master 서버 연결");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("로비 참가");
    }


    private void joinRoom()
    {
        if (nicknameFiled.text != null && nicknameFiled.text != "")
        {
            PhotonNetwork.LocalPlayer.NickName = nicknameFiled.text;
            PhotonNetwork.JoinRandomRoom();
        }

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode + " " + message);
        panel.GetComponent<PanelOperate>().showPanel();
    }


    private void createRoom()
    {
        if ((nicknameFiled.text != null && nicknameFiled.text != "") && (gameRoomFiled.text != "" && gameRoomFiled.text != null))
        {
            PhotonNetwork.LocalPlayer.NickName = nicknameFiled.text;
            PhotonNetwork.CreateRoom(gameRoomFiled.text, new RoomOptions { MaxPlayers = 4 });
        }
    }

    public override void OnCreatedRoom()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("01_GameRoom");
        }
    }

}
