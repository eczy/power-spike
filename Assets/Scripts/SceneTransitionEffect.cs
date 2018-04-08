using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
public class SceneTransitionEffect : MonoBehaviour {

    Material material;

    [Range(0, 1)]
    public float progress = 0;

    void Awake()
    {
        material = GetMaterial();
    }

    Material GetMaterial()
    {
        string material_name = "ScreenFadeEffect";
        return Resources.Load<Material>(material_name);
    }

    public void SetProgress(float p)
    {
        progress = p;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!material)
        {
            Debug.Log("Material is null");
        }
        material.SetFloat("_Progress", Mathf.Clamp01(1.0f - progress));
        Graphics.Blit(source, destination, material);
    }
}
