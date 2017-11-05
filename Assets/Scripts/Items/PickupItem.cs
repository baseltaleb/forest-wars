using UnityEngine;

public class PickupItem : MonoBehaviour {
    private Item m_item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();

        if (player)
        {
            player.PickupItem(m_item);
            Destroy(gameObject);
        }
    }

    public Item GetItem()
    {
        return m_item;
    }

    public void SetItem(Item item)
    {
        this.m_item = item;
    }
}
