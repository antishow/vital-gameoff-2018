using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour {
    private Vector3 position;
    private Quaternion rotation;

    public delegate void RespawnedEvent();
    public event RespawnedEvent OnRespawn;

    void Awake() {
        position = transform.position;
        rotation = transform.rotation;
    }

    public void Respawn() {
        Debug.LogFormat("Resetting position and rotation for {0}", name);

        transform.position = position;
        transform.rotation = rotation;

        if(OnRespawn != null) {
            OnRespawn();
        }
    }
}
