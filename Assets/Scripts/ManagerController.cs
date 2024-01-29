using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerController : MonoBehaviour
{
    public TMP_InputField goalAmount_InputText;
    public TMP_InputField currentAmount_InputText;
    public TMP_InputField todaysAmount_InputText;
    public TMP_InputField sessionNumber_InputText;

    private string previousLog_CurrentAmount_Text;

    private int goalAmount;
    private int currentAmount;
    private int todaysAmount;
    private int previousLog_CurrentAmount;

    private GameObject lastSpawned;
    private GameObject previousLog;
    public GameObject logSpawner;
    public Transform logSpawnPosition;

    public ScrollRect scrollRectBG;

    [SerializeField] public List<GameObject> wordLog = new List<GameObject>();

    // TODO: set goal value to your goal number
    // set curent value to how many words currently done
    // minus current value from goal value
    // todays value ?? calculate difference between old current value and new current value

    // print out ( list ) LOG** of numbers

    /// <summary>
    /// - set int values to the text values ( otherwise they equal nothing )
    /// - calculate // TODO:
    /// - set text values to the int values
    /// 
    /// - instantiate log of that day
    /// - add it to the hashset
    /// - set the position of a new log
    /// </summary>
    public void WordGoalDecrease()
    {
        goalAmount = int.Parse(goalAmount_InputText.text);
        currentAmount = int.Parse(currentAmount_InputText.text);
        todaysAmount = int.Parse(todaysAmount_InputText.text);

        goalAmount -= currentAmount;
        todaysAmount = currentAmount - previousLog_CurrentAmount;

        // --------------

        lastSpawned = Instantiate(logSpawner, logSpawnPosition);
        wordLog.Add(lastSpawned);

        foreach (GameObject spawnedLogs in wordLog)
        {
            spawnedLogs.transform.position += new Vector3(0, -40f, 0);

            var _currentAmount = lastSpawned.transform.Find("CurrentCount_Text");
            _currentAmount.GetComponent<TMP_Text>().text = currentAmount.ToString();

            var _todaysAmount = lastSpawned.transform.Find("TodaysCount_Text");
            _todaysAmount.GetComponent<TMP_Text>().text = todaysAmount.ToString();

            var _sessionNumber = lastSpawned.transform.Find("SessionNumber_Text");
            _sessionNumber.GetComponent<TMP_Text>().text = wordLog.Count.ToString();
        }

        previousLog = wordLog[wordLog.Count - 1];
        previousLog_CurrentAmount_Text = previousLog.transform.Find("CurrentCount_Text").GetComponent<TMP_Text>().text;
        previousLog_CurrentAmount = int.Parse(previousLog_CurrentAmount_Text);

        goalAmount_InputText.text = goalAmount.ToString();
        currentAmount_InputText.text = currentAmount.ToString();
        todaysAmount_InputText.text = todaysAmount.ToString();
        currentAmount_InputText.text = "";
        //todaysAmount_InputText.text = "";
    }

    // delete log button 
    // TODO: move list position back up when removing a log
    public void DeleteLog()
    {
        previousLog = wordLog[wordLog.Count - 1];
        wordLog.Remove(previousLog);
        Destroy(previousLog);
    }

    // 
    public void ScrollingMinMax()
    {
        var height = scrollRectBG.preferredHeight;
        foreach (GameObject spawnedLogs in wordLog)
        {
            if (spawnedLogs.transform.position.y >= scrollRectBG.preferredHeight) // max 
            {
                Debug.Log("above");
            }

            if (spawnedLogs.transform.position.y <= scrollRectBG.minHeight) // min 
            {
                Debug.Log("below");
            }
        }
    }
}