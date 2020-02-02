using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    enum GAME_STATE { RUNNING, PAUSED};

    [SerializeField]
    GameObject groundObject;

    [SerializeField]
    GAME_STATE gameState = GAME_STATE.RUNNING;

    [SerializeField]
    enum MENU_STATE
    {
        RESUME,
        EXIT
    };

    [SerializeField]
    GameObject resumeButton;

    [SerializeField]
    GameObject exitButton;

    [SerializeField]
    MENU_STATE currentState = MENU_STATE.EXIT;

    [SerializeField]
    GameObject pauseCanvas;

    
    Dictionary<PICK_UP, int> itemsInIventory = new Dictionary<PICK_UP, int>();

    [SerializeField]
    GameObject portal;

    [SerializeField]
    GameObject redGemBubble;

    [SerializeField]
    GameObject greenGemBubble;

    [SerializeField]
    GameObject blueGemBubble;
    // Start is called before the first frame update
    void Start()
    {
        //Initialize Dictionary
        var values = PICK_UP.GetValues(typeof(PICK_UP));
        foreach(PICK_UP val in values)
        {
            itemsInIventory.Add(val, 0);
        }
        itemsInIventory[PICK_UP.WRENCH] = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            handlePause();
        }

        if(gameState == GAME_STATE.PAUSED)
        {
            //TODO: add the PS4 controls
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
            {
                changeOption();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                performAction();
            }
        }
        
    }

    void performAction()
    {
        switch (currentState)
        {
            default:
            case MENU_STATE.RESUME:
                handlePause();
                break;
            case MENU_STATE.EXIT:
                Time.timeScale = 1f;
                SceneManager.LoadScene(0);
                break;
        }
    }

    //Change the option of the menu 
    void changeOption()
    {
        switch (currentState)
        {
            default:
            case MENU_STATE.RESUME:
                currentState = MENU_STATE.EXIT;
                resumeButton.GetComponent<Option>().setOption(false);
                exitButton.GetComponent<Option>().setOption(true);
                break;
            case MENU_STATE.EXIT:
                currentState = MENU_STATE.RESUME;
                resumeButton.GetComponent<Option>().setOption(true);
                exitButton.GetComponent<Option>().setOption(false);
                break;
        }
    }

    void handlePause()
    {
        if(gameState == GAME_STATE.RUNNING)
        {
            gameState = GAME_STATE.PAUSED;
            pauseCanvas.SetActive(true);
            changeOption();
            Time.timeScale = 0f;
        }
        else if(gameState == GAME_STATE.PAUSED)
        {
            gameState = GAME_STATE.RUNNING;
            currentState = MENU_STATE.EXIT;
            Time.timeScale = 1f;
            pauseCanvas.SetActive(false);
        }
    }

    //Checks if the players has the determined item
    public bool hasRequiredObject(PICK_UP item)
    {
        int amount = 0;
        itemsInIventory.TryGetValue(item, out amount);
        return (amount > 0);
    }

    //Removes item after using it
    public void removeItem(PICK_UP item)
    {
        if(itemsInIventory[item] > 0)
            itemsInIventory[item] = itemsInIventory[item] - 1;
    }
    
    public void pickUpGem(PICK_UP gemType)
    {
        switch(gemType)
        {
            default:
            case (PICK_UP.BLUE_GEM):
                Destroy(blueGemBubble);
                portal.GetComponent<SwirlControl>().pickedBlue();
                break;
            case (PICK_UP.RED_GEM):
                Destroy(redGemBubble);
                portal.GetComponent<SwirlControl>().pickedRed();
                break;
            case (PICK_UP.GREEN_GEM):
                Destroy(greenGemBubble);
                portal.GetComponent<SwirlControl>().pickedGreen();
                break;
        }
    }
}
