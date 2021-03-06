using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Emotiv;
using EmoEngineClientLibrary;
using System.IO;
using System;

public class EmotivHandler : MonoBehaviour {

    public static string DefaultProfilePath = "%appdata%";
    public string debugProfileDir = "C:/Users/Brian/Desktop/Joseph.emu";
	private static EmotivHandler instance = null;

    private EmoEngineClient engineClient = null;

	protected EmoEngine engine; // Access to the EDK is via the EmoEngine 
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
            #region Emotiv
            if (instance == null)
            {
                // Check if an EmotivHandler is already in the scene
                instance = FindObjectOfType(typeof(EmotivHandler)) as EmotivHandler;

                // We couldn't find one, let's create one!
                if (instance == null)
                {
                    GameObject obj = new GameObject("EmotivHandler");
                    DontDestroyOnLoad(obj);
                    instance = obj.AddComponent<EmotivHandler>();
                    Debug.Log("EmotivHandler created");
                }
            }
            #endregion

            #region EmotivClient
            /*if (instance == null)
            {
                instance = new EmotivHandler();
                Debug.Log("EmotivHandler created");
            }*/
            #endregion

            return instance;
		}
	}

    /*private EmotivHandler()
    {
        this.engineClient = new EmoEngineClient();
        this.engineClient.ActivePort = EmoEngineClient.ControlPanelPort;
        this.engineClient.UserID = 0;
        this.engineClient.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(engineClient_PropertyChanged);
    }*/
	
	public void OnApplicationQuit() 
	{
		instance = null;
        engineClient = null;
	}

    /*~EmotivHandler()
    {
        if (engineClient.IsEmoEngineRunning) disconnect();

        instance = null;
        engineClient = null;
    }*/


    /*IEnumerator startHandler()
    {
        Debug.Log("Creating instance of engineClient");
        if (engineClient == null) engineClient = new EmoEngineClient(); // BUG: Here is where exceptions spawn
        
        yield return new WaitForSeconds(1);
    }

    IEnumerator clearHandler()
    {
        if (engineClient.IsEmoEngineRunning) disconnect();

		instance = null;
        engineClient = null;

        Debug.Log("Destroying instance of engineClient");

        yield return new WaitForSeconds(1);
    }*/
	
	// Update is called once per frame
	void Update ()
    {
        #region Emotiv
        //if (engine == null) return;
		// Handle any waiting events
        //engine.ProcessEvents();
		EmoEngine.Instance.ProcessEvents();
		
		// This should be called every second... (NOTE: Not needed for this game, thus commented)
        /*elapsedTime += Time.deltaTime;
        if (elapsedTime > bufferSize)
        {
            data = engine.GetData(userID);
            elapsedTime = 0;

            if (data == null) return;
        }*/
       #endregion Emotiv
    }
	
	public uint getActiveUser() {
        //return engineClient.UserID;
		return (userID);
	}

    /*public static EmoEngineClient getEmoEngine()
    {
        //if (instance.engineClient == null)
        //    instance.engineClient = new EmoEngineClient();

        return instance.engineClient;
    }*/
	
	public static EmoEngine getEmoEngine() {
		if (instance.engine == null) return EmoEngine.Instance;
		else return instance.engine;
	}
	
	public void connect()
    {

        #region Emotiv
       if (engine == null)
		    engine = EmoEngine.Instance;

        engine.EmoEngineConnected += new EmoEngine.EmoEngineConnectedEventHandler(engine_EmoEngineConnected);
        engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_userAdded_event);
        
        engine.CognitivEmoStateUpdated += new EmoEngine.CognitivEmoStateUpdatedEventHandler(engine_CognitiveEmoStateUpdated);
        engine.AffectivEmoStateUpdated += new EmoEngine.AffectivEmoStateUpdatedEventHandler(engine_AffectivEmoStateUpdated);
        engine.Connect();
        #endregion Emotiv

        #region EmoClient
        /*EmoEngine.Instance.UserAdded += new EmoEngine.UserAddedEventHandler(engine_userAdded_event);
        engineClient.StartEmoEngine();*/
        #endregion EmoClient
    }

    void engineClient_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        Debug.Log("PropertyChange: " + e.PropertyName);
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
        try {
			engine.Disconnect();
		} catch {}
		
		engine = null;
        #endregion

        #region EmoClient
        //engineClient.StopDataPolling();
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
        return (engine != null && engine.EngineGetNumUser() > 0);
        #endregion

        #region EmoClient
        //return engineClient.IsEmoEngineRunning;
        #endregion
    }

    #region Emotiv
    void engine_EmoEngineConnected(object sender, EmoEngineEventArgs e)
    {
        Debug.Log("EmoEngine Connected!");
        userID = 0;
		Debug.Log ("Engine UserID: " + e.userId);
        engine.LoadUserProfile(userID, debugProfileDir); // NOTE: This is only for testing the headset.
		//engine.DataAcquisitionEnable(userID, true);
		//engine.EE_DataSetBufferSizeInSec(bufferSize); 
		Debug.Log ("User ID: " + userID);

    }
	
	void engine_userAdded_event(object sender, EmoEngineEventArgs e) {
		Debug.Log("User Added Event has occured");

		/*
		// enable data aquisition for this user
		engine.DataAcquisitionEnable(userID, true);
		
		// ask for up to 1 second of buffered data
        engine.EE_DataSetBufferSizeInSec(1.0f); 
		*/
		// I don't need to do profile handling myself.
		EmoEngine.Instance.LoadUserProfile(userID, debugProfileDir);
		profile = EmoEngine.Instance.GetUserProfile(userID);
        profile.GetBytes();
		
		
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
            CognitvEventManager.TriggerCognitivDisappear(sender, emoState.CognitivGetCurrentActionPower() * 10);
        }
        else if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_LIFT)
        {
			Debug.Log("EmoHandler sent Lift of " + emoState.CognitivGetCurrentActionPower() * 10);
            CognitvEventManager.TriggerCognitivLift(sender, emoState.CognitivGetCurrentActionPower() * 10);
        }
        else if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_LEFT)
        {
            CognitvEventManager.TriggerCognitivLeft(sender, emoState.CognitivGetCurrentActionPower() * 10);
        }
        else if (emoState.CognitivGetCurrentAction() == EdkDll.EE_CognitivAction_t.COG_PUSH)
        {
			Debug.Log("EmoHandler sent Push of " + emoState.CognitivGetCurrentActionPower() * 10);
            CognitvEventManager.TriggerCognitivPush(sender, emoState.CognitivGetCurrentActionPower() * 10);
        }
	
	}

    #endregion Emotiv


    public Profile loadProfileFromPath(string profilePath)
    {
        //engine.LoadUserProfile(userID, profilePath); // NOTE: This is disabled because we iwll manually load a file.
        return engine.GetUserProfile(userID);
    }

    public void LoadProfile(ref Player player, string userName)
    {
        // Switch directories to %appdata%
        string targetPath = Environment.GetEnvironmentVariable("appdata") + @"/CerebralAcademy/Profiles/";
        targetPath = targetPath.Replace(@"\", @"/");

        string[] profiles = Directory.GetFiles(Application.dataPath + "/Resources/Profile/", "*.emu");

        for (int i = 0; i < profiles.Length; i++)
        {
            string profile = profiles[i].Substring(profiles[i].LastIndexOf("/") + 1);
            Debug.Log("Profile " + i + " = " + profile);


            string sourceFile = profiles[i];
            string targetFile = userName + ".emu";
            string destFile = System.IO.Path.Combine(targetPath, targetFile);

            Debug.Log("Source File: " + sourceFile);
            Debug.Log("Target File: " + targetFile);
            Debug.Log("Dest File: " + destFile);

            // Check if /CerebrealAcademy/Profile/ exists
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
                Debug.Log("Created Directory at " + targetPath);
            }

            if (!System.IO.File.Exists(destFile))
            {
                Debug.Log("Profile does not exist, creating...");
                // Copy over the profile file and do not overwrite if already existing
                System.IO.File.Copy(sourceFile, destFile, false);
            }

            /*player.ProfilePath = destFile;
            player.Profile = loadProfileFromPath(sourceFile);
            engine.SetHardwarePlayerDisplay(userID, 1);
            Debug.Log("User connected: Player Profile: " + player.Profile.ToString());*/
        }
    }
}
