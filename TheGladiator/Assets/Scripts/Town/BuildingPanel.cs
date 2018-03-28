using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPanel : MonoBehaviour {

    public Constants.BuildingPanel_Type BuildingType;



    [SerializeField]
    private Constants.BuildingPanel_Status BuildingStatus = Constants.BuildingPanel_Status.AVAILABLE;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update() {

    }

    public void ChangeStatus(Constants.BuildingPanel_Status status)
    {
        BuildingStatus = status;
    }

    public Constants.BuildingPanel_Status GetStatus()
    {
        return BuildingStatus;
    }

    public bool CheckStatus()
    {
        if(BuildingStatus == Constants.BuildingPanel_Status.NOT_AVAILABLE)
        {
            MasterManager.ManagerPopup.ShowMessageBox("System", "This building is not available now", Constants.PopupType.POPUP_NO);
        }

        return (BuildingStatus == Constants.BuildingPanel_Status.AVAILABLE);
    }

}
