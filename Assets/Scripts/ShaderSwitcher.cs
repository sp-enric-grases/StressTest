using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class ShaderSwitcher : MonoBehaviour
{
    public GameObject textFPS;
    public List<GameObject> samples;
    [Space(5)]
    public GameObject orbitCam;
    public GameObject shaderCam;
    [Space(5)]
    public TMP_Text textShader; 
    public List<Material> materialsToCheck;

    private List<Material> materials = new List<Material>();
    private int mat = 0;
    private bool camShader = true;
    private bool FPSisOn = true;

    void Start ()
    {
        foreach (var item in materialsToCheck)
            if (item != null) materials.Add(item);

        SwitchMaterial(0);

    }
	
    public void SwitchMaterial(int index)
    {
        mat += index;

        if (mat < 0) mat = materials.Count-1;
        if (mat == materials.Count) mat = 0;

        foreach (var item in samples)
            item.GetComponent<MeshRenderer>().material = materials[mat];

        DebugMaterial(materials[mat]);
    }

    private void DebugMaterial(Material mat)
    {
        Shader shader = mat.shader;
        string maps = "\n\n";

#if UNITY_EDITOR
        for (int i = 0; i < ShaderUtil.GetPropertyCount(shader); i++)
        {
            if (ShaderUtil.GetPropertyType(shader, i) == ShaderUtil.ShaderPropertyType.TexEnv)
            {
                Texture texture = mat.GetTexture(ShaderUtil.GetPropertyName(shader, i));
                if (texture != null)
                    maps += string.Format("{0}\n", texture.name);
            }
        }
#endif
        textShader.text = string.Format("{0}\n{1}{2}", mat.name, shader.name, maps);
    }

    public void ChangeCamera()
    {
        camShader = !camShader;
        orbitCam.SetActive(camShader);
        shaderCam.SetActive(!camShader);
    }

    public void FPSVisibility()
    {
        if (textFPS == null) return;

        FPSisOn = !FPSisOn;
        textFPS.GetComponent<FPSCounter>().enabled = FPSisOn;
        textFPS.SetActive(FPSisOn);
    }
}
