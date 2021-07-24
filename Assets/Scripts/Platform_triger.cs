using UnityEngine;

public class Platform_triger : MonoBehaviour
{
    public GameObject Spawn;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Die_Or_Reposition(collision.gameObject);
    }

    private void Die_Or_Reposition(GameObject @object)
    {
        if (@object.CompareTag("Player"))
        {
            @object.SetActive(false);
        }
        else if (@object.CompareTag("Platform"))
        {
            @object.transform.position = Mover.Random_Place(new Vector3(@object.transform.position.x, Spawn.transform.position.y, Spawn.transform.position.z));
        }
    }
}
