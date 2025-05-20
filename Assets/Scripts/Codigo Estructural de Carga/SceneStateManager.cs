using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStateManager : MonoBehaviour
{
    public static SceneStateManager Instance;

    public HashSet<string> completedPuzzles = new HashSet<string>();
    public HashSet<string> defeatedEnemies = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void MarkPuzzleComplete(string puzzleId)
    {
        completedPuzzles.Add(puzzleId);
    }

    public bool IsPuzzleComplete(string puzzleId)
    {
        return completedPuzzles.Contains(puzzleId);
    }

    void Start()
{
    if (SceneStateManager.Instance.IsPuzzleComplete("lavadoraPuzzle"))
    {
        // Desactivar lavadora o hacerla mostrar como resuelta
    }
}
}
