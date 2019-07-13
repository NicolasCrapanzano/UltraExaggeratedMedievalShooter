using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public float distance;
    public float baseDamage=100;
    public float damage=100;
    
    public LayerMask whatIsSolid;

    private GameObject instantiatedObj;
    private Player _player;
    public GameObject destroyEffect;
    private int _randCrit;
    private GameManager _gameManager;
    private CursorScript _cursor;
    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        _player = GameObject.Find("Player").GetComponent<Player>();
        _cursor = GameObject.FindGameObjectWithTag("Cursor").GetComponent<CursorScript>();
    }
    private void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if(hitInfo.collider != null)
        {
            _randCrit = Random.Range(0, 100);
            //Debug.Log(_randCrit);
            if(_randCrit > 0 && _randCrit < 10)
            {
                _player.CriticDamage();
                damage = baseDamage * 2;
            }else
            {
                damage = baseDamage;
            }
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                //Debug.Log("projectile does " + damage);

                hitInfo.collider.GetComponent<EnemyScript>().TakeDamage(damage);
                _cursor.RutineInterceptor();
            }
            else if (hitInfo.collider.CompareTag("Boss"))
            {
                hitInfo.collider.GetComponent<BossBehavoiur>().TakeDamage(damage);
                _cursor.RutineInterceptor();
            }
            DestroyProjectile();
        }
        
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile()
    {
        instantiatedObj= Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(instantiatedObj, 1);
        Destroy(gameObject);
    }
}
