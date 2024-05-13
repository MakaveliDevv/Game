using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Button btn;

    void Start() 
    {
        btn = FindAnyObjectByType<Button>();
    }

    public void SwitchScene() 
    {
        StartCoroutine(WaitBeforeSwitchScene());
        
        SceneManager.LoadScene(btn.SceneName);
    }

    public IEnumerator WaitBeforeSwitchScene() 
    {
        yield return new WaitForSeconds(3f);
    }
}
