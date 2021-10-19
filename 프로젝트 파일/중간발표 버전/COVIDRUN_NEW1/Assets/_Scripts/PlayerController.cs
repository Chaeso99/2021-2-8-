using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SoundManager soundManager; // 오디오 클립
    public GameObject PlayerObject;  //플레이어 오브젝트의 색상 정보를 저장

    public float jumpForce = 500f; // 점프 힘

    public BoxCollider2D playerRun; // 기본 콜라이더
    public BoxCollider2D playerSlide; // 슬라이더, 사망 전용 콜라이더

    private int jumpCount = 0; // 누적 점프 횟수
    private bool isDead = false; // 사망 상태
    private bool isSlide = false; // 슬라이딩 상태
    private Rigidbody2D playerRigidbody; // 사용할 리지드바디 컴포넌트
    private Animator playerAnimator; // 사용할 애니메이터 컴포넌트
    SpriteRenderer spriteRenderer; //캐릭터 무적시, 색상을 변경하기 위한 변수

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = PlayerObject.GetComponent<SpriteRenderer>();

        playerSlide.enabled = false; // 기본 콜라이더만 활성화
    }

    void Update()
    {
        // 플레이어 캐릭터가 죽으면 메서드 실행 종료
        if (isDead)
        {
            return;
        }

        // 게임이 일시정지 상태이면 메서드 실행 종료 (키 입력 방지)
        if (GameManager.instance.isPause)
        {
            return;
        }

        // 점프
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

        // 슬라이딩
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

    // 플레이어 캐릭터가 죽으면 실행
    public void Die()
    {
        playerAnimator.SetTrigger("Die"); // 사망 처리
        playerSlide.enabled = true;
        playerRun.enabled = false;

        playerRigidbody.velocity = Vector3.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 장애물과 부딪히면
        if (other.tag == "Obstacle" && !isDead)
        {
            //playerAnimator.SetBool("Hit", true);

            HpBar.Hp -= 0.1f; //체력 0.1감소
            OnDamaged();  //무적상태 돌입
            
        }

        // 낙사하면 실행
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

    void OnDamaged() //피격시
     { 
        gameObject.layer = 8;
        spriteRenderer.material.color = new Color(255.0f/255.0f, 200.0f/255.0f, 200.0f / 255.0f, 200.0f / 255.0f);
        //빨간 투명색

        Invoke("DamagedOff", 1.0f);
    }

    void DamagedOff() //무적상태 풀림
    {
        gameObject.layer = 20; //플레이어 레이어로
        spriteRenderer.material.color = Color.white;
    }
}
