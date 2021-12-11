using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject PlayerObject;  // �÷��̾� ������Ʈ�� ���� ������ ����

    public float jumpForce = 500f; // ���� ��

    public CapsuleCollider2D playerRun; // �⺻ �ݶ��̴�
    public BoxCollider2D playerSlide; // �����̴�, ��� ���� �ݶ��̴�

    private int jumpCount = 0; // ���� ���� Ƚ��
    private bool isDead = false; // ��� ����
    private bool isSlide = false; // �����̵� ����
    private Rigidbody2D playerRigidbody; // ����� ������ٵ� ������Ʈ
    private Animator playerAnimator; // ����� �ִϸ����� ������Ʈ
    private SpriteRenderer spriteRenderer; // ĳ���� ������, ������ �����ϱ� ���� ����

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = PlayerObject.GetComponent<SpriteRenderer>();

        playerSlide.enabled = false; // �⺻ �ݶ��̴��� Ȱ��ȭ
    }

    void Update()
    {
        // ���� ���� ���°� �ƴϸ� Ű �Է��� ���� �ʵ��� ��
        if (!GameManager.instance.isGameStart)
        {
            return;
        }
        
        // �÷��̾� ĳ���Ͱ� ������ �޼��� ���� ����
        if (isDead)
        {
            return;
        }

        // ������ �Ͻ����� �����̸� Ű �Է��� ���� �ʵ��� ��
        if (GameManager.instance.isPause)
        {
            return;
        }
        
        // ���� (���� ����Ű, Space �� ���)
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < 2 && !isSlide)
        {
            jumpCount++;
            playerAnimator.SetInteger("Jump", jumpCount);
            playerAnimator.SetBool("Run", false);

            playerRigidbody.velocity = Vector2.zero; // new Vector2(0, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            SoundManager.instance.PlayJumpSound();
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2 && !isSlide)
        {
            jumpCount++;
            playerAnimator.SetInteger("Jump", jumpCount);
            playerAnimator.SetBool("Run", false);

            playerRigidbody.velocity = Vector2.zero; // new Vector2(0, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            SoundManager.instance.PlayJumpSound();
        }

        /*
        else if (Input.GetKeyDown(KeyCode.UpArrow) && playerRigidbody.velocity.y > 0)
        {
            playerRigidbody.velocity = playerRigidbody.velocity * 0.5f;
        }
        */

        // �����̵� (�Ʒ��� ����Ű, ���� Shift Ű ���)
        if (Input.GetKeyDown(KeyCode.DownArrow) && jumpCount == 0)
        {
            playerAnimator.SetBool("Slide", true);
            isSlide = true;

            playerSlide.enabled = true;
            playerRun.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && jumpCount == 0)
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

        if (Input.GetKeyUp(KeyCode.LeftShift))
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
        SoundManager.instance.PlayDeathSound();

        playerAnimator.SetTrigger("Die"); 
        playerSlide.enabled = true;
        playerRun.enabled = false;

        playerRigidbody.velocity = Vector3.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // ��ֹ��� �ε����� HP�� 0.1 ����
        if ((other.tag == "Obstacle" || other.tag == "Person") && !isDead)
        {
            SoundManager.instance.PlayHitSound();
            HpBar.HP -= 100f;
            OnDamaged();  // �������� ����
        }

        // ��ġ��(���ƿ��� ��)�� �ε����� HP�� 150 ����
        if (other.tag == "BeachBall" && !isDead)
        {
            SoundManager.instance.PlayHitSound();
            HpBar.HP -= 150f;
            OnDamaged();  // �������� ����
        }
        
        // �����ϸ� ��� �޼ҵ� ����
        if (other.tag == "Dead" && !isDead)
        {
            Die();
        }
    }
    
    // ���� ī��Ʈ ���� (1�� ����, 2�� ���� ����)
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.7f)
        {
            jumpCount = 0;
            playerAnimator.SetInteger("Jump", jumpCount);
            playerAnimator.SetBool("Run", true);
        }
    }

    void OnDamaged() // �ǰݽ�
    {
        gameObject.layer = 8;
        spriteRenderer.material.color = new Color(255.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f, 200.0f / 255.0f);  // ���� �����

        Invoke("DamagedOff", 1.0f);
    }

    void DamagedOff() // �������� Ǯ��
    {
        if (Syringe.isGetSyringe == true)
        {
            Debug.Log("PlayerControllerDamagedoff�޼ҵ�");
            return;
        }
        else
        {
            gameObject.layer = 20; // �÷��̾� ���̾��
            spriteRenderer.material.color = Color.white;
        }
    }
}