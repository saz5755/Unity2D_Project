using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopBase : MonoBehaviour
{
    protected GameObject parentManager;
    public abstract void OpenUI(GameObject uiManager);
    protected abstract void RefleshUI();

    public void Close()
    {
        if (this.gameObject.activeSelf)
            this.gameObject.SetActive(false);
    }
}
