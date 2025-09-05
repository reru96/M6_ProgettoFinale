using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LifeController : MonoBehaviour
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int currentHp = 10;
    [SerializeField] private bool fullHpOnAwake = true;
    [SerializeField] private DeathAction death = DeathAction.Destroy;
   
    public int GetMax() => maxHp;
    public int GetHp() => currentHp;

    [SerializeField] private UnityEvent<int, int> onLifeChanged;
    [SerializeField] private UnityEvent onDeath;

    private void Awake()
    {
        if (fullHpOnAwake)
        {
            SetHp(maxHp);
        }
    }

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


    public void SetHp(int hp)
    {
        int oldHp = currentHp;
        currentHp = Mathf.Clamp(hp, 0, maxHp);

        Debug.Log($"HP aggiornati: {currentHp}/{maxHp}");
        onLifeChanged?.Invoke(currentHp, maxHp);

        if (oldHp > 0 && currentHp == 0)
        {
            Debug.Log($"Il personaggio {gameObject.name} è deceduto");
            HandleDeath();
        }
    }

    public void AddHp(int amount)
    {
        SetHp(currentHp + amount);
    }
}
