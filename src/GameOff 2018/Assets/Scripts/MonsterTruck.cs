using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterTruck : MonoBehaviour {
    public Color color;
    public float targetUpdateFrequency;

    [Range(0f, 1.0f)]
    public float chanceToTargetPlayer;

    [Range(0f, 1.0f)]
    public float chanceToTargetNearPlayer;

    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Vector3 target;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = (NavMeshAgent)GetComponent("NavMeshAgent");

        foreach (Renderer r in GetComponentsInChildren<Renderer>()) {
            r.material.color = color;
        }
    }

    // Use this for initialization
    void Start () {
        InvokeRepeating("UpdateTarget", 0, targetUpdateFrequency);
	}

    private void OnDrawGizmos() {
        Gizmos.color = color;
        Gizmos.DrawWireSphere(target, 0.3f);

        if (navMeshAgent != null && navMeshAgent.path != null) {
            Vector3 last = transform.position;
            foreach (Vector3 p in navMeshAgent.path.corners) {
                Gizmos.DrawLine(last, p);
                Gizmos.DrawWireSphere(p, 0.1f);
                last = p;
            }
        }
    }

    private void UpdateTarget() {
        //Roll for attack!
        float roll = Random.Range(0, 1.0f);

        //First see if it's enough to target directly
        if (roll <= chanceToTargetPlayer) {
            target = TargetPlayerLocation();

        //Then check again the chance to target *near* the player
        } else if (roll <= chanceToTargetNearPlayer) {
            target = TargetNearPlayer();

        //If that fails, just go someplace random 
        } else {
            target = TargetRandomLocation();
        }

        navMeshAgent.SetDestination(target);
    }

    Vector3 TargetPlayerLocation() {
        Vector3 ret;

        if (player != null) {
            ret = player.transform.position;
        } else {
            ret = TargetRandomLocation();
        }

        return ret;
    }

    Vector3 TargetNearPlayer() {
        Vector3 ret;

        if (player != null)
        {
            float offsetRadius = 4.0f;
            float theta = Random.Range(0, Mathf.PI * 2.0f);
            Vector3 p = player.transform.position;
            Vector3 offset = new Vector3(Mathf.Cos(theta), 0, Mathf.Sin(theta));
            ret = p + (offset * offsetRadius);
        }
        else
        {
            ret = TargetRandomLocation();
        }

        return ret;
    }

    Vector3 TargetRandomLocation() {
        return new Vector3(Random.Range(-17.0f, 17.0f), 0, Random.Range(-17.0f, 17.0f));
    }
}
