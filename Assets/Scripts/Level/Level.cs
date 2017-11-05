using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour {
    public static Level instance;
    public int maxInvetoryItems;
    public bool autoCreatePlayer;
    [HideInInspector]
    public Player player;
    [SerializeField]
    private List<Item> m_itemsToSpawn = new List<Item>();
    [SerializeField]
    private List<Transform> m_itemSpawnLoactions = new List<Transform>();

    private void OnEnable()
    {
        EventsManager.PlayerCreated += PlayerCreated;
        EventsManager.PlayerDied += PlayerDied;
    }

    private void OnDisable()
    {
        EventsManager.PlayerCreated -= PlayerCreated;
        EventsManager.PlayerDied -= PlayerDied;
    }
    private void Awake()
    {
        if (!instance)
            instance = this;
        Time.timeScale = 1;
    }

    private void Start () {
        if (autoCreatePlayer)
        {
            player = Instantiate(PrefabsReference.instance.player).GetComponent<Player>();
        }
        SpawnLevelPickups();
        if (PrefabsReference.instance.levelMusic)
            AudioManager.instance.PlayLooped(PrefabsReference.instance.levelMusic);
	}

    private void PlayerCreated(Player player)
    {
        if(!player)
            this.player = player;
    }

    private void SpawnLevelPickups()
    {
        for (int i = 0; i < m_itemsToSpawn.Count; i++)
        {
            if(i < m_itemSpawnLoactions.Count)
            {
                Spawner.instance.SpawnPickupItem(m_itemsToSpawn[i], m_itemSpawnLoactions[i].position);
            }
            else
            {
                Debug.LogWarning("There are less spawn locations than spawn items. Add more spawn locations");
            }
        }
    }

    private void PlayerDied(Player player)
    {
        Time.timeScale = 0f;
    }

    public void TryAgain()
    {
        AudioManager.instance.StopAllSounds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
