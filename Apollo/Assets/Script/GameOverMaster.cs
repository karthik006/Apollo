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
            if (words[0] == Apollo.CurrentUser)
            {
                pos = Int32.Parse(words[1]);
                break;
            }
        }

        streamReader.Close();

        string dataPath = "Assets/Data/data.txt";
        StreamReader inputStream = new StreamReader(dataPath);
        inputStream.BaseStream.Seek(pos, SeekOrigin.Begin);
        //Debug.Log(inputStream.BaseStream.Position);
        line = inputStream.ReadLine();
        //Debug.Log("Line1: " + line);
        if (string.IsNullOrEmpty(line))
            line = inputStream.ReadLine();
        Debug.Log("Line2: " + line);
        string[] word = line.Split('|');
        int curCoins = Int32.Parse(word[1]);
        int curDist = Int32.Parse(word[2]);
        coin += curCoins;
        if (totalScore < curDist)
        {
            totalScore = curDist;
            highScore.enabled = false;
        }
        else
        {
            highScore.enabled = true;
        }
        inputStream.Close();

        string tempPath = "Assets/Data/temp.txt";
        StreamWriter outputStream = new StreamWriter(tempPath, false);
        inputStream = new StreamReader(dataPath);
        //string data = inputStream.ReadToEnd();
        //data = data.Replace(Apollo.CurrentUser + "|" + curCoins + "|" + curDist + "*", Apollo.CurrentUser + "|" + coin + "|" + totalScore + "*");
        while ((line = inputStream.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            if (words[0] == Apollo.CurrentUser)
            {
                outputStream.WriteLine(Apollo.CurrentUser + "|" + coin + "|" + totalScore);
            }
            else
            {
                outputStream.WriteLine(line);
            }
        }

        inputStream.Close();
        outputStream.Close();

        File.Delete(dataPath);
        File.Move(tempPath, dataPath);

        CalculateIndex();
    }

    public static void CalculateIndex()
    {
        string dataPath = "Assets/Data/data.txt";
        StreamReader inputStream = new StreamReader(dataPath);
        string indexPath = "Assets/Data/indexdata.txt";
        StreamWriter streamWriter = new StreamWriter(indexPath);
        string line;
        //inputStream.DiscardBufferedData();
        long pos = 0;

        while ((line = inputStream.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            streamWriter.WriteLine(words[0] + "|" + pos);
            pos += line.Length + 2;
            //pos = inputStream.BaseStream.Position;
        }

        inputStream.Close();
        streamWriter.Close();

        LoginScript.SortIndex();
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
