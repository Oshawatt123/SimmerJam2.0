using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{

    public GameObject silverCoin;
    public GameObject HealthPickUp;

    public Transform[] SpawnPoints;
    public void CoinShower(int min, int max, bool isLeft, bool coin)
    {
        GameObject newCoin;
        GameObject spawnObject;
        int index;
        if(isLeft)
        {
            index = 0;
        }
        else
        {
            index = 1;
        }
        if(coin)
        {
            spawnObject = silverCoin;
        }
        else
        {
            spawnObject = HealthPickUp;
        }
        for (int i = 0; i < Random.Range(min, max); i++)
        {
            
            newCoin = Instantiate(spawnObject, SpawnPoints[index].position, Quaternion.identity);
            newCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), 0f);
        }
    }

    public void CoinShower(int min, int max, Vector3 Position, bool coin)
    {
        GameObject newCoin;
        GameObject spawnObject;
        if (coin)
        {
            spawnObject = silverCoin;
        }
        else
        {
            spawnObject = HealthPickUp;
        }
        for (int i = 0; i < Random.Range(min, max); i++)
        {
            newCoin = Instantiate(spawnObject, Position, Quaternion.identity);
        }
    }
}