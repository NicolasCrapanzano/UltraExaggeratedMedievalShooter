using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    
    //level dificulty
    [Header("test")]
    public static int levelDificulty;
    public GameObject bossHealth;
    public bool bossDead=false;
    private bool _winupdate;
    private bool _saving;
    [SerializeField]
    private GameObject monedaPrefab;
    [SerializeField]
    private GameObject _endHistory;
    [SerializeField]
    private Text monedaTxt;

    [SerializeField]
    private Text GameOverTxt;

    [SerializeField]
    private GameObject _Tutorial;

    
    
    private Player _player;
    private GameObject _goPlayer;
    //private Text GuiaTxt;
    // private Text GuiaTxt2;

    private int _pDamage;
    public int monedasRecolectadas;
    private static GameManager instance;
    public static bool GameOver = false;
    public static int pHealth;
    public GameObject heart1, heart2, heart3;

    [SerializeField]
    private GameObject[] Shields;
    [SerializeField]
    private GameObject[] _powerUPs;
    public int actualShield;
    private float countDown = 10;
    private float _timer;
    //the ammount needed to win the game
    
    public int winCondition = 100;
    private Text _winCondTxt;
    //sound manager for the win state
    private AudioSource _audioSourceGM;
    [SerializeField]
    private AudioClip[] _youWin;
    private int _winAmount = 0;
    private int _winSoundPatch;
    public int specialObjective;
    private bool _specialFight = false;
    private EnemySpawner _enemySpawner;
    public bool _bossFight;
    private MainMenu _mainMenu;
    private Character _character;
    private MusicManager _mainCamera;
    private SaveSystem _save;
    private int _levelProgres=0;

    
    //check if the player win this level , in this way the game dont actually save in harddrive this update so if the player closes and open de game can abuse the sistem passing some levels 
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }    
    }

    public GameObject MonedaPrefab
    {
        get
        {
            return monedaPrefab;
        }
    }

    public int MonedasRecolectadas
    {
        get
        {
            return monedasRecolectadas;
        }
        set
        {
            monedaTxt.text = value.ToString();
            this.monedasRecolectadas = value;
        }  
    }


    void Start()
    {

        _character = GameObject.Find("Player").GetComponent<Character>();
        _bossFight = false;
        _mainMenu = GameObject.Find("GameManager").GetComponent<MainMenu>();
        _save = GameObject.Find("GameManager").GetComponent<SaveSystem>();
        _levelProgres = PlayerPrefs.GetInt("LevelProgress");
        //Debug.Log(levelDificulty);
        //
        GameOver = false;
        Player.gameOver = false;
        EnemySpawner.GameOver = false;
        EnemyScript.GameOver = false;
        CoinSpawner.GameOver = false;
        PotionSpawner.GameOver = false;
        FireImp.GameOver = false;
        GameOverTxt.enabled = false;
        //
        _enemySpawner = GameObject.Find("Spawner").GetComponent<EnemySpawner>();
        LevelDificulty();
        pHealth = 3;
        _player =  GameObject.Find("Player").GetComponent<Player>();
        _goPlayer = GameObject.Find("Player");
        _mainCamera = GameObject.Find("Main Camera").GetComponent<MusicManager>();
        GameOverTxt = GameObject.FindGameObjectWithTag("GAMEOVER").GetComponent<Text>();
        _winCondTxt = GameObject.FindGameObjectWithTag("objective").GetComponent<Text>();
        _winCondTxt.text = "Objective: " + "Collect " + winCondition + " Coins";
        _winSoundPatch = 0;
        _winupdate = false;

        if (winCondition > 0 & winCondition < 100)
        {
            _winAmount = 0;
        }else
        {
            _winAmount = 1;
        }
        if (_bossFight == false)
        {
           _goPlayer.transform.position = new Vector3(-2.71f, 0, 0);
        }else
        {
            _goPlayer.transform.position = new Vector3(-21.3f, 0, 0);
        }


    }


    void Update()
    {
        
        if (pHealth > 3)
            pHealth = 3;
        switch(pHealth)
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
            case -1:
            case -2:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                GameOverTxt.text = "GameOver";
                GameOverTxt.enabled = true;
                GameOver = true;
                Player.gameOver = true;
                EnemySpawner.GameOver = true;
                EnemyScript.GameOver = true;
                CoinSpawner.GameOver = true;
                PotionSpawner.GameOver = true;
                FireImp.GameOver = true;
                StartCoroutine("GoToMenu",2);
                break;
        }
        if (actualShield >= 0)
            
            switch(actualShield)
            {
                case 0:
                    Shields[0].gameObject.SetActive(false);
                    Shields[1].gameObject.SetActive(false);
                    Shields[2].gameObject.SetActive(false);
                    break;
                case 1:
                    Shields[0].gameObject.SetActive(true);
                    Shields[1].gameObject.SetActive(false);
                    Shields[2].gameObject.SetActive(false);
                    break;
                case 2:
                    Shields[0].gameObject.SetActive(true);
                    Shields[1].gameObject.SetActive(true);
                    Shields[2].gameObject.SetActive(false);
                    break;
                case 3:
                    Shields[0].gameObject.SetActive(true);
                    Shields[1].gameObject.SetActive(true);
                    Shields[2].gameObject.SetActive(true);
                    break;

            }
        if (levelDificulty == 5)
        {
            if(monedasRecolectadas >= winCondition)
            {
                if (_specialFight == false)
                {
                    _specialFight = true;
                    _enemySpawner.RutineInterceptor();
                }
                if (specialObjective == 1)
                {
                    WinState();
                }
            }
            
        }
        else
        {
            WinState();
        }
    }
    public void WinState()
    {
        if (monedasRecolectadas >= winCondition || bossDead == true)
        {
                if (_winSoundPatch < 1)
                {
                    AudioSource.PlayClipAtPoint(_youWin[_winAmount], Camera.main.transform.position, 1f);
                    _winSoundPatch++;
                }
                GameOver = true;
                Player.gameOver = true;
                EnemySpawner.GameOver = true;
                EnemyScript.GameOver = true;
                CoinSpawner.GameOver = true;
                PotionSpawner.GameOver = true;
                FireImp.GameOver = true;
                UpdateProgress();
                if (bossDead == true)
                {
                    GameOverTxt.text = "Congratulations";
                    _endHistory.SetActive(true);
                    StartCoroutine("GoToMenu", 10);
                }
                else
                {
                    StartCoroutine("GoToMenu", 2);
                }
                GameOverTxt.enabled = true;
            
      

        }
       

    }
    private void UpdateProgress() //
    {
        
        if (_winupdate == false && _levelProgres < 7)
        {
            Debug.Log("entro");
            if (levelDificulty == 1 && _mainMenu.data.levelProgress == 0)
            {
                
                _winupdate = true;
                _mainMenu.data.levelProgress = 1 ;
                
                
            }
            else if (levelDificulty == 2 && _mainMenu.data.levelProgress == 1)
            {
                _winupdate = true;
                _mainMenu.data.levelProgress = 2 ;


            }
            else if (levelDificulty == 3 && _mainMenu.data.levelProgress == 2)
            {
                _winupdate = true;
                _mainMenu.data.levelProgress = 3;

            }
            else if (levelDificulty == 4 && _mainMenu.data.levelProgress == 3)
            {
                _winupdate = true;

                _mainMenu.data.levelProgress = 4;

            }
            else if (levelDificulty == 5 && _mainMenu.data.levelProgress == 4)
            {
                _winupdate = true;

                _mainMenu.data.levelProgress = 5;

            }
            else if (levelDificulty == 6 && _mainMenu.data.levelProgress == 5)
            {
                _winupdate = true;

                _mainMenu.data.levelProgress =6;
            }
            else if (levelDificulty == 7 && _mainMenu.data.levelProgress == 6)
            {
                _winupdate = true;

                _mainMenu.data.levelProgress = 7;
            }
            if (_saving == false)
            {
                _save.SaveLevel();
                _saving = true;
            }
        }
    }
    IEnumerator GoToMenu(int t)
    {
        yield return new WaitForSeconds(t);
        SceneManager.LoadScene(0);
    }
    public void PowerUpS(int ID, int State)
    {
        if(ID==0 & State == 1)//Damage boost ON
        {
            _powerUPs[0].gameObject.SetActive(true);
        }else if (ID==0 & State == 0)//OFF
        {
            _powerUPs[0].gameObject.SetActive(false);
        }

        if(ID== 1 & State == 1)//Recolection boost ON
        {
            _powerUPs[1].gameObject.SetActive(true);
        }else if (ID==1 & State == 0)//OFF
        {
            _powerUPs[1].gameObject.SetActive(false);
        }

        if(ID == 2 & State == 1)//Fast shoting boost ON
        {
            _powerUPs[2].gameObject.SetActive(true);
        }else if(ID == 2 & State == 0)//OFF
        {
            _powerUPs[2].gameObject.SetActive(false);
        }
    }


    private void LevelDificulty()
    {
        if (levelDificulty == 1)//tutorial  enemy 1
        {
            
            //Debug.Log("function");
            winCondition = 20;
            _Tutorial.SetActive(true);
            _enemySpawner.spawnRate = 20f;
            _enemySpawner.spawnLimit = 5;
        }
        else if (levelDificulty == 2)//level 1  enemy 1 and 2
        {
            
            _enemySpawner.spawnRate = 15f;
            winCondition = 60;
            _enemySpawner.spawnLimit = 4;
        }
        else if (levelDificulty == 3)//level 2 enemy 2 and 3
        {
            _character.Speed = 6;
            winCondition = 100;
            _enemySpawner.spawnRate = 10f;
            _enemySpawner.spawnLimit = 3;
        }
        else if (levelDificulty == 4)//level 3  enemy 3 and 4
        {
            
            winCondition = 130; //win based on the ammount of kills?
            _enemySpawner.spawnRate = 8f;
            _enemySpawner.spawnLimit = 2;
        }
        else if (levelDificulty == 5)//level 4  puzzle?
        {
           
            winCondition = 50;
            _enemySpawner.spawnRate = 6f;
            _enemySpawner.spawnLimit = 1;
        }
        else if (levelDificulty == 6)// Boss fight
        {
            _bossFight = true;
            bossHealth.SetActive(true);
            winCondition = 999;
            _enemySpawner.spawnRate = 7f;
            _enemySpawner.spawnLimit = 4;
        }
        else if (levelDificulty == 7)// Arcade mode
        {
            winCondition = 9999;
            _enemySpawner.spawnRate = 2f;
            _enemySpawner.spawnLimit = 1;
        }

        //Debug.Log(spawnRate);

    }

}

