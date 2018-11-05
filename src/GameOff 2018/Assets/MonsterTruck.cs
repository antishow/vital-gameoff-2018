using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTruck : MonoBehaviour {
    public Color color;

    private GameObject player;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = (NavMeshAgent)GetComponent("NavMeshAgent");

        Renderer[] renderers = GetComponentsInChildren<Renderer>().Select(r =>
        {
            Material mat = r.material;
            mat.color = color;

            return r;
        }).ToArray<Renderer>();
    }

    // Use this for initialization
    void Start () {
        if (player != null) {
            InvokeRepeating("TargetPlayerLocation", 0, 2.0f);
        }
	}

    void TargetPlayerLocation()
    {
        Vector3 target = player.transform.position;
        navMeshAgent.SetDestination(player.transform.position);
    }
}
