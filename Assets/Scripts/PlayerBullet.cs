using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;
    public Vector2 moveDirection;
    private Rigidbody2D bulletRB;
    public int bulletDamage;
    private Animator anim;
    private SpriteRenderer bulletsprite;
    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletsprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = moveDirection * bulletSpeed;

        if(bulletRB.velocity.x < 0)
        {
            bulletsprite.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D bulletHit)
    {
        //bullet damage
        if (bulletHit.tag == "Enemy")
        {
            bulletHit.GetComponent<Enemy>().TakeDamage(bulletDamage);
            //bullet Effect
            anim.SetBool("Hit", true);
            bulletSpeed = 0;
            Destroy(gameObject, 0.7f);
            //AudioController.instance.PlayEffectSFX(1);
        }else if(bulletHit.tag == "Boss")
        {
            bulletHit.GetComponent<BossHealth>().TakeDamage(bulletDamage);
            //bullet Effect
            anim.SetBool("Hit", true);
            bulletSpeed = 0;
            Destroy(gameObject, 0.7f);
        }
        //else
        //{
        //    anim.SetBool("Hit", true);
        //    bulletSpeed = 0;
        //    Destroy(gameObject, 0.7f);
        //    //AudioController.instance.PlayEffectSFX(1);
        //}
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
