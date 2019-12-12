using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderBehaviour : IState {

    NavMeshAgent navAgent;
    GameObject owner;
    float wanderRadius;
    float wanderTimer;
    System.Action setNewTargetPos;

    float timer = 0;

    public WanderBehaviour(GameObject owner, NavMeshAgent navAgent, float wanderRadius, float wanderTimer, System.Action setNewTargetPos) {
        this.owner = owner;
        this.navAgent = navAgent;
        this.wanderRadius = wanderRadius;
        this.wanderTimer = wanderTimer;
        this.setNewTargetPos = setNewTargetPos;
    }

    public void Enter() {
        timer = Time.time + wanderTimer;
    }

    public void Execute() {
        if (timer < Time.time) {
            timer = Time.time + wanderTimer;
            Vector3 newPos = RandomNavSphere(this.owner.transform.position, wanderRadius, -1);
//            setNewTargetPos(newPos);
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void Exit() {

    }
}
