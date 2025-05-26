using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    // 🔹 Singleton para poder acceder desde cualquier parte
    public static DialogueManager Instance { get; private set; }
    private System.Action onDialogueEnd;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;
    public List<string> dialogueLines;
    private int currentLine = 0;

    private bool isDialogueActive = false;

    private void Awake()
    {
        // 🔸 Asegurarse de que solo hay uno
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        // Solo arranca diálogo si hay líneas iniciales
        if (dialogueLines != null && dialogueLines.Count > 0)
        {
            StartDialogue(dialogueLines, true);
        }
    }

    public void StartDialogue(List<string> newLines, bool freezeTime, System.Action callback = null)
    {
        dialogueLines = newLines;
        currentLine = 0;
        isDialogueActive = true;
        dialoguePanel.SetActive(true);

        if (freezeTime)
            Time.timeScale = 0f;

        onDialogueEnd = callback;

        ShowLine();
    }

    void ShowLine()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    void Update()
    {
        if (!isDialogueActive) return;

        // Captura tecla espacio incluso si Time.timeScale == 0
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            currentLine++;

            if (currentLine < dialogueLines.Count)
            {
                ShowLine();
            }
            else
            {
                EndDialogue();
            }
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;

        // ✅ Ejecuta acción tras diálogo
        onDialogueEnd?.Invoke();
        onDialogueEnd = null;
    }
}
