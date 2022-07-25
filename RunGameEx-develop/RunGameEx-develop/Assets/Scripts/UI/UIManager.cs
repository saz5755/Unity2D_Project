using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private GameManager gm;

    public Text txtScore;

    [SerializeField]
    private GameObject gameOverPanel;

    private void Awake()
    {
        gm = GameData.Instance.GetGameManagerCompornent();
    }

    public void OnGameOver(bool bOn)
    {
        if (gameOverPanel == null)
            return;

        gameOverPanel.SetActive(bOn);
    }

    public void OnClickGiveup()
    {
        gm.IsGameOver = true;
        SceneManager.LoadScene("MainScene");
    }
}
