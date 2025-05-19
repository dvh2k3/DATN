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

    public GameObject deadFX;
    public GameObject ExitLevel;
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
            ExitLevel.SetActive(true);
        }
    }

    void Die()
    {
        enemyanim.SetBool("IsDead", true);
        StartCoroutine(BossDieBehavior());
        Destroy(gameObject, 5f);
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

    [Header("Die Effect")]
    public GameObject dieExplosionFX;
    public Vector2 dieExplosionSize = new Vector2(2, 3);

    IEnumerator BossDieBehavior()
    {
        yield return new WaitForSeconds(1f);

        for(int i=0; i<4; i++)
        {
            Instantiate(dieExplosionFX, transform.position + new Vector3(Random.Range(-dieExplosionSize.x, dieExplosionSize.x), Random.Range(0, dieExplosionSize.y), 0), Quaternion.identity);
            AudioController.instance.PlayPlayerSFX(0);
            yield return new WaitForSeconds(0.5f);
        }

        for(int i=0; i<5; i++)
        {
            Instantiate(dieExplosionFX, transform.position + new Vector3(Random.Range(-dieExplosionSize.x, dieExplosionSize.x), Random.Range(0, dieExplosionSize.y), 0), Quaternion.identity);
            AudioController.instance.PlayPlayerSFX(0);
            yield return new WaitForSeconds(0.5f);
        }
        gameObject.SetActive(false);
    }
}
