using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public bool isInvulnerable = false;
    public Animator enemyanim;
    public int maxHeath = 100;
    public int currentHealth;
    public int EXPToGive;

    [Header("Loot table")]
    public GameObject item1Drop;
    public float item1DropChance;
    public GameObject item2Drop;
    public float item2DropChance;

    [Header("Health UI")]
    public GameObject BossHPUI;
    public Slider bossHPSlider;
    public Sprite bossImage;
    public Sprite bossEnrageImage;
    public Image bossIconUI;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHeath;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
    }
    public void TakeDamage(int damage)
    {
        //Boss will not get any damage
        if (isInvulnerable)
            return;
        currentHealth -= damage;
        BossHPUI.SetActive(true);
        if(currentHealth <= maxHeath/2)
        {
            GetComponent<Animator>().SetBool("Enrage", true);
        }

        //play hurt animation
        //enemyanim.SetTrigger("Hurt");
        AudioController.instance.PlayEnemyDeathSFX(3);
        if (currentHealth <= 0)
        {
            
            Die();
            
        }
    }

    void Die()
    {
        enemyanim.SetBool("IsDead", true);
        Destroy(gameObject, 2f);
        BossHPUI.SetActive(false);
    }
    public void UpdateHealth()
    {
        bossHPSlider.maxValue = maxHeath;
        bossHPSlider.value = currentHealth;
        if (currentHealth <= maxHeath / 2)
        {
            bossIconUI.sprite = bossEnrageImage;
        }
        else
        {
            bossIconUI.sprite = bossImage;
        }
    }
}
