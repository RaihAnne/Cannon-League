using System.Collections.Generic;
using Unity.Netcode;

public static class ClientSendParamsCache
{
    private static Dictionary<ulong, ClientRpcParams> ClientParams = new Dictionary<ulong, ClientRpcParams>();

    public static void RegisterClientForParams(ulong clientID, ClientRpcParams param)
    {
        if (ClientParams.ContainsKey(clientID))
        {
            return;
        }
        ClientParams.Add(clientID, param);
    }

    public static ClientRpcParams GetSendParamsForClient(ulong clientID)
    {
        if (ClientParams.ContainsKey(clientID))
        {
            return ClientParams[clientID];
        }

        ClientRpcParams newParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[]
                {
                    clientID
                }
            }
        };
        ClientParams.Add(clientID, newParams);

        return newParams;
    }
}
