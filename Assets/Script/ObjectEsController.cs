using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectEsController : MonoBehaviour
{
    public Sprite[] opcionObject;
    SpriteRenderer[] listObjectE;

    private void Awake()
    {
        listObjectE = GetComponentsInChildren<SpriteRenderer>();
    }
    void Start()
    {
        Debug.Log(listObjectE.Length);
        ChangeEstanteria();
        //ChangeItem(0);
        //Test(1);
    }
    //change all
    void ChangeEstanteria()
    {
        for (int i = 0; i < listObjectE.Length; i++)
        {
            ChangeItem(i);
        }
    }
    //cambio por numero en la lista
    void ChangeItem(int numberObject)
    {
        int opcionS = Random.Range(0, 16);
        listObjectE[numberObject].sprite = opcionObject[opcionS];

    }
    //cambio del objeto arrastrado(nombre en escena)
    void ChangeItem(string nameObject)
    {
        int opcionS = Random.Range(0, 16);
        for (int i = 0; i < listObjectE.Length; i++)
        {
            if (listObjectE[i].gameObject.name == nameObject)
            {
                listObjectE[i].sprite = opcionObject[opcionS];
                //Debug.Log("soy el objeto: " + listObjectE[i].gameObject.name);
                break;
            }
        }
    }
    void ColorearObjeto(string nameObject)
    {
        for (int i = 0; i < listObjectE.Length; i++)
        {
            if (listObjectE[i].gameObject.name == nameObject)
            {
                listObjectE[i].color = Color.white;                
                break;
            }
        }
    }
}
