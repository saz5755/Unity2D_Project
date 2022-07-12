using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bgLayer;
    public GameObject unitLayer;

    public UIManager um;

    public bool IsGameOver = false;

    public Camera mainCamera;

    private PlayerUnit player;

    int maxItems = 100;
    List<FoodItem> foodItems = new List<FoodItem>();

    List<CoinItem> coinItems = new List<CoinItem>();

    public Camera GetMainCamera()
    {
        if (mainCamera != null)
        {
            return mainCamera;
        }
        else
        {
            return GameObject.Find("MainCamera").GetComponent<Camera>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (bgLayer != null)
        {
            string strMapName = "Map";
            GameObject go = Resources.Load<GameObject>("Map/Prefab/" + strMapName);
            Map map = Instantiate(go, bgLayer.transform).GetComponent<Map>();
            map.mainCam = GetMainCamera().transform;
        }

        if (unitLayer != null)
        {
            string playerName = "Player";
            GameObject go = Resources.Load<GameObject>("Unit/hero/Prefab/" + playerName);
            Transform StartTransform = unitLayer.transform;
            player = Instantiate(go, StartTransform).GetComponent<PlayerUnit>();

            bgLayer.transform.GetChild(0).GetComponent<Map>().groundSpeed = player.GetPlayerSpeed();
        }
        ClearItems();

        if (um != null)
        {
            um.txtScore.text = "0";
        }
    }

    private float timer;
    private float delay = 5f;

    private float timerCoin;
    private float delayCoin = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver == true)
            return;

        timer += Time.deltaTime;
        timerCoin += Time.deltaTime;

        if (timer >= delay)
        {
            timer = 0;
            timerCoin = 0;
            string itemName = "Food";
            GameObject go = Resources.Load<GameObject>("Unit/Items/Prefab/" + itemName);

            Map map = bgLayer.transform.GetChild(0).GetComponent<Map>();

            Vector3 SpownPosition = mainCamera.transform.position;

            SpownPosition = new Vector3(8f, map.transform.position.y + Random.Range(0f, 1f), 1f);

            int id = foodItems.Count;
            FoodItem foodItem = Instantiate(go, SpownPosition, Quaternion.identity).GetComponent<FoodItem>();
            foodItem.groundSpeed = map.groundSpeed;
            foodItem.ID = id;
            foodItems.Add(foodItem);
        }
        else
        {
            if (timerCoin > delayCoin)
            {
                timerCoin = 0;

                string itemName = "Coin";
                GameObject go = Resources.Load<GameObject>("Unit/Items/Prefab/" + itemName);

                Map map = bgLayer.transform.GetChild(0).GetComponent<Map>();

                Vector3 SpownPosition = mainCamera.transform.position;

                SpownPosition = new Vector3(8f, map.transform.position.y + Random.Range(-2f, 2f), 1f);

                int id = coinItems.Count + 10000;
                CoinItem coinItem = Instantiate(go, SpownPosition, Quaternion.identity).GetComponent<CoinItem>();
                coinItem.groundSpeed = map.groundSpeed;
                coinItem.ID = id;
                coinItems.Add(coinItem);
            }
        }
        

        
    }

    void ClearItems()
    {
        for (int i = 0; i < foodItems.Count; i++)
        {
            if (foodItems[i].gameObject != null)
            {
                Destroy(foodItems[i].gameObject);
            }
        }

        foodItems.Clear();

        for (int i = 0; i < coinItems.Count; i++)
        {
            if (coinItems[i].gameObject != null)
            {
                Destroy(coinItems[i].gameObject);
            }
        }

        coinItems.Clear();
    }

}
