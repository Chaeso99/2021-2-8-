using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        // ����, ������
        if (other.CompareTag("Player"))
        {
            // ����
            if (gameObject.tag == "Coin")
            {
                SoundManager.instance.PlayCoinSound();
                Debug.Log("Coin Sound Play!");
                GameManager.instance.score += 100;
                Destroy(gameObject);
                //other.gameObject.SetActive(false);
            }

            // �޷�
            else if (gameObject.tag == "Dollar1")
            {
                SoundManager.instance.PlayCoinSound();
                GameManager.instance.score += 300;
                Destroy(gameObject);
            }

            // �޷� ��ġ
            else if (gameObject.tag == "Dollar2")
            {
                SoundManager.instance.PlayCoinSound();
                GameManager.instance.score += 500;
                Destroy(gameObject);
            }

            // ������
            else if (gameObject.tag == "Truncheon")
            {
                SoundManager.instance.PlayItemSound();

                GameObject[] PersonObstacles = GameObject.FindGameObjectsWithTag("Person");
                for (int i = 0; i < PersonObstacles.Length; i++)
                {
                    Destroy(PersonObstacles[i]);
                }

                Destroy(gameObject);
            }

            // ����ũ
            else if (gameObject.tag == "Mask")
            {
                SoundManager.instance.PlayItemSound();
                // ü�� ȸ��
                // HpBar.Hp += 0.1f;
                Destroy(gameObject);
            }

            // �ֻ��
            else if (gameObject.tag == "Syringe")
            {
                SoundManager.instance.PlayItemSound();
                // 3�� ����

                Destroy(gameObject);
            }
        }
    }
}
