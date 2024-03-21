using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Enums;

public class TowerMenu : MonoBehaviour
{
    public event Action<ConstructionSite> SiteSelected;
    public event Action MenuUpdated;

    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private VisualElement root;

    private ConstructionSite selectedSite;
    private GameManager gameManager;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Root element verkrijgen
        root = GetComponent<UIDocument>().rootVisualElement;
    }

    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        archerButton = root.Q<Button>("archer-button");
        swordButton = root.Q<Button>("sword-button");
        wizardButton = root.Q<Button>("wizard-button");
        updateButton = root.Q<Button>("button-upgrade");
        destroyButton = root.Q<Button>("button-destroy");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }
        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }
        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }
        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }
        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }
        root.visible = false;
    }
    private void OnArcherButtonClicked()    
    {

    }
    private void OnSwordButtonClicked()
    {

    }
    private void OnWizardButtonClicked()
    {

    }
    private void OnUpdateButtonClicked()
    {

    }
    private void OnDestroyButtonClicked()
    {

    }
    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }
    // Functie om het menu te evalueren en knoppen in- of uit te schakelen op basis van de geselecteerde bouwplaats
    public void EvaluateMenu()
    {
        if (selectedSite == null)
            return;

        // Haal het niveau van de geselecteerde constructieplaats op
        SiteLevel siteLevel = selectedSite.Level;

        // Schakel alle knoppen in het torenmenu uit
        archerButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);
        // Gebruik een switch om de knoppen in te schakelen op basis van het niveau van de constructieplaats
        switch (siteLevel)
        {
            case Enums.SiteLevel.Onbebouwd:
                // Alleen de torenknoppen moeten worden ingeschakeld
                archerButton.SetEnabled(true);
                swordButton.SetEnabled(true);
                wizardButton.SetEnabled(true);
                break;
            case Enums.SiteLevel.level1:
            case Enums.SiteLevel.level2:
                // Alleen de update- en destroy-knoppen moeten werken
                updateButton.SetEnabled(true);
                destroyButton.SetEnabled(true);
                break;
            case Enums.SiteLevel.level3:
                // Alleen de destroy-knop moet werken
                destroyButton.SetEnabled(true);
                break;
            default:
                Debug.LogWarning("Unknown site level: " + siteLevel);
                break;
        }
    }
    // Functie om een bouwplaats in te stellen en het menu dienovereenkomstig bij te werken
    public void SetSite(ConstructionSite site)
    {
        // Bouwplaats toewijzen aan geselecteerdeSite
        selectedSite = site;

        // Als de geselecteerde site null is, verberg het menu
        if (selectedSite == null)
        {
            root.visible = false;
            return;
        }

        // Menu zichtbaar maken en menu evalueren
        root.visible = true;
        EvaluateMenu();

        SiteSelected?.Invoke(selectedSite);
    }
    public void SetGameManager(GameManager manager)
    {
        gameManager = manager;
    }
    public void NotifyGameManagerOfMenuUpdate()
    {
        Debug.Log("TowerMenu informs GameManager of menu update.");
    }
}
