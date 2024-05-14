using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject difficultyButton;
    [SerializeField] private GameObject difficultyPanel;
    [SerializeField] private string selectedDifficulty;

    [SerializeField] private TextMeshProUGUI text;

    void Awake()
    {
        text = difficultyButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SwitchScene(string sceneName) 
    {
        PlayerPrefs.SetString("Difficulty", selectedDifficulty);
        SceneManager.LoadScene(sceneName);
    }

    public void SwitchToDifficultyPanel() 
    {
        if(difficultyButton != null) 
        {
            difficultyButton.GetComponent<Button>().enabled = false;
            difficultyButton.GetComponent<Image>().enabled = false;
            text.gameObject.SetActive(false);
            // difficultyButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(false);


            difficultyPanel.SetActive(true);
        }
    }

    public void SelectDifficulty(string difficulty) 
    {
        // Store the selected difficulty
        selectedDifficulty = difficulty;

        difficultyPanel.SetActive(false);

        difficultyButton.SetActive(true);
        difficultyButton.GetComponent<Button>().enabled = true;
        difficultyButton.GetComponent<Image>().enabled = true;
        text.gameObject.SetActive(true);
        // difficultyButton.GetComponentInChildren<TextMeshProUGUI>().gameObject.SetActive(true);
    }
}
