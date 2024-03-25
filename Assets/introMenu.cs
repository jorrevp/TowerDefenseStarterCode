using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introMenu : MonoBehaviour
{
    public Button playGameButton;
    public Button quitGameButton;


    private void Start()
    {
        // Zoek en wijs de knoppen toe
        playGameButton = GameObject.Find("PlayGameButton").GetComponent<Button>();
        quitGameButton = GameObject.Find("QuitGameButton").GetComponent<Button>();

        // Voeg luisteraars toe aan de knoppen
        playGameButton.onClick.AddListener(PlayGame);
        quitGameButton.onClick.AddListener(QuitGame);

        // Disable de "Start" knop wanneer het menu opent
        playGameButton.interactable = false;
    }
    void PlayGame()
    {
       
        // Laad de GameScene
        SceneManager.LoadScene("GameScene");
    }
    void QuitGame()
    {
        // Voer hier de logica uit om het spel af te sluiten
        Application.Quit();
    }
    // Verwijder luisteraars bij het vernietigen van het object
    private void OnDestroy()
    {
        playGameButton.onClick.RemoveAllListeners();
        quitGameButton.onClick.RemoveAllListeners();
    }
}
