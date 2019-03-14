using TMPro;
using UnityEngine;

public class SpawnerCallsTrisVerts : Spawner
{
    // This is an approximate  intitial number of draw calls when playing a scene with a Directional Light with NO SHADOWS
    public const int INIT_DRAWCALLS = 5;
    // This value is approximate and depends directly on the prefab we are using
    // In this demo, we are using a box with 14 materials and each box needs 14 draw calls to be rendered.
    public const int DRAWCALLS = 14;

    [Space(5)]
    public TMP_Text draws;
    public TMP_Text objs;
    public TMP_Text tris;
    public TMP_Text verts;
    
    private bool addButtonIsPressed = false;
    private bool removeButtonIsPressed = false;
    private int numVerts;
    private int numTris;

    public static SpawnerCallsTrisVerts Instance;

    public override void Awake()
    {
        base.Awake();

        Instance = this;
        numVerts = sample.GetComponent<MeshFilter>().sharedMesh.vertexCount;
        numTris = sample.GetComponent<MeshFilter>().sharedMesh.triangles.Length / 3;
    }

    public void CreateSpheres ()
    {
        addButtonIsPressed = true;
	}

    public void StopCreatingSpheres()
    {
        addButtonIsPressed = false;
    }

    public void RemoveSpheres()
    {
        removeButtonIsPressed = true;
    }

    public void StopRemovingSpheres()
    {
        removeButtonIsPressed = false;
    }

    private void Update()
    {
        if (addButtonIsPressed)
        {
            CreateObject();
            DebugStats();
        }

        if (removeButtonIsPressed)
        {
            if (sampleList.Count > 0)
            {
                GameObject sphere = sampleList[0];
                sampleList.RemoveAt(0);
                DestroyImmediate(sphere);

                DebugStats();
            }
        }
    }

    public void CreateObject()
    {
        transform.position = new Vector3(Random.Range(-0.2f, 0.2f), 5 + Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        GameObject newSphere = Instantiate(sample, transform.position, Quaternion.identity);
        sampleList.Add(newSphere);
    }

    public override void DebugStats()
    {
        if (draws != null) draws.text = GetDrawCalls().ToString();
        if (objs != null) objs.text = sampleList.Count.ToString();
        if (tris != null) tris.text = GetTriangles().ToString();
        if (verts != null) verts.text = GetVertices().ToString();
    }

    public int GetDrawCalls()
    {
        return sampleList.Count * DRAWCALLS + INIT_DRAWCALLS;
    }

    public int GetTriangles()
    {
        return sampleList.Count * numTris;
    }

    public int GetVertices()
    {
        return sampleList.Count * numVerts;
    }
}
