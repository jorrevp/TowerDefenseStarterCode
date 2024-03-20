using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TowerMenu : MonoBehaviour
{
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;

    private VisualElement root;

    private ConstructionSite selectedSite;


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
        root.visible = true;
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
        // Als er geen geselecteerde bouwplaats is, return
        if (selectedSite == null)
            return;

        // Schakel alle knoppen uit
        root.Q<Button>("archerButton").SetEnabled(false);
        root.Q<Button>("swordButton").SetEnabled(false);
        root.Q<Button>("wizardButton").SetEnabled(false);
        root.Q<Button>("upgradeButton").SetEnabled(false);
        root.Q<Button>("destroyButton").SetEnabled(false);

        // Gebruik een switch om de logica voor het inschakelen van knoppen te bepalen op basis van de siteLevel van de geselecteerde site
        switch (selectedSite.Level)
        {
            case Enums.SiteLevel.Onbebouwd:
                root.Q<Button>("archerButton").SetEnabled(true);
                root.Q<Button>("swordButton").SetEnabled(true);
                root.Q<Button>("wizardButton").SetEnabled(true);
                break;
            case Enums.SiteLevel.level1:
            case Enums.SiteLevel.level2:
                root.Q<Button>("upgradeButton").SetEnabled(true);
                root.Q<Button>("destroyButton").SetEnabled(true);
                break;
            case Enums.SiteLevel.level3:
                root.Q<Button>("destroyButton").SetEnabled(true);
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
    }
}
