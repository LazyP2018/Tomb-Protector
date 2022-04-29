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

    private void Update()
    {
        if (!tomb)
            return;

        if (isEater)
        {
            if (bushFruitsTarget && bushFruitsTarget.enabled && bushFruitsTarget.HasFruits() && !killingBush)
            {
                //walk to bush if not close enough
                if (Vector2.Distance(transform.position, bushFruitsTarget.transform.position) > 0.5f)
                {
                    float step = moveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position,
                        bushFruitsTarget.transform.position, step);
                    isMoving = true;
                }
                else
                {
                    isMoving = false;
                    bushFruitsTarget.HarvestFruit();
                    eatTimer = Time.time + eatTimeTreshold;
                    killingBush = true;
                }
            }
            else if (killingBush)
            {
                if (Time.time > eatTimer)
                {
                    bushFruitsTarget.EatBushFruits();
                    killingBush = false;
                    SearchForTarget();
                }
            }
            else
            {
                SearchForTarget();
            }

            if (bushFruitsTarget)
            {
                if (bushFruitsTarget.transform.position.x < transform.position.x)
                    left = true;
                else
                    left = false;
            }

            if (!bushFruitsTarget)
                SearchForTarget();

        }
        else
        {
            if (Vector2.Distance(transform.position, tomb.transform.position) > 1.5f)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, tomb.transform.position, step);
                isMoving = true;
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                attackTimer = Time.time + attackTimeTreshold;
                isMoving = false;
            }
            if (isAttacking)
            {
                if (Time.time > attackTimer)
                {
                    Attack();
                    attackTimer = Time.time + attackTimeTreshold;
                }
            }
            if (tomb.transform.position.x < transform.position.x)
                left = true;
            else
                left = false;
        }
    }

    void SearchForTarget()
    {
        bushFruitsTarget = null;
        Collider2D[] hits;
        isMoving = false;

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
