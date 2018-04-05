using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour {

    static SceneTransitionController singleton;

    SceneTransitionEffect effect;
    SceneTransitionState currentState = SceneTransitionState.NOT_TRANSITIONING;
    static AsyncOperation loadingOperation;

    void Awake()
    {
        if (singleton && singleton != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start () {
        RefreshEffectComponentOnCamera();
	}
	
	void Update () {
        if (!effect)
        {
            RefreshEffectComponentOnCamera();
        }
	}

    void RefreshEffectComponentOnCamera()
    {
        effect = Camera.main.GetComponent<SceneTransitionEffect>();
        if (!effect)
        {
            effect = Camera.main.gameObject.AddComponent<SceneTransitionEffect>();
        }
    }

    public static bool RequestSceneTransition(string sceneName, float duration, AnimationCurve easeCurve = null)
    {
        if (singleton.currentState != SceneTransitionState.NOT_TRANSITIONING)
        {
            return false;
        }

        singleton.currentState = SceneTransitionState.LEAVING_SCENE;
        singleton.StartCoroutine(DoTransition(sceneName, duration));
        return true;
    }

    static IEnumerator DoTransition(string sceneName, float duration, AnimationCurve easeCurve = null)
    {
        // Transition out of current scene.
        float endingTime = Time.time + duration * 0.5f;
        while (Time.time < endingTime)
        {
            float progress = Mathf.Clamp01(1.0f - (endingTime - Time.time) / (duration * 0.5f));
            if (easeCurve != null)
            {
                progress = easeCurve.Evaluate(progress);
            }
            singleton.effect.SetProgress(progress);
            yield return null;
        }

        //Load new scene
        singleton.currentState = SceneTransitionState.LOADING_SCENE;
        loadingOperation = SceneManager.LoadSceneAsync(sceneName);

        float loadingStartTime = Time.time;
        float minimumLoadDuration = 0.5f;

        while (!loadingOperation.isDone || Time.time - loadingStartTime < minimumLoadDuration)
        {
            singleton.effect.SetProgress(1.0f);
            yield return null;
        }

        // Transition to into next scene.
        singleton.currentState = SceneTransitionState.ENTERING_SCENE;

        endingTime = Time.time + duration * 0.5f;
        while (Time.time < endingTime)
        {
            float progress = Mathf.Clamp01((endingTime - Time.time) / (duration * 0.5f));
            if (easeCurve != null)
            {
                progress = easeCurve.Evaluate(progress);
            }
            singleton.effect.SetProgress(progress);

            yield return null;
        }

        singleton.effect.SetProgress(0.0f);
        singleton.currentState = SceneTransitionState.NOT_TRANSITIONING;
    }
}

public enum SceneTransitionState { NOT_TRANSITIONING, LEAVING_SCENE, LOADING_SCENE, ENTERING_SCENE };
