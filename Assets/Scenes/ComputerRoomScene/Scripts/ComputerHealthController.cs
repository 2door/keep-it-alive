using UnityEngine;
using UnityEngine.UI;

public class ComputerHealthController : MonoBehaviour {
    
    public int maxHealth;
    public int enemyAttackDamage;
    public GameObject healthBar;
    public GameEvent computerAttackEvent;

    private int hp;
    private GameEventListener computerAttackListener;

    void Start() {
        hp = maxHealth;
        healthBar.GetComponent<Slider>().value = hp;

        computerAttackListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        computerAttackListener.SetupListener(computerAttackEvent, TakeDamage);
    }

    private void TakeDamage() {
        hp -= enemyAttackDamage;
        if (hp <= 0) {
            // TODO Issue game over event
            Debug.Log("Computer Dead");
        }

        healthBar.GetComponent<Slider>().value = hp;
    }
}
