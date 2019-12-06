using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private Inventory _inventory;

    public Item item;
    // Start is called before the first frame update
    void Start()
    {
        _inventory = Inventory.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _inventory.Add(item);
            Destroy(this.gameObject);
        }
    }
}
