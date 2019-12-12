using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SearchBehaviour : IState {


    LayerMask searchLayer;
    float radius = 0;
    GameObject owner;
    string tagToLookFor;

    public bool SearchCompleted = false;
    private System.Action<SearchResults> searchResultCallback;
    /**
     * SearchLayer, Owner, searchRadius, TagsToLookFor, NavMeshAgent
     * */
    public SearchBehaviour(LayerMask layer, GameObject owner, float searchRadius, string tagToLookFor, System.Action<SearchResults> searchResultCallback) {
        this.searchLayer = layer;
        this.owner = owner;
        this.radius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.searchResultCallback = searchResultCallback;

    }

    public void Enter() {
        
    }

    public void Execute() {
        if (!SearchCompleted) {


            Collider[] hitObjs = Physics.OverlapSphere(this.owner.transform.position, this.radius);
            List<Collider> allObjsWithReqTag = new List<Collider>();
            for (int i = 0; i < hitObjs.Length; i++) {
                if (hitObjs[i].CompareTag(tagToLookFor)) {
                    //this.navAgent.SetDestination(hitObjs[i].transform.position);
                    allObjsWithReqTag.Add(hitObjs[i]);
                }
            }

            GameObject closestItem = null;
            float shortestDist = float.PositiveInfinity;
            foreach (var obj in allObjsWithReqTag) {
                float distToObj = Vector3.Distance(owner.transform.position, obj.transform.position);
                if (distToObj < shortestDist) {
                    closestItem = obj.gameObject;
                    shortestDist = distToObj;
                }
            }
            SearchResults searchResults = new SearchResults(hitObjs, allObjsWithReqTag, closestItem);
            //This is where we should send the info back

            SearchCompleted = true;

            this.searchResultCallback(searchResults);

        }
    }



    public void Exit() {

    }
}

public class SearchResults {
    public Collider[] allHitObjsInSearchRadius;
    public List<Collider> allHitObjsWithReqTag;
    public GameObject closestObjWithTag;

    public SearchResults(Collider[] allHitObjsInSearchRadius, List<Collider> allHitObjsWithReqTag, GameObject closestObjWithTag) {
        this.allHitObjsInSearchRadius = allHitObjsInSearchRadius;
        this.allHitObjsWithReqTag = allHitObjsWithReqTag;
        this.closestObjWithTag = closestObjWithTag;
        //other methods to handle the data.
    }
}
