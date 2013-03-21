using UnityEngine;
using System.Collections;

public class SpeechManager : MonoBehaviour {


    public Transform speechBubblePrefab;

    

    // A list of speech bubbles currently active
    private ArrayList speechBubbles = new ArrayList();
    private SpeechBubble bubble;

	// Use this for initialization
	void Start () {
        NotificationCenter.DefaultCenter.AddObserver(this, "NewSpeechBubble");
	    
	}
	
	// Update is called once per frame
	void Update () {

        // Go through all the speech bubbles and check if they have expired
        for (int i = 0; i < speechBubbles.Count; i++)
        {
            bubble = speechBubbles[i] as SpeechBubble;
            float expiryDate = bubble.duration;
            if (Time.time >= expiryDate)
            {
                // Remove speech Bubble
                Destroy(bubble.gameObject);
                bubble.gameObject = null;
                speechBubbles.RemoveAt(i);
                Debug.Log("Speech Bubble Destroyed!");
            }
        }
	
	}

    //void setSpeechBubble(Vector3 position, string text, float time) { }

    void NewSpeechBubble(Notification notification)
    {
        if (!notification.data.ContainsKey("pos"))
        {
            // TODO: Throw an exception
            return;
        }
        
        SpeechBubble bubble = new SpeechBubble((Vector3)notification.data["pos"], (string)notification.data["text"], Time.time + (float)notification.data["duration"]);

        bubble.gameObject = (GameObject) Instantiate(speechBubblePrefab, bubble.position, Quaternion.identity);
        bubble.gameObject.GetComponentInChildren<GUIText>().text = bubble.text;
        //bubble.setText();

        speechBubbles.Add(bubble);
        Debug.Log("Speech Bubble created!");
    }

    


    
}


