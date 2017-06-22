using System.Collections;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MyButton : MonoBehaviour {
    public UnityEvent signalOnClick = new UnityEvent();
    public MyButton playButton;
    public void _onClick() {
        
        this.signalOnClick.Invoke();
    }
   
    void Start()
    {
        playButton.signalOnClick.AddListener(this.OnPlay);
    }
    void OnPlay()
    {
        SceneManager.LoadScene("ChoseLVL");
    }
}
