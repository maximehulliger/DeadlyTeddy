using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HistoryCanvas : MonoBehaviour {
    //public
    public UnityEngine.UI.Text textBoxHistory;
    public GameObject StartCanvas;
    public GameObject HistoryScreen;

    //privates
    private string HistoryText = "Backlog entires dating back to 1939.\n\n\n What would you like to see?";

    // Should we make it into diary entries, or more like research logs?
    //    private string historyFirst = "Research logs - 1939.05.06. \n\n" +
    private string historyFirst = "1939.05.06 - Research logs.\n\n" +
                                    "There hasn't been any progress in days. The blueprints are gathering dust " +
                                    "and all I can do is pray for his return. " +
                                    "I will continue melding the head piece in his abscence " +
                                    "in order to keep my mind busy. \n\n --Bork";

    private string historySecond = "1939.08.06 - Research logs.\n\n" +
                                    "The letter I received today confirmed my worst fear. Father has been " +
                                    "forced to join the Führers army. He told me that they are planning on " +
                                    "invading the neighbouring country to the east and that he doesn't know when " +
                                    "he'll be coming back. I'm awaiting my uncles arrival who " +
                                    "he will take care of me in my fathers absence. " +
                                    "I hope he lets me bring the research with me. " +
                                    " --Bork";

    private string historyThird = "1940.05.06 - Research logs.\n\n" +
                                    "War rages on but it has not reached our doorstep yet. " +
                                    "I have decided to continue my fathers research, supervised by my uncle " +
                                    "who also shares my passion for machines and puzzles. We have managed to assemble " +
                                    "the torso container which will host the generator powering the machines. " +
                                    "Our attempts to stabilizie the core has proven to be difficult - " +
                                    "but we are making progress. --Bork";


    private float writeSpeed = 0.02f;

	// Use this for initialization
	/*void Start () {
        StartCanvas.SetActive(true);
        HistoryScreen.SetActive(false);
        StartCoroutine(writeText(textBoxHistory, HistoryText, writeSpeed));
	}*/

    //Used when object is enabled
    void OnEnable()
    {
        StartCoroutine(writeText(textBoxHistory, HistoryText, writeSpeed));
    }
	// Update is called once per frame
	void Update () {
	
	}

    /*
     * Switches to the startscreen.
     * 
     */
    void SwitchToStart()
    {
		StartCanvas.GetComponent<Canvas>().enabled = true;
		HistoryScreen.GetComponent<Canvas>().enabled = false;
    }

    /*
     * Scripts run when buttons are pressed.
     * Each prints a certain String to the Canvas textBox.
     */
    public void HistoryFirst()
    {
        textBoxHistory.text = "";
        StartCoroutine(writeText(textBoxHistory, historyFirst, writeSpeed));
    }

    public void HistorySecond()
    {
        textBoxHistory.text = "";
        StartCoroutine(writeText(textBoxHistory, historySecond, writeSpeed));
    }

    public void HistoryThird()
    {
        textBoxHistory.text = "";
        StartCoroutine(writeText(textBoxHistory, historyThird, writeSpeed));
    }

    public void BackButton()
    {
        SwitchToStart();
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

}
