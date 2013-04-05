using UnityEngine;
using System.Collections;

public class Player {
	

	public Player() {
        UserName = string.Empty;
        Gender = string.Empty;
        EmotivID = 0;
	}

    public string UserName { get; set; } // A unique name for the user
    public string Gender { get; set; } // "male" or "female"
    public uint EmotivID { get; set; } // UserID for Emotiv's Control Panel
    public float Mana { get; set; } // The amount of mana the user currently owns
}