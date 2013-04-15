using UnityEngine;
using System.Collections;

public class Player {
	
    public enum RelaxationTechnique { };
    public enum CognitivSkill { LIFT, PUSH, PULL };

	public Player() {
        UserName = string.Empty;
        Gender = string.Empty;
        EmotivID = 0;
        Mana = 0;
	}

    public string UserName { get; set; } // A unique name for the user
    public string Gender { get; set; } // "male" or "female"
    public uint EmotivID { get; set; } // UserID for Emotiv's Control Panel
    public float Mana { get; set; } // The amount of mana the user currently owns

    public bool hasLearnedLift { get; set; } // If the player has learned Lift and thus can use it
    public bool hasLearnedPush { get; set; } // If the player has learned Push and thus can use it
    public bool hasLearnedPull { get; set; } // If the player has learned Pull and thus can use it

    public CognitivSkill CurrentSkillEquipped { get; set; };
    public RelaxationTechnique CurrentRelaxationEquipped { get; set; };
}
