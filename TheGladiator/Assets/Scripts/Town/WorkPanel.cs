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
    

    void Start()
    {
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
        int temp, randomIndex;
        for (int i = 0; i < availiableWorkNum; ++i)
        {
            randomIndex = Random.Range(0, listSize);
            workList.Add(allWorkInfo.workList[randomIndex]);

            //move the element to the end the list
            temp = randomIndecies[randomIndex];
            randomIndecies[randomIndex] = randomIndecies[listSize-1];
            randomIndecies[listSize-1] = temp;

            listSize--;
        }

        //current work page setup
       SetWorkPage(workList[currentIndex], Current);
    }

    private void OnEnable()
    {
        //set up current work
    }

    public void OnNext()
    {
        if (bMoving)
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
        if (bMoving)
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

        SetWorkPage(workList[currentIndex], Current);
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

       SetWorkPage(workList[currentIndex], Current);
    }

    public void OnOK()
    {
        //check stamina
        ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        if (playerData.statsList[0].Stamina < workList[currentIndex].stamina)
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
        long earnedGold = workList[currentIndex].gold;
        MasterManager.ManagerGlobalData.GetEnvData().gold += earnedGold;
        MasterManager.ManagerGlobalData.SaveEnvData();

        //reduce stamina
        ListDataInfo playerData = MasterManager.ManagerGlobalData.GetPlayerDataInfo();
        playerData.statsList[0].Stamina -= workList[currentIndex].stamina;
        MasterManager.ManagerGlobalData.SavePlayerData();
       
        MasterManager.ManagerPopup.ShowMessageBox("Hey!", "You made " + earnedGold.ToString() + " gold", Constants.PopupType.POPUP_SYSTEM);

        //take our the work from the list
        workList.RemoveAt(currentIndex);
    }
}
