using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iuppy : MonoBehaviour
{
    [SerializeField]
    private int _ID;
    private Transform _move;
    private int _spedo;
    void Start()
    {
        if (_ID == 0 || _ID == 1)
        {
            _move = GetComponent<Transform>();
            _spedo = Random.Range(3, 5);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(_ID == 0)
        {
            
            _move.transform.Translate(Vector2.down * _spedo * Time.deltaTime);
            if(transform.position.x < -20)
            {
                Destroy(this.gameObject);
            }
        }
        if(_ID == 1)
        {
            _move.transform.Translate(Vector2.left * _spedo * 2 * Time.deltaTime);
            if(transform.position.x < -30)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
