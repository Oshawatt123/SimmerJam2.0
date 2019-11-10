using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShootCoin : MonoBehaviour
{

    public GameObject projectile;
    public Transform projectileSpawn;

    public PlayerMove playerStats;

    private bool axisInUse = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Shoot") != 0)
        {
            if(!axisInUse && playerStats.coins > 0)
            {
                axisInUse = true;
                if(playerStats.lookingRight)
                {
                    Instantiate(projectile, projectileSpawn.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(projectile, projectileSpawn.position, Quaternion.Euler(Vector3.down*180f));
                }

                playerStats.coins -= 1;
            }
        }
        else if(Input.GetAxis("Shoot") == 0)
        {
            axisInUse = false;
        }
    }
}
