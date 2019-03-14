using UnityEngine;

public class SpawnerSkinnedModels : SpawnerModels
{
    public SkinnedMeshRenderer[] skinnedModels;
    public int numBones = 0;

    public override void Awake()
    {
        base.Awake();

        skinnedModels = sample.GetComponentsInChildren<SkinnedMeshRenderer>();

        foreach (var skinned in skinnedModels)
            numBones += skinned.rootBone.gameObject.GetComponentsInChildren<Transform>().Length;
    }

    public override void DebugStats()
    {
        base.DebugStats();

        models.text = (numSamples * skinnedModels.Length).ToString();
        totalBones = numSamples * numBones;
        bones.text = totalBones.ToString();
    }
}
