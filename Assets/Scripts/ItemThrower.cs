using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemThrower : MonoBehaviour
{
    // We spawn an item on top of the player kart on key down, and shoot it on key up

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    Transform spawnPosition;

    bool hasItem = false;

    GameRules Rules;

    GameObject currentItem;
    void Start()
    {
        Rules = GameObject.FindGameObjectWithTag("Rules").GetComponent<GameRules>();
    }
    
    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire") && !hasItem)
        {
            currentItem = Instantiate(itemPrefab, spawnPosition);
            Rules.AddItemToDestroyList(currentItem);

            hasItem = true;
        }
        if (Input.GetButtonUp("Fire"))
        {
            currentItem.transform.parent = null;
            Rigidbody itemRigidbody = currentItem.GetComponent<Rigidbody>();
            //itemRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            hasItem = false;
        }
    }
}
