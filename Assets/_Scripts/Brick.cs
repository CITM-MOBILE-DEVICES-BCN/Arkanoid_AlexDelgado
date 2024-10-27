using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private BrickData spriteData;
    public enum Type
    {
        white = 0,
        yellow,
        orange,
        lightblue,
        green,
        red,
        blue,
        pink,
        ANIMATEDBLOCKS,
        grey = ANIMATEDBLOCKS,
        gold
    }

    private bool hasHitAnim;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public int hitPoints;
    public Type type;

    public static event Action<Brick> OnBrickDestroy;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        spriteRenderer.sprite = spriteData.sprites[(int)type];

        if (type == Type.grey)
        {
            animator.runtimeAnimatorController = spriteData.animators[(int)type - (int)Type.ANIMATEDBLOCKS];
            hitPoints = 3;
        }
        else if(type == Type.gold)
        {
            animator.runtimeAnimatorController = spriteData.animators[(int)type - (int)Type.ANIMATEDBLOCKS];
            hitPoints = -1;
        }
        else
        {
            animator.enabled = false;
        }
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        BallController ball = collision.gameObject.GetComponent<BallController>();
        BallCollision(ball);
    }

    private void BallCollision(BallController ball)
    {
        hitPoints--;

        if(hitPoints < 1 && hitPoints > -1)
        {
            OnBrickDestroy?.Invoke(this);
            Destroy(gameObject);
        }

        if((int)type >= (int)Type.ANIMATEDBLOCKS)
        {
            animator.SetBool("Hit", true);
            animator.SetBool("Hit", false);
            animator.SetTrigger("Hit");
        }
    }
}
