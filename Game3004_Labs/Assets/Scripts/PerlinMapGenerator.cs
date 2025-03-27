using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMapGenerator : MonoBehaviour
{
    [Header("World Properties")]
    [Range(8, 64)]
    public int height = 8;
    [Range(8, 64)]
    public int width = 8;
    [Range(8, 64)]
    public int depth = 8;

    [Header("Scaling Values")]
    [Range(8, 64)]
    public float min = 16.0f;
    [Range(8, 64)]
    public float max = 24.0f;

    [Header("Tile Properties")]
    public Transform tileParent;
    public GameObject threeDTile;
    List<GameObject> grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new List<GameObject>();
        Generate();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Reset();
            Generate();
            DisableCollideres();
        }
    }

    private void Generate()
    {
        // world generation happens here
        float randomScale = Random.Range(min, max);
        float offsetX = Random.Range(-1024.0f, 1024.0f);
        float offsetZ = Random.Range(-1024.0f, 1024.0f);

        for (int y = 0; y < height; y++)
        {
            for (int z = 0; z < depth; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    float perlinValue = Mathf.PerlinNoise((x + offsetX) / randomScale, (z + offsetZ) / randomScale) * depth * 0.5f;

                    if (y < perlinValue)
                    {
                        var tile = Instantiate(threeDTile, new Vector3(x, y, z), Quaternion.identity);
                        tile.transform.parent = tileParent;
                        grid.Add(tile);
                    }
                }
            }
        }
    }

    private void DisableCollideres()
    {
        //detect if each tile has a collider with each face
        var normalArray = new Vector3[] { Vector3.forward, Vector3.back, Vector3.left, Vector3.right, Vector3.up, Vector3.down };
        List<GameObject> disabledTiles = new List<GameObject>();
        foreach (var tile in grid)
        {
            int collisionCounter = 0;
            for (int i = 0; i < normalArray.Length; i++)
            {
                if (Physics.Raycast(tile.transform.position, normalArray[i], tile.transform.localScale.magnitude * 0.3f))
                {
                    collisionCounter++;
                }
                if (collisionCounter > 5)
                {
                    disabledTiles.Add(tile);
                }
            }
        }

        foreach (var insideTile in disabledTiles)
        {
            var boxCollider = insideTile.GetComponent<BoxCollider>();
            var meshRenderer = insideTile.GetComponent<MeshRenderer>();
            boxCollider.enabled = false;
            meshRenderer.enabled = false;
        }
    }

    private void Reset()
    {
        // Clear previous grid
        foreach (var tile in grid)
        {
            Destroy(tile);
        }
        grid.Clear();
    }
}