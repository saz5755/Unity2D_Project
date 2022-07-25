using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    /*private float timer;
    private float delay = 5f;*/
    public int ID = 0;

    PolygonCollider2D polygon;

    public float groundSpeed = 0f;
    public Camera mainCamera;
    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        polygon = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        MoveEnermy();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.transform.position);

            rigid.velocity = new Vector3(-collision.transform.position.x * 2.5f, -collision.transform.position.y * 2f);
        }
    }
    public void MoveEnermy()
    {
        Vector3 vector = transform.position;
        vector.x -= 0.011f;
        this.transform.position = vector;

        if (Camera.main.WorldToScreenPoint(transform.position).x < 0)
            Destroy(gameObject); 
        if (Camera.main.WorldToScreenPoint(transform.position).x > Screen.width + 20f)
            Destroy(gameObject);

        if (Camera.main.WorldToScreenPoint(transform.position).y > Screen.height + 20f)
            Destroy(gameObject);
    }
/*
    public void SpawnEnermy()
    {
        timer += Time.deltaTime;
        if (timer >= delay)
        {
            timer -= delay;
            Vector3 SpawnPosition = mainCamera.transform.position;
            string itemName = "Enermy";
            GameObject go = Resources.Load<GameObject>("Unit/Enermy/Prefab/" + itemName);


            SpawnPosition = new Vector3(8f, transform.position.y + Random.Range(0f, 2f), 1f);

           // FoodItem foodItem = Instantiate(go, SpownPosition, Quaternion.identity).GetComponent<FoodItem>();



            foodItem.groundSpeed = map.groundSpeed;
            foodItem.ID = id;
            foodItems.Add(foodItem);
        }
    }*/
}
