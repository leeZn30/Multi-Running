using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CamerSetup : MonoBehaviourPun
{
    public Transform target;
    private Transform tr;

    void Start()
    {
        if (photonView.IsMine)
        {
            target = transform;
            tr = Camera.main.transform;
        }

    }

    void LateUpdate()
    {
        if (photonView.IsMine)
        {
            tr.position = new Vector3(target.position.x - 0.5f, tr.position.y, target.position.z - 6.5f);

            tr.LookAt(target);

        }
    }
}
