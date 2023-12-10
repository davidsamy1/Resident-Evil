using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIError : MonoBehaviour
{
    public GameObject errorCanvas;
    public TMP_Text errorMsgText;

     public IEnumerator ShowErrorMessage(string message, float duration)
    {
        Debug.Log("Error Message: " + message);
        errorMsgText.text = message;
        errorCanvas.SetActive(true); // show msg

        yield return new WaitForSeconds(duration); // wait

        errorCanvas.SetActive(false); // hide msg
    }
}
