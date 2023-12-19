using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   #region Singleton

    public static PlayerManager instance;

    void Awake() 
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public void KillPlayer() 
    {
        Debug.Log("You died!");

        // Invoke the ReloadScene method after 5 seconds
        Invoke("ReloadScene", 5f);
    }

    private void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
