using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;

public class ShopKeeper : MonoBehaviour
{
    public Text message;
    public Text coins;

    public GameObject panel;
    Text textMessage;

    int curCoins;
    bool isExist;

    // Start is called before the first frame update
    void Start()
    {
        textMessage = panel.GetComponentInChildren<Text>(true);
        panel.SetActive(false);
        string indexPath = "Assets/Data/indexdata.txt";
        StreamReader indexReader = new StreamReader(indexPath);
        string line;
        int pos = 0;
        isExist = false;

        while ((line = indexReader.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            if (words[0] == Apollo.CurrentUser)
            {
                pos = Int32.Parse(words[1]);
                break;
            }
        }

        indexReader.Close();

        string dataPath = "Assets/Data/data.txt";
        StreamReader streamReader = new StreamReader(dataPath);
        streamReader.BaseStream.Seek(pos, SeekOrigin.Begin);
        line = streamReader.ReadLine();
        //Debug.Log("Line1: " + line);
        if (string.IsNullOrEmpty(line))
            line = streamReader.ReadLine();
        //Debug.Log("Line2: " + line);
        string[] word = line.Split('|');
        curCoins = Int32.Parse(word[1]);

        coins.text = "Coins: " + curCoins;

        streamReader.Close();

        string shopPath = "Assets/Data/shop.txt";
        StreamReader inputStream = new StreamReader(shopPath);
        while ((line = inputStream.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            if (words[0] == Apollo.CurrentUser)
            {
                isExist = true;
                message.text = "Current upgrades: " + words[1];
                break;
            }
        }
        inputStream.Close();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BaackToHome()
    {
        SceneManager.LoadScene(1);
    }

    public void HealthUpgrade()
    {
        if (!isExist)
        {
            string name = EventSystem.current.currentSelectedGameObject.name;
            string line, msg = "";
            int reqCoins = 50;

            if (name == "H1")
            {
                reqCoins = 10;
                msg = "You will have an extra life the next time you play endless mode.";
            }
            else if (name == "H2")
            {
                reqCoins = 20;
                msg = "You will have 2 extra lives the next time you play endless mode.";
            }
            else if (name == "H3")
            {
                reqCoins = 30;
                msg = "You will have 3 extra lives the next time you play endless mode.";
            }
            else if (name == "S1")
            {
                reqCoins = 20;
                msg = "You will be 25% faster the next time you play endless mode.";
            }
            else if (name == "S2")
            {
                reqCoins = 30;
                msg = "You will be 50% faster the next time you play endless mode.";
            }

            if (curCoins >= reqCoins)
            {
                curCoins -= reqCoins;

                string tempPath = "Assets/Data/temp.txt";
                string dataPath = "Assets/Data/data.txt";
                StreamWriter outputStream = new StreamWriter(tempPath, false);
                StreamReader inputStream = new StreamReader(dataPath);
                while ((line = inputStream.ReadLine()) != null)
                {
                    string[] words = line.Split('|');
                    if (words[0] == Apollo.CurrentUser)
                    {
                        outputStream.WriteLine(Apollo.CurrentUser + "|" + curCoins + "|" + words[2]);
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

                GameOverMaster.CalculateIndex();

                string shopPath = "Assets/Data/shop.txt";
                StreamWriter outStream = new StreamWriter(shopPath, true);
                outStream.WriteLine(Apollo.CurrentUser + "|" + name);
                outStream.Close();

                panel.SetActive(true);
                textMessage.text = msg;
            }
            else
            {
                panel.SetActive(true);
                textMessage.text = "You don't have enough coins. :(";
            }
        }
        else
        {
            panel.SetActive(true);
            textMessage.text = "You already have upgrades.";
        }
    }

    public void PanelButton()
    {
        panel.SetActive(false);
        if(textMessage.text.StartsWith("You will"))
            SceneManager.LoadScene(1);
    }
}
