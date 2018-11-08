using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collectible))]
public class Dot : MonoBehaviour {
    private Collectible collectible;
    private Animator animator;

    private void Awake() {
        collectible = GetComponent<Collectible>();
        collectible.OnCollect += OnCollect;

        animator = GetComponent<Animator>();
        float wave = 4.0f;
        int index = transform.GetSiblingIndex();
        float offset = Mathf.Repeat(index, wave) / wave;

        animator.Play("Dot", 0, offset);
    }

    void OnCollect() {
        GameController.EatDot();
    }
}
