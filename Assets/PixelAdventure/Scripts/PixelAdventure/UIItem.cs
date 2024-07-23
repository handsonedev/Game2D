using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIItem : MonoBehaviour
{
    public Text melonsText;

    public void setScore(string text )
    {
        if(melonsText)
            melonsText.text = text;
    }
}
