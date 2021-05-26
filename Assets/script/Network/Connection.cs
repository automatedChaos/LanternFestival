using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using NativeWebSocket;
using Random = UnityEngine.Random;
public class Connection : MonoBehaviour
{
  private WebSocket websocket;
  private string clientKey;

  IDictionary<string, GameObject> players = new Dictionary<string, GameObject>();

  [SerializeField]
  private GameObject SpawnPoint;

  [SerializeField]
  private GameObject playerPrefab; 
  
  public GameObject[] crabTypes = {null, null, null, null, null, null};

  // Start is called before the first frame update
  async void Start()
  {
    websocket = new WebSocket("ws://34.80.38.153:8081");

    websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
    };

    websocket.OnMessage += (bytes) =>
    {
      string payloadString = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
      
      Payload payload = JsonUtility.FromJson<Payload>(payloadString);
      
      Debug.Log(payload.data); 

      switch (payload.action){
        case "new-connection":
          clientKey = payload.key;
          SendWebSocketMessage();
          break;
        case "player-type":
          playerStart(payload);
          break;
        case "player-position":
          playerPosition(payload);
          break;
        case "player-stop":
          playerStop(payload);
          break;
      }

      // if (payload.action.Equals()) {
        
      // }
      // getting the message as a string
      // var message = System.Text.Encoding.UTF8.GetString(bytes);
      // Debug.Log("OnMessage! " + message);
    };

    // Keep sending messages at every 0.3s
    // InvokeRepeating("SendWebSocketMessage", 0.0f, 0.3f);

    // waiting for messages
    await websocket.Connect();
  }

  private void playerStart (Payload payload) {

    Vector3 initPosition = randomPosition();
    if (SpawnPoint != null) initPosition = SpawnPoint.transform.position;
    
    GameObject newPlayer = Instantiate(crabTypes[Int16.Parse(payload.data)], initPosition, Quaternion.identity);
    newPlayer.name = payload.key;
    players[payload.key] = newPlayer;
  }

  private void playerPosition (Payload payload) {
    if (players.ContainsKey(payload.key))
    {
      // TODO: Legacy code, remove after new proof: players[payload.key].transform.position = players[payload.key].transform.position + payload.getDataAsVector3();
      Vector3 networkControls = payload.getDataAsVector3();

      // get the new crab
      GameObject crab = players[payload.key];
      CrabMovement movement = crab.GetComponent<CrabMovement>();

      // something is missing - stop function
      if (crab == null || movement == null) return;
      
      // WASD
      if (networkControls.y ==  1) movement.setNetworkWASD(new int[] { 1, 0, 0, 0, 0 });
      if (networkControls.x == -1) movement.setNetworkWASD(new int[] { 0, 1, 0, 0, 0 });
      if (networkControls.x ==  1) movement.setNetworkWASD(new int[] { 0, 0, 1, 0, 0 });
      if (networkControls.y == -1) movement.setNetworkWASD(new int[] { 0, 0, 0, 1, 0 });

      if (networkControls.x == 0 && networkControls.y == 0) {
        
        movement.setNetworkWASD(new int[] { 0, 0, 0, 0, 1 });
      }
    }
   
  }

  private void playerStop (Payload payload) {
    if (players.ContainsKey(payload.key))
    {
      Destroy(players[payload.key]);
    }else{
      Debug.Log("Cannot Destroy: " + payload.key);
    }
  }

  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.DispatchMessageQueue();
    #endif
  }

  void registerAsDisplay () {

  }

  private Vector3 randomPosition () {
    float x = Random.Range(-5, 5);
    float y = Random.Range(-5, 5);
    float z = 3;
    
    return new Vector3(x, y, z);
  }

  async void SendWebSocketMessage()
  {
    if (websocket.State == WebSocketState.Open)
    {
      await websocket.SendText("{\"action\": \"register\", \"data\" : \"display\", \"key\": \"" + clientKey + "\"}");
    }
  }

  private async void OnApplicationQuit()
  {
    await websocket.Close();
  }
}