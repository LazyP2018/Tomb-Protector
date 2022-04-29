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
    private float attackTimeTreshold = 1f;

    [SerializeField]
    private float eatTimeTreshold = 2f;

    [SerializeField]
    private LayerMask bushMask;



}
