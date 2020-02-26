using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class loadmenu : MonoBehaviour
{
    public string LevelToLoadName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("TopMenu");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
