using UnityEngine;

public class MetaballManager : MonoBehaviour
{
    public Material metaballMaterial;
    public Transform[] metaballObjects;

    void Update()
    {
        Vector4[] positions = new Vector4[10]; // Supports up to 10 objects
        for (int i = 0; i < metaballObjects.Length; i++)
        {
            positions[i] = Camera.main.WorldToViewportPoint(metaballObjects[i].position);
        }
        metaballMaterial.SetVectorArray("_Metaballs", positions);
    }
}

