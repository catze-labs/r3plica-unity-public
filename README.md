![image](https://user-images.githubusercontent.com/65929678/210930652-7847adbb-aa5d-4407-a111-6b43f568742e.png)
# r3plica-unity-public
The r3plica game client engine uses Unity 2021.3.11f1 (LTS).

### Assets list in use
| Name | Range | Link |
| --- | --- | --- |
| PlayFab SDK | PlayFab Development Kit for Unity | https://learn.microsoft.com/ko-kr/gaming/playfab/sdks/unity3d/ |
| RPG Builder | RPG template asset for Unity | https://assetstore.unity.com/packages/tools/game-toolkits/rpg-builder-177657 |

The r3plica game server uses PlayFab. To integrate with PlayFab from the Unity client, you need to use the SDK provided by PlayFab.

RPG Builder is a template asset that helps implement RPG games in a short amount of time. The demo game was implemented using this asset.

RPG Builder is a paid asset, so it has been excluded from the public repository.

### User sign in


https://user-images.githubusercontent.com/104552234/208288468-55de56f8-7341-4c33-b513-bd8fe50d54dc.mp4


You can use the PlayFabClientAPI provided by the PlayFab SDK to implement the login feature.

The login feature has been implemented using the LoginWithEmailAddress function. Upon successful login, you can receive the PlayFabId from the result received in the callback, and use it in other APIs as needed. You can refer to the [following page](https://api.playfab.com/Documentation/Client/method/LoginWithEmailAddress) for more information on this function.

### 

### CharacterData Save & Load


https://user-images.githubusercontent.com/104552234/208288478-a5a0f0fe-f098-4e8f-970c-707e6bddbbbe.mp4


In RPG Builder, CharacterData is saved to or loaded from the PlayFab server. The APIs used for saving and loading character data are as follows:

| Name | Description | Link |
| --- | --- | --- |
| GetUserData | Retrieves the title-specific custom data for the user which is readable and writable by the client | https://learn.microsoft.com/en-us/rest/api/playfab/client/player-data-management/get-user-data |
| UpdateUserData | Creates and updates the title-specific custom data for the user which is readable and writable by the client | https://learn.microsoft.com/en-us/rest/api/playfab/client/player-data-management/update-user-data?view=playfab-rest |

### RPG Contents

RPG Builder assets are a collection of assets that provide basic elements for building an RPG (Role-Playing Game). These assets can include character models, weapons, environments, and other game assets that are commonly found in RPGs.

#### example video

https://user-images.githubusercontent.com/104552234/208288513-e2315b68-f4cc-432b-96a7-f692bb296061.mp4



https://user-images.githubusercontent.com/104552234/208288571-55ab4a04-13ed-415a-87fc-a04a951ab833.mp4


