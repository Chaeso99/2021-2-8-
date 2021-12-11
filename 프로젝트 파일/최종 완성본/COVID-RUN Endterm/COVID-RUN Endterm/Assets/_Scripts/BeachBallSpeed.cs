using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeachBallSpeed : MonoBehaviour
{
    private float BeachBallScrollSpeed = -5.5f;

    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.Translate(Time.deltaTime * BeachBallScrollSpeed, 0, 0);
        }
    }
}
