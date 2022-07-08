using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private int jumpCount = 0;
    [SerializeField]
    private int jumpmaxCount = 2;
    private bool isJump = false;

    private CapsuleCollider2D capsule;
    private Animator animator;
    private Rigidbody2D rigid;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        bool isOntheGround = CheckOnTheGround();

        if (isOntheGround)
        {
            isJump = true; 
            jumpCount = jumpmaxCount;

        }
        else
        {
        }

        if (isJump)
        {
            if (jumpCount > 0)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    Debug.Log("มกวม");
                    rigid.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                    jumpCount--;
                }
            }
            else 
            {
                isJump = false;
            }
        }
        EatFood();
    }

    bool CheckOnTheGround()
    {
        RaycastHit2D raycast = Physics2D.Raycast(
            capsule.bounds.center,
            Vector2.down,
            capsule.bounds.extents.y + .02f,
            ~(1 << LayerMask.NameToLayer("Player")));

        if (raycast.collider != null)   
        {
            animator.SetBool("isAir", false);   
            return true;
        }
        
        animator.SetBool("isAir", true);

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
            FoodItem item = raycast.collider.gameObject.GetComponent<FoodItem>();

            if (item != null)
            {
                hp += item.GetHP();

                Destroy(raycast.collider.gameObject);
            }
        }
    }
}
