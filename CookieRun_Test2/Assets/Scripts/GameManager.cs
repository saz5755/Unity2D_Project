using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bgLayer;
    public GameObject unitLayer;

    public UIManager um;

    public Camera mainCamera;

    private PlayerUnit player;
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
            GameObject go = Resources.Load<GameObject>("Map/Prefab/Map");
            Map map = Instantiate(go, bgLayer.transform).GetComponent<Map>();
            map.mainCam = GetMainCamera().transform;
        }

        if (unitLayer != null)
        {
            GameObject go = Resources.Load<GameObject>("Unit/hero/Prefab/Player");
            player = Instantiate(go, unitLayer.transform).GetComponent<PlayerUnit>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
