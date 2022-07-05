using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TImerScript : MonoBehaviour
{
    //[SerializeField]
     Image timerBar;

    public float maxTimer = 5f;
    float timeLeft;
    public GameObject timeUpText;


    private void Awake()
    {
        EventManager.AddData("CurrentTimerValue", (p) => timerBar.fillAmount);
    }
    private void Start()
    {
        timeUpText.SetActive(false);
        timerBar = GetComponent<Image>();
        timeLeft = maxTimer;
    }

  
    

    private void Update()
    {
        if(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTimer;
        }
        else
        {   //프로그레스바 0되면이제 팝업창 괜찮은거 띄워놓고 다시하기 할지 말지 띄우기~

            timeUpText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
