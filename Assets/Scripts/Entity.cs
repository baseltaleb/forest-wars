using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public float maxHealth;
    public bool isDamagable;
    public float m_health;

    private void Awake()
    {
        m_health = maxHealth;
    }

    public virtual void TakeDamage(float amount)
    {
        if (isDamagable)
        {
            m_health -= amount;
        }
        if (m_health <= 0)
            Kill();
    }

    public virtual void Heal(float amount)
    {
        m_health += amount;
        if (m_health > maxHealth)
            m_health = maxHealth;
        EventsManager.OnPlayerHealed(amount);
    }

    public virtual void Kill()
    {
        //TODO effects
        Destroy(gameObject);
    }
}
