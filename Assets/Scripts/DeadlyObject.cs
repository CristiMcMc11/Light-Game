using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(collision.gameObject.GetComponent<PlayerManager>().Die());
        }
    }
}
