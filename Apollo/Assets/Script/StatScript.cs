using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StatScript : MonoBehaviour
{
    public Text stats;

    // Start is called before the first frame update
    void Start()
    {
        string indexPath = "Assets/Data/data.txt";
        StreamReader inputStream = new StreamReader(indexPath);
        string line;
        Dictionary<string, string> dict = new Dictionary<string, string>();
        int i = 0, j = 0;

        while ((line = inputStream.ReadLine()) != null)
        {
            string[] words = line.Split('|');
            dict.Add(words[0], words[2]);
            //arr[i] = Int32.Parse(words[2]);
            i++;
        }

        inputStream.Close();

        int[] arr = new int[i];

        foreach(string x in dict.Values)
        {
            arr[j] = Int32.Parse(x);
            j++;
        }

        Sort(arr);
        Debug.Log(arr[0] + "x" + arr[1]);

        string data = "";
        foreach(int x in arr)
        {
            var keys = from entry in dict
                       where entry.Value == x.ToString()
                       select entry.Key;

            foreach (var key in keys)
                data = key + "\nScore: " + x + "\n\n" + data;
        }

        stats.text = data;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BaackToHome()
    {
        SceneManager.LoadScene(1);
    }

    void Sort(int[] arr)
    {
        int n = arr.Length;

        for (int i = n / 2 - 1; i >= 0; i--)
            Heapify(arr, n, i);

        for (int i = n - 1; i >= 0; i--)
        {
            int temp = arr[0];
            arr[0] = arr[i];
            arr[i] = temp;

            Heapify(arr, i, 0);
        }
    }

    void Heapify(int[] arr, int n, int i)
    {
        int largest = i;
        int l = 2 * i + 1;
        int r = 2 * i + 2;

        if (l < n && arr[l] > arr[largest])
            largest = l;

        if (r < n && arr[r] > arr[largest])
            largest = r;

        if (largest != i)
        {
            int swap = arr[i];
            arr[i] = arr[largest];
            arr[largest] = swap;

            Heapify(arr, n, largest);
        }
    }
}
