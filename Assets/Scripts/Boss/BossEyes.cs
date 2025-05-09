using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossEyes : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if(transform.position.x > player.position.x && !isFlipped )
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0);
            isFlipped = true;
        }

        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0);
            isFlipped = false;
        }
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
