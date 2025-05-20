using UnityEngine;

public class BillarPuzzle : MonoBehaviour
{
    public Rigidbody2D[] billiardBalls;
    public Transform[] ballLaunchPoints;
    public float launchForce = 3f;

    public GameObject rewardPrefab;
    public Transform rewardSpawnPoint;

    private bool puzzleStarted = false;

    public void StartPuzzle()
    {
        if (puzzleStarted) return;
        puzzleStarted = true;

        LaunchBalls();
    }

    void LaunchBalls()
    {
        for (int i = 0; i < billiardBalls.Length && i < ballLaunchPoints.Length; i++)
        {
            billiardBalls[i].transform.position = ballLaunchPoints[i].position;
            Vector2 direction = Random.insideUnitCircle.normalized;
            billiardBalls[i].AddForce(direction * launchForce, ForceMode2D.Impulse);
        }
    }

    public void CompletePuzzle()
    {
        if (rewardPrefab != null && rewardSpawnPoint != null)
        {
            Instantiate(rewardPrefab, rewardSpawnPoint.position, Quaternion.identity);
            Debug.Log("Puzzle de billar completado. Objeto generado.");
        }
    }
}
