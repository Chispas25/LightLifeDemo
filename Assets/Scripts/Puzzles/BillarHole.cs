using UnityEngine;

public class BillarHole : MonoBehaviour
{
    public string correctBallTag = "WhiteBall";
    public BillarPuzzle puzzleController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(correctBallTag))
        {
            puzzleController.CompletePuzzle();
            Destroy(collision.gameObject); // opcional
        }
    }
}
