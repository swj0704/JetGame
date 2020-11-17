using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum State : int{
        None = -1,
        Ready = 0,
        Appear,
        Battle,
        Dead,
        Disappear,
    }

    [SerializeField]
    State CurrentState =State.None;

    const float maxSpeed = 10.0f;
    const float maxSpeedTime = 0.5f;
    [SerializeField]
    Vector3 TargetPosition;

    float CurrentSpeed;

    Vector3 CurrentVelocity;
    float moveStartTime = 0f;

    float BattleStateTime = 0f;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        switch(CurrentState){
            case State.None : 
            case State.Ready : 
                break;
            case State.Dead : 
                break;
            case State.Appear : 
            case State.Disappear :
                UpdateSpeed();
                UpdateMove();
                break;
            case State.Battle:
                UpdateBattle();
                break;

        }


    }

    void UpdateSpeed(){
        CurrentSpeed = Mathf.Lerp(CurrentSpeed, maxSpeed, (Time.time - moveStartTime)/maxSpeedTime);
    }

    void UpdateMove(){
        float distance = Vector3.Distance(TargetPosition, transform.position);

        if(distance == 0){
            Arrived();
            return;
        }

        CurrentVelocity = (TargetPosition - transform.position).normalized * CurrentSpeed;

        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref CurrentVelocity, distance / CurrentSpeed, maxSpeed);

    }

    void Arrived(){
        CurrentSpeed = 0f;
        if(CurrentState == State.Appear){
            CurrentState = State.Battle;
            BattleStateTime = Time.time;
        } else {
            CurrentState = State.None;
        }
    }

    public void Appear(Vector3 targetPos){
        TargetPosition = targetPos;
        CurrentSpeed = maxSpeed;

        moveStartTime = Time.time;
        CurrentState = State.Appear;
    }

    void Disappear(Vector3 targetPos){
        TargetPosition = targetPos;

        CurrentSpeed = 0;

        moveStartTime = Time.time;


        CurrentState = State.Disappear;
    }
    
    void UpdateBattle(){
        if(Time.time - BattleStateTime > 3.0f){
            Disappear(new Vector3(-15.0f, transform.position.y, transform.position.z));
        }
    }

    private void OnTriggerEnter(Collider other){
        
        Player player = other.GetComponentInParent<Player>();
        if(player){
            player.OnCrash(this);
        }
    }

    public void OnCrash(Player player){
        
    }
}
