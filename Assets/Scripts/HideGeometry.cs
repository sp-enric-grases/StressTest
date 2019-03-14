using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGeometry : MonoBehaviour
{
    public bool hideGeometry = true;

    private void Awake()
    {
        if (!hideGeometry) return;

        if (GetComponent<MeshRenderer>()) DestroyImmediate(GetComponent<MeshRenderer>());
        if (GetComponent<MeshFilter>()) DestroyImmediate(GetComponent<MeshFilter>());

        Destroy(this);
    }
}
