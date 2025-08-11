using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<Transform> SpawnPoints = new List<Transform>();
    public List<Transform> Items = new List<Transform>();

    List<int> usedIndex = new List<int>();

    public void SpawnAnObject()
    {
        usedIndex.Clear();

        for (int i = 0; i < Items.Count; i++)
        {
            int RandomIndex = Random.Range(0, SpawnPoints.Count);

            while(usedIndex.Contains(RandomIndex))
            {
                RandomIndex = Random.Range(0, SpawnPoints.Count);
            }
            usedIndex.Add(RandomIndex);
            Items[i].position = SpawnPoints[RandomIndex].position;
            Items[i].gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        SpawnAnObject();
    }

    private void Update()
    {
        bool allInactive = true;

        foreach (Transform item in Items)
        {
            if (item.gameObject.activeInHierarchy)
            {
                allInactive = false;
                break;
            }
        }

        if (allInactive)
        {
            SpawnAnObject();
        }
    }
}