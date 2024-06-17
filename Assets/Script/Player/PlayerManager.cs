using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public enum PlayerState { IDLE, WALK, RUN, DASH, ATTACK, TELEPORT }
    public PlayerState state;
    public PlayerState previousState; 

    public GameObject player;

    #region Singleton

    public static PlayerManager instance;

    void Awake() 
    {
        instance = this;
    }

    #endregion


    public void KillPlayer() 
    {
        Debug.Log("You died!");

        // Invoke the ReloadScene method after 5 seconds
        // Invoke(nameof(ReloadScene), 5f);
        SceneManager.LoadScene("GameOverScene");
    }

    private void ReloadScene()
    {
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
