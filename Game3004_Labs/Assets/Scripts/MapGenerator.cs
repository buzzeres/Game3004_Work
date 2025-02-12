using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject[] regularTiles;
    [SerializeField]
    GameObject goalTile;
    [SerializeField]
    GameObject startTile;

    [SerializeField]
    int mapSize;
    [SerializeField]
    float tileSize;

    void Start()
    {
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                Instantiate(regularTiles[Random.Range(0, regularTiles.Length)], new Vector3(i * tileSize, 0, j * tileSize),
                    Quaternion.Euler(-90, 0, Random.Range(0, 4) * 90), transform);
            }
        }
    }
}
