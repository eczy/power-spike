using UnityEngine;
using UnityEngine.UI;

public class ScoreScreenManager : MonoBehaviour
{
	[Header("UI Objects")]
	public Text victoryText;
	public Text victoryTextShadow;
	public GameObject redPlayers;
	public GameObject bluePlayers;
	
	[Header("Transition Parameters")]
	public float timeOnStatScreen;
	public string mainMenuScene;

	private void Start ()
	{
		SetVictoryText();
		ShowPlayers();
	}

	private void Update()
	{
		if (timeOnStatScreen > 0)
		{
			timeOnStatScreen -= Time.deltaTime;
		}
		else
		{
			SceneTransitionController.RequestSceneTransition(mainMenuScene, 1f);
		}
	}

	private void SetVictoryText()
	{
		if (StatManager.GetWinningTeam() == Team.Red)
		{
			victoryText.text = "Red Team Wins!";
			victoryTextShadow.text = "Red Team Wins!";
		}
		else
		{
			victoryText.text = "Blue Team Wins!";
			victoryTextShadow.text = "Blue Team Wins!";
		}
		
	}

	private void ShowPlayers()
	{
		if (StatManager.GetWinningTeam() == Team.Red)
		{
			redPlayers.SetActive(true);
			bluePlayers.SetActive(false);
		}
		else
		{
			bluePlayers.SetActive(true);
			redPlayers.SetActive(false);
		}
	}
}
