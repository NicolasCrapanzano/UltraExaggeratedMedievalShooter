using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //StartCoroutine(LoadScene());
    //public Animator transitionAnim;
    [SerializeField]
    private GameObject _History;
    [SerializeField]
    private GameObject _yup;
    [SerializeField]
    private GameObject _levelSelect;
    [SerializeField]
    private GameObject _options;
    private GameObject _instantiatedObj;
    private GameManager _gameManager;
    private SaveSystem _saveS;
    public Data data;
    private void Start()
    {
        _saveS = GameObject.FindObjectOfType<SaveSystem>();
        _saveS.LoadLevel();
        if (_History != null)
        {
            if (data.initialLore == false)
            {
                _History.SetActive(false);
            }
            else if (data.initialLore == true)
            {
                _History.SetActive(true);
            }
        }
    }
    public void Lore(int L)
    {
        if(L == 0)
        {
            data.initialLore = false;
            _saveS.SaveLevel();
        }
        if(L == 1)
        {
            data.initialLore = true;
            _saveS.SaveLevel();
        }
    }
    public void Options()
    {
        //SceneManager.LoadScene(2);
        _options.SetActive(true);
        
    }
    public void ChangeCursor(int c)
    {
        CursorScript.selectedCursor = c;
        data.actualCursor = c;
        _saveS.SaveLevel();
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Backtomenu()
    {
        
        SceneManager.LoadScene(0);
    }
    public void Play()
    {
        _levelSelect.SetActive(true);
    }
    public void QuitSelector(int Q)
    {
        if (Q == 0)
        {
            _levelSelect.SetActive(false);
        }else if (Q == 1)
        {
            _options.SetActive(false);
        }else if(Q == 2)
        {
            _History.SetActive(false);
        }
    }
    public void Level0()//Tutorial
    {
        GameManager.levelDificulty = 1;
        BackGroundManager.LevelDificulty = 1;
        EnemySpawner.bossFight = false;
        SceneManager.LoadScene(1);
        
    }
    public void Level1()
    {
        if (data.levelProgress >= 1)
        {

            BackGroundManager.LevelDificulty = 2;
            GameManager.levelDificulty = 2;
            EnemySpawner.bossFight = false;
            SceneManager.LoadScene(1);
        }
        
    }
    public void Level2()
    {
        if (data.levelProgress >= 2)
        {
            BackGroundManager.LevelDificulty = 3;
            GameManager.levelDificulty = 3;
            EnemySpawner.bossFight = false;
            SceneManager.LoadScene(1);
        }
        
    }
    public void Level3()
    {
        if (data.levelProgress >= 3)
        {
            BackGroundManager.LevelDificulty = 4;
            GameManager.levelDificulty = 4;
            EnemySpawner.bossFight = false;
            SceneManager.LoadScene(1);
        }
        
    }
    public void Level4()
    {
        if (data.levelProgress >= 4)
        {
            BackGroundManager.LevelDificulty = 5;
            GameManager.levelDificulty = 5;
            EnemySpawner.bossFight = false;
            SceneManager.LoadScene(1);
        }
        
    }
    public void Level5() //boss fight
    {
        if (data.levelProgress >= 5)
        {
            BackGroundManager.LevelDificulty = 6;
            GameManager.levelDificulty = 6;
            EnemySpawner.bossFight = true;
            SceneManager.LoadScene(1);
        }   
        
    }
    public void ArcadeMode() // how much can you ressist?
    {
        if (data.levelProgress >= 6)
        {
            BackGroundManager.LevelDificulty = 7;
            GameManager.levelDificulty = 7;
            EnemySpawner.bossFight = false;
            GameManager.GameOver = false;
            EnemySpawner.GameOver = false;
            EnemyScript.GameOver = false;
            CoinSpawner.GameOver = false;
            PotionSpawner.GameOver = false;
            FireImp.GameOver = false;
            Player.gameOver = false;
            SceneManager.LoadScene(1);
        }
        
    }
    public void Restart()
    {
        
        SceneManager.LoadScene(1);
    }
    public void ResetScore()
    {
        PlayerPrefs.DeleteAll();
    }
    public void Yup()
    {
        for (int i = 0; i < 100;i++)
        {
            float randX = Random.Range(-8.43f, 8.43f);
            float randY = Random.Range(5.30f,6f);
            _instantiatedObj = Instantiate(_yup, new Vector2(randX, randY), Quaternion.identity);
            Destroy(_instantiatedObj, 4);
        }
 
    }
    public void HereComesTheMoney()
    {
        
    }
    /*
        IEnumerator LoadScene()
        {
            transitionAnim.SetTrigger("end");
            yield return new WaitForSeconds(1.5f);

        }
        */
}
