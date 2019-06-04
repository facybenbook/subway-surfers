using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public GameObject[] teenageTilePrefabs;
    private Transform playerTransform;
    private float spawnZ = -5.0f;
    private float tileLength = 10.0f;
    private int numTilesOnScreen = 7;
    private float saveZone = 12.0f;
    private List<GameObject> activeTiles;
    private int lastPrefabIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        for (int i = 0; i < numTilesOnScreen; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z - saveZone > (spawnZ - numTilesOnScreen * tileLength))
        {
            //activeTiles[0].SetActive(false);
            SpawnTile();
            deleteTile();
        }
    }

    void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;

        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[randomPrefabIndex()]) as GameObject;
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;

        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    void deleteTile()
    {
        //activeTiles[0].SetActive(false);
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    int randomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;

        switch (score.overallLevel)
        {
            case 1:
                while (randomIndex == lastPrefabIndex)
                {
                    randomIndex = Random.Range(0, 9);
                }

                break;
            case 2:
                while (randomIndex == lastPrefabIndex)
                {
                    randomIndex = Random.Range(0, tilePrefabs.Length);
                }
                break;
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;

    }

}
