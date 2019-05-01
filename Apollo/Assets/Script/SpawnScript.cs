using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] obj;
    //public float spawnMin = 3f;
    //public float spawnMax = 5f;

    public Transform[] points;

    public GameObject enemy;

    float dist;

    // Start is called before the first frame update
    void Start()
    {

        dist = 0;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (dist + 15 <= transform.position.x)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        int x = Random.Range(0, obj.GetLength(0));
        Instantiate(obj[x], transform.position, Quaternion.identity);
        if (transform.tag == "Spawn1" && (x == 2 || x == 3))
        {
            Instantiate(enemy, points[2].position, Quaternion.identity);
        }
        else if(transform.tag == "Spawn2")
        {

            if(x != 2)
                Instantiate(enemy, points[Random.Range(0, 2)].position, Quaternion.identity);
        }
        
        dist = transform.position.x;
        //Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
