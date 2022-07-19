using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameManager gm;

    public UIProgress hpUI;

    public Text txtScore;

    // Update is called once per frame
    void Update()
    {
        if (gm.IsGameOver == true)
        {
            return;
        }
    }
}
