using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public int PointValue;
    public GameObject CollectionEffect;

    public delegate void CollectedEvent();
    public event CollectedEvent OnCollect;

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player" && OnCollect != null) {
            gameObject.SetActive(false);
            GameObject effect = Instantiate(CollectionEffect, transform.position, Quaternion.identity, transform.parent);
            OnCollect();
            Destroy(effect, 3.0f);
            Destroy(this, 3.1f);
        }
    }
}
