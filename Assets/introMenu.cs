using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class introMenu : MonoBehaviour
{
    private Button playGameButton;
    private Button quitGameButton;
    private TextField textField;

    private void Start()
    {
        // Haal de root VisualElement op
        var root = GetComponent<UIDocument>().rootVisualElement;
        Debug.Log("Root VisualElement: " + root);

        // Zoek de knoppen en het tekstveld in de root VisualElement
        playGameButton = root.Q<Button>("StartButton");
        quitGameButton = root.Q<Button>("QuitButton");
        textField = root.Q<TextField>("TextField");

        // Controleer of de knoppen en het tekstveld zijn gevonden
        if (playGameButton != null)
            playGameButton.clicked += StartButtonClicked;
        else
            Debug.LogError("Start Button not found!");

        if (quitGameButton != null)
            quitGameButton.clicked += QuitButtonClicked;
        else
            Debug.LogError("Quit Button not found!");
    }
    private void OnDestroy()
    {
        // Verwijder de callbackfuncties om geheugenlekken te voorkomen
        if (playGameButton != null)
        {
            playGameButton.clicked -= StartButtonClicked;
        }

        if (quitGameButton != null)
        {
            quitGameButton.clicked -= QuitButtonClicked;
        }
    }

    private void StartButtonClicked()
    {
        // Laad de GameScene
        SceneManager.LoadScene("GameScene");
    }

    private void QuitButtonClicked()
    {
        // Sluit de game af
        Application.Quit();
    }
}
