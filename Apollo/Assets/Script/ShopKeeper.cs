using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BaackToHome()
    {
        SceneManager.LoadScene(1);
    }

    public void HealthUpgrade1()
    {
        string shopPath = "Assets/Data/shop.txt";
        


    }
}
