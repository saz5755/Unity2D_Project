using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StObjectBase : MonoBehaviour
{
    public virtual void Initialize() { }
}

public class TObjSingleton<T> : StObjectBase where T : StObjectBase
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go;

                go = GameObject.Find(typeof(T).Name);

                if (go == null)
                {
                    go = new GameObject(typeof(T).Name);
                    _instance = go.AddComponent<T>();
                    _instance.Initialize();

                    DontDestroyOnLoad(go);
                }
                else
                {
                    _instance = go.GetComponent<T>();
                }
            }

            return _instance;
        }
    }
}
