using UnityEngine;

public class CamFollow : MonoBehaviour {
    public Transform player;
    private Transform m_myTransform;

    private void OnEnable()
    {
        EventsManager.PlayerCreated += PlayerCreated;
    }

    private void OnDisable()
    {
        EventsManager.PlayerCreated -= PlayerCreated;
    }

    private void Awake()
    {
        m_myTransform = transform;
    }

    private void Update()
    {
        if (player)
        {
            Vector3 pos = player.position;
            pos.z = m_myTransform.position.z;
            m_myTransform.position = pos;
        }

    }

    private void PlayerCreated(Player player)
    {
        this.player = player.transform;
    }
}
