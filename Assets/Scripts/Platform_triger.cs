using UnityEngine;

public class Platform_triger : MonoBehaviour
{
    public GameObject Spawn;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Die_Or_Reposition(collision.gameObject);
    }

    private void Die_Or_Reposition(GameObject Object)
    {
        if (Object.CompareTag("Player"))
        {
            Object.SetActive(false);
        }
        else if (Object.CompareTag("Platform"))
        {
            Object.transform.position = Mover.Random_Place(new Vector3(Object.transform.position.x, Spawn.transform.position.y, Spawn.transform.position.z));
        }
    }
}
