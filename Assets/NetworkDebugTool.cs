using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;

public class NetworkDebugTool : MonoBehaviour
{
    [SerializeField] private InputField roomIDInputField;
    [SerializeField] private InputField portInputField;

    private UnityTransport transport;

    public void OnStartHostPressed()
    {
        if (!string.IsNullOrEmpty(portInputField.text))
        {
            if (!int.TryParse(portInputField.text, out int port))
            {
                Debug.LogWarning("Port parsing failed! try again");
                return;
            }

            SetConnectionData((ushort)port);
        }


        NetworkManager.Singleton.StartHost();
        
        roomIDInputField.text = RoomID.IpToRoomID(LocalIPAddress.GetLocalIP());
        roomIDInputField.interactable = false;
        portInputField.interactable = false;
        portInputField.text = GetTransport().ConnectionData.Port.ToString();
    }

    public void OnStartClientPressed()
    {
        if (!string.IsNullOrEmpty(portInputField.text))
        {
            if (!int.TryParse(portInputField.text, out int port))
            {
                Debug.LogWarning("Port parsing failed! try again");
                return;
            }

            SetConnectionData((ushort)port, roomIDInputField.text);
            portInputField.text = port.ToString();
        }

        NetworkManager.Singleton.StartClient();
    }
    public void OnStartServerPressed()
    {
        NetworkManager.Singleton.StartServer();
    }

    private void SetConnectionData(ushort port = 7777, string roomID = "")
    {
        string ip = "";

        if (string.IsNullOrEmpty(roomID))
        {
            ip = LocalIPAddress.GetLocalIP();
        }
        else
        {
            ip = RoomID.RoomIDToIp(roomID);
        }

        GetTransport().SetConnectionData(ip, (ushort)port);
    }

    private UnityTransport GetTransport()
    {
        if (transport == null)
        {
            transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
        }

        return transport;
    }
}
