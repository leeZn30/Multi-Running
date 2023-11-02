using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Line : MonoBehaviourPun
{
    [SerializeField] GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && gameManager.isStart)
        {
            gameManager.callWinner(other.GetComponentInParent<PlayerController>().name);
            gameManager.finishGame();
        }
    }
}
