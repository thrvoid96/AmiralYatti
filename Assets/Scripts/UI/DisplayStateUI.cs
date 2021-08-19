using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayStateUI : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private TextMeshProUGUI turnTextBox;
    [SerializeField] private TextMeshProUGUI stateTextBox;


    public void changeTexts(string turnTxt, string stateTxt)
    {
        turnTxt = turnTxt.Replace('$', '\n');
        stateTxt = stateTxt.Replace('$', '\n');
        turnTextBox.text = turnTxt;
        stateTextBox.text = stateTxt;
    }

}
