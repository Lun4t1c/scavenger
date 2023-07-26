using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomisedCrateScript : DestructibleBase
{
    public GameObject[] DropObjectPrefabs;
    public GameObject ProjectilePrefab;

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
        GameObject instantiatedObject = Instantiate(ProjectilePrefab, transform.position, Quaternion.identity);
        //AssetStoreDestroy();
        DropRandomisedItem();
        //base.DestroyDesctructible();
    }

    private void DropRandomisedItem()
    {
        Vector3 position = transform.position;
        GameObject droppedObject = Instantiate(DropObjectPrefabs[0]);
        droppedObject.transform.position = position;
    }

    private void AssetStoreDestroy()
    {
        wholeCrate.enabled = false;
        boxCollider.enabled = false;
        fracturedCrate.SetActive(true);
        crashAudioClip.Play();
    }

    #region AssetStoreScript

    [Header("Whole Create")]
    public MeshRenderer wholeCrate;
    public BoxCollider boxCollider;
    [Header("Fractured Create")]
    public GameObject fracturedCrate;
    [Header("Audio")]
    public AudioSource crashAudioClip;

    private void OnTriggerEnter(Collider other)
    {
        // Instantiate the object at the point of collision
        //GameObject instantiatedObject = Instantiate(ProjectilePrefab, other.transform.position, Quaternion.identity);

        // Optionally, you can set the rotation of the instantiated object to match the collision's normal
        //instantiatedObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, other.contacts[0].normal);

        AssetStoreDestroy();
    }

    [ContextMenu("Test")]
    public void Test()
    {
        wholeCrate.enabled = false;
        boxCollider.enabled = false;
        fracturedCrate.SetActive(true);
    }


    #endregion
}
