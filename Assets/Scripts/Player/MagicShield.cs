using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MagicShield : MonoBehaviour
{
    [SerializeField] private GameObject m_Shield;
    private bool m_ShieldDisabled = false;
    private float m_ShieldCooldown = 5f;
    private float m_ShieldCooldownTimer = 0f;

    void Start()
    {
        m_Shield.SetActive(false);
    }

    void Update()
    {
        
        if (m_ShieldDisabled)
        {
            m_ShieldCooldownTimer -= Time.deltaTime;
            if (m_ShieldCooldownTimer <= 0f)
            {
                m_ShieldDisabled = false;
            }
        }

        
        if (!m_ShieldDisabled && Input.GetMouseButton(1))
        {
            m_Shield.SetActive(true);
        }
        else
        {
            m_Shield.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet") && m_Shield.activeSelf)
        {
            Bullet bullet = other.GetComponent<Bullet>();
            LifeController life = m_Shield.GetComponent<LifeController>();
            int dmg = bullet.Dmg;
            life.AddHp(-dmg);
            bullet.gameObject.SetActive(false);
            if (life.GetHp() == 0)
            {
                m_ShieldDisabled = true;
                m_ShieldCooldownTimer = m_ShieldCooldown;
                m_Shield.SetActive(false);
                life.AddHp(20);
            }
        }
    }
}
