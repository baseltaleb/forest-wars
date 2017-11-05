using UnityEngine;
using UnityEngine.UI;
public class PrefabsReference : MonoBehaviour {
    public static PrefabsReference instance;
    public Weapon handgun;
    public Weapon assaultRifle;
    public Transform pickablePrefab;
    public GameObject player;
    public RectTransform UiInventoryPanel;
    public GameObject UiEmptyItemSlotPrefab;
    public RectTransform UiGameOver;
    public Slider UiHealthSlider;
    [Header("Audio clips")]
    public AudioClip levelMusic;
    public AudioClip pickupAudio;
    public AudioClip treeWakup;
    public AudioClip treeDeath;
    public AudioClip heal;
    public AudioClip hit;
    public AudioClip inventorySelect;
    public AudioClip death;

    private void Awake()
    {
        if (!instance)
            instance = this;
    }
}
