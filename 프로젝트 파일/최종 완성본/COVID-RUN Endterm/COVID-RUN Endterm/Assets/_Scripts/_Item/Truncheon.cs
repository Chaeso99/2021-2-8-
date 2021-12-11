using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truncheon : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlayItemSound();

            GameObject[] PersonObstacles = GameObject.FindGameObjectsWithTag("Person");
            for (int i = 0; i < PersonObstacles.Length; i++)
            {
                Destroy(PersonObstacles[i]);
            }

            Destroy(gameObject);
        }
    }
}
