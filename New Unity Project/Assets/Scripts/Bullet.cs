using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OwnerSide : int {
    Player = 0,
    Enemy = 1
}

public class Bullet : MonoBehaviour
{
    const float LifeTime = 15.0f;   

    OwnerSide ownerSide = OwnerSide.Player;
    [SerializeField]
    Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    float Speed = 0f;

    bool NeedMove = false;

    bool Hited = false;

    float FiredTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(processDisapperCondition()){
            return;
        }
        

        UpdateMove();
    
    }

    void UpdateMove(){
        if(!NeedMove)
            return;
        



        Vector3 moveVector = moveDirection.normalized * Speed * Time.deltaTime;

        moveVector = AdjustMove(moveVector);

        transform.position += moveVector;
    }

    public void Fire(OwnerSide FireOwner, Vector3 firePosition, Vector3 direction, float speed){
        ownerSide = FireOwner;
        transform.position = firePosition;
        moveDirection = direction;
        Speed = speed;
        FiredTime = Time.time;

        NeedMove = true;
    }

    Vector3 AdjustMove(Vector3 moveVector){
        RaycastHit hitInfo;

        if(Physics.Linecast(transform.position, transform.position + moveVector, out hitInfo)){

            moveVector = hitInfo.point - transform.position;
            OnBulletCollision(hitInfo.collider);
        }

        return moveVector;
    }

    void OnBulletCollision(Collider collider){

        if(Hited){
            return;
        }
        
        Collider myCollider = GetComponentInChildren<Collider>();
        myCollider.enabled = false;

        Hited = true;
        NeedMove = false;

        if(ownerSide == OwnerSide.Player){
            Enemy enemy = collider.GetComponentInParent<Enemy>();
        } else {
            Player player = collider.GetComponentInParent<Player>();
        }

    }

    private void OnTriggerEnter(Collider other){

        OnBulletCollision(other);

    }

    bool processDisapperCondition(){
        if(transform.position.x > 15.0f || transform.position.x < -15.0f || transform.position.y > 15.0f || transform.position.y < -15.0f){
            Disapper();
            return true;
        }
        else if(Time.time - FiredTime > LifeTime){
            Disapper();
            return true;
        }

        return false;
    }

    void Disapper(){
        Destroy(gameObject);
    }
}
