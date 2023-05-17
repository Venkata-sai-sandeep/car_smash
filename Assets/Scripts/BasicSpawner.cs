using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;
//using Photon.Pun;
//using Photon.Pun;




public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{

    [SerializeField] private NetworkPrefabRef[] _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();
    private Dictionary<PlayerRef, string> _spawnedPlayerID = new Dictionary<PlayerRef, string>();
    
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    { 
        if(runner.IsServer)
        {
            //Creating a unique position for the player
            Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.DefaultPlayers) * 3, 1, 0);
            
            int _random = UnityEngine.Random.Range(0, _playerPrefab.Length);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab[_random], spawnPosition, Quaternion.identity, player);
            
            //NetworkObject networkPlayerObject = runner.Swapn(_playerPrefab[_random], spawnPosition, Quaternion.identity, player);
                

            _spawnedCharacters.Add(player, networkPlayerObject);
            string _playerId = "Player_" + _spawnedCharacters.Count.ToString();
            _spawnedPlayerID.Add(player, _playerId);
            //camera = 
            //string _plaerID = null;
            //if(_spawnedPlayerID.TryGetValue(player, out _plaerID))
            //{
            //    _plaerID = _plaerID;
            //}
            //Debug.Log(_plaerID);
            ////player.PlayerId = _spawnedCharacters.Count;
        }
    }


    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if(_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
        
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        var data = new NetworkInputData();
        //Moving Forward
        if (Input.GetKey(KeyCode.W))
        {
            //data.direction += Vector3.forward;
            //data.direction += Vector3.forward;
            data.moveForward = true;
            data.canMove = true;
            //Debug.Log(data.ToString());

        }
        //Stop Movinf Forward
        else if(Input.GetKeyUp(KeyCode.W))
        {
            data.moveForward = false;
            data.canMove = false;
        }

        if(Input.GetKey(KeyCode.S))
        {
            //data.direction += Vector3.back;
            data.moveBackward = true;
            data.canMove = true;

        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            data.moveBackward = false;
            data.canMove = false;
        }


        if(Input.GetKey(KeyCode.A))
        {
            //data.direction += Vector3.forward;
            data.leftRotate = true;
        }
        else if(Input.GetKeyUp(KeyCode.A))
        {
            data.leftRotate = false;
        }
        if(Input.GetKey(KeyCode.D))
        {
            //data.direction += Vector3.right;
            data.rightRotate = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            data.rightRotate = false;
        }


        input.Set(data);
    }
    
    
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) 
    { 

    }


    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    { 
    
    }
    
    
    public void OnConnectedToServer(NetworkRunner runner)
    { 
    
    }
    
    
    public void OnDisconnectedFromServer(NetworkRunner runner)
    {
    
    }
    
    
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) 
    {
    
    }
    
    
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) 
    {
    
    }
    
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    { 
    
    }
    
    
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    { 
    
    }
    
    
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) 
    { 
    
    }
    
    
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) 
    { 
    
    }
    
    
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    { 
    
    }
    
    
    public void OnSceneLoadDone(NetworkRunner runner)
    {
    
    }
    
    
    public void OnSceneLoadStart(NetworkRunner runner) 
    { 
    
    }

    private NetworkRunner _runner;

    async void StartGame(GameMode mode)
    {
        //Creating the fusion runner and letting it know that we will be providing the user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true; //allows to provide the user input

        //Start or join (depends on gamemode) a session with a specific name
        await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = "Testgame",
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()

        });
        
    }

    private void OnGUI()
    {
        if(_runner == null)
        {
            if(GUI.Button(new Rect(0,0,200,40), "Host"))
            {
                StartGame(GameMode.Host);
            }
            if(GUI.Button(new Rect(0,40,200,40), "Join"))
            {
                StartGame(GameMode.Client);
            }
        }
    }


    public void onClickClose()
    {
        Application.Quit();
        Debug.Log("Application Closed");
    }
}
