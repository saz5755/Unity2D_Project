using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImerScript : MonoBehaviour
{
    //[SerializeField]
    Image timerBar;
    // public GameObject timeUpText;
    private void Awake()
    {
    //EventManager.AddData("CurrentTimerValue", (p) => timerBar.fillAmount);

    }
    private void Start()
    {
        //timeUpText.SetActive(false);
        timerBar = GetComponent<Image>();
    }

    public void RefreshHp(float value)
    {
        timerBar.fillAmount = value;

    }
}
