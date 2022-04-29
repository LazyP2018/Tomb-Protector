using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    [SerializeField]
    private bool isEater;
    
    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private int attackDamage = 5;

    [SerializeField]
    private float attackTimeTreshold = 1f;

    [SerializeField]
    private float eatTimeTreshold = 2f;

    [SerializeField]
    private LayerMask bushMask;

    public bool isMoving, left;

    private TombScript tomb;
    private BushFruits bushFruitsTarget;
    private float attackTimer;
    private float eatTimer;
    private bool killingBush;
    private bool isAttacking;

    private void Start()
    {
        if (isEater)
        {
            SearchForTarget();
            killingBush = false;
        }
        else
        {
            isAttacking = false;
        }

        tomb = GameObject.FindWithTag("Tomb").GetComponent<TombScript>();
    }

    void SearchForTarget()
    {
        bushFruitsTarget = null;
        Collider2D[] hits;

        for (int i = 1; i < 50; i++)
        {
            hits = Physics2D.OverlapCircleAll(transform.position, Mathf.Exp(i), bushMask);
            foreach (Collider2D hit in hits)
            {
                if(hit && (hit.GetComponent<BushFruits>().HasFruits() &&
                    hit.GetComponent<BushFruits>().enabled))
                {
                    bushFruitsTarget = hit.GetComponent<BushFruits>();
                    break;
                }
            }
            if (bushFruitsTarget)
                break;
        }
    }

    void Attack()
    {
        tomb.TakeDamage(attackDamage);
    }

}
