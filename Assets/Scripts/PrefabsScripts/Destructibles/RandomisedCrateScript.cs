using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedCrateScript : DestructibleBase
{
    public GameObject[] DropObjectPrefabs;

    protected override void Start()
    {
        currentHealth = 3;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void DestroyDesctructible()
    {
        DropRandomisedItem();
        base.DestroyDesctructible();
    }

    private void DropRandomisedItem()
    {
        Vector3 position = transform.position;
        GameObject droppedObject = Instantiate(DropObjectPrefabs[0]);
        droppedObject.transform.position = position;
    }
}
