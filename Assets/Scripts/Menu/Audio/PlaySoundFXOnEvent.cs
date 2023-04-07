using OVR;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySoundFXOnEvent : MonoBehaviour
{
    [System.Serializable]
    public struct Item
    {
        public SoundFXRef sound;
        public EventTriggerType eventTriggerType;
    }

    public Item[] items;

    void Awake()
    {
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
        {
            eventTrigger = gameObject.AddComponent<EventTrigger>();
        }

        foreach (Item item in items)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();

            entry.eventID = item.eventTriggerType;

            entry.callback.AddListener((eventData) => OnEventTriggered((BaseEventData)eventData, item));

            eventTrigger.triggers.Add(entry);
        }


    }

    private void OnEventTriggered(BaseEventData eventData, Item item)
    {
        item.sound.PlaySoundAt(transform.position);
    }

}
