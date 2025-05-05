using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SaveManager.instance.activeSave.currentCoins = GameManager.instance.currentCoins;
    }

    public void HealthUpgrade50()
    {
        if (GameManager.instance.currentCoins >= 100)
        {
            Player.instance.maxHealth += 50;
            Player.instance.currentHealth += 50;
            GameManager.instance.currentCoins -= 100;
            SaveManager.instance.activeSave.maxHealth = Player.instance.maxHealth;
            GameManager.instance.UpdateCoin();
            Player.instance.UpdateHealth();
            AudioController.instance.PlayUiSFX(6);
        }
    }

    public void MagicUpgrade5()
    {
        if (GameManager.instance.currentCoins >= 100)
        {
            Player.instance.maxmagic += 5;
            Player.instance.currentmagic += 5;
            GameManager.instance.currentCoins -= 100;
            SaveManager.instance.activeSave.maxMagic = Player.instance.maxmagic;
            GameManager.instance.UpdateCoin();
            Player.instance.UpdateMagic();
            AudioController.instance.PlayUiSFX(6);
        }
    }

    public void HealthRegenUpgrade()
    {
        if (GameManager.instance.currentCoins >= 100)
        {
            Player.instance.healthRegenSpeed += 1;
            GameManager.instance.currentCoins -= 100;
            SaveManager.instance.activeSave.healthRegenSpeed = Player.instance.healthRegenSpeed;
            GameManager.instance.UpdateCoin();
            Player.instance.healthRegen();
            AudioController.instance.PlayUiSFX(6);
        }
    }

    public void MagicRegenUpgrade()
    {
        if (GameManager.instance.currentCoins >= 100)
        {
            Player.instance.magicRegenSpeed += 1;
            GameManager.instance.currentCoins -= 100;
            SaveManager.instance.activeSave.magicRegenSpeed = Player.instance.magicRegenSpeed;
            GameManager.instance.UpdateCoin();
            Player.instance.RegenMagic();
            AudioController.instance.PlayUiSFX(6);
        }
    }
    public void AttackDamageUpgrade()
    {
        if (GameManager.instance.currentCoins >= 100)
        {
            Player.instance.attackDamage += 1;
            GameManager.instance.currentCoins -= 100;
            SaveManager.instance.activeSave.attackDamage = Player.instance.attackDamage;
            GameManager.instance.UpdateCoin();
            AudioController.instance.PlayUiSFX(6);
        }
    }
}
