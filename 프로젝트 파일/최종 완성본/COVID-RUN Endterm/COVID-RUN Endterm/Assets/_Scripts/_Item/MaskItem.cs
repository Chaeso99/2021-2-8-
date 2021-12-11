using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.instance.PlayItemSound();
            HpBar.HP += 200.0f;
            Destroy(gameObject);
        }
    }
}