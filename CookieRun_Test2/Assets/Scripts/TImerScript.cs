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
        {   //프로그레스바 0되면이제 팝업창 괜찮은거 띄워놓고 다시하기 할지 말지 띄우기~

            //timeUpText.SetActive(true);
            Time.timeScale = 0;
        }
    }

    
}
