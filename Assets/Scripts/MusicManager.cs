using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _mainSong;
    [SerializeField]
    private AudioClip _bossFightSong;
    [SerializeField]
    private AudioClip _lastMomentsSong;
    [SerializeField]
    private AudioClip _loseState;
    [SerializeField]
    private AudioClip _winState;

    //
    

    //
    public int nearTheEnd;
    private GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        nearTheEnd = _gameManager.winCondition - 40;
    }

    
    void Update()
    {
           if (nearTheEnd <= _gameManager.monedasRecolectadas)
           {
                
           }
    }
    void NearTheEnd()
    {
        //if the player reaches -40 from the objective this song starts playing
       
    }
}
