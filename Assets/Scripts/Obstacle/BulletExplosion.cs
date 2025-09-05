using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : Bullet
{
    [SerializeField] protected float radius;
    [SerializeField] protected bool isExploded = false;
    //[SerializeField] protected Animator anim;

    protected override void Start()
    {
        base.Start();
        //anim = GetComponent<Animator>();
    }
    protected override void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in hits)
            { 
                LifeController life = collision.GetComponent<LifeController>();
                if (life != null)
                {
                    isExploded = true;
                    life.AddHp((int)-Dmg);
                    gameObject.SetActive(false);
                }
                isExploded = true;
                //anim.SetBool("Explosion", isExploded);
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, radius);
            foreach (Collider hit in hits)
            {
                LifeController life = collision.GetComponent<LifeController>();
                if (life != null)
                {
                    isExploded = true;
                    life.AddHp((int)-Dmg);
                    gameObject.SetActive(false);
                }
            }
            isExploded = true;
            //anim.SetBool("Explosion", isExploded);
            gameObject.SetActive(false);
        }

    }
}
