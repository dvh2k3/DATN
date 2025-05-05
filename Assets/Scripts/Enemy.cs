using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public Animator enemyanim;
    public int maxHeath = 100;
    int currentHealth;
    public int EXPToGive;

    [Header("Loot table")]
    public GameObject item1Drop;
    public float item1DropChance;
    public GameObject item2Drop;
    public float item2DropChance;

    private EnemyController parentEnemy;

    [Header("Health UI")]
    public Slider healthSlider;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHeath;
        UpdateHealth();

        parentEnemy = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //play hurt animation
        enemyanim.SetTrigger("Hurt");
        if(currentHealth <= 0)
        {
            parentEnemy.isDead = true;
            Die();
            AudioController.instance.PlayEnemyDeathSFX(3);
        }
    }
    void Die()
    {
        Debug.Log("Enemy Die");
        //animation die and destroy it
        enemyanim.SetBool("IsDead", true);
        //disable enemy
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Player.instance.Levelup(EXPToGive);
        Destroy(gameObject, 1);
        
        if(Random.Range(0f, 100f) < item1DropChance)
        {
            Instantiate(item1Drop, transform.position, transform.rotation);
        }

        if (Random.Range(0f, 100f) < item2DropChance)
        {
            Instantiate(item2Drop, transform.position, transform.rotation);
        }
    }

    public void UpdateHealth()
    {
        healthSlider.maxValue = maxHeath;
        healthSlider.value = currentHealth;
        
    }
}
