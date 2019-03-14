using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SkinnedModel : TypeOfModels
{
    public override void Init()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SetRandomAnimation());
    }
}
