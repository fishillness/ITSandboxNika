using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField] private WalkArea m_WalkArea;
    [SerializeField] private Animal[] m_AnimalsPrefabs;

    public Animal SpawnAnimalsAtAGivenPoint(Transform spawnPoint)
    {
        Animal animal = Instantiate(m_AnimalsPrefabs[Random.Range(0, m_AnimalsPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
        animal.SetWalkArea(m_WalkArea);
        return animal;
    }
    public Animal SpawnAnimalsAtARandomPoint()
    {
        Animal animal = Instantiate(m_AnimalsPrefabs[Random.Range(0, m_AnimalsPrefabs.Length)], m_WalkArea.GetRandomPoint(), Quaternion.Euler(transform.TransformPoint(new Vector3(Random.Range(0, 360), 0, Random.Range(0, 360)))));
        animal.SetWalkArea(m_WalkArea);
        return animal;
    }
}
