using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/KeyItem")]
public class KeyItemV : InventoryItem
{
    public string keyID;

    public override bool Use(GameObject user)
    {
        // Busca si hay alguna puerta cerca que acepte esta llave
        Door[] doors = GameObject.FindObjectsOfType<Door>();

        foreach (Door door in doors)
        {
            if (Vector2.Distance(door.transform.position, user.transform.position) < 2.5f)
            {
                if (door.TryOpen(keyID))
                {
                    Debug.Log("Puerta abierta con la llave correcta.");
                    return true; // ✅ La llave se consume
                }
            }
        }

        Debug.Log("No hay ninguna puerta cercana que acepte esta llave.");
        return false; // ❌ No se consume
    }
}
