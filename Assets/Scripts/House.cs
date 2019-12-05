using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public GameObject roof;

    private BoxCollider2D playerCollider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
            roof.active = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
            roof.active = true;
    }
}
