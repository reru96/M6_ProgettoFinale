using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    protected Rigidbody rb;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float lifetime = 10f;
 
    protected float despawnTime;
    protected float Speed => speed;
    public int Dmg => damage;
    public float Lifetime => lifetime;


    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    { 
        if(Time.time >= lifetime)
        {
            gameObject.SetActive(false);
        }
    }
    protected virtual void OnEnable()
    {
        rb.velocity = transform.forward * speed;
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            LifeController life = collision.GetComponent<LifeController>();
            if (life != null)
            {
                life.AddHp((int)-Dmg);
                gameObject.SetActive(false);
            }


        }

        if (collision.CompareTag("Player"))
        {
            LifeController life = collision.GetComponent<LifeController>();
            if (life != null)
            {
                life.AddHp((int)-Dmg);
                gameObject.SetActive(false);
            }
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            gameObject.SetActive(false);
        }

    }
}
