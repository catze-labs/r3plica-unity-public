using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public static class PlayFabAPI
{
    public class PlayFabLoginResult
    {
        public bool isComplete;
        public LoginResult result;
        public PlayFabError error;
    }

    public class PlayFabLoginInfo
    {
        public string email, password;
    }

    public class CharacterData
    {
        // game Datas
    }

    public class PlayFabLoadCharacterResult
    {
        public bool isComplete;
        public CharacterData characterData;
        public PlayFabError error;
        public string message;
    }

    public class PlayFabSaveCharacterResult
    {
        public bool isComplete;
        public UpdateUserDataResult result;
        public PlayFabError error;
        public string message;
    }

    static Timer _timer;
    static bool _isSaveCooltimeOver = true;

    static readonly double _saveCooltime = 50000;

    static void OnAutoSaveCheckTimer()
    {
        _timer = new Timer(_saveCooltime);
        _timer.Elapsed += new ElapsedEventHandler(TimerEvent);
        _timer.AutoReset = false;
        _timer.Enabled = true;
    }

    static void TimerEvent(System.Object obj, ElapsedEventArgs e)
    {
        _isSaveCooltimeOver = true;
    }

    public static void Logout(Action callback = null)
    {
        UserDataManager.Logout();
        callback?.Invoke();
    }

    public static void LoginWithEmail(PlayFabLoginInfo info, Action<PlayFabLoginResult> callback = null)
    {
        PlayFabClientAPI.LoginWithEmailAddress(new LoginWithEmailAddressRequest
        {
            Email = info.email,
            Password = info.password,
            TitleId = PlayFabSettings.TitleId,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        },
        result =>
        {
            UserDataManager.UserData._userId = result.PlayFabId;
            UserDataManager.UserData._email = info.email;
            UserDataManager.UserData._nickname = result.InfoResultPayload.PlayerProfile.DisplayName;

            callback?.Invoke(new PlayFabLoginResult
            {
                result = result,
                isComplete = true
            });
        },
        error =>
        {
            callback?.Invoke(new PlayFabLoginResult
            {
                error = error,
                isComplete = false
            });
        });
    }

    public static void LoadCharacterData(Action<PlayFabLoadCharacterResult> callback = null)
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest
        {
            Keys = new List<string> { "Data" },
            PlayFabId = UserDataManager.UserData._userId
        },
        result =>
        {
            if (result.Data.Count <= 0)
            {
                callback?.Invoke(new PlayFabLoadCharacterResult
                {
                    isComplete = false,
                    message = "Load Failed"
                });

                return;
            }

            var character = JsonUtility.FromJson<CharacterData>(result.Data["Data"].Value);

            callback?.Invoke(new PlayFabLoadCharacterResult
            {
                isComplete = true,
                characterData = character
            });
        },
        error =>
        {
            callback?.Invoke(new PlayFabLoadCharacterResult
            {
                isComplete = false,
                error = error
            });
        });
    }

    public static void SaveCharacterData(CharacterData cdata, bool isAutoSave, Action<PlayFabSaveCharacterResult> saveCallback = null)
    {
        if (_isSaveCooltimeOver == false && isAutoSave)
        {
            saveCallback?.Invoke(new PlayFabSaveCharacterResult
            {
                isComplete = false,
                message = "Save Delaying"
            });
            return;
        }

        _isSaveCooltimeOver = false;

        PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string> { { "Data", JsonUtility.ToJson(cdata) } }
        },
        result =>
        {
            saveCallback?.Invoke(new PlayFabSaveCharacterResult
            {
                isComplete = true,
                result = result
            });

            OnAutoSaveCheckTimer();
        },
        error =>
        {
            saveCallback?.Invoke(new PlayFabSaveCharacterResult
            {
                isComplete = false,
                error = error
            });
        });
    }
}
