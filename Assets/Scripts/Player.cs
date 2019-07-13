using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Character
{
   
   [Header("Private local variables")]
    private Shake shake;
    private Transform pPos;
    private Renderer hide;
    private GameObject _instantiatedObj;
    private PotionSpawner _potionSpawner;
    private AudioClip _actualClip; // better a glup sound?
    private bool _bossFight;

    public AudioSource _audioSource;
    private GameManager _gameManager;
    private ItemBehaviour _itemBehavoiur;

    [Header("Public variables")]
    public AudioClip[] _coinBops;
    public GameObject _tomb;
    public AudioClip _drinking;
    public static bool gameOver = false;
    public bool _extraRangeON = false;
    public bool _fastShot = false;
    public bool dmgBoostOn = false;
    public bool _shieldActive; // is the shield active?
    public int pDamage;
    public int _shieldAmmount; // how many shields are active? (max. 3)

    [Header("Serialized variables")]
    [SerializeField]
    private GameObject _hitEffect;
    [SerializeField]
    private GameObject _extraRange;
    [SerializeField]
    private Projectile _projectile;
    [SerializeField]
    private FireImp _weapon1;
    [SerializeField]
    private Text _pickedItem;
    [SerializeField]
    private Text _criticalDamage;

    protected override void Start()
    {
        base.Start();
        pPos = GetComponent<Transform>();
        _weapon1 = GameObject.Find("FireStone").GetComponent<FireImp>();
        shake = GameObject.FindGameObjectWithTag("ScreenShake").GetComponent<Shake>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
        _potionSpawner = GameObject.Find("Spawner").GetComponent<PotionSpawner>();
        pDamage = 100;

    }
    // Update is called once per frame
    protected override void Update()
    {
        GetInput();
        if (GameManager.pHealth <= 0)
        {
            Instantiate(_tomb, transform.position, Quaternion.identity);
            gameOver = true;
            GameManager.GameOver = true;
            EnemySpawner.GameOver = true;
            EnemyScript.GameOver = true;
            CoinSpawner.GameOver = true;
            PotionSpawner.GameOver = true;
            FireImp.GameOver = true;
            pPos.position = new Vector2(-5.25f, -22.26f);
        }
        if (_shieldAmmount <= 0)
        {
            _shieldActive = false;
        }
        base.Update();
        GameManager.GameOver = gameOver;
    }
       
    private void GetInput()
    {
        direction = Vector2.zero;
        if (gameOver == false)
        {
            if (Input.GetKey(KeyCode.W))
            {
                direction += Vector2.up;
            }
            if (Input.GetKey(KeyCode.S))
            {
                direction += Vector2.down;
            }
            if (Input.GetKey(KeyCode.A))
            {
                direction += Vector2.left;
            }
            if (Input.GetKey(KeyCode.D))
            {
                direction += Vector2.right;
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (gameOver == false)
        {
            
            if (other.gameObject.tag == "Enemy")
            {  
                shake.CamShake();
                _instantiatedObj = Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(_instantiatedObj, 2);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameOver == false)
        {
            if(collision.gameObject.tag =="BadProjectile")
            {
                shake.CamShake();
            }
        }
    }
    public void Coroutines(int ID)
    {
        // dmg = 0 / dmg++ = 1 / range= 2 / fast = 3
        if(ID == 0)
        {
            StartCoroutine(DMGBoost(200)); //pass the damage as a parameter
        }
        else if(ID == 1)
        {
            StartCoroutine(DMGBoost(300)); 
        }
        else if(ID == 2)
        {
            StartCoroutine(RangeCollection()); 
        }
        else if (ID == 3)
        {
            StartCoroutine(FastShot()); 
        }
    }
    public IEnumerator FastShot()
    {
        _fastShot = true;
        _weapon1.startTimeBtwShots = 0.2f; // reduction of the time needed to shot again
        _gameManager.PowerUpS(2,1);        
        yield return new WaitForSeconds(8);       
        _fastShot = false;
        _weapon1.startTimeBtwShots = 0.5f;
        _gameManager.PowerUpS(2, 0);
    }
    public IEnumerator RangeCollection()//coroutine that boost the range to collect items
    {        
        _extraRangeON = true;
        _extraRange.SetActive(true);//enables the area
        _gameManager.PowerUpS(1,1);
        yield return new WaitForSeconds(4);
        _gameManager.PowerUpS(1, 0);
        _extraRange.SetActive(false);//disables the area
        _extraRangeON = false;
    }
    public IEnumerator DMGBoost(int pDamage)//coroutine to boost damage
    { // whith an  if statement i can change pdamage depending on what damage potion i take
        
        dmgBoostOn = true;
        _projectile.baseDamage = pDamage;
        _gameManager.PowerUpS(0, 1);//power up number 0 (damgeboost1) state 1 (ON)
        yield return new WaitForSeconds(8);//the time that the game waits to take the stats to the normal
        dmgBoostOn = false;
        pDamage = 100;
        _projectile.baseDamage = pDamage;
        _gameManager.PowerUpS(0, 0);
    }
    public void PickUpAdvise(string PickUp)
    {
        _pickedItem.text = "Picked up " + PickUp;
        StartCoroutine("TextVanish");
    }
    public void CriticDamage()
    {
        _criticalDamage.gameObject.SetActive(false);
        _criticalDamage.gameObject.SetActive(true);
        StartCoroutine("CritVanish");
    }
    public IEnumerator CritVanish()
    {
        yield return new WaitForSeconds(2);
        _criticalDamage.gameObject.SetActive(false);
    }
    public IEnumerator TextVanish()//if you picl up other item just before picking other the timer doesnt reset so the message can instant vanish ¿any solution?
    {
        yield return new WaitForSeconds(2);
        _pickedItem.text = " ";
    }
}
