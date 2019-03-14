using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnerModels : Spawner
{
    [Space(5)]
    public TMP_Text models;
    public TMP_Text bones;
    public TMP_Text anims;
    
    public float limit = 2;
    private int animations = 0;

    protected GameObject newSample;
    protected int numSamples = 0;
    protected int totalBones;

    public static SpawnerModels Instance;

    public override void Awake()
    {
        base.Awake();

        Instance = this;
        animations = sample.GetComponentInChildren<Animator>().runtimeAnimatorController.animationClips.Length;
    }
	
	public virtual void AddModel()
    {
        transform.position = new Vector3(Random.Range(-limit, limit), transform.position.y, Random.Range(-limit, limit));
        newSample = Instantiate(sample, transform.position, Quaternion.identity);
        sampleList.Add(newSample);
        newSample.GetComponentInChildren<I2DAnd3Dmodels>().Init();

        DebugStats();
    }

    public void RemoveModel()
    {
        if (sampleList.Count > 0)
        {
            GameObject sample = sampleList[0];
            sampleList.RemoveAt(0);
            DestroyImmediate(sample);

            DebugStats();
        }
    }

    public override void DebugStats()
    {
        numSamples = sampleList.Count;
        anims.text = (numSamples * animations).ToString();
    }

    public int GetNumModels()
    {
        return numSamples;
    }

    public int GetNumAnims()
    {
        return numSamples * animations;
    }

    public int GetBones()
    {
        return totalBones;
    }
}
