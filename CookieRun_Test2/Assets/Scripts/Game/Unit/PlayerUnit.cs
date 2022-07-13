using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnit : UnitBase
{
    private int jumpCount = 0;
    [SerializeField]
    private int jumpmaxCount = 2;
    [SerializeField]
    private float speed = 0.04f;
    private bool isJump = false;

    private CapsuleCollider2D capsule;
    private Animator animator;
    private Rigidbody2D rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
        unitType = UnitType.Player;

    }

    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver == true)
        {
            animator.SetTrigger("SetDie");
            return;
        }

        Play();
        
        EatFood();
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
                    Debug.Log("มกวม" + jumpCount);
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
                return true;
            }
        }

        animator.SetBool("isAir", true);

        return false;
    }

    public void EatFood()
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
                            GameManager gm = GetComponent<GameManager>();

                            if (gm == null)
                            {
                                gm = GameObject.Find("GameManager").GetComponent<GameManager>();
                            }

                            if (gm != null)
                            {
                                int curScore = int.Parse(gm.um.txtScore.text);
                                curScore += ((CoinItem)unit).scorePoint;
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
}
