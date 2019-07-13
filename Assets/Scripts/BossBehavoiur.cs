using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBehavoiur : MonoBehaviour
{
    private float _bossStartHealth = 2000;
    private float _bossHealth;
    private Transform _playerPos;
    private float _offset;

    private float _timeBtwShots;
    private float _timeBtwShots2;
    private float _startTimeBtwShots = 1.5f;
    private float _destruction;
    private int _randShootWay;
    private bool _2ndFase;
    public bool boostDamage;
    private bool _dead;
    private bool _test;

    [SerializeField]
    private GameObject _projectile;
    [SerializeField]
    private Transform[] shotPoint;
    [SerializeField]
    private GameObject _particle1;
    [SerializeField]
    private GameObject _deeathParticle;
    private Image bossHealthBar;
    private Text bossHealthAmmount;
    private GameObject _instantiatedObj;

    private GameManager _gameManager;
    private SpriteRenderer _spriteR;
    private Animator _animator;
    void Start()
    {
        _timeBtwShots = _timeBtwShots + Time.time;
        _timeBtwShots2 = _timeBtwShots2 + Time.time + 3;
        _spriteR = GetComponent<SpriteRenderer>();
        bossHealthBar = GameObject.Find("BossHealthBar").GetComponent<Image>();
        bossHealthAmmount = GameObject.Find("BossHealthText").GetComponent<Text>();
        _bossHealth = _bossStartHealth;
        _playerPos = GameObject.Find("Player").GetComponent<Transform>();
        bossHealthBar.fillAmount = _bossHealth / _bossStartHealth;
        bossHealthAmmount.text = _bossHealth.ToString() + " / " + _bossStartHealth.ToString();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _animator = GetComponent<Animator>();
    }


    void Update()
    {

        /*Vector3 difference = Camera.main.ScreenToWorldPoint(_playerPos.position) - transform.position; Debug.Log("disparos");
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + _offset);*/
        if (_dead == false)
        {
            if (_2ndFase == false)
            {
                if (Time.time > _timeBtwShots)
                {
                    _randShootWay = Random.Range(0, 2);
                    //Debug.Log(_randShootWay);
                    if (_randShootWay == 0)
                    {
                        Instantiate(_projectile, shotPoint[0].position, shotPoint[0].transform.rotation);
                        Instantiate(_projectile, shotPoint[1].position, shotPoint[1].transform.rotation);
                        Instantiate(_projectile, shotPoint[2].position, shotPoint[2].transform.rotation);
                        Instantiate(_projectile, shotPoint[3].position, shotPoint[3].transform.rotation);
                    }
                    else if (_randShootWay == 1)
                    {
                        Instantiate(_projectile, shotPoint[4].position, shotPoint[4].transform.rotation);
                        Instantiate(_projectile, shotPoint[5].position, shotPoint[5].transform.rotation);
                        Instantiate(_projectile, shotPoint[6].position, shotPoint[6].transform.rotation);
                        Instantiate(_projectile, shotPoint[7].position, shotPoint[7].transform.rotation);
                    }
                    _timeBtwShots = Time.time + _startTimeBtwShots;
                }
            }


            if (_bossHealth <= _bossStartHealth / 2)
            {
                if (_2ndFase == false)
                {
                    _2ndFase = true;
                    _spriteR.color = Color.yellow;
                }
                if (Time.time > _timeBtwShots2)
                {

                    Instantiate(_projectile, shotPoint[0].position, shotPoint[0].transform.rotation);
                    Instantiate(_projectile, shotPoint[1].position, shotPoint[1].transform.rotation);
                    Instantiate(_projectile, shotPoint[2].position, shotPoint[2].transform.rotation);
                    Instantiate(_projectile, shotPoint[3].position, shotPoint[3].transform.rotation);
                    Instantiate(_projectile, shotPoint[4].position, shotPoint[4].transform.rotation);
                    Instantiate(_projectile, shotPoint[5].position, shotPoint[5].transform.rotation);
                    Instantiate(_projectile, shotPoint[6].position, shotPoint[6].transform.rotation);
                    Instantiate(_projectile, shotPoint[7].position, shotPoint[7].transform.rotation);
                    _timeBtwShots2 = Time.time + _startTimeBtwShots + 1.3f;
                }
            }
            if (_bossHealth <= _bossStartHealth / 3)
            {
                if (boostDamage == false)
                {
                    _spriteR.color = Color.magenta;
                    _instantiatedObj = Instantiate(_particle1, transform.position, transform.rotation);
                    _startTimeBtwShots = 0.5f;
                    boostDamage = true;
                }
            }
        }
        if (_bossHealth <= 0)
        {
            _dead = true;
            EnemySpawner.GameOver = true;
            _animator.SetBool("Death", true);
            Destroy(_instantiatedObj);
            Destroy(GameObject.FindWithTag("Enemy"));
            
            if (_test == false)
            {
                _spriteR.enabled = false;
                Instantiate(_deeathParticle, transform.position, Quaternion.identity);
                _timeBtwShots = Time.time + 2;
                _test = true;
            }
            if(_timeBtwShots <= Time.time)
            {
                _gameManager.bossDead = true;
            }
        }
    } 


    public void TakeDamage(float Damage)
    {
        _bossHealth -= Damage;
        bossHealthBar.fillAmount = _bossHealth / _bossStartHealth;
        bossHealthAmmount.text = _bossHealth.ToString() + " / " + _bossStartHealth.ToString();
    }
}
