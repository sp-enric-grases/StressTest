using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject sample;
    public GameObject textFPS;
    [Space(5)]
    public Destroyer destroyer;

    protected List<GameObject> sampleList = new List<GameObject>();
    private bool FPSisOn = true;

    public virtual void Awake()
    {
        if (destroyer != null)
            destroyer.ObjectDestroyingEvent += RemoveModel;
    }

    private void RemoveModel(GameObject go)
    {
        sampleList.Remove(go);
        DebugStats();
    }

    public virtual void DebugStats() { }

    public void FPSVisibility()
    {
        if (textFPS == null) return;

        FPSisOn = !FPSisOn;
        textFPS.GetComponent<FPSCounter>().enabled = FPSisOn;
        textFPS.SetActive(FPSisOn);
    }
}
