using UnityEngine;

public class coinSpawner : MonoBehaviour
{
    public int maxCoin = 5;
    public float chanceToSpawn = 0.5f;
    private GameObject[] coins;
    public bool forceSpawnAll = false;

    private void Awake()
    {
        coins = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            coins[i] = transform.GetChild(i).gameObject;
        }
        OnDisable();
    }

    private void OnEnable()
    {
        if (Random.Range(0.0f, 1.0f) > chanceToSpawn)
            return;

        if (forceSpawnAll)
            for (int i = 0; i < maxCoin; i++)
                coins[i].SetActive(true);
        else
        {
            int r = Random.Range(0, maxCoin);
            for (int i = 0; i < r; i++)
                coins[i].SetActive(true);
        }
    }

    private void OnDisable()
    {
        foreach (GameObject go in coins)
            go.SetActive(false);
    }
}