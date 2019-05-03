using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    public Text displayName;

    // Start is called before the first frame update
    void Start()
    {
        GameOverMaster.CalculateIndex();
        displayName.text = "Welcome, " + Apollo.CurrentUser + "\nScore: " + Apollo.Coins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(2);
    }

    public void Shop()
    {
        SceneManager.LoadScene(4);
    }

    public void Stats()
    {
        SceneManager.LoadScene(5);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
