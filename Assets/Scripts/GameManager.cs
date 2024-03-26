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
    private EnemySpawner enemySpawner; // Een referentie naar de EnemySpawner
    public GameObject TopMenu; // Referentie naar het TopMenu GameObject
    private TopMenu topMenu; // Referentie naar het TopMenu script

    // Variabelen voor credits, health en huidige wave
    private int credits;
    private int health;
    public int maxWave = 5; 

    public int currentWave = 0;
    private bool waveActive = false; 
    private int enemyInGameCounter = 0;

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
        enemySpawner = FindObjectOfType<EnemySpawner>(); // Zoek de EnemySpawner in de scene
    }
    public void StartGame()
    {
        if (!waveActive)
        {
            waveActive = true;          
            Debug.Log("Starting Wave " + currentWave);
            
            // Stel de waarden in voor credits, health en currentWave
            credits = 200;
            health = 10;
            

            // Gebruik de functies van TopMenu om de tekst voor elk label in te stellen
            topMenu.SetCreditsLabel("Credits: " + credits.ToString());
            topMenu.SetHealthLabel("Health: " + health.ToString());
            topMenu.SetWaveLabel("Wave: " + currentWave.ToString());
           
        }
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
           RemoveCredits(cost); // Credits toevoegen met true als argument om aan te geven dat het om een verkoop gaat
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

    // Functie om een golf te starten
    public void StartWave()
    {
        // Verhoog de waarde van currentWave
        currentWave++;

        // Roep de StartWave-functie van de EnemySpawner aan om de golf te starten
        enemySpawner.StartWave(currentWave);

        // Verander het label voor de huidige golf in topMenu
        ChangeWaveLabel(currentWave);

        // Verander waveActive naar true
        waveActive = true;

        enemyInGameCounter = 0;
        // Voeg hier eventuele andere logica toe die nodig is om een golf te starten
    }

    // Functie om een golf te beëindigen

    public void EndWave()
    {
        waveActive = false;

        // Voeg hier de logica toe om te controleren of de golf is afgelopen, en roep EnableWaveButton aan als dat het geval is

        // Als de health van de gate onder 0 is
        if (health <= 0)
        {
            // Roep de EndGame functie aan
            EndGame();
            return;
        }

        // Controleer of alle vijanden zijn verslagen
        if (enemyInGameCounter <= 0)
        {
            // Controleer of de huidige wave gelijk is aan de laatste wave
            if (currentWave == maxWave)
            {
                // Roep de EndGame functie aan
                EndGame();
            }
            else
            {
                // Roep EnableWaveButton aan om de golfknop weer interactief te maken
                topMenu.EnableWaveButton();
            }
        }
    }
    // Functie om het label van de golf te veranderen in topMenu
    private void ChangeWaveLabel(int waveNumber)
    {
        // Voeg hier code toe om het label van de golf te veranderen, bijvoorbeeld:
        topMenu.SetWaveLabel("Wave " + waveNumber);
    }
    public void AddInGameEnemy()
    {
        enemyInGameCounter++;

    }
    public void RemoveInGameEnemy()
    {
        enemyInGameCounter--;

        if (!waveActive && enemyInGameCounter <= 0)
        {
            topMenu.EnableWaveButton();
        }
    }
    public void StartNextWave()
    {
        currentWave++;
        enemySpawner.StartWave(currentWave);
        waveActive = true;
    }

}

