using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class CardPackDrag : MonoBehaviour {

    public GameObject settingTracker;
    public Vector3 origEnvelopePos;
    public Vector3 placeIntoHolderCoords;
    public GameObject cardPack;
    private GameObject inHolder = null;
    private bool packPlaced = false;
    private bool movePack = false;
    public void Update()
    {
        if(movePack && !packPlaced)
        {
            Vector3 temp = transform.position;
            //temp.y += 1;
            transform.localPosition = Vector3.Lerp(transform.localPosition, placeIntoHolderCoords, .1f);
            if(transform.localPosition==placeIntoHolderCoords)
            {
                GameObject envelopeHolder = GameObject.Find("EnvelopeHolder");
                envelopeHolder.GetComponent<EnvelopeHolderManager>().placeCardPack(gameObject);
                packPlaced = true;
            }
            //temp.y -= 1;
        }
    }
    public void OnMouseDown()
    {
        if (!GameObject.Find("SettingTracker").GetComponent<TrackSetting>().paused&&GameObject.Find("SettingTracker").GetComponent<TrackSetting>().currentSetting == "shop-open")
        {
            origEnvelopePos = transform.localPosition;
            settingTracker = GameObject.Find("SettingTracker");
            if (!settingTracker.GetComponent<TrackSetting>().cardPackInMotion)
            {
                movePack = true;
                if (GameObject.Find("AmountOfCardPacks").GetComponent<CardPackTracker>().totalPacks > 5)
                {
                    Transform parent = GameObject.Find("OpenPackDisplay").transform;
                    Quaternion rotatePack = Quaternion.identity;
                    Vector3 newCoords = origEnvelopePos;
                    rotatePack.eulerAngles = new Vector3(0, 0, 90);
                    GameObject newPack = Instantiate(cardPack, newCoords, Quaternion.identity, parent) as GameObject;
                    newPack.transform.localPosition = newCoords;
                    newPack.transform.localScale = new Vector3(.5f, .5f, .5f);
                    newPack.transform.eulerAngles = rotatePack.eulerAngles;
                    newCoords.x -= .45f;
                }
                settingTracker.GetComponent<TrackSetting>().cardPackInMotion = true;
            }
        }

    }

    public void OnMouseUp()
    {
        /*Vector3 curPos = transform.position;
        if(curPos.z>GameObject.Find("EnvelopeHolder").transform.position.z-.5&&!packPlaced&&inHolder!=gameObject)
        {
            transform.localPosition = placeIntoHolderCoords;
            GameObject envelopeHolder = GameObject.Find("EnvelopeHolder");
            envelopeHolder.GetComponent<EnvelopeHolderManager>().placeCardPack(gameObject);
            packPlaced = true;
        }
        else
        {
            if (!packPlaced&&gameObject != inHolder)
            {
                gameObject.transform.position = origPos;
            }
        }*/
    }
}
