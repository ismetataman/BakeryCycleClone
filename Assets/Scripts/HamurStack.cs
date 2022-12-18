using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class HamurStack : MonoBehaviour
{
    public TextMeshProUGUI moneyCount;
    public GameObject hamurPrefab;
    public GameObject stackObject;
    public GameObject unStackObjects;
    public Transform machineTransform;
    public Transform bakeryMachine;
    public Transform bakeryStack;
    public Transform tezgahStack;
    public Image cooldown;
    public Image bakeryCoolDown;
    public Image tezgahCoolDown;
    public float duration;
    public float durationBakery;
    public float durationTezgah;
    private float hamurY = 0;
    private Vector3 stackPos; 
    private float y = 0;
    private float lastY=0;
    private float tezgahY=0;
    private bool isBaked = false;
    private bool isSold = false;
    private int money = 0;
    
    private void Start() {
        stackPos = Vector3.zero;
        
    }
    private void FixedUpdate() 
    {
        if(isBaked == true)
        {
                durationBakery += Time.deltaTime /2;
                bakeryCoolDown.fillAmount = durationBakery/2;
            for (int i = 0; i < unStackObjects.transform.childCount; i++)
            {
                if(durationBakery >2)
                {
                    durationBakery = 0;
                    y = y + 0.3f;
                    unStackObjects.transform.GetChild(0).transform.DOMove(bakeryStack.transform.position +  new Vector3(0,transform.position.y + y,0),0.5f);
                    unStackObjects.transform.GetChild(0).parent = GameObject.Find("BakedObjects").transform;
                }
            }
            
        }
        if(isBaked== true && unStackObjects.transform.childCount == 0)
        {
            durationBakery = 0;
            bakeryCoolDown.fillAmount = 0;
            isBaked = false;
        }
        if(isSold == true)
        {
            durationTezgah += Time.deltaTime /2;
            tezgahCoolDown.fillAmount = durationTezgah/2;
            for (int i = 0; i < tezgahStack.transform.childCount; i++)
            {
                if(durationTezgah >2)
                {
                    durationTezgah = 0;
                    money = money + 5;
                    Destroy(tezgahStack.GetChild(tezgahStack.transform.childCount -1).gameObject);
                    moneyCount.text = money.ToString();
                    Debug.Log("5 Dolar Kazandınız");
                }
            }

        }
        if(isSold == true && tezgahStack.childCount ==0)
        {
            durationTezgah = 0;
            tezgahCoolDown.fillAmount = 0;
        }
        
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("ResetY"))
        {
            hamurY = 0;
            tezgahY = 0;
            stackPos = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.CompareTag("hamurStack"))    
        {
            if(Stacklist.instance.stack.Count <5)
            {
                duration += Time.deltaTime;
                cooldown.fillAmount = duration;
                if(duration > 1)
                {
                    duration = 0;
                    Stacklist.instance.stack.Add(hamurPrefab);
                    hamurY= hamurY + 0.3f;
                    GameObject go = Instantiate(hamurPrefab,machineTransform.position,Quaternion.identity);
                    go.transform.parent = GameObject.Find("StackObject").transform;
                    go.transform.DOLocalMove(stackPos,0.5f);
                    stackPos += new Vector3(0,0.3f,0);
 
                    Debug.Log("Hamur Listeye Eklendi");
                }
                if(Stacklist.instance.stack.Count == 5)
                {
                    duration = 0;
                    cooldown.fillAmount =0;
                    stackPos = Vector3.zero;
                }
            }
        }
        if(other.gameObject.CompareTag("hamurUnStack"))
        {
            isBaked = true;
            if(stackObject.transform.childCount >0)
            {
                stackObject.transform.GetChild(0).transform.DOMove(unStackObjects.transform.position,1);
                stackObject.transform.GetChild(0).transform.parent = GameObject.Find("UnStackObjects").transform;
            }
            if(stackObject.transform.childCount == 0)
            {
                Stacklist.instance.stack.Clear();
            }
            
            Debug.Log("Hamurlar Listeden Çıkarıldı");
        }
        if(other.gameObject.CompareTag("hamurCollect"))
        {
            
        }
        if(other.gameObject.CompareTag("leaveBread"))
        {
            isSold = true;
            if(stackObject.transform.childCount >0)
            {
                tezgahY= tezgahY + 0.3f;
                
                stackObject.transform.GetChild(0).transform.DOMove(tezgahStack.transform.position + new Vector3(0,transform.position.y + tezgahY,0),0.5f);
                stackObject.transform.GetChild(0).transform.parent = GameObject.Find("TezgahObjects").transform;
                
                Debug.Log("Ekmekler Satışa Konuldu");
            }
            if(stackObject.transform.childCount == 0) // == 1 di değiştirdim.
            {
                Stacklist.instance.stack.Clear();
            }
        }

    }
    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.CompareTag("hamurStack"))
        {
            duration = 0;
            cooldown.fillAmount = 0;
            stackPos = Vector3.zero;
            Debug.Log("Hamur Stackten Çıkıldı");
        }
        if(other.gameObject.CompareTag("leaveBread"))
        {
            durationTezgah = 0;
        }
        if(other.gameObject.CompareTag("hamurCollect"))
        {
            lastY =0;
            y = 0;
            if(GameObject.Find("BakedObjects").transform.childCount >0)
            {
                if(Stacklist.instance.stack.Count < 5)
                {
                    Stacklist.instance.stack.Add(hamurPrefab);
                    lastY= lastY + 0.3f;
                    int countChilds = bakeryStack.transform.childCount;

                    for (int i = 0; i < countChilds; i++)
                    {
                        bakeryStack.transform.GetChild(0).transform.DOLocalMove(stackPos,0.2f);
                        bakeryStack.transform.GetChild(0).transform.parent = GameObject.Find("StackObject").transform;
                        stackPos += new Vector3(0,0.3f,0);
                    }
                    
                    Debug.Log("Pişmiş Hamur Listeye Eklendi");
                }
                
            }
        }
        
    }

    

}
