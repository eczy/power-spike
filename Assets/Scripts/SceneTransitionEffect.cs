using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
[DisallowMultipleComponent]
public class SceneTransitionEffect : MonoBehaviour {

    public Image black;
    Color color = Color.black;

    [Range(0, 1)]
    public float progress = 0;

    void Start() {
        if (!black)
        {
            GetBlackImage();
        }
    }

    private void Update()
    {
        color.a = progress;
        black.color = color;
    }

    void GetBlackImage()
    {
        string imageName = "black";
        GameObject blackObject = GameObject.Find(imageName);
        
        if (blackObject)
        {
            black = blackObject.GetComponent<Image>();
        }

        if (!(blackObject && black))
        {
            Debug.LogWarning("Black Image 'black' not present in scene");
        }
    }

    public void SetProgress(float p)
    {
        progress = p;
    }
}
