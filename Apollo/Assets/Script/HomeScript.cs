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
        displayName.text = Apollo.CurrentUser;
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

    }

    public void Stats()
    {

    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
