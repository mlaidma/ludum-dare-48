using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameController : MonoBehaviour
{

    [SerializeField] GameObject startMenu;


    private bool gameStarted = false;
    private Dwarf player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType(typeof(Dwarf)) as Dwarf;

        if(player == null) Debug.LogError("Player not found");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) PauseGame();
    }

    public void StartGame()
    {
        gameStarted = true;
        player.GameOn(true);
        startMenu.SetActive(false);
    }

    public void PauseGame()
    {
        gameStarted = false;
        player.GameOn(false);
        startMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
