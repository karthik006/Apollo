using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class GameOverMaster : MonoBehaviour
{
    public Text highScore;
    public Text score;
    public Text coins;

    // Start is called before the first frame update
    void Start()
    {
        int coin = Apollo.Coins;
        float dist = Apollo.Distance;
        int totalScore = (int)(dist * coin);

        coins.text = "Coins: " + coin;
        score.text = "Score: " + totalScore;

        string indexPath = "Assets/Data/indexdata.txt";
        StreamReader streamReader = new StreamReader(indexPath);
        string line;
        int pos = 0;

        while ((line = streamReader.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            if(words[0] == Apollo.CurrentUser)
            {
                pos = Int32.Parse(words[1]);
                break;
            }
        }

        streamReader.Close();

        string dataPath = "Assets/Data/data.txt";
        StreamReader inputStream = new StreamReader(dataPath);
        inputStream.BaseStream.Seek(pos, SeekOrigin.Begin);
        line = inputStream.ReadLine();
        string[] word = line.Split('|');
        int curCoins = Int32.Parse(word[1]);
        int curDist = Int32.Parse(word[2]);
        coin += curCoins;
        if(totalScore < curDist)
        {
            totalScore = curDist;
            highScore.enabled = false;
        }
        else
        {
            highScore.enabled = true;
        }
        inputStream.Close();

        inputStream = new StreamReader(dataPath);
        string data = inputStream.ReadToEnd();
        data = data.Replace(Apollo.CurrentUser + "|" + curCoins + "|" + curDist, Apollo.CurrentUser + "|" + coin + "|" + totalScore);
        inputStream.Close();

        StreamWriter outputStream = new StreamWriter(dataPath, false);
        outputStream.Write(data);
        outputStream.Close();
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadScene(2);
    }

    public void Home()
    {
        SceneManager.LoadScene(1);
    }
}
