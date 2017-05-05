using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupInterface : MonoBehaviour {
    public GameObject display;


    public void showPopupMessage(string show, float timer)
    {
        this.gameObject.SetActive(true);
        display.GetComponent<Text>().text = show;
        StartCoroutine(popupTimer(timer));
        
    }

    IEnumerator popupTimer(float time)
    {
        yield return new WaitForSeconds(time/1000f);
        this.gameObject.SetActive(false);
    }

    /*
    public void fadeIn()
    {
        this.gameObject
    }*/
}
