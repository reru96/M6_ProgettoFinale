using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    [SerializeField] private float maxHp = 10;
    [SerializeField] private float currentHp = 10;
    [SerializeField] private bool fullHpOnAwake = true;
    [SerializeField] private DeathAction death = DeathAction.Destroy;
    [SerializeField] private event Action<float, float> onLifeChanged;
    [SerializeField] private UnityEvent onDeath;


    public float GetMax() => maxHp;
    public float GetHp() => currentHp;

    public enum DeathAction
    {
        None,
        Disable,
        Destroy,
        SceneReload
    }

    private void HandleDeath()
    {
        switch (death)
        {
            case DeathAction.None:
                onDeath?.Invoke();
                break;

            case DeathAction.Disable:
                gameObject.SetActive(false);
                break;

            case DeathAction.Destroy:
                Destroy(gameObject);
                break;
            case DeathAction.SceneReload:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
        }
    }



    private void Awake()
    {
        if (fullHpOnAwake)
        {
            SetHp(maxHp);
        }
    }

    public void SetHp(float hp)
    {
        float oldHp = currentHp;
        currentHp = Mathf.Clamp(hp, 0, maxHp);

        Debug.Log($"HP aggiornati: {currentHp}/{maxHp}");
        onLifeChanged?.Invoke(currentHp, maxHp);


        if (oldHp > 0 && currentHp == 0)
        {
            Debug.Log($"Il personaggio {gameObject.name} � deceduto");
            HandleDeath();

        }
    }

    public void AddHp(float amount)
    {
        SetHp(currentHp + amount);
    }

    public void AddLifeListener(Action<float, float> callback)
    {
        onLifeChanged += callback;
    }

    public void RemoveLifeListener(Action<float, float> callback)
    {
        onLifeChanged -= callback;
    }
}
