using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _level1;
    [SerializeField]
    private Sprite[] _level2;
    [SerializeField]
    private Sprite[] _level3;
    [SerializeField]
    private Sprite[] _level4;
    private SpriteRenderer _actualBack;
    private int _randBack;

    [SerializeField]
    private GameObject[] _backDeco;
    private int _randInstObj;
    private float _randX;
    private float _randY;

    public static int LevelDificulty;
    void Start()
    {

        _actualBack = gameObject.GetComponent<SpriteRenderer>();
        _randBack = Random.Range(0,7);
        _actualBack.sprite = _level1[_randBack];
        //Debug.Log(_randBack);
        /*
        if (LevelDificulty == 0 || LevelDificulty == 1)
        {
            _actualBack.sprite = _level1[_randBack];
        }else if(LevelDificulty == 2)
        {
            _actualBack.sprite = _level2[_randBack];
        }
        else if(LevelDificulty == 3)
        {
            _actualBack.sprite = _level3[_randBack];
        }
        else if(LevelDificulty == 4)
        {
            _actualBack.sprite = _level4[_randBack];
        }
        else if(LevelDificulty == 5)
        {
            _actualBack.sprite = _level1[_randBack];
        }*/
        MapDecorationFunction();
    }

    
    void Update()
    {
        
    }

    void MapDecorationFunction()
    {
        for (int i = 0 ; i < 20 ; i++)
        {
            _randX = Random.Range(-19.94f, 20.25f);
            _randY = Random.Range(-14.1f, 12f);
            _randInstObj = Random.Range(0,7);
            Instantiate(_backDeco[_randInstObj],new Vector2(_randX,_randY),Quaternion.identity);
        }
    }
}
