﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OwnerSide : int {
    Player = 0,
    Enemy = 1
}

public class Bullet : MonoBehaviour
{

    OwnerSide ownerSide = OwnerSide.Player;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    float Speed = 0f;

    bool NeedMove = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        UpdateMove();
    
    }

    void UpdateMove(){
        if(!NeedMove)
            return;
        
        Vector3 moveVector = moveDirection.normalized * Speed * Time.deltaTime;

        transform.position += moveVector;
    }

    public void Fire(OwnerSide FireOwner, Vector3 firePosition, Vector3 direction, float speed){
        ownerSide = FireOwner;
        transform.position = firePosition;
        moveDirection = direction;
        Speed = speed;

        NeedMove = true;
    }
}
