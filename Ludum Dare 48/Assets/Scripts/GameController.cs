using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GameController : MonoBehaviour
{

    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject gameMenu;

    [SerializeField] TextMeshProUGUI depthText;
    [SerializeField] TextMeshProUGUI gemsText;


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

        GetPlayerDepth();
        GetPlayerGems();
    }

    public void StartGame()
    {
        gameStarted = true;
        player.GameOn(true);
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
    }

    public void PauseGame()
    {
        gameStarted = false;
        player.GameOn(false);
        startMenu.SetActive(true);
        gameMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void GetPlayerDepth()
    {
        depthText.SetText("Depth: " + player.PlayerDepth.ToString() + " M");
    }

    private void GetPlayerGems()
    {
        gemsText.SetText("Gems: " + player.PlayerGems.ToString());
    }
}
