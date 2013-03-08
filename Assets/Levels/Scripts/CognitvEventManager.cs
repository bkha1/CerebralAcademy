using UnityEngine;
using System.Collections;

public static class CognitvEventManager {

	public delegate void CognitivEvent(object sender, float powerLevel);
	public static event CognitivEvent LiftEvent, PushEvent, DisappearEvent, LeftEvent, RightEvent;

	public static void TriggerCognitivLift(object sender, float powerLevel) {
		if (LiftEvent != null) {
			LiftEvent(sender, powerLevel);
		}
	}

    public static void TriggerCognitivPush(object sender, float powerLevel)
    {
		if (PushEvent != null) {
            PushEvent(sender, powerLevel);
		}
	}

    public static void TriggerCognitivDisappear(object sender, float powerLevel)
    {
		if (DisappearEvent != null) {
            DisappearEvent(sender, powerLevel);
		}
	}

    public static void TriggerCognitivLeft(object sender, float powerLevel)
    {
		if (LeftEvent != null) {
            LeftEvent(sender, powerLevel);
		}
	}

    public static void TriggerCognitivRight(object sender, float powerLevel)
    {
        if (RightEvent != null)
        {
            RightEvent(sender, powerLevel);
        }
    }
}
