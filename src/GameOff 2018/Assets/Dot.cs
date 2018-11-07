﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collectible))]
public class Dot : MonoBehaviour {
    private Collectible collectible;

    private void Awake() {
        collectible = GetComponent<Collectible>();
        collectible.OnCollect += OnCollect;
    }

    void OnCollect() {
        Debug.Log("MONCH");
    }
}