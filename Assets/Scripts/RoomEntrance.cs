using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using UnityEngine.UIElements;

public class RoomEntrance : MonoBehaviour
{
    [SerializeField] public string connectedSceneName;
    public bool caveStart;
    public GameObject blackScreen;

    private BoxCollider2D boxCollider;

    public bool checkForPlayer = true;
    private float transitionDuration = 0.4f;

    public enum Direction
    {
        Right, 
        Left 
    };
    public Direction playerEnterDirection = Direction.Right;

    //Setting checkForPlayer to true when player leaves the collision box
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            checkForPlayer = true;
        }
    }

    //Exiting room on touch
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (checkForPlayer)
            {
                //Cutscene and teleport
                StartCoroutine(ExitRoom(collision.gameObject));
            }
        }
    }

    private IEnumerator ExitRoom(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        GameData.Instance.previousSceneName = SceneManager.GetActiveScene().name;

        //Moving player manually based off direction
        if (playerEnterDirection == Direction.Left)
        {
            playerMovement.ManualMove(-1);
        }
        else
        {
            playerMovement.ManualMove(1);
        }

        //doing black screen thingy
        blackScreen.transform.position = new Vector2(player.transform.position.x, player.transform.position.y);
        blackScreen.LeanAlpha(1, transitionDuration);

        yield return new WaitForSeconds(transitionDuration);

        SceneManager.LoadScene(connectedSceneName);
    }

    public IEnumerator EnterRoom(GameObject player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        //making sure the black screen is active cuz i disactivate it cuz its super annoying ts pmo
        blackScreen.SetActive(true);
        blackScreen.GetComponent<SpriteRenderer>().color = Color.black;

        //Manually moving player
        if (playerEnterDirection == Direction.Left)
        {
            playerMovement.ManualMove(1);
        }
        else
        {
            player.transform.rotation = new Quaternion(0, 180, 0, 0);
            playerMovement.ManualMove(-1);
        }

        if (caveStart)
        {
            playerMovement.StopManualMovement();
        }

        yield return new WaitForSeconds(0.25f); //hides the camera moving to the player

        //black screen thingy
        blackScreen.LeanAlpha(0, transitionDuration);

        yield return new WaitForSeconds(transitionDuration);

        playerMovement.StopManualMovement();
    }

    //start coroutine wasn't working :(
    public void Enter(GameObject player)
    {
        StartCoroutine(EnterRoom(player));
    }
}