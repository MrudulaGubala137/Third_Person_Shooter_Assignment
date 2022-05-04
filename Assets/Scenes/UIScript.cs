
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Button play;
    public Button go;
    public Button settings;
    public Button back;
    public GameObject instructionsPannel;
    public GameObject homePanel;
   
    void Start()
    {
        
        play.onClick.AddListener(Play);
        settings.onClick.AddListener(Settings);
        back.onClick.AddListener(Instructions);
        go.onClick.AddListener(GoToHome);
    }

    // Update is called once per frame

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Instructions()
    {
        instructionsPannel.SetActive(true);
        homePanel.SetActive(false);
        back.gameObject.SetActive(true);
    }
    public void Settings()
    {

    }
    public void GoToHome()
    {
        homePanel.SetActive(true);
        instructionsPannel.SetActive(false );

    }
   

}

