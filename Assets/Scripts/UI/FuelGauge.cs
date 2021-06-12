using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    [SerializeField]
    float sliderSpeed = 1;
    [SerializeField]
    Player player;
    [SerializeField]
    GameObject _emptyText;

    Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        _slider.value = player.fuel;
    }

    void LateUpdate()
    {
        _slider.value = Mathf.MoveTowards(
            _slider.value, 
            player.fuel, 
            sliderSpeed * Time.deltaTime);

        if (_slider.value < 0.1f)
        {
            _emptyText.SetActive(true);
        }
    }
}
