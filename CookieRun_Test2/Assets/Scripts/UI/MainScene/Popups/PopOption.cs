using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOption : PopBase
{
    public override void OpenUI(GameObject uiManager)
    {
        parentManager = uiManager;
        RefleshUI();
    }

    protected override void RefleshUI()
    {

    }
}
