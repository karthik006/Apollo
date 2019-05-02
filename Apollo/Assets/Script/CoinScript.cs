using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, 45 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerController.i == 0)
        {
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
            r.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (PlayerController.i == 0)
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = true;
        else
            foreach (Renderer r in GetComponentsInChildren<Renderer>())
                r.enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerController.i == 0)
        {
            Destroy(gameObject);
            CameraController.coins += 1;
        }
    }
}
