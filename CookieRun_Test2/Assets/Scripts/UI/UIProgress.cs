using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgress : MonoBehaviour
{
    public Image backImage;
    public Image frontImage;

    public void SetValue(float _value)
    {
        if (frontImage != null)
        {
            Vector3 scale = frontImage.rectTransform.localScale;
            scale.x = _value;
            frontImage.rectTransform.localScale = scale;
        }
    }

    private void Awake()
    {
        if (frontImage != null)
        {
            Vector3 scale = frontImage.rectTransform.localScale;
            scale.x = 1f;
            frontImage.rectTransform.localScale = scale;
        }
    }
}
