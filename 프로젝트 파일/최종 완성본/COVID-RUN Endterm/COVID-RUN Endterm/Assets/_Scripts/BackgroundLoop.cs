using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ������ �̵��� ����� ������ ������ ���ġ�ϴ� ��ũ��Ʈ
public class BackgroundLoop : MonoBehaviour
{
    private float width; // ����� ���� ����

    private void Awake()
    {
        BoxCollider2D backgroundColider = GetComponent<BoxCollider2D>();
        width = backgroundColider.size.x;
        // ���� ���̸� �����ϴ� ó��
    }

    private void Update()
    {
        if (transform.position.x <= -width)
        {
            Reposition();
        }
        // ���� ��ġ�� �������� �������� width �̻� �̵������� ��ġ�� ����
    }

    // ��ġ�� �����ϴ� �޼���
    private void Reposition()
    {
        Vector2 offset = new Vector2 (width * 2f, 0);
        transform.position = (Vector2)transform.position + offset;
    }
}
