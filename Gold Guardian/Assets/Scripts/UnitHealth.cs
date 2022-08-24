using UnityEngine;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour {
    public int startHealth;

    private int curHealth;
    private UnitType unitType;
    private Slider healthSlider;

    // Start is called before the first frame update
    void Start() {
        curHealth = startHealth;
        unitType = GetComponent<UnitType>();
        healthSlider = GetComponentInChildren<Slider>();
        healthSlider.maxValue = startHealth;
        healthSlider.value = startHealth;
        Image sliderBackground = healthSlider.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        if (unitType.type == UnitType.Type.enemy) {
            sliderBackground.color = Color.red;
        } else {
            sliderBackground.color = Color.green;
        }
    }

    public void Damage(int damage) {
        curHealth -= damage;
        healthSlider.maxValue = curHealth;
        if (curHealth <= 0) {
            curHealth = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(int health) {
        curHealth += health;
        healthSlider.maxValue = curHealth;
        if (curHealth > startHealth) {
            curHealth = startHealth;
        }
    }
}