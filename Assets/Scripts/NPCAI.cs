using System.Collections;
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

    private void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        npcStateMashine.ChangeState(new SearchBehaviour(searchLayers, this.gameObject, this.viewRange, this.tagToLookFor, SearchResultFound));
    }

    private void Update() {
        npcStateMashine.ExecuteStateUpdate();
    }

    void StartSearching() {
        npcStateMashine.ChangeState(new SearchBehaviour(searchLayers, this.gameObject, this.viewRange, this.tagToLookFor, SearchResultFound));
    }

    public void SearchResultFound(SearchResults searchResult) {
        List<Collider> foundSerchedResult = searchResult.allHitObjsWithReqTag;

        if (searchResult.closestObjWithTag != null) {
            //Move to state
            npcStateMashine.ChangeState(new MoveToBehaviour(this.navAgent, this.gameObject,searchResult.closestObjWithTag, 1f, StartSearching));
        }
        else {
            //Wander state
            npcStateMashine.ChangeState(new WanderBehaviour(this.gameObject, this.navAgent, this.wanderRadius, this.wanderTimer, MoveToPosition)
        }
    }

    void MoveToPosition(GameObject target) {
        npcStateMashine.ChangeState(new MoveToBehaviour(this.navAgent, this.gameObject, target, 1f, StartSearching));

    }

}
