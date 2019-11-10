using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCoins : MonoBehaviour
{

    public GameObject silverCoin;
    public GameObject HealthPickUp;

    public Transform[] SpawnPoints;
    public void CoinShower(int min, int max, bool isLeft)
    {
        GameObject newCoin;
        for (int i = 0; i < Random.Range(min, max); i++)
        {
            if(isLeft)
            {
                newCoin = Instantiate(silverCoin, SpawnPoints[0].position, Quaternion.identity);
            }
            else
            {
                newCoin = Instantiate(silverCoin, SpawnPoints[1].position, Quaternion.identity);
            }
            newCoin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-2, 2), 0f);
        }
    }

    public void CoinShower(int min, int max, Vector3 Position)
    {
        GameObject newCoin;
        for (int i = 0; i < Random.Range(min, max); i++)
        {
            newCoin = Instantiate(silverCoin, Position, Quaternion.identity);
        }
    }
}