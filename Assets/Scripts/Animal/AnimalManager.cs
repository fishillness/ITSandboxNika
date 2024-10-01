using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalManager : MonoBehaviour
{
    [SerializeField] private float m_AnimalValue;
    [SerializeField] private ValueManager m_ValueManager;
    [SerializeField] private AnimalSpawner m_AnimalSpawner;
    [SerializeField] private PlacedBuildings m_PlacedBuildings;
    [SerializeField] private float TheTimeOfTheAppearanceOfAnimals;

    private Coroutine coroutine;
    private List<Animal> animals = new List<Animal>();

    public void UpdateAnimalCount()
    {
        AnimalManagerReset();

        int difference = animals.Count - CalculatingRequiredNumberOfAnimals();
        

        if (difference == 0)
        {
            return;
        }
        else if (difference > 0)
        {
            if (m_PlacedBuildings.GetABuildingsWithAnEntry().Count == 0)
            {                
                for (int i = 0; i < difference; i++)
                {
                    animals[animals.Count - 1 - i].Walk();
                }
            }                
            else
            {
                for (int i = 0; i < difference; i++)
                {
                    animals[animals.Count - 1 - i].ReturningHome(m_PlacedBuildings.GetABuildingsWithAnEntry());
                    animals[animals.Count - 1 - i].TheGoalHasBeenAchievedEvent += RemoveAnimal;
                }                
            }
        }
        else if (difference < 0)
        {
            coroutine = StartCoroutine(AnimalSpawn(-difference));            
        }
    }

    private void RemoveAnimal(Animal animal)
    {
        animal.TheGoalHasBeenAchievedEvent -= RemoveAnimal;
        animals.Remove(animal);
        Destroy(animal.gameObject);
    }

    private int CalculatingRequiredNumberOfAnimals()
    {
        return Mathf.FloorToInt(m_ValueManager.Advancement / m_AnimalValue);
    }

    private void AnimalManagerReset()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        for (int i = 0; i < animals.Count; i++)
        {
            animals[i].TheGoalHasBeenAchievedEvent -= RemoveAnimal;
        }
    }

    IEnumerator AnimalSpawn(int count)
    {
        for (int i = 0; i < count; i++)
        {
            animals.Add(m_AnimalSpawner.AnimalSpawn(m_PlacedBuildings.GetARandomBuildingWithAnEntry().GetEntryPoint()));
            yield return new WaitForSeconds(TheTimeOfTheAppearanceOfAnimals);
        }       
        
    }
}
