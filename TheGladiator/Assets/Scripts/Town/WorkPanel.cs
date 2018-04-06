using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ListWorkInfo
{
    public List<Work> workList;
}

[System.Serializable]
public class Work
{
    public string title;
    public string imageName;
    public string desc;
    public long gold;
    public short stamina;
    public float turn;
}

public class WorkPanel : MonoBehaviour
{
    [SerializeField]
    private List<Work> workList = new List<Work>();

    [SerializeField]
    private int currentIndex = 0;

    [SerializeField]
    private int availiableWorkNum = 4;

    public RectTransform Preivious;
    public RectTransform Current;
    public RectTransform Next;
    public GameObject workPopup;
    // Use this for initialization
    private bool bMoving = false;

    public Text goldText;
    public Text staminaText;
    public Text TimeText;
    ListDataInfo playerData;
    void Awake()
    {
        playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        ResetWork();
        bMoving = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ResetWork()
    {
        workList.Clear();
        ListWorkInfo allWorkInfo = MasterManager.ManagerGlobalData.GetAllWorkData();

        int listSize = allWorkInfo.workList.Count;
        int[] randomIndecies = new int[listSize];
        for (int i = 0; i < listSize; ++i)
            randomIndecies[i] = i;

        //random pick
        int temp, randomIndex, actualIndex;
        for (int i = 0; i < availiableWorkNum; ++i)
        {
            randomIndex = Random.Range(0, listSize);
            actualIndex = randomIndecies[randomIndex];

            workList.Add(allWorkInfo.workList[actualIndex]);

            //move the element to the end the list
            temp = randomIndecies[randomIndex];
            randomIndecies[randomIndex] = randomIndecies[listSize-1];
            randomIndecies[listSize-1] = temp;

            listSize--;
        }

        //current work page setup
        SetCurrentWork();
    }

    private void OnEnable()
    {
        //check if there isn't work in the work list
        if(workList.Count == 0)
        {
            MasterManager.ManagerPopup.ShowMessageBox("Hey!", "Don't have work this week\nWait until next weekend", Constants.PopupType.POPUP_NO, OnCloseWindow);
        }
        else
        {
            SetCurrentWork();
        }
    }

    public void OnNext()
    {
        if (bMoving || workList.Count == 1)
            return;

        //set previous page
        int prevIndex = currentIndex - 1;
        if(prevIndex < 0 )
        {
            prevIndex = workList.Count - 1;
        }
        
        SetWorkPage(workList[prevIndex], Preivious);
        StartCoroutine(IE_MoveToRight());
        
    }

    public void OnPrevious()
    {
        if (bMoving || workList.Count == 1)
            return;

        int nextIndex = currentIndex + 1;
        if (nextIndex >= workList.Count)
        {
            nextIndex = 0;
        }

        SetWorkPage(workList[nextIndex], Next);
        StartCoroutine(IE_MoveToLeft());        
    }

    void SetWorkPage(Work workInfo, RectTransform parent)
    {
        parent.GetComponentInChildren<WorkWindow>().SetWorkWindow(workInfo.title, workInfo.desc, workInfo.imageName);
    }

    IEnumerator IE_MoveToLeft()
    {
        bMoving = true;
        Vector3 curPos = Current.GetComponentInChildren<Image>().rectTransform.position;
        Vector3 nextPos = Next.GetComponentInChildren<Image>().rectTransform.position;
        
        Vector3 destForCur = Preivious.position;
        Vector3 destForNext = Current.position;

        float t = 0.0f;
        while (Vector3.Distance(curPos, destForCur) > 0.01f)
        {
            curPos = Vector3.Lerp(curPos, destForCur, t);
            nextPos = Vector3.Lerp(nextPos, destForNext, t);

            Current.GetComponentInChildren<Image>().rectTransform.position = curPos;
            Next.GetComponentInChildren<Image>().rectTransform.position = nextPos;
            t += Time.deltaTime;
            yield return null;
        }


        Debug.Log("Done");
        bMoving = false;
        //go back to original pos
        Current.GetComponentInChildren<Image>().rectTransform.position = Current.position;
        Next.GetComponentInChildren<Image>().rectTransform.position = Next.position;

        //set current page
        currentIndex++;
        if (currentIndex >= workList.Count)
            currentIndex = 0;

        SetCurrentWork();
    }

    IEnumerator IE_MoveToRight()
    {
        //
        bMoving = true;
        Vector3 prevPos = Preivious.GetComponentInChildren<Image>().rectTransform.position;
        Vector3 curPos = Current.GetComponentInChildren<Image>().rectTransform.position;

        Vector3 destForCur = Next.position;
        Vector3 destForPrev = Current.position;
        float t = 0.0f;
        while(Vector3.Distance(curPos, destForCur) > 0.01f)
        {
            curPos = Vector3.Lerp(curPos, destForCur, t);
            prevPos = Vector3.Lerp(prevPos, destForPrev, t);

            Current.GetComponentInChildren<Image>().rectTransform.position = curPos;
            Preivious.GetComponentInChildren<Image>().rectTransform.position = prevPos;
            t += Time.deltaTime;
            yield return null;
        }

       
        Debug.Log("Done");
        bMoving = false;
        //go back to original pos
        Current.GetComponentInChildren<Image>().rectTransform.position = Current.position;
        Preivious.GetComponentInChildren<Image>().rectTransform.position = Preivious.position;

        //set current page
        currentIndex--;
        if (currentIndex < 0)
            currentIndex = workList.Count - 1;

        SetCurrentWork();
    }

    public void OnOK()
    {
        //check stamina
        if (playerData.statsList[0].Stamina < workList[currentIndex].stamina * playerData.playerTier)
        {
            MasterManager.ManagerPopup.ShowMessageBox("Hey!", "Not Enough Stamina", Constants.PopupType.POPUP_NO);
        }
        else
        {
            TownManager.Instance.CloseCurrentWindow(true, CallBackEndWork, workList[currentIndex].turn);
        }

        workPopup.SetActive(false);
    }

    public void OnCancel()
    {
        workPopup.SetActive(false);
    }

    public void OnWork()
    {
        workPopup.SetActive(true);
    }

    public void OnCloseWindow()
    {
        TownManager.Instance.CloseCurrentWindow(false);
    }

    public void CallBackEndWork()
    {
        //make money
        long earnedGold = workList[currentIndex].gold * playerData.playerTier;
        MasterManager.ManagerGlobalData.GetEnvData().gold += earnedGold;
        MasterManager.ManagerGlobalData.SaveEnvData();

        //reduce stamina
        //ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerData.statsList[0].Stamina -= (short)(workList[currentIndex].stamina * playerData.playerTier);
        MasterManager.ManagerGlobalData.SavePlayerData();
       
        MasterManager.ManagerPopup.ShowMessageBox("Hey!", "You made " + earnedGold.ToString() + " gold", Constants.PopupType.POPUP_SYSTEM);

        //update player ui
        TownManager.Instance.UpdatePlayerUI();

        //take out the work from the list
        workList.RemoveAt(currentIndex);
        currentIndex = 0;

        //only if there is still work availiable in the work list
        if (workList.Count > 0)
        {
            SetCurrentWork();
        }
        else
        {
            Current.GetComponentInChildren<WorkWindow>().SetWorkWindow("", "", "");
            goldText.text = "";
            staminaText.text = "";
            TimeText.text = "";
        }
    }

    private void SetCurrentWork()
    {
        if(workList.Count > 0)
        {
            if(playerData == null)
                playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();

            SetWorkPage(workList[currentIndex], Current);
            goldText.text = (workList[currentIndex].gold * playerData.playerTier).ToString("N0");
            staminaText.text = (workList[currentIndex].stamina * playerData.playerTier).ToString() + "  [" + MasterManager.ManagerGlobalData.GetPlayerDataInfo().statsList[0].Stamina.ToString() + "]";
            TimeText.text = ((int)(workList[currentIndex].turn * Constants.HOUR_SPENT)).ToString();
        }
    }
}
