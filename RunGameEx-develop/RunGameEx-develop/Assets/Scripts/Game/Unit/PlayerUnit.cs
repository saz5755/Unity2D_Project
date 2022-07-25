using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    [SerializeField]
    private string unitName;

    public string UnitName
    {
        get
        {
            return unitName;
        }
    }
    
    [SerializeField]
    private int jumpmaxCount = 2;
    private int jumpCount = 0;
    
    [SerializeField]
    private float speed = 0.04f;
    private bool isJump = false;

    private CapsuleCollider2D capsule;
    private Animator animator;
    private Animator hatAnimator;
    private Rigidbody2D rigid;

    TImerScript timer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameObject go = GameObject.Find("Hat");
        if (go != null)
        {
            hatAnimator = go.GetComponent<Animator>();
        }
        
        rigid = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        unitType = UnitType.Player;
    }

    private void Update()
    {
        if (GameData.Instance.GetGameManagerCompornent().IsGameOver == true || IsLive() == false)
        {
            return;
        }

        Play();
        EatFood();
        HpTimer();
    }

    public void HpTimer()
    {
        if (hp > 0)
        {
            hp -= Time.deltaTime;
        }
        else
        {   //프로그레스바 0되면이제 팝업창 괜찮은거 띄워놓고 다시하기 할지 말지 띄우기~
            hp = 0;
            animator.SetTrigger("SetDie");
            //timeUpText.SetActive(true);
            Time.timeScale = 0;
        }
        timer.RefreshHp(Mathf.Clamp(hp / maxHp, 0f, 1f));

    }

    public bool IsLive()
    {
        return hp > 0;
    }

    public void Play()
    {
        bool isOntheGround = CheckOnTheGround();

        if (isOntheGround)
        {
            isJump = true;
            jumpCount = jumpmaxCount;
            Debug.Log("OntheGround" + string.Format(" JumpCount {0}", jumpCount));
        }
        else
        {
            Debug.Log("isAir" + string.Format(" JumpCount {0}", jumpCount));
        }

        if (isJump)
        {
            if (jumpCount > 0)
            {
                if (Input.GetButtonDown("Jump"))
                {

                    rigid.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                    jumpCount--;
                    Debug.Log("점프" + jumpCount);
                }
            }
            else
            {
                isJump = false;
            }
        }
    }

    bool CheckOnTheGround()
    {
        RaycastHit2D raycast = Physics2D.Raycast(
            capsule.bounds.center,
            Vector2.down,
            capsule.bounds.extents.y + .02f,
            ~(1 << LayerMask.NameToLayer("Player") | 1 << LayerMask.NameToLayer("Magnet")));

        if (raycast.collider != null)
        {
            if (raycast.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                Debug.Log(raycast.collider.gameObject.name);
                animator.SetBool("isAir", false);
                if (hatAnimator != null) hatAnimator.SetBool("isJump", false);
                return true;
            }
        }

        animator.SetBool("isAir", true);
        if (hatAnimator != null) hatAnimator.SetBool("isJump", true);

        return false;
    }

    void EatFood()
    {
        RaycastHit2D raycast = Physics2D.Raycast(
            capsule.bounds.center,
            Vector2.down,
            capsule.bounds.extents.y + .02f,
            (1 << LayerMask.NameToLayer("Item")));

        if (raycast.collider != null)
        {
            UnitBase unit = raycast.collider.gameObject.GetComponent<UnitBase>();

            if (unit != null)
            {
                switch( unit.GetUnitType())
                {
                    case UnitType.Food:
                        {
                            hp += ((FoodItem)unit).GetHP();

                            if (hp > maxHp)
                                hp = maxHp;

                            Destroy(raycast.collider.gameObject);
                            break;
                        }

                    case UnitType.Coin:
                        {
                            GameManager gm = GameData.Instance.GetGameManagerCompornent();

                            if (gm == null)
                            {
                                gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                            }

                            if (gm != null)
                            {
                                int curScore = int.Parse(gm.um.txtScore.text);
                                curScore += ((CoinItem)unit).scorePoint;
                                GameData.Instance.playerScore = curScore;
                                gm.um.txtScore.text = curScore.ToString();
                            }

                            Destroy(raycast.collider.gameObject);
                            break;
                        }
                        
                }

                
            }
        }
    }

    public float GetPlayerSpeed()
    {
        return speed;
    }

    public void Init(TImerScript timer)
    {
        hp = maxHp;
        this.timer = timer;
    }
}
