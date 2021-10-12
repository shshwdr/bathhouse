using Doozy;
using Doozy.Engine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PopupManager : Singleton<PopupManager>
{
    [Header("Popup Settings")]
    public string PopupName = "popupYesNo";

    private UIPopup m_popup;

    public void ShowPopup(string Title, string Message, PopupButtonData[] buttonDatas)
    {
        //get a clone of the UIPopup, with the given PopupName, from the UIPopup Database
        m_popup = UIPopup.GetPopup(PopupName);

        //make sure that a popup clone was actually created
        if (m_popup == null)
            return;

        //we assume (because we know) this UIPopup has a Title and a Message text objects referenced, thus we set their values
        m_popup.Data.SetLabelsTexts(Title, Message);

        //get the values from the label input fields
        //LabelButtonOne = LabelButtonOneInput.text;
        //LabelButtonTwo = LabelButtonTwoInput.text;

        //set the button labels
        //m_popup.Data.SetButtonsLabels(LabelButtonOne, LabelButtonTwo);

        //set the buttons callbacks as methods
        
        //m_popup.Data.SetButtonsCallbacks(ClickButtonOne, ClickButtonTwo);
       // m_popup.Data.Buttons[0].gameObject.SetActive(false);
        //OR set the buttons callbacks as lambda expressions
        //m_popup.Data.SetButtonsCallbacks(() => { ClickButtonOne(); ClickButtonOneDefault(); }, () => { ClickButtonTwo(); ClickButtonTwoDefault(); });
        m_popup.Data.SetButtonsData(buttonDatas, ClickButtonOneDefault);
        //if the developer did not enable at least one button to hide it, make the UIPopup hide when its Overlay is clicked
        //if (!HideOnButtonOne && !HideOnButtonTwo)
        //{
        //    m_popup.HideOnClickOverlay = true;
        //    DDebug.Log("Popup '" + PopupName + "' is set to close when clicking its Overlay because you did not enable any hide option");
        //}

        m_popup.Show(); //show the popup
    }

    private void ClickButtonOneDefault()
    {
        //DDebug.Log("Clicked button ONE: " );
        //if (HideOnButtonOne) 
            ClosePopup();
    }

    private void ClickButtonTwoDefault()
    {
        //DDebug.Log("Clicked button TWO: " );
        //if (HideOnButtonTwo) 
            ClosePopup();
    }

    private void ClosePopup()
    {
        if (m_popup != null) m_popup.Hide();
    }
}
