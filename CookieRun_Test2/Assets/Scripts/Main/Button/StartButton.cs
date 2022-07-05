using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    private void Awake()
    {
        startButton = GetComponent<Button>();
    }

    private void Start()
    {
        goInGameScene();
    }

    private void InGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void goInGameScene()
    {
        startButton.onClick.AddListener(InGameScene);
        Debug.Log("��ư Ŭ��");
    }
}
