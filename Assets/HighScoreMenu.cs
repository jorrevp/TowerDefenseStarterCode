using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HighScoreMenu : MonoBehaviour
{
    public Label gameResultLabel; // Label voor het weergeven van het spelresultaat (gewonnen of verloren)
    public Button newGameButton; // Button om een nieuw spel te starten
    private void Start()
    {

        // Vraag de waarde van GameIsWon op uit de HighScoreManager
        bool gameIsWon = HighScoreManager.Instance.GameIsWon;

        // Pas de tekst van het label aan op basis van het resultaat van het spel
        gameResultLabel.text = gameIsWon ? "You Win" : "You Lost";
    }

    private void OnEnable()
    {
        // Voeg een callback functie toe aan de button voor het starten van een nieuw spel
        newGameButton.clicked += StartNewGame;
    }

    private void OnDisable()
    {
        // Verwijder de callback functie wanneer het object is uitgeschakeld om mogelijke lekken te voorkomen
        newGameButton.clicked -= StartNewGame;
    }

    // Functie om een nieuw spel te starten en terug te gaan naar de GameScene
    private void StartNewGame()
    {
        // Vraag de GameManager om het spel te starten
        GameManager.Instance.StartGame();

        // Keer terug naar de GameScene
        SceneManager.LoadScene("GameScene");
    }
}
