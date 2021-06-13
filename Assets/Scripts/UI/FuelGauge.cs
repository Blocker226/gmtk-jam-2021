using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    [SerializeField]
    float sliderSpeed = 1;
    [SerializeField]
    Player.Player player;
    [SerializeField]
    TextMeshProUGUI emptyText;

    float _maxFuel;
    Slider _slider;
    
    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>();
        
        if (!player)
        {
            player = GameObject.FindWithTag("Player").GetComponent<Player.Player>();
        }
        _slider.value = player.fuel;
        _maxFuel = player.fuel;
    }

    void LateUpdate()
    {
        _slider.value = Mathf.MoveTowards(
            _slider.value, 
            player.fuel, 
            sliderSpeed * Time.deltaTime);

        if (player.fuel < 0.25f)
        {
            emptyText.text = "EMPTY";
            emptyText.gameObject.SetActive(true);
        }
        else if (player.fuel < _maxFuel / 2)
        {
            emptyText.text = "LOW FUEL";
            emptyText.gameObject.SetActive(true);
        }
        else if (emptyText.gameObject.activeSelf)
        {
            emptyText.gameObject.SetActive(false);
        }
    }
}
