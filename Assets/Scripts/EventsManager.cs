using System;

public static class EventsManager {

    public static event Action<Player> PlayerCreated;
    public static event Action<float> PlayerDamaged;
    public static event Action<float> PlayerHealed;
    public static event Action<Player> PlayerDied;
    public static event Action<Item> ItemPickedUp;
    public static event Action<Item> ItemRemoved;
    public static event Action<int> ItemEquipped;

    public static void OnPlayerCreated(Player player)
    {
        if (PlayerCreated != null)
        {
            PlayerCreated.Invoke(player);
        }
    }

    public static void OnDamagePlayer(float amount)
    {
        if(PlayerDamaged != null)
        {
            PlayerDamaged.Invoke(amount);
        }
    }

    public static void OnPlayerHealed(float amount)
    {
        if(PlayerHealed != null)
        {
            PlayerHealed.Invoke(amount);
        }
    }

    public static void OnPlayerDied(Player player)
    {
        if(PlayerDied != null)
        {
            PlayerDied.Invoke(player);
        }
    }

    public static void OnItemPickedUp(Item item)
    {
        if(ItemPickedUp != null)
        {
            ItemPickedUp.Invoke(item);
        }
    }

    public static void OnItemRemoved(Item item)
    {
        if(ItemRemoved != null)
        {
            ItemRemoved.Invoke(item);
        }
    }

    public static void OnItemEquipped(int itemIndex)
    {
        if (ItemEquipped != null)
        {
            ItemEquipped.Invoke(itemIndex);
        }
    }
}
