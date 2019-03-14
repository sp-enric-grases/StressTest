using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderHandler : MonoBehaviour
{
    public GameObject sample;

    public Material mat;
    public Texture albedo;
    public Texture metallic;
    public Texture rough;
    public Texture normal;
    public Texture height;
    public Texture occlusion;
    public Texture detail;
    public Texture nrmDetail;

    public List<ChangeSprite> buttons;

    void Start ()
    {
        mat = sample.GetComponent<MeshRenderer>().material;

        albedo =    mat.GetTexture("_MainTex");
        metallic =  mat.GetTexture("_MetallicGlossMap");
        rough =     mat.GetTexture("_SpecGlossMap");
        normal =    mat.GetTexture("_BumpMap");
        height =    mat.GetTexture("_ParallaxMap");
        occlusion = mat.GetTexture("_OcclusionMap");
        detail =    mat.GetTexture("_DetailAlbedoMap");
        nrmDetail = mat.GetTexture("_DetailNormalMap");

        foreach (var item in buttons)
        {
            item.ButtonHasBeenPressedRequest += TextureHandler;
        }
    }

    public void TextureHandler(bool state, string map)
    {
        if (state)
        {
            switch (map)
            {
                case "_MainTex": mat.SetTexture(map, albedo); break;
                case "_MetallicGlossMap": mat.SetTexture(map, metallic); break;
                case "_SpecGlossMap": mat.SetTexture(map, rough); break;
                case "_BumpMap": mat.SetTexture(map, normal); break;
                case "_ParallaxMap": mat.SetTexture(map, height); break;
                case "_OcclusionMap": mat.SetTexture(map, occlusion); break;
                case "_DetailAlbedoMap": mat.SetTexture(map, detail); break;
                case "_DetailNormalMap": mat.SetTexture(map, nrmDetail); break;
            }
        }
        else
            mat.SetTexture(map, null);
    }

}

