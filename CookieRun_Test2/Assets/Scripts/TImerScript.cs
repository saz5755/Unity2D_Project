using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImerScript : PlayerUnit
{
    //[SerializeField]
    Image timerBar;

    // public GameObject timeUpText;
    PlayerUnit play;

    private void Awake()
    {
        EventManager.AddData("CurrentTimerValue", (p) => timerBar.fillAmount);
        hp = maxHp;

    }
    private void Start()
    {
        //timeUpText.SetActive(false);
        timerBar = GetComponent<Image>();
    }

    private void Update()
    {
        if (hp > 0)
        {
            hp -= Time.deltaTime;
            timerBar.fillAmount = hp / maxHp;              
        }
        else
        {   //���α׷����� 0�Ǹ����� �˾�â �������� ������� �ٽ��ϱ� ���� ���� ����~

            //timeUpText.SetActive(true);
            Time.timeScale = 0;
        }
    }

    
}
