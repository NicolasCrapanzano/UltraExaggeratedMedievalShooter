using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireImp : MonoBehaviour
{
    public float offset;

    public GameObject projectile;
    public Transform shotPoint;

    public static bool GameOver = false;

    private float timeBtwShots;
    public float startTimeBtwShots = 0.5f;
    private bool _nextShoot=false;
    private void Update()
    {
        if (GameOver == false)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
            if(Input.GetMouseButtonDown(0) && timeBtwShots > 0)
            {
                _nextShoot = true;
            }
            if (timeBtwShots <= 0)
            {
                if (Input.GetMouseButtonDown(0) || _nextShoot == true)
                {
                    _nextShoot = false;
                    Instantiate(projectile, shotPoint.position, transform.rotation);
                    timeBtwShots = startTimeBtwShots;
                }
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

}
