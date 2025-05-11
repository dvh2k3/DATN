using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public static Player instance;

    public bool rageMode;

    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth;
    public float healthRegenSpeed;

    [Header("Health UI")]
    public Slider healthSlider;
    public TMP_Text healthText;

    [Header("Magic")]
    public float maxmagic = 100;
    public float currentmagic;
    public float magicRegenSpeed;
    public float bulletMagicCost;

    [Header("Magic UI")]
    public Slider magicSlider;
    public TMP_Text magicText;

    [Header("Rage")]
    public float maxRage;
    public float currentRage;
    public float rageRegenSpeed;
    public float derageAmount;

    [Header("Rage UI")]
    public Slider rageSlider;
    public TMP_Text rageText;

    [Header("EXP UI")]
    public Slider currentXpSlider;
    public TMP_Text levelText;

    public bool isDead;

    [Header("Movement")]
    public float runSpeed;
    public Animator playerAnimator;
    private Rigidbody2D playerRigidbody;

    [Header("Jump")]
    public float jumpForce;
    public Transform groundcheck;
    private bool isGrounded;
    private bool doDoubleJump;
    public LayerMask groundFloor;
    public GameObject jumpEffect;

    [Header("Attack")]
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public LayerMask BossLayer;
    public int attackDamage = 20;
    public float attackRate = 2f;
    public float nextAttackTime = 0f;

    [Header("Range Attack")]
    public PlayerBullet bullet;
    public Transform shootingPoint;

    [Header("Level up System")]
    public int playerLevel = 1;
    public int[] expToNextLevel;
    public int maxLevel = 5;
    public int currentEXP;
    public int baseEXP = 500;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerLevel = SaveManager.instance.activeSave.playerLevel;
        expToNextLevel = SaveManager.instance.activeSave.expToNextLevel;
        maxLevel = SaveManager.instance.activeSave.maxLevel;
        currentEXP = SaveManager.instance.activeSave.currentEXP;
        baseEXP = SaveManager.instance.activeSave.baseEXP;

        attackDamage = SaveManager.instance.activeSave.attackDamage;
        healthRegenSpeed = SaveManager.instance.activeSave.healthRegenSpeed;
        magicRegenSpeed = SaveManager.instance.activeSave.magicRegenSpeed;
        maxHealth = SaveManager.instance.activeSave.maxHealth;
        maxmagic = SaveManager.instance.activeSave.maxMagic;
        UpdateHealth();
        UpdateMagic();
        currentHealth = maxHealth;
        currentmagic = maxmagic;
        currentRage = maxRage;
        //Get component
        playerRigidbody = GetComponent<Rigidbody2D>();

        expToNextLevel = new int[maxLevel];
        expToNextLevel[1] = baseEXP;

        for(int i = 2; i<expToNextLevel.Length; i++)
        {
            expToNextLevel[i] = Mathf.FloorToInt(expToNextLevel[i-1] * 1.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        healthRegen();
        RegenMagic();
        RegenRage();
        UpdateHealth();
        UpdateMagic();
        UpdateLevel();
        UpdateRage();

        if (rageMode)
        {
            Derage();
        }
        if (!isDead && !DialogManager.Instance.dialogPanel.activeInHierarchy)
        {


            //player movement
            playerRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * runSpeed, playerRigidbody.velocity.y);

            //Flipping the Player
            if (playerRigidbody.velocity.x > 0)
            {

                transform.localScale = new Vector3(2, 2, 2);
            }
            else if (playerRigidbody.velocity.x < 0)
            {

                transform.localScale = new Vector3(-2, 2, 2);
            }

            //ground checkpoint
            isGrounded = Physics2D.OverlapCircle(groundcheck.position, 0.1f, groundFloor);

            //jumping
            if (Input.GetButtonDown("Jump") && (isGrounded || doDoubleJump))
            {
                Instantiate(jumpEffect, transform.position, transform.rotation);

                if (isGrounded)
                {
                    doDoubleJump = true;
                    AudioController.instance.PlayPlayerSFX(6);
                   
                }
                else
                {
                    doDoubleJump = false;
                    AudioController.instance.PlayPlayerSFX(7);
                    playerAnimator.SetTrigger("Double Jump");
                }
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, jumpForce);
            }

            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    MeeleAttack();
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }

            playerAnimator.SetBool("IsGrounded", isGrounded);
            playerAnimator.SetFloat("Speed", Mathf.Abs(playerRigidbody.velocity.x));


            //shooting
            if (Time.time >= nextAttackTime)
            {
                if (Input.GetKeyDown(KeyCode.W) && currentmagic >= bulletMagicCost && rageMode)
                {
                    playerAnimator.SetTrigger("Spell");
                    Instantiate(bullet, shootingPoint.position, shootingPoint.rotation).moveDirection = new Vector2(transform.localScale.x, 0);
                    AudioController.instance.PlayPlayerSFX(0);
                    currentmagic -= bulletMagicCost;
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }
        }
        else
        {
            playerRigidbody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Levelup(100);
        }
    } 

    public void Levelup(int XP)
    {
        currentEXP += XP;
        SaveManager.instance.activeSave.playerLevel = playerLevel;
        SaveManager.instance.activeSave.expToNextLevel = expToNextLevel;
        SaveManager.instance.activeSave.currentEXP = currentEXP;
        UpdateLevel();
        if(playerLevel < maxLevel)
        {
            if (currentEXP > expToNextLevel[playerLevel])
            {
                currentEXP -= expToNextLevel[playerLevel];
                playerLevel++;
                SaveManager.instance.activeSave.playerLevel = playerLevel;
                SaveManager.instance.activeSave.expToNextLevel = expToNextLevel;
                SaveManager.instance.activeSave.currentEXP = currentEXP;
                UpdateLevel();

                maxHealth += 10;
                SaveManager.instance.activeSave.maxHealth = Player.instance.maxHealth;
                maxmagic += 5;
                SaveManager.instance.activeSave.maxMagic = Player.instance.maxmagic;
                attackDamage += 2;
                SaveManager.instance.activeSave.attackDamage = Player.instance.attackDamage;
                currentHealth = maxHealth;
                currentmagic = maxmagic;
                

            }
        }
        else
        {
            currentEXP = 0;
            SaveManager.instance.activeSave.currentEXP = currentEXP;
        }
    }
    public void MeeleAttack()
    {
        //play an attack animation
        playerAnimator.SetTrigger("Attack");
        AudioController.instance.PlayPlayerSFX(8);

        //detect enemies in range of attack
        Collider2D[] hitEnemies =  Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        Collider2D[] hitBosses = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, BossLayer);

        //damage them
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }

        foreach (Collider2D boss in hitBosses)
        {
            boss.GetComponent<BossHealth>().TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int Damage)
    {
        currentHealth -= Damage;

        playerAnimator.SetTrigger("Hurt");
        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealth();
    }
    public void UpdateRage()
    {
        rageSlider.maxValue = maxRage;
        rageSlider.value = currentRage;
        rageText.text = Mathf.RoundToInt(currentRage) + "/" + maxRage;
    }
    public void UpdateHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
        healthText.text =Mathf.RoundToInt (currentHealth) + "/" + maxHealth;
    }

    public void UpdateMagic()
    {
        magicSlider.maxValue = maxmagic;
        magicSlider.value = currentmagic;
        magicText.text = Mathf.RoundToInt (currentmagic) + "/" + maxmagic;
    }
    public void Derage()
    {
        currentRage -= derageAmount * Time.deltaTime;
       
        if (currentRage <= 0)
        {
            rageMode = false;
            playerAnimator.SetBool("Rage", false);
        }
        UpdateRage();
    }
    public void RegenRage()
    {
        if (!rageMode)
        {
            currentRage += rageRegenSpeed * Time.deltaTime;
            if (currentRage > maxRage)
            {
                currentRage = maxRage;
            }
        }
    }

    public void UpdateLevel()
    {
        if(playerLevel < maxLevel)
        {
            currentXpSlider.maxValue = expToNextLevel[playerLevel];
            currentXpSlider.value = currentEXP;
            levelText.text = "LV" + playerLevel;
        }
        if(playerLevel == maxLevel)
        {
            currentXpSlider.maxValue = 0;
            levelText.text = "MAX LV";
        }
        
    }

    public void RegenMagic()
    {
        currentmagic += magicRegenSpeed * Time.deltaTime;
        if(currentmagic > maxmagic)
        {
            currentmagic = maxmagic;
        }
    }

    public void healthRegen()
    {
        currentHealth += healthRegenSpeed * Time.deltaTime;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void Die()
    {
        Debug.Log("player Die");
        //animation die and destroy it
        playerAnimator.SetBool("IsDead", true);
        //audio
        AudioController.instance.PlayPlayerSFX(5);

        GetComponent<Collider2D>().enabled = false;
        GameManager.instance.GameOver();
        isDead = true;
    }

    public void RestoreHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealth() ;
    }
    public void RageMode()
    {
        rageMode = true;
        playerAnimator.SetBool("Rage", true);
        
    }
    public void UnrageMode()
    {
        rageMode = false;
        playerAnimator.SetBool("Rage", false);

    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
