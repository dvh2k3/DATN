using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinsToGive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.GetCoins(coinsToGive);
            AudioController.instance.PlayUiSFX(1);
            Destroy(gameObject);
        }
    }
}
