using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    List<GameObject> maze;

    void Start()
    {
        maze = new List<GameObject>();
        for (int i = 0; i < mapSize; i++)
        {
            for (int j = 0; j < mapSize; j++)
            {
                GameObject tile = Instantiate(regularTiles[Random.Range(0, regularTiles.Length)], new Vector3(i * tileSize, 0, j * tileSize),
                    Quaternion.Euler(-90, 0, Random.Range(0, 4) * 90), transform);
                maze.Add(tile);
            }
        }

        int startTileIndex = Random.Range(0, maze.Count);
        ReplaceTile(startTileIndex, startTile);
        FindObjectOfType<PlayerBehaviour>().transform.position = maze[startTileIndex].transform.position + Vector3.up * 2;

        int goalTileIndex;
        do
        {
            goalTileIndex = Random.Range(0, maze.Count);
        }
        while (goalTileIndex == startTileIndex);
        ReplaceTile(goalTileIndex, goalTile);
    }

    void ReplaceTile(int replaceIndex, GameObject replaceObject)
    {
        GameObject newTile = Instantiate(replaceObject, maze[replaceIndex].transform.position, Quaternion.identity, transform);
        Destroy(maze[replaceIndex]);
        maze[replaceIndex] = newTile;
    }
}
