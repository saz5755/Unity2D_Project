using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private int jumpCount = 0;
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
                    Debug.Log("점프");
                    rigid.AddForce(Vector2.up * 4, ForceMode2D.Impulse);
                    jumpCount--;
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
        Vector2 pos = new Vector2(capsule.bounds.center.x, capsule.bounds.center.y - capsule.bounds.extents.y);
        RaycastHit2D raycast = Physics2D.Raycast(pos, Vector2.down, 0.001f);

        if (raycast.collider != null)   
        {
            if (raycast.collider.tag == "Tile") //레이에 닿은게 tile 
            {            
                animator.SetBool("isAir", false);   
                return true;
            }
        }
        
        animator.SetBool("isAir", true);

        return false;
    }

}
