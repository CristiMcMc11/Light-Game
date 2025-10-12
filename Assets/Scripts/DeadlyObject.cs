using System.Collections;
using UnityEngine;

public class DeadlyObject : MonoBehaviour
{
    public Vector2 playerVelocityOnKill = new Vector2(0, 10);
    private bool killActive = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && killActive)
        {
            StartCoroutine(KillPlayer(collision));
        }
    }

    private IEnumerator KillPlayer(Collider2D collision)
    {
        killActive = false;
        yield return StartCoroutine(collision.gameObject.GetComponent<PlayerManager>().Die(playerVelocityOnKill));
        killActive = true;
    }
}
