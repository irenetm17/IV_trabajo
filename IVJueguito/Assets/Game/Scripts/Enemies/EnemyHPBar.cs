using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{

    private Slider healthSlider;
    private float hpVal;
    private Enemy enemy;
    private bool initialized;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        healthSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null) return;

        if (!initialized)
        {
            if (enemy.flyweightData != null)
            {
                healthSlider.maxValue = enemy.flyweightData.maxHP;
                initialized = true;
            }
        }
        else
        {
            healthSlider.value = enemy.currentHp;
        }
    }
}
