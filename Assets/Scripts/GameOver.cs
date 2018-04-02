using UnityEngine;

public class GameOver : MonoBehaviour {
	public string statScene;
	public BatteryGoal redTeam;
	public BatteryGoal blueTeam;

    Timer timer;
    Overtime overtime;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    private void Update () {
		if (redTeam.GetBatteries() == redTeam.maxBatteries) {
			EndGame(Team.Red);
		} else if (blueTeam.GetBatteries() == blueTeam.maxBatteries) {
			EndGame(Team.Blue);
		}
        else if (timer && timer.IsTimeOut())
        {
            CalculateTimeoutWinner();
        }
	}
	
    private void EndGame(Team winningTeam)
    {
	    StatManager.SetWinningTeam(winningTeam);
	    SceneTransitionController.RequestSceneTransition(statScene, 1f);
    }

    void CalculateTimeoutWinner()
    {
        if (redTeam.GetBatteries() > blueTeam.GetBatteries())
        {
            EndGame(Team.Red);
        }
        else if (blueTeam.GetBatteries() > redTeam.GetBatteries())
        {
            EndGame(Team.Blue);
        }
        else
        {
            overtime.StartOvertime();
        }
    }
	


}
