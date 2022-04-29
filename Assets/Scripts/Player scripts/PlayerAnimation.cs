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

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timer)
        {
            sr.sprite = animSprites[state % animSprites.Length];
            state++;
            timer = Time.time + timeTreshold;
        }
        else
        {
            sr.sprite = animSprites[0];
        }
    }
}
