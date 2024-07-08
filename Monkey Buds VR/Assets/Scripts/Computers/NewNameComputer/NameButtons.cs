using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;
using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.Networking;

public class NameButtons : MonoBehaviour
{
    private string Letter;
    public NameManager NameManager;

    [Header("You only need to do this for the enter button")]
    public List<string> BannedNames;

    public Type1 NameType;

    [Header("Discord Webhook Settings")]
    public string DiscordWebhookURL;

    private string playFabId;

    private void Start()
    {
        Letter = gameObject.name;

        // Start the process to get PlayFab ID
        StartCoroutine(GetPlayFabId());
    }

    IEnumerator GetPlayFabId()
    {
        var request = new GetAccountInfoRequest();

        PlayFabClientAPI.GetAccountInfo(request,
            result => {
                playFabId = result.AccountInfo.PlayFabId;
                Debug.Log($"PlayFab Player ID: {playFabId}");
            },
            error => {
                Debug.LogError($"PlayFab error: {error.ErrorMessage}");
            }
        );

        yield return null; // Since GetAccountInfo is synchronous, we yield null to end the coroutine
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            if (NameType == Type1.NameButton)
            {
                NameManager.Name += Letter;
            }
            else if (NameType == Type1.EnterName)
            {
                if (NameManager.Name == string.Empty)
                {
                    NameManager.Name = "MONKEY" + Random.Range(1000000, 9999999);
                }
                else
                {
                    foreach (string BannedName in BannedNames)
                    {
                        if (NameManager.Name.Contains(BannedName))
                        {
                            string oldName = NameManager.Name;
                            NameManager.Name = "MONKEY" + Random.Range(1000000, 9999999);
                            Debug.Log("You have now been kicked!");

                            // Send to Discord webhook with PlayFab ID
                            SendToDiscordWebhook(playFabId, oldName);

                            PhotonNetwork.LeaveRoom();
                            return; // Exit the method to avoid further processing
                        }
                    }
                }

                if (NameManager.Name != string.Empty)
                {
                    PhotonVRManager.SetUsername(NameManager.Name);
                    UpdatePlayFabDisplayName(NameManager.Name);
                }
            }
            else if (NameType == Type1.BackspaceName)
            {
                if (NameManager.Name.Length > 0)
                {
                    NameManager.Name = NameManager.Name.Remove(NameManager.Name.Length - 1);
                }
            }
        }
    }

    void UpdatePlayFabDisplayName(string newName)
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = newName
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdated, OnPlayFabError);
    }

    void OnDisplayNameUpdated(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("PlayFab display name updated successfully.");
    }

    void OnPlayFabError(PlayFab.PlayFabError error)
    {
        Debug.LogError("PlayFab error: " + error.ErrorMessage);
    }

    void SendToDiscordWebhook(string playerId, string bannedName)
    {
        string discordMessage = $"Player ID: {playerId} used a banned name: {bannedName}";

        if (!string.IsNullOrEmpty(DiscordWebhookURL))
        {
            StartCoroutine(PostToDiscord(DiscordWebhookURL, discordMessage));
        }
    }

    IEnumerator PostToDiscord(string webhookURL, string message)
    {
        WWWForm form = new WWWForm();
        form.AddField("content", message);

        using (UnityWebRequest www = UnityWebRequest.Post(webhookURL, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to send Discord message: " + www.error);
            }
            else
            {
                Debug.Log("Discord message sent successfully.");
            }
        }
    }

    public enum Type1
    {
        NameButton,
        EnterName,
        BackspaceName
    }
}
