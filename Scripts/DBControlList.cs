using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DBControlList : MonoBehaviour
{
    [SerializeField]
    private Text dbRowText;

    public void SetText(object db)
    {
        dbRowText.text= db.ToString();
    }
}
