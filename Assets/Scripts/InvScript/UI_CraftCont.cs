using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class UI_CraftCont : MonoBehaviour
{
        private Inventory inventory;
        private Transform SelectedItemOneCont;
        private Transform SelectedItemTwoCont;
        private Transform NeededItemOneCont;
        private Transform NeededItemTwoCont;
        private Transform ResultItemOneCont;
        private Transform ResultItemTwoCont;

        private Transform CloseBTN;
        private Transform CraftCont;
        
  
        

public void setInventory(Inventory inventory){
    this.inventory=inventory;
}

void Awake(){
     CraftCont=transform.Find("CraftCont");
    Transform Combination1=CraftCont.Find("combination1");
    Transform Combination2=CraftCont.Find("combination2");
    SelectedItemOneCont=Combination1.Find("selectedCont");
    SelectedItemTwoCont=Combination2.Find("selectedCont");
    NeededItemOneCont=Combination1.Find("neededCont");
    NeededItemTwoCont=Combination2.Find("neededCont");
    ResultItemOneCont=Combination1.Find("resultCont");
    ResultItemTwoCont=Combination2.Find("resultCont");
    CloseBTN=CraftCont.Find("closeBTN");
    CloseBTN.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(()=>{CancelCraft();});
    RefreshCraft();
    
}


public void CancelCraft(){
    inventory.isCrafting=false;
    CraftStationDisappear();
    
}

    public void setUIToolTip(Transform itemSlotRectTransform,Item inventoryItem ){
                    //Get the ToolTip plane and then go to tis child and add a title and a description
            Transform ToolTip=itemSlotRectTransform.Find("toolTip");
            Transform Title=ToolTip.Find("Title");
            Transform Description=ToolTip.Find("Desc");
            Title.GetComponent<TMPro.TextMeshProUGUI>().SetText(inventoryItem.Title);
            Description.GetComponent<TMPro.TextMeshProUGUI>().SetText(inventoryItem.Description);
         
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener((eventData) => {ToolTip.gameObject.SetActive(true);});   
            itemSlotRectTransform.GetComponent<EventTrigger>().triggers.Add(entry);

            EventTrigger.Entry entry2 = new EventTrigger.Entry();
            entry2.eventID = EventTriggerType.PointerExit;
            entry2.callback.AddListener((eventData) => {ToolTip.gameObject.SetActive(false);});
            itemSlotRectTransform.GetComponent<EventTrigger>().triggers.Add(entry2);
        }
public void RefreshCraft(){
    if(inventory==null){
        return;
    }
     Item CurrSelected=inventory.getSelected();
      Item[][] options=inventory.CraftOptions(CurrSelected);
      Transform SelectedItemOneContItem=SelectedItemOneCont.Find("item");
      Transform SelectedItemOneContAmount=SelectedItemOneCont.Find("amount");
      Transform SelectedItemTwoContItem=SelectedItemTwoCont.Find("item");
        Transform SelectedItemTwoContAmount=SelectedItemTwoCont.Find("amount");
        Transform NeededItemOneContItem=NeededItemOneCont.Find("item");
        Transform NeededItemOneContAmount=NeededItemOneCont.Find("amount");
        Transform NeededItemTwoContItem=NeededItemTwoCont.Find("item");
        Transform NeededItemTwoContAmount=NeededItemTwoCont.Find("amount");
        Transform ResultItemOneContItem=ResultItemOneCont.Find("item");
        Transform ResultItemOneContAmount=ResultItemOneCont.Find("amount");
        Transform ResultItemTwoContItem=ResultItemTwoCont.Find("item");
        Transform ResultItemTwoContAmount=ResultItemTwoCont.Find("amount");
        if(options!=null){
        Item neededOne=options[0][0];
        Item resultOne=options[0][1];
        Item neededTwo=options[1][0];
        Item resultTwo=options[1][1];
        SelectedItemOneContItem.GetComponent<UnityEngine.UI.Image>().sprite=CurrSelected.sprite;
        SelectedItemOneContAmount.GetComponent<TMPro.TextMeshProUGUI>().SetText("1");
        SelectedItemTwoContItem.GetComponent<UnityEngine.UI.Image>().sprite=CurrSelected.sprite;
        SelectedItemTwoContAmount.GetComponent<TMPro.TextMeshProUGUI>().SetText("1");
        NeededItemOneContItem.GetComponent<UnityEngine.UI.Image>().sprite=neededOne.sprite;
        NeededItemOneContAmount.GetComponent<TMPro.TextMeshProUGUI>().SetText("1");
        NeededItemTwoContItem.GetComponent<UnityEngine.UI.Image>().sprite=neededTwo.sprite;
        NeededItemTwoContAmount.GetComponent<TMPro.TextMeshProUGUI>().SetText("1");
        ResultItemOneContItem.GetComponent<UnityEngine.UI.Image>().sprite=resultOne.sprite;
        ResultItemOneContAmount.GetComponent<TMPro.TextMeshProUGUI>().SetText(resultOne.quantity.ToString());
        ResultItemTwoContItem.GetComponent<UnityEngine.UI.Image>().sprite=resultTwo.sprite;
        ResultItemTwoContAmount.GetComponent<TMPro.TextMeshProUGUI>().SetText(resultTwo.quantity.ToString());
        setUIToolTip(ResultItemOneCont,resultOne);
        setUIToolTip(ResultItemTwoCont,resultTwo);

        

  
        }
}
public void CraftStationApear(){
    CraftCont.gameObject.SetActive(true);
}
public void CraftStationDisappear(){
    CraftCont.gameObject.SetActive(false);
}

    // Start is called before the first frame update
  

  
}
