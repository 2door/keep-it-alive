using UnityEngine;
using UnityEngine.UI;

public class ComputerHealthController : MonoBehaviour {
    
    public int maxHealth;
    public int enemyAttackDamage;
    public GameObject healthBar;
    public GameEvent computerAttackEvent;
    public GameEvent gameOverEvent;

    private int hp;
    private GameEventListener computerAttackListener;

    void Start() {
        hp = maxHealth;
        healthBar.GetComponent<Slider>().value = hp;

        GameEventListener computerAttackListener = (GameEventListener) ScriptableObject.CreateInstance("GameEventListener");
        computerAttackListener.SetupListener(computerAttackEvent, TakeDamage);
    }

    private void TakeDamage() {
        hp -= enemyAttackDamage;
        if (hp <= 0) {
            // TODO Explode computer
            Debug.Log("Computer Dead");
            gameOverEvent.Raise();
        }

        healthBar.GetComponent<Slider>().value = hp;
    }
}
