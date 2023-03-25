using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkDebugTool : MonoBehaviour
{
    public void OnStartHostPressed()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void OnStartClientPressed()
    {
        NetworkManager.Singleton.StartClient();
    }
    public void OnStartServerPressed()
    {
        NetworkManager.Singleton.StartServer();
    }
}
