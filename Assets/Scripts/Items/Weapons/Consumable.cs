using UnityEngine;

public class Consumable : Item {
    public float healthResotreAmount;
}

#if UNITY_EDITOR
public static class ConsumableSOMenuItem
{
    [UnityEditor.MenuItem("Items/Create/Consumable")]
    public static void CreateAsset()
    {
        var so = ScriptableObject.CreateInstance<Consumable>();
        UnityEditor.AssetDatabase.CreateAsset(so,
            UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Items/Consumables/Consumable.asset"));
    }

}
#endif
