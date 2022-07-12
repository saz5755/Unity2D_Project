using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : UnitBase
{
    public int ID = 0;

    CapsuleCollider2D capsule;

    public float groundSpeed = 0f;

    public int scorePoint = 10;

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        unitType = UnitType.Coin;
    }

    private void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver == true)
        {
            return;
        }
        MoveItems();
    }


    void MoveItems()
    {
        Vector3 vector = transform.position;
        vector.x -= groundSpeed;
        this.transform.position = vector;

        if (Camera.main.WorldToScreenPoint(transform.position).x < 0)
            Destroy(gameObject);
    }


}
