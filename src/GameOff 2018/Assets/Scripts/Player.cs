using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public GameObject Body;

    private void Awake() {
        Respawner respawner = GetComponent<Respawner>();
        respawner.OnRespawn += OnRespawn;
    }

    void OnRespawn() {
        Debug.Log("BACK TO LIFE!");

        Body.transform.localPosition = new Vector3(0, -0.572f, 0);
        Body.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

        GetComponent<Rigidbody>().isKinematic = false;
    }

    void TriggerRespawn() {
        Debug.Log("WISE FWOM YOUW GWAVE");
        GameController.ResetActors();
    }

    public void Die() {
        GetComponent<Rigidbody>().isKinematic = true;
        Body.transform.localPosition = new Vector3(Body.transform.position.x, 0.015f, Body.transform.position.z);
        Body.transform.localScale = new Vector3(Body.transform.localScale.x, 0.03f, Body.transform.localScale.z);
        Invoke("TriggerRespawn", 2.0f);
    }
}
