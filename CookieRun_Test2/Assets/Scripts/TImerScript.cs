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
        {   //���α׷����� 0�Ǹ����� �˾�â �������� ������� �ٽ��ϱ� ���� ���� ����~

            timeUpText.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
