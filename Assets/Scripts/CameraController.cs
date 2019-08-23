﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Player;
    private Vector3 CameraToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        CameraToPlayer = transform.position - Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position + CameraToPlayer;
    }
}
