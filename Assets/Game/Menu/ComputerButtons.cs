using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComputerButtons : MonoBehaviour {
    //public
    public UnityEngine.UI.Text textBoxStart;
    public GameObject StartCanvas;
    public GameObject HistoryCanvas;

    //private
    private string StartMessage = "Welcome back, I've missed you so!\n\n\n";
	private string StartText = "What do you want to do today? (^_^)";

   

    private string PurgeText = "Wait, did you just delete all of Borgs research?!";
    private string GeneratorKillText = "Wait, what are you doing?!\n...\n...\n...YOU KILLED ME!";
	private string noBackLogText = "History is empty !";
	private bool historyPurged = false;

    private float writeSpeed = 0.02f;

	// Use this for initialization
	void Start () {
		StartCanvas.GetComponent<Canvas>().enabled = true;
		HistoryCanvas.GetComponent<Canvas>().enabled = false;
		StartCoroutine(writeText(textBoxStart, StartMessage+StartText, writeSpeed));
	}

    void OnEnable()
    {
        StartCoroutine(writeText(textBoxStart, StartText, writeSpeed));
    }

    void SwitchToHistory()
    {
		StartCanvas.GetComponent<Canvas>().enabled = false;
		HistoryCanvas.GetComponent<Canvas>().enabled = true;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void HistroyButton()
    {
		if (historyPurged)
			StartCoroutine(writeText(textBoxStart, noBackLogText, writeSpeed));
		else
			SwitchToHistory();

    }

    public void PurgeButton()
    {
		historyPurged = true;
        textBoxStart.text = "";
         StartCoroutine(writeText(textBoxStart, PurgeText, writeSpeed));
    }

    public void KillGeneratorButton()
    {
        textBoxStart.text = "";
        StartCoroutine(writeText(textBoxStart, GeneratorKillText, writeSpeed));
		Invoke ("goToMenu", 3);
    }

    public void ExitTerminalButton()
    {
		goToMenu();
    }

    public void activateStartScreen()
    {
        StartCoroutine(writeText(textBoxStart, StartText, writeSpeed));
    }

    IEnumerator writeText(Text obj, string text, float speed)
    {
        string gradualText = "";
        obj.text = "_";
        yield return new WaitForSeconds(0.2f);
        obj.text = " ";
        yield return new WaitForSeconds(0.2f);
        obj.text = "_";
        yield return new WaitForSeconds(0.2f);
        obj.text = " ";
        yield return new WaitForSeconds(0.2f);
        obj.text = "";
        for (int i = 0; i < text.Length; i++)
        {
            gradualText += text[i];
            obj.text = gradualText + "_";
            yield return new WaitForSeconds(speed);
        }
        obj.text = text;
    }

	private void goToMenu() {
		Application.LoadLevel("Menu");
	}
}
