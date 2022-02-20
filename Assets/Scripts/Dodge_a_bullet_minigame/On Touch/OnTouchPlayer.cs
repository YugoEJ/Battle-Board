using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTouchPlayer : MonoBehaviour
{
    public GameManagerScript board;
    public Player player;
    public Player computer;
    public static int playerHealthPoints;
    public bool appliedEffects;
    public bool gameDraw;

    public GameObject WinText;
    public GameObject LoseText;
    public GameObject Draw;

    public static bool isGameOver = true;

    void Update()
    {
        if (board.duringMinigame && !appliedEffects)
        {
            Debug.Log("ontouch player IF in UPDATE");
            Timer.timeValue = 17.5f;

            playerHealthPoints = 1 + player.GetExtraLife();
            OnTouchComputer.computerHealthPoints = 1 + computer.GetExtraLife();

            appliedEffects = true;
            gameDraw = true;
            isGameOver = false;
        }

        if (playerHealthPoints != 0 && OnTouchComputer.computerHealthPoints != 0 && Timer.timeValue <= 0.2f)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            LoseText.gameObject.SetActive(false);
            player.RemoveMinigameWinner();
            computer.RemoveMinigameWinner();
            //board.boardSFX.loseMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }

        /*if (playerHealthPoints != 0 && OnTouchComputer.computerHealthPoints != 0 && isGameOver && !gameDraw)
        {
            Draw.gameObject.SetActive(true);
            WinText.gameObject.SetActive(false);
            LoseText.gameObject.SetActive(false);
            player.RemoveMinigameWinner();
            computer.RemoveMinigameWinner();
            //board.boardSFX.loseMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }*/
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "obstacle")
        {
            Destroy(col.gameObject);

            if (isGameOver == false)
            {
                player.RemoveExtraLife();
                board.boardUI.PlayerExtraLifeText.text = "Extra Life: " + player.GetExtraLife();
                playerHealthPoints--;
                board.boardSFX.hurtMinigameSFX.Play();
            }
        }

        if (playerHealthPoints != 0 && OnTouchComputer.computerHealthPoints == 0 && !isGameOver)
        {
            //isGameOver = true;
            WinText.gameObject.SetActive(true);
            player.SetMinigameWinner();
            computer.RemoveMinigameWinner();
            board.boardSFX.winMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }

        if (playerHealthPoints == 0 && OnTouchComputer.computerHealthPoints != 0 && !isGameOver)
        {
            LoseText.gameObject.SetActive(true);
            player.RemoveMinigameWinner();
            computer.SetMinigameWinner();
            board.boardSFX.loseMinigameSFX.Play();
            StartCoroutine(DelayGoToBoard());
        }
    }

    public IEnumerator DelayGoToBoard()
    {
        board.duringMinigame = false;
        appliedEffects = false;
        gameDraw = false;
        Timer.timeValue = 0.3f;
        isGameOver = true;

        yield return new WaitForSeconds(2f);

        board.minigameCam.enabled = false;
        board.boardCam.enabled = true;
        board.boardSFX.boardBGM.Play();
        board.boardSFX.minigameBGM.Stop();
        board.boardUI.ShowAllTexts();
        HideTexts();
    }

    public void HideTexts()
    {
        WinText.gameObject.SetActive(false);
        LoseText.gameObject.SetActive(false);
        Draw.gameObject.SetActive(false);
    }
}
