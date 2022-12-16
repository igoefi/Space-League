using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoScript : MonoBehaviour
{
    public Weapons weapons;
    private TextMesh _text;

    void Start()
    {
        _text = GetComponent<TextMesh>();
    }

   
    void Update()
    {
        _text.text =weapons.Ammo();
    }
}
