using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Interaccion2D : MonoBehaviour,
                            IPointerClickHandler, IPointerEnterHandler,
                            IPointerExitHandler, IDragHandler, IBeginDragHandler,
                            IEndDragHandler
{
    //eliminar despues
    public Text textBox;
    public Text textBox2;

    //pertenece a logica de interaccion event sistem
    private float zAxis = 0;
    private Vector3 clickOffset = Vector3.zero;
    private Vector3 inicialPosition;
    //-------


    private bool isSecurePosition;
    private bool isDelete;
    private ObjectEsController myObjControl;
    private ObjectEsController estanteriaObjControl;
    private SpriteRenderer spriteRender;
    string lastTrigger;

    private void Awake()
    {
        //propios
        spriteRender = gameObject.GetComponent<SpriteRenderer>();
        myObjControl = gameObject.GetComponentInParent<ObjectEsController>();
        zAxis = transform.position.z;
        inicialPosition = transform.position;
    }
    private void Start()
    {
        //externo
        estanteriaObjControl = GameObject.FindGameObjectWithTag("Estanteria").GetComponentInParent<ObjectEsController>();
    }

    //Se ejecuta repetidamente mientras se esté arrastrando
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = CalculateScreenPointToWoldOnPlane(eventData.position, zAxis) + clickOffset;
        textBox.text = "Está siendo arrastrado";
    }

    //Se ejecuta cuando ha empezado a arrastrarse, antes del OnDrag
    public void OnBeginDrag(PointerEventData eventData)
    {
        clickOffset = transform.position - CalculateScreenPointToWoldOnPlane(eventData.position, zAxis);
        textBox.text = "Va a ser arrastrado";
    }

    //Se ejecuta cuando se ha soltado, antes del OnDrop
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = inicialPosition;
        if (isSecurePosition)
        {
            myObjControl.SendMessage("ChangeItem", this.gameObject.name);
            estanteriaObjControl.SendMessage("ColorearObjeto", lastTrigger);
        }
        else if (isDelete)
        {
            myObjControl.SendMessage("ChangeItem", this.gameObject.name);
        }
    }
    //Se ejecuta al finalizar la pulsación completa (levantar el dedo o ratón)
    public void OnPointerClick(PointerEventData eventData)
    {
        textBox.text = "Ha sido pulsado";
    }

    //Se ejecuta cuando el punto del ratón pasa por encima
    public void OnPointerEnter(PointerEventData eventData)
    {
        textBox.text = "El ratón está encima";
    }

    //Se ejecuta cuando el puntero, después de haber pasado por encima, sale de su collider
    public void OnPointerExit(PointerEventData eventData)
    {
        textBox.text = "El ratón ya NO está encima";
    }
    //rectificador de posicion en la camara
    public Vector3 CalculateScreenPointToWoldOnPlane(Vector3 screenPosition, float zPos)
    {
        float enterDist;
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPos));
        Ray rayCast = Camera.main.ScreenPointToRay(screenPosition);
        plane.Raycast(rayCast, out enterDist);
        return rayCast.GetPoint(enterDist);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //textBox2.text = "sobre zona segura3";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        textBox2.text = "sobre zona segura2";
        isSecurePosition = false;
        isDelete = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<SpriteRenderer>().sprite.name == spriteRender.sprite.name)
        {
            textBox2.text = "sobre zona segura1";
            isSecurePosition = true;
            lastTrigger = collision.gameObject.name;
        }
        else if (collision.gameObject.tag =="Papelera")
        {
            textBox2.text = "sobre Pepelera";
            isDelete = true;
        }
    }
}
