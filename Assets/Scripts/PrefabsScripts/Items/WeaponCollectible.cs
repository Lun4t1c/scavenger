using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollectible : CollectibleBase
{
    public GameObject WeaponPrefabHandle;
    public GameObject WeaponObject;

    private MeshFilter meshFilter;

    // Start is called before the first frame update
    void Start()
    {
        WeaponObject = Instantiate(WeaponPrefabHandle);
        WeaponObject.SetActive(false);
        base.Start();
        ApplyChild3dModel();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    protected override void Collect(Collider playerCollider)
    {
        playerCollider.gameObject.GetComponentInParent<Player>().TakeCollectible(this);
        base.Collect(playerCollider);
    }

    protected void ApplyChild3dModel()
    {
        meshFilter = GetComponent<MeshFilter>();

        MeshFilter childMeshFilter = WeaponObject.GetComponent<MeshFilter>();
        Material childMaterial = WeaponObject.GetComponent<Renderer>().sharedMaterial;

        if (childMeshFilter != null)
        {
            Mesh mesh = childMeshFilter.sharedMesh;
            meshFilter.sharedMesh = childMeshFilter.sharedMesh;

            Renderer parentRenderer = GetComponent<Renderer>();
            if (parentRenderer != null)
                parentRenderer.sharedMaterial = childMaterial;
            else Debug.LogError("Renderer component not found on the parent object.");
        }
        else Debug.LogError("MeshFilter component not found on the child prefab object.");
    }
}
