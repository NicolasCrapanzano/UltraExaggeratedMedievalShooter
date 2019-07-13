using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public float eStartHealth = 100f;
    [SerializeField]
    private float eHealth;
    private GameManager _gameManager;

    public float speed;
    public static bool GameOver;
    private int randObj;
    [SerializeField]
    private int _enemyID;

    private Transform target;
    private Player _player;

    [Header("HealthBar Management")]
    public Image heathBar;
    public Text HealthAmmount;
    private float test;

    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _hurtSound;
    [SerializeField]
    private AudioClip[] _deadSound;
    private int _randHurtSound;

    [SerializeField]
    private GameObject[] Drops;
    public GameObject deathEffect;

    private GameObject instantiatedObj;
    private CursorScript _cursor;

    private void Start()
    {

        eHealth = eStartHealth;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        HealthAmmount.text = eHealth.ToString() + " / " + eStartHealth.ToString();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _randHurtSound = Random.Range(0, 3);
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorScript>();
    }
   
    private void Update()
    {
        if (GameOver == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if (eHealth <= 0)
            {
                if(_enemyID == 5)
                {
                    _gameManager.specialObjective = 1;
                }
                
                AudioSource.PlayClipAtPoint(_deadSound[_randHurtSound], Camera.main.transform.position, 1f);
                Looting();
            }

        }

    }
    void Looting()
    {
          
         instantiatedObj = Instantiate(deathEffect, transform.position, Quaternion.identity);
         Destroy(instantiatedObj, 2);
         Destroy(gameObject);
         randObj = Random.Range(1, 100);
        if (_enemyID == 0) 
        {
            if (randObj >= 1 && randObj <= 81) // 81 numbers
            {
                Instantiate(Drops[0], transform.position, Quaternion.identity);

            }
            else if (randObj > 81 && randObj <= 86) //5 numbers
            {
                Instantiate(Drops[1], transform.position, Quaternion.identity);

            }
            else if (randObj > 86 && randObj <= 90) //4 numbers
            {
                Instantiate(Drops[2], transform.position, Quaternion.identity);

            }
            else if (randObj > 90 && randObj <= 93)// 3 numbers
            {
                Instantiate(Drops[3], transform.position, Quaternion.identity);

            }
            else if (randObj > 93 && randObj <= 95)// 2 numbers
            {
                Instantiate(Drops[4], transform.position, Quaternion.identity);

            }
        }
        else if(_enemyID ==1)
        {
            if (randObj >= 1 && randObj <= 76)// 76 numbers
            {
                Instantiate(Drops[0], transform.position, Quaternion.identity);

            }
            else if (randObj > 76 && randObj <= 86) // 10 numbers
            {
                Instantiate(Drops[1], transform.position, Quaternion.identity);

            }
            else if (randObj > 86 && randObj <= 92) // 6 numbers
            {
                Instantiate(Drops[2], transform.position, Quaternion.identity);

            }
            else if (randObj > 92 && randObj <= 96) // 4 numbers
            {
                Instantiate(Drops[3], transform.position, Quaternion.identity);

            }
            else if (randObj > 96 && randObj <= 99) // 3 numbers
            {
                Instantiate(Drops[4], transform.position, Quaternion.identity);

            }
        }
        else if(_enemyID ==2)
        {
            if (randObj >= 1 && randObj <= 66) // 66
            {
                Instantiate(Drops[0], transform.position, Quaternion.identity);

            }
            else if (randObj > 66 && randObj <= 81) // 15
            {
                Instantiate(Drops[1], transform.position, Quaternion.identity);

            }
            else if (randObj > 81 && randObj <= 90) // 9
            {
                Instantiate(Drops[2], transform.position, Quaternion.identity);

            }
            else if (randObj > 90 && randObj <= 95) // 5
            {
                Instantiate(Drops[3], transform.position, Quaternion.identity);

            }
            else if (randObj > 95 && randObj <= 99)// 4
            {
                Instantiate(Drops[4], transform.position, Quaternion.identity);

            }
        }
        else if (_enemyID == 3)
        {
            if (randObj >= 1 && randObj <= 58) // 58
            {
                Instantiate(Drops[0], transform.position, Quaternion.identity);

            }
            else if (randObj > 58 && randObj <= 78) // 20
            {
                Instantiate(Drops[1], transform.position, Quaternion.identity);

            }
            else if (randObj > 78 && randObj <= 88) // 10
            {
                Instantiate(Drops[2], transform.position, Quaternion.identity);

            }
            else if (randObj > 88 && randObj <= 94) // 6
            {
                Instantiate(Drops[3], transform.position, Quaternion.identity);

            }
            else if (randObj > 94 && randObj <= 99)// 5
            {
                Instantiate(Drops[4], transform.position, Quaternion.identity);

            }
        }
        else if (_enemyID == 4)
        {
            Debug.Log("this musnt exist");
        }
    }
    public void TakeDamage(float damage)
    {
        
        if (eHealth > eStartHealth - eStartHealth + 100)
        {
            AudioSource.PlayClipAtPoint(_hurtSound[_randHurtSound], Camera.main.transform.position, 1f);
        }
        eHealth -= damage;
        heathBar.fillAmount = eHealth / eStartHealth;
        HealthAmmount.text = eHealth.ToString() + " / " + eStartHealth.ToString();
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (_player._shieldActive == false)
            {
                GameManager.pHealth -= 1;
            }else
            {
                _player._shieldAmmount--;
                _gameManager.actualShield--;
            }
        }
    }
}

