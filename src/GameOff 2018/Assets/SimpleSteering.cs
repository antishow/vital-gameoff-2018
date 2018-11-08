using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSteering : MonoBehaviour {
    private Vector3 Velocity;

    private void Update() {
        //Decay the velocity so we'll slow to a stop if not doing anything
        Velocity *= 0.4f;

        //Get the input, note turn is proportional with throttle so we don't 
        //turn unless moving and will turn in the opposite direction if reversing
        float throttle = Input.GetAxis("Vertical");
        float turn = throttle * Input.GetAxis("Horizontal");

        //Add the turn
        Vector3 rot = transform.rotation.eulerAngles;
        rot.y += turn;
        transform.rotation = Quaternion.Euler(rot);

        //Move forward
        Velocity += (transform.forward* throttle);
        Velocity *= Time.deltaTime;
        transform.position += Velocity;

    }
}
