using System.Collections;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{
    [HideInInspector]
    public Player owner;
    public bool isPickable;
    public bool hasItem;
    public Weapon weapon { get; private set; }
    public Consumable consumable { get; private set; }
    private float m_nextFireTime = 0f;

    public void SetOwner (Player owner)
    {
        this.owner = owner;
    }
    public void SetEquippedItem(Item item)
    {
        if (item is Weapon)
        {
            weapon = item as Weapon;
            consumable = null;
        }
        else if (item is Consumable)
        {
            consumable = item as Consumable;
            weapon = null;
        }
        hasItem = true;
    }

    public Item GetEquippedItem()
    {
        return weapon ?? (Item)consumable;
    }

    public void TriggerItem()
    {
        if (weapon && Time.time >= m_nextFireTime)
        {
            Projectile p = Instantiate(
                weapon.projectilePrefab,
                weapon.muzzleExit.position,
                weapon.muzzleExit.rotation)
                .GetComponent<Projectile>();

            p.damage = weapon.damage;
            p.speed = weapon.projectileSpeed;
            p.range = weapon.range;
            if (weapon.muzzleEffect)
                weapon.muzzleEffect.Play();
            StartCoroutine(IRecoil(weapon.recoilAmount, weapon.recoiDuration));
            AudioManager.instance.PlaySound(weapon.shootSound);
            m_nextFireTime = Time.time + 1f / weapon.fireRate;
        }
        else if (consumable)
        {
            owner.Heal(consumable.healthResotreAmount);
            hasItem = false;
            consumable = null;
            m_nextFireTime = Time.time + .5f;
        }
    }

    private IEnumerator IRecoil(float amount, float duration)
    {
        Transform t = weapon.itemVisual.transform;
        t.position -= t.right * amount;
        yield return new WaitForSeconds(duration);
        t.position += t.right * amount;
    }
}

