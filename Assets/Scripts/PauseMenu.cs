using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using InControl;

public class PauseMenu : MonoBehaviour {
	public GameObject pauseMenu;
	public Text[] texts;
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

	private void Start(){
		try
		{
			inactiveColor = texts[0].color;
		}
		catch (NullReferenceException)
		{
			inactiveColor = Color.white;
		}

		shownPosition = pauseMenu.transform.position;
		hiddenPosition = shownPosition;
		
		// I have no clue why this is divided by 2 but it seems to work
		hiddenPosition.x = hiddenPosition.x - pauseMenu.GetComponent<RectTransform>().rect.width / 2;

		pauseMenu.transform.position = hiddenPosition;
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
		indexActive = (int) Mathf.Repeat(indexActive - 1, texts.Length);
        delayCounter = 0;
	}

	private void ScrollDown()
	{
		indexActive = (int) Mathf.Repeat(indexActive + 1, texts.Length);
		texts[indexActive].color = activeColor;
        delayCounter = 0;
	}
	
	private void UpdateColors()
	{
		foreach (Text text in texts)
		{
			text.color = inactiveColor;
		}
		
		texts[indexActive].color = activeColor;
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

			Vector3 newPosition = pauseMenu.transform.position;
			newPosition.x = Mathf.Lerp(startPosition.x, endPosition.x, progress);

			pauseMenu.transform.position = newPosition;
			time += Time.unscaledDeltaTime;
			yield return null;
		}
	}
}
