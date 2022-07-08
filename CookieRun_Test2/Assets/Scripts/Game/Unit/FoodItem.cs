using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : UnitBase
{
    public int ID = 0;

    CapsuleCollider2D capsule;

    public float groundSpeed = 0f;

    Vector3 targetPos;
    bool isSuction;

    public float GetHP()
    {
        return hp;
    }

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        if (isSuction == true)
        {
            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 3f);
        }
        else
        {
            MoveItems();
        }
    }

    void MoveItems()
    {
        Vector3 vector = transform.position;
        vector.x -= groundSpeed;
        this.transform.position = vector;
        
        if (Camera.main.WorldToScreenPoint(transform.position).x < 0)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))
        {
            isSuction = true;
            targetPos = collision.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Magnet"))
        {
            isSuction = false;
        }
    }
}
