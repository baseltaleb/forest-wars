using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityTrigger : MonoBehaviour {
    private EvilTree myTree;
    private void Start()
    {
        myTree = transform.parent.GetComponent<EvilTree>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myTree.turned)
            return;
        Player p = collision.GetComponent<Player>();
        if (p)
        {
            myTree.Turn();
            gameObject.SetActive(false);
        }
    }
}
