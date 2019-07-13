using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    private Transform _playerpos;
    private Player _player;
    private bool _collected = false;
    private int _ID;
    private int _randCoinSound;
    private GameManager _gameManager;
    private PotionSpawner _potionSpawner;

    [Header("Local vars")]
    public bool dmgBoostOn;
    public bool extraRangeON;
    public bool fastShot;
    private float _followTime;

    private void Start()
    {
        dmgBoostOn = false;
        extraRangeON = false;
        fastShot = false;
        //Debug.Log("started boii");
        _playerpos = GameObject.Find("Player").GetComponent<Transform>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _potionSpawner = GameObject.Find("Spawner").GetComponent<PotionSpawner>();
    }
    private void Update()
    {

        if (_collected == true)
        {
            if (_followTime > Time.time)
            {
                Touched();
            }
        }
    }
    public void Touched()
    {
        transform.position = Vector2.MoveTowards(transform.position, _playerpos.position, 6 * Time.deltaTime);
    }
    private void PickedUp()
        {

            if (this.tag == "Moneda")
            {
                _randCoinSound = Random.Range(0, 8);
                _player._audioSource.clip =_player._coinBops[_randCoinSound];
                _player._audioSource.Play();
                _player.PickUpAdvise("Coin +1");
                GameManager.Instance.MonedasRecolectadas++;
                Destroy(gameObject);
            }
            else if (this.tag == "Coin2")
            {
                _randCoinSound = Random.Range(0, 8);
                _player._audioSource.clip = _player._coinBops[_randCoinSound];
                _player._audioSource.Play();
                _player.PickUpAdvise("Orange coin +5");
                GameManager.Instance.MonedasRecolectadas += 5;
                Destroy(gameObject);
            }
            else if (this.tag == "Coin3")
            {
                _randCoinSound = Random.Range(0, 8);
                _player._audioSource.clip = _player._coinBops[_randCoinSound];
                _player._audioSource.Play();
                _player.PickUpAdvise("Red coin +10");
                GameManager.Instance.MonedasRecolectadas += 10;
                Destroy(gameObject);
            }
            else if (this.tag == "Coin4")
            {
                _randCoinSound = Random.Range(0, 8);
                _player._audioSource.clip = _player._coinBops[_randCoinSound];
                _player._audioSource.Play();
                _player.PickUpAdvise("Purple coin +15");
                GameManager.Instance.MonedasRecolectadas += 15;
                Destroy(gameObject);
            }
            else if (this.tag == "Potion" && GameManager.pHealth < 3) // health potion 1
            {
                _player._audioSource.clip =_player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Basic health potion");
                GameManager.pHealth += 1;
                _potionSpawner.maxPotion--; // discount 1 for the maximum to let the spawner keep spawning
                Destroy(gameObject);
            }
            else if (this.tag == "Potion2"&& GameManager.pHealth < 3) // health potion 2
            {
                _player._audioSource.clip = _player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Strong health potion");
                GameManager.pHealth += 2;
                _potionSpawner.maxPotion--;
                Destroy(gameObject);
            }
            else if (this.tag == "Potion3" && _player._shieldAmmount < 3)//shield
            {
                _player._audioSource.clip = _player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Shield potion");
                _player._shieldAmmount++;
                _gameManager.actualShield++;
                _player._shieldActive = true;
                Destroy(gameObject);
            }
            else if (this.tag == "Potion4" && dmgBoostOn == false)//damage boost 1
            {
                _player._audioSource.clip = _player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Damage boost potion");
                _player.Coroutines(0);
                _potionSpawner.maxPotion--;
                Destroy(gameObject);
            }
            else if (this.tag == "Potion5" && dmgBoostOn == false)//damage boost 2
            {
                _player._audioSource.clip = _player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Strong damage boost potion");
                _player.Coroutines(1);
                _potionSpawner.maxPotion--;
                Destroy(gameObject);
            }
            else if (this.tag == "Potion6" && extraRangeON == false)//magnet boost
            {
                _player._audioSource.clip = _player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Magnet potion");
                _player.Coroutines(2);
                _potionSpawner.maxPotion--;
                Destroy(gameObject);
            }
            else if (this.tag == "Potion7" && fastShot == false)//fast shoot
            {
                _player._audioSource.clip = _player._drinking;
                _player._audioSource.Play();
                _player.PickUpAdvise("Fast shooting potion");
                _player.Coroutines(3);
                _potionSpawner.maxPotion--;
                Destroy(gameObject);
            }      
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.gameObject.tag == "Player" || Other.gameObject.name == "ExtraRecolectionArea")
        {
                _followTime = Time.time + 10f;
                _collected = true;
            
        }

        if (_collected == true)
        {
            if (Other.gameObject.tag == "Player")
            {
                dmgBoostOn = _player.dmgBoostOn;
                extraRangeON = _player._extraRangeON;
                fastShot = _player._fastShot;
                PickedUp();
            }
        }      
    }
}
