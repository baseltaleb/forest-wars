using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTree : Entity {
    public GameObject evilModeGO;
    public GameObject normalModeGO;
    public float damage;
    public float movementSpeed = 1f;
    public float secondsBeforeTurning;
    [HideInInspector]
    public bool turned;
    private Player player;
    private Rigidbody2D rigidBody;
    private float m_nextHitTime = 0f;
    private void OnEnable()
    {
        EventsManager.PlayerCreated += SetPlayerTransform;
    }
    private void OnDisable()
    {
        EventsManager.PlayerCreated -= SetPlayerTransform;
    }

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        normalModeGO.SetActive(true);
        if(secondsBeforeTurning > 0)
            StartCoroutine(WaitThenTurn());
    }

    private void Update()
    {
        if(turned && player)
        {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            rigidBody.MovePosition(transform.position + direction * movementSpeed * Time.deltaTime);
        }
    }

    private void SetPlayerTransform(Player player)
    {
        this.player = player;
    }

    private IEnumerator WaitThenTurn()
    {
        yield return new WaitForSeconds(secondsBeforeTurning);
        Turn();
    }

    public void Turn()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
        normalModeGO.SetActive(false);
        evilModeGO.SetActive(true);
        AudioManager.instance.PlaySound(PrefabsReference.instance.treeWakup);
        turned = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == player.gameObject)
        {
            if (Time.time >= m_nextHitTime)
            {
                player.TakeDamage(damage);
                AudioManager.instance.PlaySound(PrefabsReference.instance.hit);
                m_nextHitTime = Time.time + 1f / 1;
            }
        }
    }

    private void OnDestroy()
    {
        AudioManager.instance.PlaySound(PrefabsReference.instance.treeDeath);
    }
}
