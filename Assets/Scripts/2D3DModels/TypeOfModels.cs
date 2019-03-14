using System.Collections;
using UnityEngine;

public class TypeOfModels : MonoBehaviour, I2DAnd3Dmodels
{
    public float minTimeAnimation = 2;
    public float maxTimeAnimation = 4;

    protected Animator anim;

    public virtual void Init() { }

    public IEnumerator SetRandomAnimation()
    {
        anim.SetInteger("animation", Random.Range(0, anim.runtimeAnimatorController.animationClips.Length));

        yield return new WaitForSeconds(Random.Range(minTimeAnimation, maxTimeAnimation));
        StartCoroutine(SetRandomAnimation());
    }
}
