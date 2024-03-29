﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    
    public float Speed;

    protected Vector2 direction;

    private Animator animator;

    // Start is called before the first frame update

    protected virtual void Start()
    {
        
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        Move();
    }
     public void Move()
    {
        transform.Translate(direction * Speed * Time.deltaTime);

        if(direction.x != 0 || direction.y !=0)
        {
            AnimateMovement(direction);
        }else
        {
            animator.SetLayerWeight(1, 0);
        }
        
    }

    public void AnimateMovement(Vector2 direction)
    {
        animator.SetLayerWeight(1, 1);

        animator.SetFloat("X", direction.x);
        animator.SetFloat("Y", direction.y);
    }
}
