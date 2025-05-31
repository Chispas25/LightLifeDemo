using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;   // Sonido al pasar el cursor
    public AudioClip clickSound;   // Sonido al hacer clic

    private AudioSource _audioSource;

    void Start()
    {
    _audioSource = FindObjectOfType<AudioSource>();

    if (_audioSource == null)
    {
        Debug.LogWarning("No hay AudioSource en la escena.");
    }

    _audioSource.loop = true;
    _audioSource.volume = 1f;
    _audioSource.Play();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(hoverSound);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickSound != null && _audioSource != null)
        {
            _audioSource.PlayOneShot(clickSound);
        }
    }
}