using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public BallShooter Billar;

    // Start is called before the first frame update
    public int totalBolas = 1;
    public int bolasMetidas = 0;

    public void BolaMetida() {
        bolasMetidas++;
        if (bolasMetidas >= totalBolas)
        {
            Debug.Log("¡Puzle resuelto!");
            //Destroy(Billar.gameObject);
            // Mostrar mensaje, pasar de nivel, etc.
        }
    }
}
