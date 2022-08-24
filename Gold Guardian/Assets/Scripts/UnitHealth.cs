using UnityEngine;

public class UnitHealth : MonoBehaviour {

    public int health;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Damage(int damage) {
        health -= damage;
        if (health <= damage) {
            Destroy(gameObject);
        }
    }
}