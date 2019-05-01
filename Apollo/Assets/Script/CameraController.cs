﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static int playerHealth;
    public float speed;
    public static float incr;
    float totalDistance;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        incr = 0;
        playerHealth = 100;
        totalDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        incr += Time.deltaTime;
        totalDistance += Time.deltaTime;
        transform.position += Vector3.right * Time.deltaTime * speed;
        if(incr >= 15)
        {
            incr = 0;
            speed += 0.5f;
        }

        if(playerHealth == 0)
        {
            Debug.Log("Score: " + (int)(totalDistance * 100));
            Destroy(player);
            Debug.Break();
            //return;
        }
    }
}