using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public List<GameObject> Archers; // Lijst van Archer tower prefabs
    public List<GameObject> Swords; // Lijst van Sword tower prefabs
    public List<GameObject> Wizards; // Lijst van Wizard tower prefabs

    public GameObject TowerMenu; // Referentie naar het TowerMenu GameObject
    private TowerMenu towerMenu; // Referentie naar het TowerMenu script

    private ConstructionSite selectedSite; // Variabele om geselecteerde bouwplaats te onthouden

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

        // TowerMenu script referentie verkrijgen
        towerMenu = TowerMenu.GetComponent<TowerMenu>();

        if (towerMenu == null)
        {
            Debug.LogError("TowerMenu script is not attached to the TowerMenu GameObject!");
        }
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

    // Functie om de geselecteerde bouwplaats in te stellen
    public void SetSelectedSite(ConstructionSite site)
    {
        selectedSite = site;
    }

    // Functie om de geselecteerde bouwplaats op te vragen
    public ConstructionSite GetSelectedSite()
    {
        return selectedSite;
    }
}
