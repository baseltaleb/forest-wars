using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour{
    public Item currentItem;
    public int maxItems;
    private int m_nextAvailableSlot;
    private List<Item> m_items = new List<Item>();
    private Player m_owner;

    public Player GetOwner()
    {
        return m_owner;
    }

    public void SetOwner(Player player)
    {
        m_owner = player;
    }

    public int GetItemsCount()
    {
        return m_items.Count;
    }

    public void AddItem(Item item)
    {
        SetupVisuals(item);
        item.index = m_nextAvailableSlot;
        m_items.Insert(item.index, item);
        item.itemVisual.SetActive(false);
        if (!currentItem)
        {
            SwitchItem(true);
        }
        m_nextAvailableSlot++;
    }

    public void RemoveCurrentItem()
    {
        m_nextAvailableSlot = currentItem.index;
        m_items[currentItem.index] = null;
        Destroy(currentItem.itemVisual);
        EventsManager.OnItemRemoved(currentItem);
        currentItem = null;
        SwitchItem(false);
    }

    public void SwitchItem(bool forward)
    {
        if (m_items.Count == 0)
            return;
        Item i = currentItem;
        if (!currentItem)
        {
            currentItem = m_items[FindNextItem(-1)];
        }
        else
        {
            currentItem.itemVisual.SetActive(false);
            int selectionIndex = currentItem.index;
            if (forward)
            {
                selectionIndex = FindNextItem(selectionIndex);
            }
            else
            {
                selectionIndex = FindPreviousItem(selectionIndex);
            }
            currentItem = m_items[selectionIndex];
        }
        m_owner.itemHolder.SetEquippedItem(currentItem);
        currentItem.itemVisual.SetActive(true);
        EventsManager.OnItemEquipped(currentItem.index);
        if(i != currentItem)
            AudioManager.instance.PlaySound(PrefabsReference.instance.inventorySelect);
    }

    private int FindNextItem(int index)
    {
        for (int i = index + 1; i < m_items.Count; i++)
        {
            if (m_items[i] != null)
            {
                index = i;
                break;
            }
        }
        return index;
    }

    private int FindPreviousItem(int index)
    {
        if (index <= 0)
            return 0;
        for (int i = index - 1; i < m_items.Count; i--)
        {
            if (m_items[i] != null)
            {
                index = i;
                break;
            }
        }
        return index;

    }

    private void SetupVisuals(Item item)
    {
        Transform t = item.itemVisual.transform;
        t.SetParent(m_owner.itemHolder.transform);
        t.position = m_owner.itemHolder.transform.position;
        if (item is Weapon)
        {
            t.rotation = m_owner.itemHolder.transform.rotation;
            t.localScale = Vector2.one;
        }
    }

}
