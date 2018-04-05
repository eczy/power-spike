using UnityEngine;

public class GameOver : MonoBehaviour {
	public string statScene;
	public BatteryGoal redTeam;
	public BatteryGoal blueTeam;
	
    private void Update () {
		if (redTeam.GetBatteries () == redTeam.maxBatteries) {
			EndGame(Team.Red);
		} else if (blueTeam.GetBatteries () == blueTeam.maxBatteries) {
			EndGame(Team.Blue);
		}
	}
	
    private void EndGame(Team winningTeam)
    {
	    StatManager.SetWinningTeam(winningTeam);
	    SceneTransitionController.RequestSceneTransition(statScene, 1f);
    }
	
}
