using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Enums;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> Archers = new List<GameObject>(); // Lijst van Archer tower prefabs
    public List<GameObject> Swords = new List<GameObject>(); // Lijst van Sword tower prefabs
    public List<GameObject> Wizards = new List<GameObject>(); // Lijst van Wizard tower prefabs

    public GameObject TowerMenu; // Referentie naar het TowerMenu GameObject
    private TowerMenu towerMenu; // Referentie naar het TowerMenu script

    public GameObject TopMenu; // Referentie naar het TopMenu GameObject
    private TopMenu topMenu; // Referentie naar het TopMenu script

    // Variabelen voor credits, health en huidige wave
    private int credits;
    private int health;
    private int currentWave;

    private ConstructionSite selectedSite; // Variabele om geselecteerde bouwplaats te onthouden

    

    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
        topMenu = TopMenu.GetComponent<TopMenu>();

        StartGame();
    }
    void Awake()
    {
        // Singleton instantiëren
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void StartGame()
    {
        // Stel de waarden in voor credits, health en currentWave
        credits = 200;
        health = 10;
        currentWave = 0;

        // Gebruik de functies van TopMenu om de tekst voor elk label in te stellen
        topMenu.SetCreditsLabel("Credits: " + credits.ToString());
        topMenu.SetHealthLabel("Health: " + health.ToString());
        topMenu.SetWaveLabel("Wave: " + currentWave.ToString());
        // Zet waveActive op false bij het starten van de game
     
    }
    public void SelectSite(ConstructionSite site)
    {
        selectedSite = site;

        if (towerMenu != null)
        {
            towerMenu.SetSite(site);
        }
        else
        {
            Debug.LogError("TowerMenu component is null in GameManager.");
        }
    }
    public void Build(Enums.TowerType type, Enums.SiteLevel level)
    {
        if (selectedSite == null)
        {
            Debug.LogError("Er is geen bouwplaats geselecteerd. Kan de toren niet bouwen.");
            return; 
        }

        if (selectedSite.Level != Enums.SiteLevel.Onbebouwd && selectedSite.Level != Enums.SiteLevel.level1 && selectedSite.Level != Enums.SiteLevel.level2 && selectedSite.Level != Enums.SiteLevel.level3)
        {
            Debug.LogWarning("Invalid site level for building.");
            return;
        }

        GameObject towerPrefab = null;

        int prefabIndex = (int)level - 1;

        switch (type)
        {
            case Enums.TowerType.Archer:
                if (prefabIndex < Archers.Count)
                    towerPrefab = Archers[prefabIndex];
                break;
            case Enums.TowerType.Sword:
                if (prefabIndex < Swords.Count)
                    towerPrefab = Swords[prefabIndex];
                break;
            case Enums.TowerType.Wizard:
                if (prefabIndex < Wizards.Count)
                    towerPrefab = Wizards[prefabIndex];
                break;
        }


        if (towerPrefab == null)
        {
            Debug.LogError("Geen tower prefab gevonden voor het geselecteerde type en niveau.");
            return;
        }

        int cost = GetCost(type, level);

        if (selectedSite.Level == Enums.SiteLevel.Onbebouwd) // Verkoop van toren
        {
            AddCredits(cost); // Credits toevoegen met true als argument om aan te geven dat het om een verkoop gaat
        }
        else // Aankoop van nieuwe toren
        {
            if (GetCredits() >= cost)
            {
                RemoveCredits(cost); // Credits aftrekken voor aankoop
            }
            else
            {
                Debug.LogWarning("Onvoldoende credits om de toren te bouwen.");
                return;
            }
        }

        GameObject tower = Instantiate(towerPrefab, selectedSite.WorldPosition, Quaternion.identity);

        selectedSite.SetTower(tower, level, type);

        if (towerMenu != null)
        {
            towerMenu.SetSite(null); 
        }
    }

    public void DestroyTower()
    {
        if (selectedSite == null)
        {
            Debug.LogError("Er is geen bouwplaats geselecteerd. Kan de toren niet verwijderen.");
            return;
        }

        selectedSite.RemoveTower();

        if (towerMenu != null)
        {
            towerMenu.SetSite(null);
        }
    }
    public ConstructionSite GetSelectedSite()
    {
        return selectedSite;
    }
    public void AttackGate()
    {
        // Verminder de gezondheid van de poort met 1
        health--;

        // Update the label in the TopMenu
        if (topMenu != null)
        {
            topMenu.SetHealthLabel("Health: " + health.ToString());
        }
        else
        {
            Debug.LogError("TopMenu script niet gevonden in de scene.");
        }

        // Controleer of de gezondheid van de poort onder 0 is
        if (health <= 0)
        {
            EndGame(); // Roep een methode aan om het einde van de game te verwerken
        }
    }
    private void EndGame()
    {
        // Hier voeg je code toe om de game te beëindigen en terug te gaan naar de IntroScene
        // Bijvoorbeeld:
        SceneManager.LoadScene("IntroScene");
    }
    public void AddCredits(int amount)
    {
        // Voeg het bedrag toe aan de huidige credits
        credits += amount;

        // Update het label in het TopMenu
        topMenu.SetCreditsLabel("Credits: " + credits.ToString());

        // Beoordeel het torenmenu
        EvaluateTowerMenu();
    }

    private void EvaluateTowerMenu()
    {
        // Voeg hier code toe om het torenmenu te evalueren op basis van credits
        // bijvoorbeeld: schakel knoppen in/uit op basis van beschikbare credits
    }
    public void RemoveCredits(int amount)
    {
        // Trek het bedrag af van de huidige credits
        credits -= amount;

        // Zorg ervoor dat de credits niet onder nul kunnen gaan
        if (credits < 0)
        {
            credits = 0;
        }

        // Update het label in het TopMenu
        topMenu.SetCreditsLabel("Credits: " + credits.ToString());

        // Beoordeel het torenmenu
        EvaluateTowerMenu();
    }
    public int GetCredits()
    {
        return credits;
    }
    public int GetCost(TowerType type, SiteLevel level, bool selling = false)
    {
        int cost = 0;

        // Bepaal de kosten op basis van het type toren en het niveau
        switch (type)
        {
            case TowerType.Archer:
                cost = (level == Enums.SiteLevel.level1) ? 50 : (level == Enums.SiteLevel.level2) ? 75 : (level == Enums.SiteLevel.level3 && !selling) ? 150 : 0;
                break;
            case TowerType.Sword:
                cost = (level == Enums.SiteLevel.level1) ? 75 : (level == Enums.SiteLevel.level2) ? 100 : (level == Enums.SiteLevel.level3 && !selling) ? 200 : 0;
                break;
            case TowerType.Wizard:
                cost = (level == Enums.SiteLevel.level1) ? 100 : (level == Enums.SiteLevel.level2) ? 125 : (level == Enums.SiteLevel.level3 && !selling) ? 250 : 0;
                break;
            default:
                Debug.LogError("Unknown tower type: " + type);
                break;
        }

        return cost;
    }
    
}

