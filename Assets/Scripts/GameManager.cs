using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> Archers = new List<GameObject>(); // Lijst van Archer tower prefabs
    public List<GameObject> Swords = new List<GameObject>(); // Lijst van Sword tower prefabs
    public List<GameObject> Wizards = new List<GameObject>(); // Lijst van Wizard tower prefabs

    public GameObject TowerMenu; // Referentie naar het TowerMenu GameObject
    private TowerMenu towerMenu; // Referentie naar het TowerMenu script

    private ConstructionSite selectedSite; // Variabele om geselecteerde bouwplaats te onthouden

    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
    }
    void Awake()
    {
        // Singleton instantiëren
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Trying to instantiate another GameManager, destroying this one.");
            Destroy(gameObject);
        }

        
    }
    public void SelectSite(ConstructionSite site)
    {
        // Onthoud de geselecteerde site
        selectedSite = site;

        // Stuur de geselecteerde site naar het TowerMenu door SetSite aan te roepen
        towerMenu.SetSite(selectedSite);
    }
    private void SetTowerLists()
    {
        // Zorg ervoor dat de lijsten leeg zijn voordat we de prefabs toevoegen
        Archers.Clear();
        Swords.Clear();
        Wizards.Clear();

        // Voeg de prefabs toe aan de lijsten in de gewenste volgorde
        foreach (Transform child in transform)
        {
            Enums.TowerType type = child.GetComponent<Tower>().type;
            switch (type)
            {
                case Enums.TowerType.Archer:
                    Archers.Add(child.gameObject);
                    break;
                case Enums.TowerType.Sword:
                    Swords.Add(child.gameObject);
                    break;
                case Enums.TowerType.Wizard:
                    Wizards.Add(child.gameObject);
                    break;
                default:
                    Debug.LogWarning("Tower type not recognized: " + type);
                    break;
            }
        }
    }
    public void ExampleMethod()
    {
        // Roep een methode aan in het TowerMenu-script
        towerMenu.NotifyGameManagerOfMenuUpdate();
    }
    // Functie om de geselecteerde bouwplaats in te stellen

    // Functie om de geselecteerde bouwplaats op te vragen
    public ConstructionSite GetSelectedSite()
    {
        return selectedSite;
    }
}
