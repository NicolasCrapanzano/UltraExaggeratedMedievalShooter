using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemy;
    [SerializeField]
    private GameObject Boss;
    [SerializeField]
    private GameObject _specialSpwanEffect;
    public GameObject SpawnEffect;//circular
    public GameObject SpawnEffect2;//izq
    public GameObject SpawnEffect3;//der
    public GameObject SpawnEffect4;//abajo
    private GameObject instantiatedObj;
    private int _nextEnemy = 0;
    private int randDificult;
    private float _increaseDif= 20f;
    public static bool GameOver=false;
    private int _levelDificulty;
    public float spawnLimit;
    public static bool bossFight = false;

    float randX;
    float randY;
    Vector2 whereToSpawn;
    public float spawnRate = 6f;
    [SerializeField]
    float nextSpawn = 0;
    private GameManager _gameManager;
    void Start()
    {
        //Debug.Log("spawner");
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _levelDificulty = GameManager.levelDificulty;
        if(bossFight==true)
        {

            Instantiate(Boss, new Vector3(-2.71f, 0, 0), Quaternion.identity);
        }
        if(_levelDificulty == 1)
        {
            nextSpawn = Time.time + spawnRate;
        }
    }

    void Update()
    {
        if (GameOver==false)
        {
            if (Time.time > nextSpawn)
            {
                //Debug.Log(_spawnLimit);
                if (spawnRate > spawnLimit)
                {
                    spawnRate = spawnRate - 0.5f;
                    //Debug.Log(spawnRate);
                }
                nextSpawn = Time.time + spawnRate;
                randX = Random.Range(-27.61f, 20.25f);
                randY = Random.Range(-14.1f, 12f);
                whereToSpawn = new Vector2(randX, randY);
                if (_levelDificulty == 1)
                {
                    _nextEnemy = 0;
                }
                else if (_levelDificulty == 2)
                {
                    randDificult = Random.Range(0, 100);
                    if (randDificult >= 0 && randDificult <= 50)
                    {
                        _nextEnemy = 0;
                    }
                    else
                    {
                        _nextEnemy = 1;
                    }
                }
                else if (_levelDificulty == 3)
                {
                    randDificult = Random.Range(0, 100);
                    if (randDificult >= 0 && randDificult <= 50)
                    {
                        _nextEnemy = 1;
                    }
                    else
                    {
                        _nextEnemy = 2;
                    }
                }
                else if (_levelDificulty == 4)
                {
                    randDificult = Random.Range(0, 100);
                    if (randDificult >= 0 && randDificult <= 50)
                    {
                        _nextEnemy = 2;
                    }
                    else
                    {
                        _nextEnemy = 3;
                    }
                }
                else if (_levelDificulty == 5 && _levelDificulty == 7)
                {
                    randDificult = Random.Range(0, 150);
                    if (randDificult >= 0 && randDificult <= 50)
                    {
                        _nextEnemy = 1;
                    }
                    else if (randDificult > 50 && randDificult <= 100)
                    {
                        _nextEnemy = 2;
                    }
                    else if (randDificult > 100 && randDificult <= 150)
                    {
                        _nextEnemy = 3;
                    }
                }
                else if (_levelDificulty == 6)
                {
                    randDificult = Random.Range(0, 150);
                    if (randDificult >= 0 && randDificult <= 50)
                    {
                        _nextEnemy = 0;
                    }
                    else if (randDificult > 50 && randDificult <= 100)
                    {
                        _nextEnemy = 1;
                    }
                    else if (randDificult > 100 && randDificult <= 150)
                    {
                        _nextEnemy = 2;
                    }
                }
                    StartCoroutine("delaySpawn");
                
            }
        }
    }
    public void RutineInterceptor()
    {
        StartCoroutine("SpecialEnemy");
    }
    IEnumerator SpecialEnemy()
    {
        GameOver = true;
        Destroy(GameObject.FindWithTag("Enemy"));
        Instantiate(_specialSpwanEffect, new Vector3(-2.71f, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(5); 
        Instantiate(enemy[5],new Vector3(-2.71f, 0, 0), Quaternion.identity);
        
    }

    IEnumerator delaySpawn()
    {
        if (bossFight==false)
        {
            instantiatedObj = Instantiate(SpawnEffect, whereToSpawn, Quaternion.identity);
            Destroy(instantiatedObj, 2);
        }
        if (spawnRate <= 10 && spawnRate >= 7)
        {
            
            instantiatedObj = Instantiate(SpawnEffect3, new Vector2(22.15f, -0.56f), Quaternion.identity);//spawnderecha
            Destroy(instantiatedObj, 2);
            instantiatedObj = Instantiate(SpawnEffect2, new Vector2(-29.36f, -0.56f), Quaternion.identity);
            Destroy(instantiatedObj, 2);
        }
        if (spawnRate < 7f && spawnRate >= 3)
        {
            
            instantiatedObj = Instantiate(SpawnEffect4, new Vector2(-2.73f, -15), Quaternion.identity);
            Destroy(instantiatedObj, 2);
        }

        yield return new WaitForSeconds(2);

        if (bossFight == false)
        {
            Instantiate(enemy[_nextEnemy], whereToSpawn, Quaternion.identity);
        }
        if (spawnRate <= 10 && spawnRate >= 7)
        {
            Instantiate(enemy[_nextEnemy], new Vector2(23.2f, -0.56f), Quaternion.identity);
            Instantiate(enemy[_nextEnemy], new Vector2(-29.09f, -0.56f), Quaternion.identity);
        }
        if (spawnRate < 7f && spawnRate >= 3)
        {
            Instantiate(enemy[_nextEnemy], new Vector2(-2.73f, -15), Quaternion.identity);
        }
        
    }

}
