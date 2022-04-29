using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite[] animSprites;
    private float timeTreshold = 0.1f;
    private float timer;
    private int state = 0;

    private PlayerMovement playerMovement;
    private SpriteRenderer sr;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isMoving)
        {
            if (Time.time > timer)
            {
                sr.sprite = animSprites[state % animSprites.Length];
                state++;
                timer = Time.time + timeTreshold;
            }
        }
        else if (!playerMovement.isMoving)
        {
            sr.sprite = animSprites[0];
        }
    }
}
