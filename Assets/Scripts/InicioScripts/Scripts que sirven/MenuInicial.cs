using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class MenuInicial : MonoBehaviour
{

    [Header("Opciones Generales")]
    [SerializeField] float tiempoCambiaOpcion;
    [SerializeField] GameObject panelInicio;
    [SerializeField] GameObject panelOpciones;




    [Header("Elementos de menu")]
    [SerializeField] SpriteRenderer jugar;
    [SerializeField] SpriteRenderer opciones;
    [SerializeField] SpriteRenderer salir;


    [Header("Sprites de menu")]
    [SerializeField] Sprite jugar_off;
    [SerializeField] Sprite jugar_on;

    [SerializeField] Sprite opciones_off;
    [SerializeField] Sprite opciones_on;

    [SerializeField] Sprite salir_off;

    [SerializeField] Sprite salir_on;

    [Header("Sprites de opciones")]
    [SerializeField] Sprite musica_off;
    [SerializeField] Sprite musica_on;
    [SerializeField] Sprite sonido_off;
    [SerializeField] Sprite sonido_on;
    [SerializeField] Sprite volver_off;
    [SerializeField] Sprite volver_on;
    [SerializeField] Sprite vol_off;
    [SerializeField] Sprite vol_on;
    [SerializeField] SpriteRenderer[] musica_spr;
    [SerializeField] SpriteRenderer[] sonido_spr;


    [Header("sonidos")]
    [SerializeField] AudioSource musicaMenu;
    [SerializeField] AudioSource snd_opcion;
    [SerializeField] AudioSource snd_seleccion;



    int pantalla;
    int opcionMenu, opcionMenuAnt;
    int opcionOpciones, opcionOpcionesAnt;
    bool pulsadoSubmit;
    float v, h;
    float tiempoV, tiempoH;

    void Awake()
    {
        pantalla = 0;
        tiempoV = tiempoH = 0;
        opcionMenu = opcionMenuAnt = 1;
        //opcionOpciones = opcionOpcionesAnt = 1;
        AjustaOpciones();

    }

    void AjustaOpciones()
    {

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        v = Input.GetAxisRaw("Vertical");
        h = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonUp("Submit")) pulsadoSubmit = false;
        if (v == 0) tiempoV = 0;
        if (pantalla == 0) MenuPrincipal();

    }

    void MenuPrincipal()
    {
        if (v != 0)
        {
            if (tiempoV == 0 || tiempoV > tiempoCambiaOpcion)
            {

                if (v == 1 && opcionMenu > 1) SeleccionaMenu(opcionMenu - 1);
                if (v == -1 && opcionMenu > 3) SeleccionaMenu(opcionMenu + 1);
                if (tiempoV > tiempoCambiaOpcion) tiempoV = 0;
            }
            tiempoV += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        { }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {

            if (Input.GetKeyDown(KeyCode.Return)) // Intro grande o Intro normal
            {
                snd_seleccion.Play();
                EjecutarOpcionSeleccionada();
            }

            if (Input.GetKeyDown(KeyCode.Escape)) // ESC para salir o volver
            {
                Application.Quit(); // o lógica de volver si estás en otro menú
            }


        }
    }

    void SeleccionaMenu(int op)
    {
        snd_opcion.Play();
        opcionMenu = op;
        if (op == 1) jugar.sprite = jugar_on;
        if (op == 2) opciones.sprite = opciones_on;
        if (op == 3) salir.sprite = salir_on;


        if (opcionMenuAnt == 1) jugar.sprite = jugar_off;
        if (opcionMenuAnt == 2) opciones.sprite = opciones_off;
        if (opcionMenuAnt == 3) salir.sprite = salir_off;
        opcionMenuAnt = op;

    }



    void EjecutarOpcionSeleccionada()
    {
        if (opcionMenu == 1)
        {
            // JUGAR
            Debug.Log("JUGAR");
            // Aquí puedes cargar la escena correspondiente si ya la tienes
            // SceneManager.LoadScene("NombreDeTuEscena");
        }
        else if (opcionMenu == 2)
        {
            // OPCIONES
            Debug.Log("OPCIONES");
            pantalla = 1;
            panelInicio.SetActive(false);
            panelOpciones.SetActive(true);
        }
        else if (opcionMenu == 3)
        {
            // SALIR
            Debug.Log("SALIR");
            Application.Quit();
        }



    }
}
