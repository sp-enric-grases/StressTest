using UnityEngine;

public class SpawnerSpineModels : SpawnerModels
{
    [Space(5)]
    public int numBones;
    
    public override void DebugStats()
    {
        base.DebugStats();

        models.text = numSamples.ToString();
        totalBones = numSamples * numBones;
        bones.text = totalBones.ToString();
    }
}
