using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public UIDocument uiDocument; 

    private Label waveLabel;
    private Label creditsLabel;
    private Label healthLabel;
    private Button startWaveButton;

    private GameManager gameManager;

    private VisualElement root;
    private void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        // Zoek de labels en button in de UI-document hiërarchie
        waveLabel = uiDocument.rootVisualElement.Q<Label>("waveLabel");
        creditsLabel = uiDocument.rootVisualElement.Q<Label>("creditsLabel");
        healthLabel = uiDocument.rootVisualElement.Q<Label>("healthLabel");
        startWaveButton = uiDocument.rootVisualElement.Q<Button>("startWaveButton");

        // Controleer of de labels en button zijn gevonden
        if (waveLabel == null || creditsLabel == null || healthLabel == null || startWaveButton == null)
        {
            Debug.LogError("One or more UI elements not found in UI document!");
        }

        // Voeg een event listener toe aan de button
        startWaveButton.clicked += StartWave;
    }

    // Voeg hier je functie toe om een wave te starten
    void StartWave()
    {
        GameManager.Instance.StartWave();

    }

    // Voeg hier de functies toe om de labels aan te passen
    public void SetWaveLabel(string text)
    {
        waveLabel.text = text;
    }

    public void SetCreditsLabel(string text)
    {
        creditsLabel.text = text;
    }

    public void SetHealthLabel(string text)
    {
        healthLabel.text = text;
    }

    // Voeg OnDestroy toe om de callback van de button te verwijderen
    void OnDestroy()
    {
        startWaveButton.clicked -= StartWave;
    }
    public void startWaveButton_clicked()
    {
        
        if (gameManager != null)
        {
            gameManager.StartWave();
            DisableWaveButton();
        }
        else
        {
            Debug.LogWarning("GameManager not found!");
        }
    }
    public void EnableWaveButton()
    {
        if (startWaveButton != null)
        {
            startWaveButton.SetEnabled(true);
        }
        else
        {
            Debug.LogWarning("WaveButton not assigned!");
        }
    }
    private void DisableWaveButton()
    {
        if (startWaveButton != null)
        {
            startWaveButton.SetEnabled(false);
        }
        else
        {
            Debug.LogWarning("WaveButton not assigned!");
        }
    }
}
