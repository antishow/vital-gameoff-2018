﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
    public int PointValue;
    public GameObject CollectionEffect;

    public delegate void CollectedEvent();
    public event CollectedEvent OnCollect;

    private void OnTriggerEnter(Collider other){
        if (other.tag == "Player" && OnCollect != null) {
            Trigger();
        }
    }

    private void Trigger()
    {
        GameController.GetPoints(PointValue);
        GameObject effect = Instantiate(CollectionEffect, transform.position, Quaternion.identity, transform.parent);
        gameObject.SetActive(false);
        Destroy(effect, 1.0f);
        Destroy(this.gameObject);
        OnCollect();
    }
}
