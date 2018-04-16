using System.Collections;
using UnityEngine;

public class GameOver : MonoBehaviour {
	public string statScene;
	public BatteryGoal redTeam;
	public BatteryGoal blueTeam;
    public float loserDeathDelay = 1f;
    public float loserThrowForce = 1f;
    public float loserThrowTorque = 1f;
    public Rigidbody[] redTeamPlayers;
    public Rigidbody[] blueTeamPlayers;

    Timer timer;
    Overtime overtime;
    bool gameOver = false;

    private void Start()
    {
        timer = FindObjectOfType<Timer>();
        overtime = FindObjectOfType<Overtime>();
    }

    private void Update () {
        if (gameOver)
            return;
		if (redTeam.GetBatteries() == redTeam.maxBatteries) {
			StartCoroutine(EndGame(Team.Red));
		} else if (blueTeam.GetBatteries() == blueTeam.maxBatteries) {
			StartCoroutine(EndGame(Team.Blue));
		}
        else if (timer && timer.IsTimeOut())
        {
            CalculateTimeoutWinner();
        }
	}
	
    private IEnumerator EndGame(Team winningTeam)
    {
        gameOver = true;
	    StatManager.SetWinningTeam(winningTeam);
        if (winningTeam == Team.Red)
            KillLosers(Team.Blue);
        else
            KillLosers(Team.Red);
        yield return new WaitForSeconds(loserDeathDelay);
	    SceneTransitionController.RequestSceneTransition(statScene, 1f);
        yield return null;
    }

    void CalculateTimeoutWinner()
    {
        if (redTeam.GetBatteries() > blueTeam.GetBatteries())
        {
            StartCoroutine(EndGame(Team.Red));
        }
        else if (blueTeam.GetBatteries() > redTeam.GetBatteries())
        {
            StartCoroutine(EndGame(Team.Blue));
        }
        else
        {
            overtime.StartOvertime();
        }
    }

    void KillLosers(Team team)
    {
        Rigidbody[] losers = null;
        if (team == Team.Red)
        {
            Debug.Log("killing red team");
            losers = redTeamPlayers;
        }
        else
        {
            print("killing blue team");
            losers = blueTeamPlayers;
        }

        foreach (Rigidbody r in losers)
        {
            r.constraints = RigidbodyConstraints.None;
            r.AddForce(new Vector3(0, 0, Random.Range(-loserThrowForce, loserThrowForce)), ForceMode.Impulse);
            r.AddTorque(new Vector3(0, Random.Range(-loserThrowTorque, loserThrowTorque), 0), ForceMode.Impulse);
        }
    }
	


}
