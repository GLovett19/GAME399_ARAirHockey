using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopMenu_Script : MonoBehaviour
{

    public string str_toUnload;
    // Start is called before the first frame update
    void Start()
    {
        str_toUnload = ActiveSceneManager.GetSceneName();
        Debug.Log(str_toUnload);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SceneTransition(string toLoad)
    {
        ActiveSceneManager.UnloadScene(str_toUnload);
        ActiveSceneManager.LoadScene(toLoad, false);
        
    }
}
