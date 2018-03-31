using UnityEngine;

public class StatManager : MonoBehaviour {
    private static StatManager original;
    private static Team winningTeam;

    private void Start()
    {
        if (original == null)
        {
            original = this;
        }

        if (original != this)
        {
            Destroy(gameObject);
        }

        if (GetComponent<ResetStats>())
        {
            ResetAllStats();
        }

        DontDestroyOnLoad(gameObject);
    }

    private void ResetAllStats()
    {
        foreach (StatsToUI stat in GetComponentsInChildren<StatsToUI>()) {
            stat.ResetStats();
        }
    }

    public static void SetWinningTeam(Team team)
    {
        winningTeam = team;
    }

    public static Team GetWinningTeam()
    {
        return winningTeam;
    }
}
