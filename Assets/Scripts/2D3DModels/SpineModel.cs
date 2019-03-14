using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class SpineModel : TypeOfModels, I2DAnd3Dmodels
{
    public override void Init()
    {
        anim = GetComponent<Animator>();
        SettingConstrintProperties(GetComponent<RotationConstraint>());
        StartCoroutine(SetRandomAnimation());
    }

    private void SettingConstrintProperties(RotationConstraint rotConst)
    {
        ConstraintSource constraintSource = new ConstraintSource() { sourceTransform = Camera.main.transform, weight = 1 };
        rotConst.constraintActive = true;
        rotConst.AddSource(constraintSource);
        rotConst.rotationOffset = Vector3.zero;
    }
}
