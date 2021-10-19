using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SoundManager soundManager; // ����� Ŭ��
    public GameObject PlayerObject;  //�÷��̾� ������Ʈ�� ���� ������ ����

    public float jumpForce = 500f; // ���� ��

    public BoxCollider2D playerRun; // �⺻ �ݶ��̴�
    public BoxCollider2D playerSlide; // �����̴�, ��� ���� �ݶ��̴�

    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isDead = false; // ��� ����
    private bool isSlide = false; // �����̵� ����
    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator playerAnimator; // ����� �ִϸ����� ������Ʈ
    SpriteRenderer spriteRenderer; //ĳ���� ������, ������ �����ϱ� ���� ����

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = PlayerObject.GetComponent<SpriteRenderer>();

        playerSlide.enabled = false; // �⺻ �ݶ��̴��� Ȱ��ȭ
    }

    void Update()
    {
        // �÷��̾� ĳ���Ͱ� ������ �޼��� ���� ����
        if (isDead)
        {
            return;
        }

        // ������ �Ͻ����� �����̸� �޼��� ���� ���� (Ű �Է� ����)
        if (GameManager.instance.isPause)
        {
            return;
        }

        // ����
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2 && !isSlide)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero; // new Vector2(0, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            soundManager.PlayJumpSound();

            playerAnimator.SetInteger("Jump", jumpCount);
            playerAnimator.SetBool("Run", false);
        }

        /*
        else if (Input.GetKeyDown(KeyCode.UpArrow) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        */

        // �����̵�
        if (Input.GetKeyDown(KeyCode.DownArrow) && jumpCount == 0)
        {
            playerAnimator.SetBool("Slide", true);
            isSlide = true;

            playerSlide.enabled = true;
            playerRun.enabled = false;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            playerAnimator.SetBool("Slide", false);
            isSlide = false;

            playerRun.enabled = true;
            playerSlide.enabled = false;
        }
    }

    // �÷��̾� ĳ���Ͱ� ������ ����
    public void Die()
    {
        playerAnimator.SetTrigger("Die"); // ��� ó��
        playerSlide.enabled = true;
        playerRun.enabled = false;

        playerRigidbody.velocity = Vector3.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��ֹ��� �ε�����
        if (other.tag == "Obstacle" && !isDead)
        {
            //playerAnimator.SetBool("Hit", true);

            HpBar.Hp -= 0.1f; //ü�� 0.1����
            OnDamaged();  //�������� ����
            
        }

        // �����ϸ� ����
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
            playerAnimator.SetInteger("Jump", jumpCount);
            playerAnimator.SetBool("Run", true);
        }
    }

    void OnDamaged() //�ǰݽ�
     { 
        gameObject.layer = 8;
        spriteRenderer.material.color = new Color(255.0f/255.0f, 200.0f/255.0f, 200.0f / 255.0f, 200.0f / 255.0f);
        //���� �����

        Invoke("DamagedOff", 1.0f);
    }

    void DamagedOff() //�������� Ǯ��
    {
        gameObject.layer = 20; //�÷��̾� ���̾��
        spriteRenderer.material.color = Color.white;
    }
}
