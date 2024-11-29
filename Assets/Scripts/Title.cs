using Unity.Netcode;
using UnityEngine;

public class Title : MonoBehaviour
{
    public void StartHost()
    {
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene
            ("Game", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request,
        NetworkManager.ConnectionApprovalResponse response)
    {
        response.Pending = true;

        if (NetworkManager.Singleton.ConnectedClients.Count >= 4)
        {
            response.Approved = false;
            response.Pending = false;
            return;
        }

        response.Approved = true;

        response.CreatePlayerObject = true;

        response.PlayerPrefabHash = null;

        var position = new Vector3(0, 1, -5);
        position.x = -5 + 5 * (NetworkManager.Singleton.ConnectedClients.Count % 3);
        response.Position = position;

        response.Rotation = Quaternion.identity;
        response.Pending = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
