using System;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;


    void Start()
    {
        curValue = startValue;
    }


    void Update()
    {
        //ui 업데이트
        uiBar.fillAmount = GetPercentage();
        Debug.Log(uiBar.fillAmount);
    }

    private float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float value)
    {
        curValue = Math.Min(curValue + value, maxValue);
    }

    public void Subtract(float value)
    {
        curValue = Math.Min(curValue - value, 0);
    }

}
