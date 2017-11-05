using UnityEngine;

public class Spawner : MonoBehaviour{
    public static Spawner instance;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    public void SpawnPickupItem(Item item, Vector2 spawnPosition)
    {
        item = Instantiate(item);
        GameObject go = Instantiate(PrefabsReference.instance.pickablePrefab.gameObject);
        go.name = item.name + " pickable";
        go.GetComponent<PickupItem>().SetItem(item);
        item.itemVisual = Instantiate(item.itemVisual, go.transform);
        if(item is Weapon)
        {
            Weapon w = (Weapon)item;
            w.muzzleExit = w.itemVisual.transform.Find("Muzzle exit");
            w.muzzleEffect = Instantiate(w.muzzleEffectPrefab.gameObject, w.muzzleExit).GetComponent<ParticleSystem>();
        }
        go.transform.position = spawnPosition;
    }
}
