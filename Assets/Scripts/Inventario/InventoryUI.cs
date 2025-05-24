using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Referencias")]
    public PlayerInventory playerInventory;      // Se puede asignar manualmente o automáticamente
    public Image[] slotImages;                   // Marcos visuales de los slots
    public Image[] itemIcons;                    // Iconos de los objetos en cada slot

    [Header("Colores")]
    public Color activeColor = Color.yellow;     // Slot activo
    public Color inactiveColor = Color.white;    // Slots inactivos

    private int currentSlotIndex = 0;

    private void Awake()
    {
        // Autoasignación si se olvidó conectar en el inspector
        if (playerInventory == null)
        {
            playerInventory = GetComponentInParent<PlayerInventory>();
        }
    }

    private void Start()
    {
        UpdateInventoryUI();
    }

    public void SetCurrentSlot(int index)
    {
        currentSlotIndex = index;
        UpdateInventoryUI();
    }

    public void UpdateInventoryUI()
    {
        if (playerInventory == null || slotImages == null || itemIcons == null)
        {
            Debug.LogWarning("InventoryUI: Faltan referencias asignadas.");
            return;
        }

        for (int i = 0; i < slotImages.Length; i++)
        {
            if (i >= playerInventory.items.Length) break;

            InventoryItem item = playerInventory.GetItem(i);
            if (item != null)
            {
                itemIcons[i].sprite = item.icon;
                itemIcons[i].enabled = true;
            }
            else
            {
                itemIcons[i].enabled = false;
            }

            slotImages[i].color = (i == currentSlotIndex) ? activeColor : inactiveColor;
        }
    }
}
