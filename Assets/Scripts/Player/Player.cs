using System;
using UnityEngine;

public class Player : Entity {
    public float speed = 10;
    public Transform playerVisuals;
    public ItemHolder itemHolder;
    private Transform m_itemHolderTransform;
    private Rigidbody2D m_rigidBody;
    private Inventory m_inventory;
    private bool m_flipped = false;

    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_inventory = GetComponent<Inventory>();
        m_inventory.maxItems = Level.instance.maxInvetoryItems;
        itemHolder = GetComponentInChildren<ItemHolder>();
        itemHolder.SetOwner(this);
        m_itemHolderTransform = itemHolder.transform;
        m_inventory.SetOwner(this);
        EventsManager.OnPlayerCreated(this);
    }

    public int GetMaxInventory()
    {
        return m_inventory.maxItems;
    }

    public void Move(Vector2 direction)
    {
            direction *= speed;
            m_rigidBody.MovePosition(m_rigidBody.position + direction * Time.deltaTime);
    }

    public void WeaponRotation(Quaternion rotation, bool dirChanged)
    {
        if (itemHolder.consumable)
            return;
        if (dirChanged)
        {
            if (!m_flipped)
            {
                Flip();
                m_flipped = true;
            }
        }
        else if (m_flipped)
        {
            Flip();
            m_flipped = false; 
        }
        m_itemHolderTransform.rotation = rotation;
    }

    private void Flip()
    {
        Vector2 vScale = playerVisuals.localScale;
        vScale.x = -vScale.x;
        playerVisuals.localScale = vScale;

        if (itemHolder)
        {
            Transform t = itemHolder.transform;
            Vector2 scale = t.localScale;
            scale.y = -scale.y;
            t.localScale = scale;
        }
    }

    public void PickupItem(Item item)
    {
        if (m_inventory.GetItemsCount() == m_inventory.maxItems)
        {
            return;
        }
        else
        {
            m_inventory.AddItem(item);
            EventsManager.OnItemPickedUp(item);
        }
    }

    public void TriggerItem()
    {
        if (itemHolder.weapon)
        {
            itemHolder.TriggerItem();
        }
        else if (itemHolder.consumable)
        {
            itemHolder.TriggerItem();
            m_inventory.RemoveCurrentItem();
            AudioManager.instance.PlaySound(PrefabsReference.instance.heal);
        }

    }

    public void CycleItem(bool forward)
    {
        if (!m_inventory.currentItem)
            return;
        m_inventory.SwitchItem(forward);
    }

    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount);
        EventsManager.OnDamagePlayer(amount);
    }
    public override void Kill()
    {
        EventsManager.OnPlayerDied(this);
        Destroy(gameObject);
    }
}
