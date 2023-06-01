using UnityEngine;
using Unity.Netcode;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Wall : NetworkBehaviour
{
    [SerializeField] private bool IsAGoalPost = false;
    private PlayerScoreData playerData = new PlayerScoreData();

    public static Action OnBallGoalEvent;

    public int GetCurrentScore()
    {
        return playerData.GetScore();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!IsAGoalPost)
        {
            return;
        }

        if (collision.gameObject.tag == "Ball")
        {
            OnBallGoalClientRpc();
        }
    }

    [ClientRpc]
    private void OnBallGoalClientRpc()
    {
        if (OnBallGoalEvent != null)
        {
            playerData.IncrementScore();
            OnBallGoalEvent();
            
            return;
        }
    }
}

public static class LocalIPAddress
{
    private static string LocalIP;

    public static string GetLocalIP() 
    {
        if (string.IsNullOrEmpty(LocalIP))
        {
            LocalIP = FetchLocalIPAddress();
        }

        return LocalIP;
    }

    private static string FetchLocalIPAddress()
    {
        // Get the host name of the local device
        string hostName = Dns.GetHostName();

        // Get the IP address of the local device
        IPAddress[] addresses = Dns.GetHostAddresses(hostName);

        foreach (IPAddress address in addresses)
        {
            if (address.AddressFamily == AddressFamily.InterNetwork)
            {
                return address.ToString();
            }
        }

        return "";
    }
}

public static class RoomID
{
    private static string prefixIP = "192.168.";
    //assumes that ipAddress always starts with 192.168...
    public static string IpToRoomID(string ipAdress)
    {
        StringBuilder builder = new StringBuilder();

       for(int i = 0; i < ipAdress.Length; i++)
       {
            if (i <= 7)
            {
                continue;
            }

            char letter = ipAdress[i];

            if (letter == '.')
            {
                builder.Append('-');
                continue;
            }

            var value = Convert.ToInt32(letter);
            builder.Append(value.ToString("x"));
        }

        return builder.ToString();
    }

    public static string RoomIDToIp(string roomID)
    {
        StringBuilder builder = new StringBuilder(prefixIP);
        StringBuilder convertBuilder = new StringBuilder();

        byte count = 0;

        foreach (char currentLetter in roomID)
        {
            if (currentLetter == '-')
            {
                builder.Append('.');
                continue;
            }
            convertBuilder.Append(currentLetter);

            if (count >= 1)
            {
                int value = Convert.ToInt32(convertBuilder.ToString(), 16);
                builder.Append((char)value);
                count = 0;
                convertBuilder.Clear();
                continue;
            }

            count++;
        }
        return builder.ToString();
    }
}

