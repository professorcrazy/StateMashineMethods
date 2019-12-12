using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveToBehaviour : IState {

    GameObject targetObj = null;
    Vector3? targetVec = null;
    NavMeshAgent navAgent;
    GameObject owner;
    float stoppingDistance;
    bool hasReachedTarget;
    private System.Action searchForNew;
    Animator anim;


    public MoveToBehaviour(NavMeshAgent navAgent, GameObject owner, TargetPos target, float stoppingDistance, Animator anim, System.Action searchForNew) {
        this.navAgent = navAgent;
        this.anim = anim;
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
        if (targetVec != null) {
            Debug.Log("target: " + targetVec);
        //    Vector3 tempVec = targetVec.Value; 
            this.navAgent.SetDestination(targetVec.Value);
        }

    }

    public void Execute() {
//        this.anim.SetFloat("Speed", Mathf.Abs(navAgent.velocity.magnitude));
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
