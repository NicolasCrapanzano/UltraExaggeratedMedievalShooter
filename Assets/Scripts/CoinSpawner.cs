using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin; //si se repite este patron se puede hace aparecer la cantidad de monedas que se quiera, hay que agregar algo de probabilidad para que no se superponga
    public GameObject coin2;
    public GameObject coin3;
    public GameObject coin4;
    [SerializeField]
    private GameObject _spawnEffect;
    public static bool GameOver;
    private int randCoin;
    private GameObject _instantiatedObj1;
    private GameObject instantiatedObj2;

    float randX; // posicion en x para aparecer
    float randY; // posicion en y para aparecer
    Vector2 whereToSpawn;
    public float spawnRate = 1f;
    float nextSpawn = 0;

    // Start is called before the first frame update
    void Start()
    {
        nextSpawn = Time.time + 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameOver==false)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(-19.94f, 20.25f);
                randY = Random.Range(-14.09f, 8.39f);
                whereToSpawn = new Vector2(randX, randY);
                if(spawnRate<10)
                {
                    spawnRate = spawnRate + 0.5f;
                }
                randCoin = Random.Range(1, 90);
                if (randCoin >= 1 && randCoin <= 80)
                {
                    _instantiatedObj1 = Instantiate(_spawnEffect,whereToSpawn,Quaternion.identity);
                    instantiatedObj2 = Instantiate(coin, whereToSpawn, Quaternion.identity);
                    Destroy(_instantiatedObj1, 2);
                    Destroy(instantiatedObj2, 20);
                }
                else if (randCoin > 80 && randCoin <= 85)
                {
                    _instantiatedObj1 = Instantiate(_spawnEffect, whereToSpawn, Quaternion.identity);
                    instantiatedObj2 = Instantiate(coin2, whereToSpawn, Quaternion.identity);
                    Destroy(_instantiatedObj1, 2);
                    Destroy(instantiatedObj2, 20);
                }
                else if (randCoin > 85 && randCoin <= 88)
                {
                    _instantiatedObj1 = Instantiate(_spawnEffect, whereToSpawn, Quaternion.identity);
                    instantiatedObj2 = Instantiate(coin3, whereToSpawn, Quaternion.identity);
                    Destroy(_instantiatedObj1, 2);
                    Destroy(instantiatedObj2, 20);
                }
                else if (randCoin > 88 && randCoin <= 90)
                {
                    _instantiatedObj1 = Instantiate(_spawnEffect, whereToSpawn, Quaternion.identity);
                    instantiatedObj2 = Instantiate(coin4, whereToSpawn, Quaternion.identity);
                    Destroy(_instantiatedObj1, 2);
                    Destroy(instantiatedObj2, 25);
                }
                               
            }
        }
    }
    
}