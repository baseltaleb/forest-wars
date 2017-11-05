using UnityEngine;

public class Weapon : Item {
    public GameObject projectilePrefab;
    public GameObject muzzleEffectPrefab;
    public float damage = 1f;
    public float range = 5f;
    public float fireRate = 1f;
    public float projectileSpeed = 15f;
    public float recoilAmount = 0.1f;
    public float recoiDuration = 0.1f;
    public AudioClip shootSound;
    [HideInInspector]
    public Transform muzzleExit;
    [HideInInspector]
    public ParticleSystem muzzleEffect;

    public Weapon()
    {
        //Compensate for range difference resulting from difference in speed
        range *= 10 / projectileSpeed;
    }
}

#if UNITY_EDITOR
public static class WeaponSOMenuItem
{
    [UnityEditor.MenuItem("Items/Create/Weapon")]
    public static void CreateAsset()
    {
        var so = ScriptableObject.CreateInstance<Weapon>();
        UnityEditor.AssetDatabase.CreateAsset(so,
            UnityEditor.AssetDatabase.GenerateUniqueAssetPath("Assets/Items/Weapons/Weapon.asset"));
    }

}
#endif
