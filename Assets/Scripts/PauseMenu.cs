using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class PauseMenu : MonoBehaviour {
	public RectTransform pauseMenu;
	public Image[] options;
	public string[] sceneNames;
    public float inputDelay = 1f;
	public Color activeColor;
	public float lerpDuration;

	private bool getInput = true;
	private int indexActive;
	private bool isPaused;
	private float delayCounter;
	private Color inactiveColor;
	private Vector3 shownPosition;
	private Vector3 hiddenPosition;
	private bool menuHidden;

	private void Start(){
		try
		{
			inactiveColor = options[0].color;
		}
		catch (NullReferenceException)
		{
			inactiveColor = Color.white;
		}

		shownPosition = pauseMenu.anchoredPosition;
		hiddenPosition = shownPosition;
		
		float width = pauseMenu.rect.width;
		
		hiddenPosition.x = hiddenPosition.x - width;

		pauseMenu.anchoredPosition = hiddenPosition;
	}

	private void Update () {
		UpdateColors();
		
        delayCounter += Time.unscaledDeltaTime;
        getInput = !(delayCounter < inputDelay);

		CheckPause();
		
		if (isPaused)
		{
            CheckScroll();
            CheckSelect();
		}
	}

	private void CheckPause()
	{
		InputDevice device = InputManager.ActiveDevice;
		if (!device.MenuWasPressed) return;
		
		SetPaused(!isPaused);
	}

	private void CheckScroll()
	{
		InputDevice device = InputManager.ActiveDevice;
		
		if (device.Direction.Y < 0 && getInput)
		{
			ScrollDown();
		} else if (device.Direction.Y > 0 && getInput)
		{
			ScrollUp();
		}
	}

	private void CheckSelect()
	{
		InputDevice device = InputManager.ActiveDevice;
		if (!device.Action1.WasPressed) return;
		
		if (indexActive == 0)
		{
			SetPaused(false);
			return;
		}

		Time.timeScale = 1;
		SceneTransitionController.RequestSceneTransition(sceneNames[indexActive], 1f);
	}

	private void SetPaused(bool paused)
	{
		indexActive = 0;
		StartCoroutine(ShowMenu(paused));
		Time.timeScale = paused ? 0 : 1;
		isPaused = paused;
	}

	private void ScrollUp()
	{
		indexActive = (int) Mathf.Repeat(indexActive - 1, options.Length);
        delayCounter = 0;
	}

	private void ScrollDown()
	{
		indexActive = (int) Mathf.Repeat(indexActive + 1, options.Length);
		options[indexActive].color = activeColor;
        delayCounter = 0;
	}
	
	private void UpdateColors()
	{
		foreach (Image option in options)
		{
			option.color = inactiveColor;
		}
		
		options[indexActive].color = activeColor;
	}

	private IEnumerator ShowMenu(bool show)
	{
		Vector3 startPosition;
		Vector3 endPosition;
		float time = 0;
		
		if (show)
		{
			startPosition = hiddenPosition;
			endPosition = shownPosition;
		}
		else
		{
			startPosition = shownPosition;
			endPosition = hiddenPosition;
		}

		while (time < lerpDuration)
		{
			float progress = time / lerpDuration;

			Vector3 newPosition = pauseMenu.anchoredPosition;
			newPosition.x = Mathf.Lerp(startPosition.x, endPosition.x, progress);

			pauseMenu.anchoredPosition = newPosition;
			time += Time.unscaledDeltaTime;
			yield return null;
		}

		pauseMenu.anchoredPosition = endPosition;
	}
}
