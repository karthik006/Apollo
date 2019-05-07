using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public static int playerHealth;
    public float speed;
    public static float incr;
    float totalDistance;
    public static int coins;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        incr = 0;
        totalDistance = 0;
        coins = 0;
        playerHealth = 100;

        string line;
        string tempPath = "Assets/Data/temp.txt";
        StreamWriter outputStream = new StreamWriter(tempPath, false);
        string shopPath = "Assets/Data/shop.txt";
        StreamReader inputStream = new StreamReader(shopPath);
        while ((line = inputStream.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            if (words[0] == Apollo.CurrentUser)
            {
                if (words[1] == "H1")
                    playerHealth += 100;
                else if (words[1] == "H2")
                    playerHealth += 200;
                else if (words[1] == "H3")
                    playerHealth += 300;
                else if (words[1] == "S1")
                    speed -= 0.5f;
                else if (words[1] == "S2")
                    speed -= 1f;
                break;
            }
            else
            {
                outputStream.WriteLine(line);
            }
        }
        inputStream.Close();
        outputStream.Close();

        File.Delete(shopPath);
        File.Move(tempPath, shopPath);

        Debug.Log(speed + " " + playerHealth);
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

        if(playerHealth <= 0)
        {
            //Debug.Log("Score: " + (int)(totalDistance * coins));
            Destroy(player);
            Apollo.Coins = coins;
            Apollo.Distance = totalDistance;
            SceneManager.LoadScene(3);
        }
    }
}
