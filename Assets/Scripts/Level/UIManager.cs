using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour {
    public static UIManager instance;
    private Player m_player;
    private RectTransform m_inventoryPanel;
    private int m_currentItemIndex = 0;
    private int m_nextItemIndex = 0;
    private int m_maxItems;
    private Color m_slotColor;
    private List<Image> m_itemSlots = new List<Image>();

    private void OnEnable()
    {
        EventsManager.PlayerCreated += PlayerCreated;
        EventsManager.ItemPickedUp += ItemPickedUp;
        EventsManager.ItemRemoved += ItemRemoved;
        EventsManager.ItemEquipped += ItemEquipped;
        EventsManager.PlayerDamaged += PlayerDamaged;
        EventsManager.PlayerHealed += PlayerHealed;
        EventsManager.PlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        EventsManager.PlayerCreated -= PlayerCreated;
        EventsManager.ItemPickedUp -= ItemPickedUp;
        EventsManager.ItemRemoved -= ItemRemoved;
        EventsManager.ItemEquipped -= ItemEquipped;
        EventsManager.PlayerDamaged -= PlayerDamaged;
        EventsManager.PlayerHealed -= PlayerHealed;
        EventsManager.PlayerDied -= PlayerDied;
    }

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    private void PlayerCreated(Player player)
    {
        m_player = player;
        m_inventoryPanel = PrefabsReference.instance.UiInventoryPanel;
        m_slotColor = PrefabsReference.instance.UiEmptyItemSlotPrefab.GetComponent<Image>().color;
        InstantiateInventoryUI(m_player.GetMaxInventory());
        PrefabsReference.instance.UiHealthSlider.maxValue = m_player.maxHealth;
        PrefabsReference.instance.UiHealthSlider.value = m_player.maxHealth;
    }

    private void InstantiateInventoryUI(int maxItems)
    {
        if (maxItems == 0)
            return;
        for (int i = 0; i < maxItems; i++)
        {
            GameObject slot = Instantiate(PrefabsReference.instance.UiEmptyItemSlotPrefab, m_inventoryPanel);
            m_itemSlots.Add(slot.GetComponent<Image>());
        }
        m_maxItems = maxItems;
        m_inventoryPanel.gameObject.SetActive(true);
    }

    private void ItemPickedUp(Item item)
    {
        if (item.index == m_maxItems)
            return;
        Instantiate(item.itemUIVisual, m_itemSlots[item.index].transform);
        m_nextItemIndex++;
    }

    private void ItemRemoved(Item item)
    {
        Destroy(m_itemSlots[item.index].transform.GetChild(0).gameObject);
        m_itemSlots[item.index].color = m_slotColor;
        m_nextItemIndex--;
    }

    private void ItemEquipped(int itemIndex)
    {
        m_itemSlots[m_currentItemIndex].color = m_slotColor;
        m_itemSlots[itemIndex].color = Color.green;
        m_currentItemIndex = itemIndex;
    }

    private void PlayerDamaged(float amount)
    {
        PrefabsReference.instance.UiHealthSlider.value -= amount;
    }

    private void PlayerHealed(float amount)
    {
        PrefabsReference.instance.UiHealthSlider.value += amount;
    }

    private void PlayerDied(Player player)
    {
        PrefabsReference.instance.UiGameOver.gameObject.SetActive(true);
    }
}
