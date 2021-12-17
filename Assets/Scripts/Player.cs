using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Route currentRoute;

    public float speed = 40f;

    int routePos;

    int dice;
    int stepsToTake;

    public static bool isPlayersTurn;
    public static bool isPlayerWinner;
    bool isMoving;

    private void Start()
    {
        isPlayersTurn = true;
        isPlayerWinner = false;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving && isPlayersTurn && !isPlayerWinner)
        {
            RollDice();

            if ((routePos + stepsToTake) < (currentRoute.childObjectList.Count - 1))                // if the amount of steps to take does not overflow, move player piece
            {
                StartCoroutine(Move());
            } 
            else
            {
                StartCoroutine(Move());
                isPlayerWinner = true;
                Debug.Log("player wins");

                // announce winner
            }
        }
    }

    IEnumerator Move()
    {
        if (isMoving)
        {
            yield break;
        }
        isMoving = true;


        while (stepsToTake > 0 && (routePos != (currentRoute.childObjectList.Count - 1)))
        {
            Vector3 initialPos = currentRoute.childObjectList[routePos].position;

            routePos++;

            Vector3 finalPos = currentRoute.childObjectList[routePos].position;
            Vector3 nextPos = transform.position;
            
            //--------first-------step--------------//

            // first step coordinates
            if (IsEdgePos(routePos - 1))
            {
                nextPos.z = finalPos.z;
            }
            else
            {
                nextPos.x = finalPos.x;
            }
            nextPos.y = 8f;


            // first step (only Y changes)
            while (transform.position != nextPos)
            {
                MoveToNextNode(nextPos);
                yield return null;
            }

            //---------second-------step-------------//

            // second step coordinates
            if (IsEdgePos(routePos - 1))
            {
                nextPos.z = finalPos.z;
                nextPos.x = (finalPos.x + initialPos.x) / 2;
            }
            else
            {
                nextPos.x = finalPos.x;
                nextPos.z = (finalPos.z + initialPos.z) / 2;
            }
            nextPos.y = 12f;

            // second step (Y changes to slightly higher, and X/Z changes to ((initialPos.X/Z + finalPos.X/Z) / 2)
            while (transform.position != nextPos)
            {
                MoveToNextNode(nextPos);
                yield return null;
            }
            
            //---------third-----step----------------//

            // third step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 8f;
            nextPos.z = finalPos.z;

            // third step (Y changes to slightly lower, and X/Z changes to (finalPos.X/Z)
            while (transform.position != nextPos)
            {
                MoveToNextNode(nextPos);
                yield return null;
            }

            //---------final------step---------------//

            // final step coordinates
            nextPos.x = finalPos.x;
            nextPos.y = 5.5f;
            nextPos.z = finalPos.z;

            // final step (Y changes to original height (5.5f))
            while (transform.position != nextPos)
            {
                MoveToNextNode(nextPos);
                yield return null;
            }

            yield return new WaitForSeconds(0.1f);

            stepsToTake--;
        }

        isMoving = false;

        // check if landed on final tile; announce winner

        // check if landed on a special tile; apply special effect / initiate minigame / etc.

        isPlayersTurn = false;
    }

    void MoveToNextNode(Vector3 goalNode)
    {
        transform.position = Vector3.MoveTowards(transform.position, goalNode, speed * Time.deltaTime);
    }

    // IsEdgePos() checks (for moving animation) whether to calculate for X or Z axes
    bool IsEdgePos(int currNode)
    {
        bool isEdgePos = false;

        while (currNode > 0)
        {
            if (currNode - 12 == 11 || currNode == 11)
            {
                isEdgePos = true;
                break;
            }

            currNode -= 12;
        }

        return isEdgePos;
    }

    void RollDice()
    {
        dice = Random.Range(1, 7);
        stepsToTake = dice;

        Debug.Log("Player rolled dice: " + stepsToTake);
    }
}
