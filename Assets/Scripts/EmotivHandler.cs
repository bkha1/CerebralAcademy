using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Emotiv;
using EmoEngineClientLibrary;

public class EmotivHandler : MonoBehaviour {


    public string debugProfileDir = "C:/Users/jvmilazz/Desktop/Joseph.emu";
	private static EmotivHandler instance;

    private EmoEngineClient engineClient;

	protected EmoEngine engine; // Access to the EDK is viaa the EmoEngine 
    private uint userID; // userID is used to uniquely identify a user's headset
	private Profile profile;
	
	private EmoState cogState = null;
	private Dictionary<EdkDll.EE_DataChannel_t, double[]> data;
	
	private static float bufferSize = 1.0f;

    private float elapsedTime = 0;
	
	public static EmotivHandler Instance
	{
		get
		{
			if (instance == null) {
				instance = new GameObject("EmotivHandler").AddComponent<EmotivHandler>();
			}
			
			return instance;
		}
	}

    void Awake()
    {
        engineClient = new EmoEngineClient();
    }
	
	public void OnApplicationQuit() 
	{
        if (engineClient.IsEmoEngineRunning) disconnect();

		instance = null;
	}
	
	// Update is called once per frame
	void Update ()
    {

        #region EmoClient
        if (engineClient != null && engineClient.IsPolling)
        {
            EmotivState emoState = engineClient.CurrentEmotivState;

            if (emoState.AffectivMeditationScore > 0)
            {
                CognitvEventManager.TriggerCognitivEmotion(null, emoState.AffectivMeditationScore);

            }

            if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_DISAPPEAR)
            {
                CognitvEventManager.TriggerCognitivDisappear(null, emoState.CognitivCurrentActionPower);
            }
            else if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_LIFT)
            {
                CognitvEventManager.TriggerCognitivLift(null, emoState.CognitivCurrentActionPower);
            }
            else if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_LEFT)
            {
                CognitvEventManager.TriggerCognitivLeft(null, emoState.CognitivCurrentActionPower);
            }
            else if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_PUSH)
            {
                CognitvEventManager.TriggerCognitivPush(null, emoState.CognitivCurrentActionPower);
            }
        }
        #endregion

        #region Emotiv
        /*if (engine == null) return;
		// Handle any waiting events
        engine.ProcessEvents();
		
		// This should be called every second...
        elapsedTime += Time.deltaTime;
        if (elapsedTime > bufferSize)
        {
            data = engine.GetData(userID);
            elapsedTime = 0;

            if (data == null) return;
        }*/
        #endregion
    }
	
	public uint getActiveUser() {
        return engineClient.UserID;
		//return (userID);
	}

    public static EmoEngineClient getEmoEngine()
    {
        if (instance.engineClient == null)
            instance.engineClient = new EmoEngineClient();

        return instance.engineClient;
    }
	
	/*public static EmoEngine getEmoEngine() {
		if (instance.engine == null) return EmoEngine.Instance;
		else return instance.engine;
	}*/
	
	public void connect()
    {

        #region Emotiv
        /*if (engine == null)
		    engine = EmoEngine.Instance;

        engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
        //engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_userAdded_event);

        engine.CognitivEmoStateUpdated += new EmoEngine.CognitivEmoStateUpdatedEventHandler(engine_CognitiveEmoStateUpdated);
        engine.AffectivEmoStateUpdated += new EmoEngine.AffectivEmoStateUpdatedEventHandler(engine_AffectivEmoStateUpdated);
        engine.Connect();*/
        #endregion

        #region EmoClient
        if (engineClient == null)
            engineClient = new EmoEngineClient();

        engineClient.StartEmoEngine();
        engineClient.UserID = 0;
        engineClient.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(engineClient_PropertyChanged);
        //engineClient.StartDataPolling();

        #endregion




    }

    void engineClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "CurrentEmotivState")
        {
            EmotivState emoState = engineClient.CurrentEmotivState;

            if (emoState.AffectivMeditationScore > 0)
            {
                CognitvEventManager.TriggerCognitivEmotion(null, emoState.AffectivMeditationScore);

            }

            if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_DISAPPEAR)
            {
                CognitvEventManager.TriggerCognitivDisappear(null, emoState.CognitivCurrentActionPower);
            }
            else if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_LIFT)
            {
                CognitvEventManager.TriggerCognitivLift(null, emoState.CognitivCurrentActionPower);
            }
            else if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_LEFT)
            {
                CognitvEventManager.TriggerCognitivLeft(null, emoState.CognitivCurrentActionPower);
            }
            else if (emoState.CognitivCurrentAction == EdkDll.EE_CognitivAction_t.COG_PUSH)
            {
                CognitvEventManager.TriggerCognitivPush(null, emoState.CognitivCurrentActionPower);
            }
        }
    }

	public void disconnect()
    {
        #region Emotiv
        /*try {
			engine.Disconnect();
		} catch {}
		
		engine = null;*/
        #endregion

        #region EmoClient
        //  engineClient.StopDataPolling();
        #endregion

    }
	
	public Dictionary<EdkDll.EE_DataChannel_t, double[]> getRawData() {
		return data;
	}
	
	public double[] getDataChannel(EdkDll.EE_DataChannel_t channel) {
		return data[channel];
	}
	
	public bool isConnected()
    {
        #region Emotiv
        //return (engine != null && engine.EngineGetNumUser() > 0);
        #endregion

        #region EmoClient
        return engineClient.IsEmoEngineRunning;
        #endregion
    }

    #region Emotiv
    void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
    {
        Debug.Log("EmoEngine Connected!");
		userID = e.userId;
		engine.LoadUserProfile(0, debugProfileDir); 
		userID = 0;
		engine.DataAcquisitionEnable(userID, true);
		engine.EE_DataSetBufferSizeInSec(bufferSize); 
		Debug.Log ("User ID: " + userID);

    }
	
	void engine_userAdded_event(object sender, EmoEngineEventArgs e) {
		Debug.Log("User Added Event has occured");

		/*
		// enable data aquisition for this user
		engine.DataAcquisitionEnable(userID, true);
		
		// ask for up to 1 second of buffered data
        engine.EE_DataSetBufferSizeInSec(1.0f); 
		
		// I don't need to do profile handling myself.
		profile = EmoEngine.Instance.GetUserProfile(userID);
        profile.GetBytes();*/
		
	}

    void engine_AffectivEmoStateUpdated(object sender, EmoStateUpdatedEventArgs args)
    {
        EmoState emoState = args.emoState;

        CognitvEventManager.TriggerCognitivEmotion(sender, emoState.AffectivGetMeditationScore());
    }
	
	void engine_CognitiveEmoStateUpdated(object sender, EmoStateUpdatedEventArgs args) {
		cogState = args.emoState;
		EmoState emoState = args.emoState;

        if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_DISAPPEAR)
        {
            CognitvEventManager.TriggerCognitivDisappear(sender, emoState.CognitivGetCurrentActionPower());
        }
        else if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_LIFT)
        {
            CognitvEventManager.TriggerCognitivLift(sender, emoState.CognitivGetCurrentActionPower());
        }
        else if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_LEFT)
        {
            CognitvEventManager.TriggerCognitivLeft(sender, emoState.CognitivGetCurrentActionPower());
        }
        else if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_PUSH)
        {
            CognitvEventManager.TriggerCognitivPush(sender, emoState.CognitivGetCurrentActionPower());
        }
	
	}

    #endregion

    /*public EmoState getCognitiveState() {
		return cogState;
	}*/

}
