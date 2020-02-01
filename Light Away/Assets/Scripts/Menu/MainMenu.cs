using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    enum MENU_STATE{
        START,
        EXIT
    };

    [SerializeField]
    GameObject startButton;

    [SerializeField]
    GameObject exitButton;

    [SerializeField]
    MENU_STATE currentState = MENU_STATE.EXIT;

    // Start is called before the first frame update
    void Start()
    {
        changeOption();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: add the PS4 controls
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W))
         {
            changeOption();
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
            performAction();
        }
    }

    void performAction()
    {
        switch (currentState)
        {
            default:
            case MENU_STATE.START:
                SceneManager.LoadScene(1);
                break;
            case MENU_STATE.EXIT:
                Application.Quit();
                break;
        }
    }
    
     //Change the option of the menu 
    void changeOption()
    {
        switch (currentState)
        {
            default:
            case MENU_STATE.START:
                currentState = MENU_STATE.EXIT;
                startButton.GetComponent<Option>().setOption(false);
                exitButton.GetComponent<Option>().setOption(true);
                break;
            case MENU_STATE.EXIT:
                currentState = MENU_STATE.START;
                startButton.GetComponent<Option>().setOption(true);
                exitButton.GetComponent<Option>().setOption(false);
                break;
        }
    }
}
