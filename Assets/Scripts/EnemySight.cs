using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemySight : MonoBehaviour {

    [SerializeField] float fieldOfViewAngles = 120;
    [SerializeField] bool playerInSight;
    [SerializeField] Vector3 personalLastSighting;
    [SerializeField] Transform raycastPos;


    MovementAI moveAI;

    SphereCollider col;
    //    Animator anim;
    //    LastPlayerSighting lastPlayerSighting;
    GameObject player;
    Vector3 previousSighting;
    Transform originalTarget;

    //Start is called before the first frame update
    void Awake() {
        col = GetComponent<SphereCollider>();
        moveAI = GetComponent<MovementAI>();
        originalTarget = moveAI.target;

    }

    private void Update() {
        Debug.DrawRay(raycastPos.position, transform.forward, Color.red);
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            playerInSight = false;
            Debug.Log("PlayerEnters");
            Vector3 direction = other.transform.position - raycastPos.position;
            float angle = Vector3.Angle(direction, raycastPos.forward);
            if (angle <= fieldOfViewAngles*0.5f) {
                Debug.Log("PlayerInView" + col.radius);

                RaycastHit hit;
                if (Physics.Raycast(raycastPos.position, direction.normalized, out hit, col.radius)) {
                    Debug.Log("Raycast hit something");
//                    Debug.DrawRay(raycastPos.position, direction.normalized, Color.red, 5f);
                    if (hit.collider.tag == "Player") {
                        Debug.Log("PlayerInView not obstructed");
                        playerInSight = true;
                        moveAI.target = other.transform;
                        //Start "Chase state"
                    }
                }
            }
            //make funtionallity for sound heard AKA "Searching state"
           
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            moveAI.target = originalTarget;
        }
    }
}
