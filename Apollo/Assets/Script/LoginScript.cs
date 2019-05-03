using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginScript : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text message;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Login()
    {
        string user = username.text.Trim();
        string pass = password.text.Trim();

        if (user.Length > 5 && pass.Length > 5)
        {
            string encryptedPass = Md5Sum(pass);
            string path = "Assets/Data/login.txt";
            string line;
            bool flag = false;

            StreamReader inputStream = new StreamReader(path);

            while ((line = inputStream.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                if (words[0] == user && words[1] == encryptedPass)
                {
                    flag = true;
                    message.text = "Signed in.";
                    username.text = "";
                    password.text = "";
                    Apollo.CurrentUser = user;
                    SceneManager.LoadScene(1);
                    break;
                }
            }

            if (!flag)
            {
                message.text = "Incorrect username or password.";
                username.text = "";
                password.text = "";
            }
        }
        else
        {
            message.text = "Minimum 6 chars";
        }
    }

    public void Register()
    {
        string user = username.text.Trim();
        string pass = password.text.Trim();

        if (user.Length > 5 && pass.Length > 5)
        {
            string encryptedPass = Md5Sum(pass);
            string path = "Assets/Data/login.txt";
            string line;
            bool flag = false;

            StreamReader inputStream = new StreamReader(path);

            while ((line = inputStream.ReadLine()) != null)
            {
                string[] words = line.Split('|');
                if (words[0] == user)
                {
                    flag = true;
                    break;
                }
            }

            inputStream.Close();

            if (!flag)
            {
                StreamWriter outputStream = new StreamWriter(path, true);
                outputStream.WriteLine(user + "|" + encryptedPass);
                outputStream.Close();

                message.text = "User created.";
                username.text = "";
                password.text = "";

                string dataPath = "Assets/Data/data.txt";
                outputStream = new StreamWriter(dataPath, true);
                long pos = outputStream.BaseStream.Position;
                outputStream.WriteLine(user + "|0|0");
                outputStream.Close();

                string indexPath = "Assets/Data/indexdata.txt";
                outputStream = new StreamWriter(indexPath, true);
                outputStream.WriteLine(user + "|" + pos);
                outputStream.Close();
                SortIndex();
            }
            else
            {
                message.text = "Username exists";
            }
        }
        else
        {
            message.text = "Minimum 6 chars";
        }
    }

     public static void SortIndex()
    {
        string indexPath = "Assets/Data/indexdata.txt";
        StreamReader inputStream = new StreamReader(indexPath);
        string line;
        Dictionary<string, string> dict = new Dictionary<string, string>();
        List<string> l1 = new List<string>();

        while ((line = inputStream.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            Debug.Log(line + " " + words[0]);
            dict.Add(words[0], words[1]);
            l1.Add(words[0]);
        }
        l1.Sort();

        inputStream.Close();

        StreamWriter outputStream = new StreamWriter(indexPath);

        foreach (string item in l1)
        {
            dict.TryGetValue(item, out string pos);
            outputStream.WriteLine(item + "|" + pos);
        }

        outputStream.Close();
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
