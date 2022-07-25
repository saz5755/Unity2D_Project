using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopOption : PopBase
{
    [SerializeField]
    private InputField txtNewName;
    public override void OpenUI(GameObject uiManager)
    {
        parentManager = uiManager;
        RefleshUI();
    }

    protected override void RefleshUI()
    {

    }

    public void OnClickResetPoint()
    {
        GameData.Instance.playerScore = 0;
        parentManager.GetComponent<MainSceneUIManager>().RefleshUI();
    }

    public void OnClickSetName()
    {
        GameData.Instance.playerName = txtNewName.text;
        parentManager.GetComponent<MainSceneUIManager>().RefleshUI();
    }
}
