using UnityEngine;
public abstract class Item : ScriptableObject {
    public string itemName;
    public GameObject itemVisual;
    public GameObject itemUIVisual;
    [HideInInspector]
    public int index;
}


