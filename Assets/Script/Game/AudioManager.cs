using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public string targetSceneName; // The name of the scene where you want to play the sound

    private void Awake()
    {
        // Ensure this object persists between scene loads
        DontDestroyOnLoad(gameObject);

        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the sceneLoaded event when this object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Check if the loaded scene is the target scene
        if (scene.name == targetSceneName)
        {
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (source != null && clip != null)
        {
            source.clip = clip;
            source.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not set.");
        }
    }
}
