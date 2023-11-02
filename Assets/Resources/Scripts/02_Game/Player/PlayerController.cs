using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerController : MonoBehaviourPun
{
    [Header("Variable")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Material material;
    [SerializeField] GameManager gameManager;
    public string name;

    [Header("Components")]
    [SerializeField] private Rigidbody rigid;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        if (photonView.IsMine)
        {
            GetComponentInChildren<MeshRenderer>().material = material;
            name = PhotonNetwork.LocalPlayer.NickName;

            rigid = GetComponent<Rigidbody>();
        }
    }

    void Update()
    {
        if (gameManager.isStart)
        {
            move();
            Jump();
        }
    }

    private void move()
    {
        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                gameObject.transform.Translate(Vector3.back * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                gameObject.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gameObject.transform.Translate(Vector3.left * Time.deltaTime * speed);
            }
        }
    }

    void Jump() 
    { 
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            }
        }
    }

}
