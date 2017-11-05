using System;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Player m_player;
    private Vector2 m_vector;
    private bool m_ready;

    private void OnEnable()
    {
        EventsManager.PlayerCreated += PlayerReady;
    }

    private void OnDisable()
    {
        EventsManager.PlayerCreated -= PlayerReady;
    }

    private void PlayerReady(Player obj)
    {
        m_ready = true;
    }

    private void Start()
    {
        m_player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!m_ready)
            return;
        m_vector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            m_vector.y = 1f;
        if (Input.GetKey(KeyCode.S))
            m_vector.y = -1f;
        if (Input.GetKey(KeyCode.D))
            m_vector.x = 1f;
        if (Input.GetKey(KeyCode.A))
            m_vector.x = -1f;
        m_player.Move(m_vector.normalized);

        if (Input.GetButton("Fire1"))
            m_player.TriggerItem();

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            m_player.CycleItem((Input.GetAxis("Mouse ScrollWheel") > 0f));
        }

        //Weapon mouse follow
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        bool flip = false;
        if (Mathf.Abs(angle) > 90)
        {
            flip = true;
        }
        m_player.WeaponRotation(rotation, flip);
    }
}
