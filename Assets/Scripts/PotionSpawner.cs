using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : MonoBehaviour
{
    public GameObject[] Potions; //0 +heath potion, 1 ++ health potion, 2 shield potion, 3 + damage potion,4 ++damage potion , 5 magnet potion  , 6 fast shot potion
    public static bool GameOver;
    private int randPot;
    float randX; // posicion en x para aparecer
    float randY; // posicion en y para aparecer
    Vector2 whereToSpawn;
    public float spawnRate = 5f;
    float nextSpawn = 0;
    public int maxPotion=0;
    private int _levelDificulty;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = Time.time + spawnRate;

        _levelDificulty = GameManager.levelDificulty;
        if(_levelDificulty == 1)
        {
            //Debug.Log("entered");
            Instantiate(Potions[0],new Vector2(-22.45f, -10.11f),Quaternion.identity);
            Instantiate(Potions[1], new Vector2(-15.93f, -10.11f), Quaternion.identity);
            Instantiate(Potions[2], new Vector2(-9.66f, -10.11f), Quaternion.identity);
            Instantiate(Potions[3], new Vector2(-2.47f, -10.11f), Quaternion.identity);
            Instantiate(Potions[4], new Vector2(7.76f, -10.11f), Quaternion.identity);
            Instantiate(Potions[5], new Vector2(17.48f, -10.11f), Quaternion.identity);
            Instantiate(Potions[6], new Vector2(2.65f, -9.54f), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver == false)
        {
            if (Time.time > nextSpawn && maxPotion < 6)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(-19.94f, 20.25f);
                randY = Random.Range(-14.1f, 12f);
                whereToSpawn = new Vector2(randX, randY);
                randPot = Random.Range(0, 140);
                if (randPot >= 0 && randPot <= 80)
                {
                    Instantiate(Potions[0], whereToSpawn, Quaternion.identity);
                    maxPotion++;
                }
                if(randPot >= 80 && randPot <=85)
                {
                    Instantiate(Potions[1], whereToSpawn, Quaternion.identity);
                    maxPotion++;
                }
                if(randPot >= 85 && randPot <= 100)
                {
                    Instantiate(Potions[2], whereToSpawn, Quaternion.identity);
                }
                if(randPot >= 100 && randPot <= 115)
                {
                    Instantiate(Potions[3], whereToSpawn, Quaternion.identity);
                }
                if (randPot > 115 && randPot <= 120)
                {
                    Instantiate(Potions[4], whereToSpawn, Quaternion.identity);
                    maxPotion++;
                }
                if(randPot > 120 && randPot <= 125)
                {
                    Instantiate(Potions[5], whereToSpawn, Quaternion.identity);
                    maxPotion++;
                }
                if(randPot > 125 && randPot <= 140)
                {
                    Instantiate(Potions[6], whereToSpawn, Quaternion.identity);
                    maxPotion++;
                }
                
            }
        }
    }
}
