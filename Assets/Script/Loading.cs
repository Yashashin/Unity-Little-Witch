using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadGame", 5);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(2);
    }

  
}
