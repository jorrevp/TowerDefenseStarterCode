using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public UIDocument uiDocument; 

    private Label waveLabel;
    private Label creditsLabel;
    private Label healthLabel;
    private Button startWaveButton;

    private VisualElement root;
    void Awake()
    {
        // Root element verkrijgen
        root = GetComponent<UIDocument>().rootVisualElement;
    }
    void Start()
    {
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
        // Voeg hier de logica toe om een wave te starten
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
}
