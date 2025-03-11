using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsUI : MonoBehaviour
{
    [SerializeField] Slider statusSlider;
    public Gradient gradient;
    public Image fill;

    public void SetStatus(float f)
    {
        f = Mathf.Clamp(f, 0f, 100f);
        statusSlider.value = f;
        float gradientValue = f / 100;
        fill.color = gradient.Evaluate(gradientValue);
    }
}
