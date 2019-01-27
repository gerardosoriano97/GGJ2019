using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KeyToScene : MonoBehaviour
{
    public string sceneToLoad;
    public KeyCode key;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key)){
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
