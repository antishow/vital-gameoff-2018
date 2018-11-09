using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSteering : MonoBehaviour {
    private Rigidbody rb;

    [Range(0, 1.0f)]
    public float Decay = 0.6f;
    public float Speed = 10.0f;
    public float TurnSpeed = 4.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        //Get the input, note turn is proportional with throttle so we don't 
        //turn unless moving and will turn in the opposite direction if reversing
        float throttle = Input.GetAxis("Vertical");
        float turn = throttle * Input.GetAxis("Horizontal");

        //Add the turn
        if (System.Math.Abs(turn) > 0.01f) {
            Vector3 rot = transform.rotation.eulerAngles;
            rot.y += turn * TurnSpeed;
            rb.MoveRotation(Quaternion.Euler(rot));
        }

        rb.AddForce(transform.forward * throttle * Speed, ForceMode.Acceleration);
    }
}
