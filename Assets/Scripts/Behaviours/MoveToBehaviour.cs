using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToBehaviour : IState {

    GameObject target;
    NavMeshAgent navAgent;
    GameObject owner;
    float stoppingDistance;
    bool hasReachedTarget;
    private System.Action searchForNew;



    public MoveToBehaviour(NavMeshAgent navAgent, GameObject owner, GameObject targetPos, float stoppingDistance, System.Action searchForNew) {
        this.navAgent = navAgent;
        this.owner = owner;
        this.target = targetPos;
        this.stoppingDistance = stoppingDistance;
        this.searchForNew = searchForNew;
    }

    public void Enter() {
        navAgent.isStopped = false;
        this.navAgent.SetDestination(target.transform.position);

    }

    public void Execute() {
        if (!hasReachedTarget) {
            if (this.navAgent.remainingDistance < stoppingDistance) {
                navAgent.isStopped = true;
                hasReachedTarget = true;
                target.SetActive(false);
                this.searchForNew();
            }
        }
    }

    public void Exit() {

    }
}
