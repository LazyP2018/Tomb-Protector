using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 3f;

    private Rigidbody2D myBody;
    private Vector2 moveVector;
    private SpriteRenderer sr;
    private Animator anim;
    private float harvestTimer;
    private bool isHarvesting;
    private GameObject artifact;
    private string MOVEMENT_AXIS_X = "Horizontal";
    private string MOVEMENT_AXIS_Y = "Vertical";
    private string WALK_ANIMATION = "Walking";

    
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        anim.SetBool(WALK_ANIMATION, false);
    }

    private void Update()
    {
        FlipSprite();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
        if (isHarvesting)
        {
            myBody.velocity = Vector2.zero;
        }
        else
        {
            moveVector = new Vector2(Input.GetAxis(MOVEMENT_AXIS_X), Input.GetAxis(MOVEMENT_AXIS_Y));
            if (moveVector.sqrMagnitude > 1)
                moveVector = moveVector.normalized;
            myBody.velocity = new Vector2(moveVector.x * movementSpeed, moveVector.y * movementSpeed);
        }
    }
    
    private void FlipSprite()
    {
        if (Input.GetAxisRaw(MOVEMENT_AXIS_X) == 1)
        {
            sr.flipX = false;
        } 
        else if (Input.GetAxisRaw(MOVEMENT_AXIS_X) == -1)
        { 
            sr.flipX = true;
        }
    }

    private void AnimatePlayer()
    {
        if (Input.GetAxisRaw(MOVEMENT_AXIS_X) > 0 || Input.GetAxisRaw(MOVEMENT_AXIS_X) < 0 || Input.GetAxisRaw(MOVEMENT_AXIS_Y) > 0 || Input.GetAxisRaw(MOVEMENT_AXIS_Y) < 0)
        {
            anim.SetBool(WALK_ANIMATION, true);
        }
        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bush"))
        {
            Debug.Log("The value of fruit is: " +
                collision.gameObject.GetComponent<BushFruits>().HarvestFruit());
        }
    }
}
