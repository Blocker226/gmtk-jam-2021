using UnityEngine;
using UnityEngine.UI;

public class FuelGauge : MonoBehaviour
{
    [SerializeField]
    float sliderSpeed = 1;
    [SerializeField]
    Player.Player player;
    [SerializeField]
    GameObject emptyText;

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
    }

    void LateUpdate()
    {
        _slider.value = Mathf.MoveTowards(
            _slider.value, 
            player.fuel, 
            sliderSpeed * Time.deltaTime);

        if (_slider.value < 0.25f)
        {
            emptyText.SetActive(true);
        }
        else if (emptyText.activeSelf)
        {
            emptyText.SetActive(false);
        }
    }
}
