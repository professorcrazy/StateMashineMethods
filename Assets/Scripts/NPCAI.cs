﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class NPCAI : MonoBehaviour
{
    StateMashine npcStateMashine = new StateMashine();

    [SerializeField] LayerMask searchLayers;
    [SerializeField] float viewRange;
    [SerializeField] string tagToLookFor;
    [SerializeField] float wanderRadius;
    [SerializeField] float wanderTimer;

    NavMeshAgent navAgent;
    Animator anim;

    private void Start() {
        anim = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
        npcStateMashine.ChangeState(new SearchBehaviour(searchLayers, this.gameObject, this.viewRange, this.tagToLookFor, SearchResultFound));
    }

    private void Update() {
        npcStateMashine.ExecuteStateUpdate();
        this.anim.SetFloat("Speed", Mathf.Abs(navAgent.velocity.magnitude));

    }

    void StartSearching() {
        npcStateMashine.ChangeState(new SearchBehaviour(searchLayers, this.gameObject, this.viewRange, this.tagToLookFor, SearchResultFound));
    }

    public void SearchResultFound(SearchResults searchResult) {
        List<Collider> foundSerchedResult = searchResult.allHitObjsWithReqTag;

        if (searchResult.closestObjWithTag != null) {
            //Move to state
            TargetPos newSearchTarget = new TargetPos(searchResult.closestObjWithTag);
            npcStateMashine.ChangeState(new MoveToBehaviour(this.navAgent, this.gameObject, newSearchTarget, 1f, anim, StartSearching));
        }
        else {
            Debug.Log("Entered: Wander stage");
            //Wander state
            npcStateMashine.ChangeState(new WanderBehaviour(this.gameObject, this.navAgent, this.wanderRadius, this.wanderTimer, MoveToPosition));
        }
    }

    void MoveToPosition(TargetPos targetPos) {

        if (targetPos.targetVector != null) {
            npcStateMashine.ChangeState(new MoveToBehaviour(this.navAgent, this.gameObject, targetPos, 1f, anim, StartSearching));
        }
    }

}
