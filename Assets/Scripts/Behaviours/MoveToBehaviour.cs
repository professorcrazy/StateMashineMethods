using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToBehaviour : IState {

    GameObject targetObj = null;
    Vector3 targetVec;
    NavMeshAgent navAgent;
    GameObject owner;
    float stoppingDistance;
    bool hasReachedTarget;
    private System.Action searchForNew;



    public MoveToBehaviour(NavMeshAgent navAgent, GameObject owner, TargetPos target, float stoppingDistance, System.Action searchForNew) {
        this.navAgent = navAgent;
        this.owner = owner;
        this.targetObj = target.targetObj;
        this.targetVec = target.targetVector;
        this.stoppingDistance = stoppingDistance;
        this.searchForNew = searchForNew;
    }

    public void Enter() {
        navAgent.isStopped = false;
        if (targetObj != null) {
            this.navAgent.SetDestination(targetObj.transform.position);
        }
        if (targetVec != Vector3.zero) {
            Debug.Log("target: " + targetVec);
            this.navAgent.SetDestination(targetVec);
        }

    }

    public void Execute() {
        if (!hasReachedTarget) {
            if (this.navAgent.remainingDistance < stoppingDistance) {
                navAgent.isStopped = true;
                hasReachedTarget = true;
                if (targetObj != null) {
                    targetObj.SetActive(false);
                }
                //if (targetVec != null) {
                //    this.searchForNew();
                //}

                this.searchForNew();
            }
        }
    }

    public void Exit() {

    }
}
