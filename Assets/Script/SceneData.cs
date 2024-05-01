using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public GameObject pos1;
    public GameObject pos2;
    // Start is called before the first frame update
    void Start()
    {
        //PlayerPrefs.SetInt("isClear", 0);
        int num=PlayerPrefs.GetInt("isClear", 0);
        Debug.Log(num);
        if(num==0)
        {
            Debug.Log("jha");
            pos1.SetActive(true);
            pos2.SetActive(false);
        }
        else
        {
            pos1.SetActive(false);
            pos2.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
