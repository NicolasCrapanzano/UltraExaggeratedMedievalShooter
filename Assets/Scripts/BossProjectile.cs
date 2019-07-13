using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private float _speed;
    private float _distance;
    public LayerMask whatIsSolid;
    private int _damage;
    private Player _player;
    private GameManager _gameManager;
    private BossBehavoiur _bossBehavoiur;
    private SpriteRenderer _spriteR;
    // Start is called before the first frame update
    void Start()
    {
        _bossBehavoiur = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossBehavoiur>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spriteR = GetComponent<SpriteRenderer>();
        _speed = 10f;
        _damage = 1;
        if (_bossBehavoiur.boostDamage == true)
        {
            _spriteR.color = Color.magenta;
            _damage = 2;
        }
        Destroy(this.gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, transform.up, _distance, whatIsSolid);
        if (hit2D.collider != null)
        {
            if (hit2D.collider.name == "Player")
            {
                if(_player._shieldActive == false)
                {
                    GameManager.pHealth -= _damage;
                }else if(_player._shieldActive == true)
                {
                    _player._shieldAmmount -- ;
                    _gameManager.actualShield--;
                }
                Destroy(this.gameObject);
            }
        }
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

}
