using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceCrate : MonoBehaviour
{

    public GameObject[] crates;
    public Item item;
    public int quantity;

    private InventoryScript inventory;

    private void Start()
    {
        int index = Random.Range(0, crates.Length);
        GetComponent<MeshFilter>().sharedMesh = crates[index].GetComponent<MeshFilter>().sharedMesh;
        GetComponent<Renderer>().sharedMaterial = crates[index].GetComponent<Renderer>().sharedMaterial;
        GetComponent<MeshCollider>().sharedMesh = crates[index].GetComponent<MeshCollider>().sharedMesh;
        inventory = GameObject.FindGameObjectWithTag("Menu").GetComponent<InventoryScript>();
        transform.Rotate(new Vector3(Random.Range(0f,90f), Random.Range(0f, 90f), Random.Range(0f, 90f)));
        RollItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if(inventory.AvailableWeight() > item.weight && !other.gameObject.GetComponent<BoatController>().died)
            {
                inventory.AddItem(item, quantity);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    public void RollItem()
    {
        item = inventory.items[Random.Range(0, inventory.items.Length)];
        if (Random.Range(0, 100) >= (item.rarity * 10) - 10)
        {
            quantity = Random.Range(1, 200 / (item.rarity * 10));
        }
        else
        {
            RollItem();
        }
    }
}
