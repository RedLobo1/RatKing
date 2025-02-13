using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] trailPrefabs; // Array of trail prefabs
    public characterMovement character;
    private Vector3 lastPosition;

    void Start()
    {
        if (character != null)
        {
            character.OnMove += SpawnTrail;
        }

        lastPosition = transform.position; // Set initial position
    }

    private void SpawnTrail()
    {
        if (trailPrefabs.Length == 0) return; // Ensure array is not empty

        Debug.Log("spawn");
        // Choose a random prefab from the array
        GameObject trailPrefab = trailPrefabs[Random.Range(0, trailPrefabs.Length)];

        float rotation = Random.Range(0, 360);
        Instantiate(trailPrefab,transform.position,Quaternion.Euler(0, rotation,0));

        // Update last position
        lastPosition = transform.position;
    }

    void OnDestroy()
    {
        if (character != null)
        {
            character.OnMove -= SpawnTrail; // Unsubscribe to avoid memory leaks
        }
    }
}
