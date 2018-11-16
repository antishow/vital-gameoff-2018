using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotContainer : MonoBehaviour {

    public GameObject DotPrefab;
    public LayerMask World;

    public int DotGridWidth = 35;
    public int DotGridHeight = 35;

    // Use this for initialization
    void Start () {
        GenerateDots();
	}

    private void GenerateDots()
    {
        int row, col;
        float x, y;

        GameObject DotContainer = GameObject.Find("Dots");

        for (col = 0; col < DotGridWidth; col++)
        {
            for (row = 0; row < DotGridHeight; row++)
            {
                x = col - Mathf.Ceil(DotGridWidth / 2);
                y = row - Mathf.Ceil(DotGridHeight / 2);

                Vector3 pos = new Vector3(x, 0.3f, y);
                GameObject dot = Instantiate(DotPrefab, pos, Quaternion.identity, transform) as GameObject;
                if (Physics.CheckSphere(pos, 0.3f, World)) {
                    Destroy(dot);
                }
            }
        }
    }
}
