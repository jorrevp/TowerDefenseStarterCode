using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Enums;

public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private VisualElement root;

    private ConstructionSite selectedSite;
    private GameManager gameManager;




    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        archerButton = root.Q<Button>("archerButton");
        swordButton = root.Q<Button>("swordButton");
        wizardButton = root.Q<Button>("wizardButton");
        updateButton = root.Q<Button>("updateButton");
        destroyButton = root.Q<Button>("destroyButton");

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
        GameManager.Instance.Build(Enums.TowerType.Archer, Enums.SiteLevel.level1);
    }
    private void OnSwordButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Sword, Enums.SiteLevel.level1);
    }
    private void OnWizardButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Wizard, Enums.SiteLevel.level1);
    }
    private void OnUpdateButtonClicked()
    {
        if (selectedSite == null) return;

        Enums.SiteLevel nextLevel = selectedSite.Level + 1; 
        GameManager.Instance.Build(selectedSite.TowerType, nextLevel);
    }
    private void OnDestroyButtonClicked()
    {
        if (selectedSite == null) return;

        GameManager.Instance.DestroyTower();
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

        // Haal de beschikbare credits op van de GameManager
        int availableCredits = GameManager.Instance.GetCredits();

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
                // Alleen de torenknoppen moeten worden ingeschakeld als er voldoende credits zijn
                if (availableCredits >= GameManager.Instance.GetCost(TowerType.Archer, Enums.SiteLevel.Onbebouwd))
                    archerButton.SetEnabled(true);
                if (availableCredits >= GameManager.Instance.GetCost(TowerType.Sword, Enums.SiteLevel.Onbebouwd))
                    swordButton.SetEnabled(true);
                if (availableCredits >= GameManager.Instance.GetCost(TowerType.Wizard, Enums.SiteLevel.Onbebouwd))  
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
        else
        {
            root.visible = true;
            EvaluateMenu();
        }
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
