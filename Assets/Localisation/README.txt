==========================================================================================
================================LOCALIZATION PACKAGE======================================
==========================================================================================

=====================================Description:=========================================
This package is designed to help you integrate and modify localized text in a Unity game.
It as a simplified integration for Text Assets directly in the Editor, and methods
accessible for a more dynamic usage almost everywhere in CS Scripts

==========================================================================================
=====================================Integration:=========================================

[Localization Manager]
Place the “LocalizationManager.prefab” prefab on your startup scene, it will transfer itself between the scenes
And specify the “LanguageSelection.tsv” file in the “TSV File” parameter of the “LocalizationComponent.cs” script.

Parameters:
languages -> list of languages you want to be supported by your game
default Language -> a language by default, in case of an error or if your game needs it

current Language -> the current language selected for the game (change depending on the languages & startRoutine varaibles)
start Routine -> while defin how the language is choosen at the start of the game, can be changed with "SetStartRoutine"
Language Selection -> Be sure the Local LocalizationComponent is referenced here

Refesh Logs -> will show in the logs when the texts are called for a refresh and which language is currently used
Return Default Language if Possible -> if a language isn't correclty supported in a file, or if the text of said language is empty,
									   The default language will be displayed with a warning message in the logs, avoiding an error.

========================
[Localization Component]

Set the “LocalizationComponent.cs” component to the supposed gameObject containing the dialogs,
Then specify in the parameter the .tsv file containing the dialogs.

Parameters:
TSV File -> The .tsv file containing the dialogs you want the component to be linked to

Get Text Assets in Scene -> Will open a window to select which Text Asset present on the current scene you want to assign a Localization
Reset Selction -> Will reset the selection of Texts Assets


========================
[File .TSV]
the .tsv file must look like:

- The first line, with the exception of the first column, must contain the name of each language that will be assigned to the column,
  and therefore to the language of the dialogues that will be written in this same column.
  Language names should be written in English, according to the spelling of the “SystemLanguage” variables.

- The rest of the first column should contain, on each line, the identification key used to identify each dialog.
  It is this key that must be given when the dialogues are called up,
  and which will determine the translation into each language according to the texts written on the same line as the identification key.

If you have any doubts, refer to the .tsv files in the Sample Scene.

==========================================================================================
==================================Outer Script Usage:=====================================

In the LocalisationComponent of your choice, clic the "Get Text Assets in Scene" Button

A window will open showing every gameobject on the scene containing a Text Component, separating it in 3 types:
- Text Mesh Pro (with TextMeshPropUGUI type component, most widely used)
- Text Legacy (The Legacy text system of Unity)
- Text Mesh (Old system of mesh text)

Select all the desired GameObject you want with the checkbox, you can click on there name to check who & where they are on the Scene

Once you selected everything you wanted, you can click "Validate" and all the Text Assets while be added to a list on the component
Or, if you need to, you can always close or click the "Cancel" button to cancel your action

Once in the List, you can still check where they are by clicking on they're name
You can remove manually every Asset by clicking the "Remove" button by their side
You can remove every Asset on the list by clicking the "Reset Selection" Button

And more importantly, you can assign every Text Assets the "Key" of the desired dialogue, it will automatically assign them to the Text Asset
Or you can also assign multiple Keys with a line break between them just by putting "&&" symbol between the Keys

Exemple: "EchoPart1 && EchoPart2 && EchoPart3"
note that it will display them all at the same time

==========================================================================================
=====================================Script Usage:========================================

"using LocalizationPackage;" & "LocalizationManager.Instance.xxx"
-> Using & variables to call the localization manager's methods.

=====================
[LocalizationManager]
"UniGetText(string TSVFileName, string Key)" method
-> Give a text depending on the TSV file asked for, the key provided in parameter, and the current game language.
   TSV File must be in a LocalizationComponent present on the current scene.
-> Name of the TSV File must not include the file extension.

"OnRefresh" event
-> Called everytime the text needs to be refreshed (for changing the language or anything else)
-> Returns the current language is was refreshed with

"CallRefresh()" method
-> Call the "OnRefresh" event

"ChangeLanguage(SystemLanguage newLanguage)" method
-> Change the language to the given parameter
-> If the given language doesn't exist, the language will be set to the default value

"ChangeLanguageRoutine(startBehavior routine, bool callRefresh)" method
-> used to change the start routine of the localization manager, which can be:
	- Default : for setting to the default language of the game
	- User PC : for selectng the user's PC language, if not supported, it will use the default language
	- Hand Choosen : usefull when the user wants to select a language for himself, if not supported, it will use the default language
-> You can choose if you want to refresh or not the texts after changing routine, by default at "True"

"GetLanguageForSelection(SystemLanguage languageToGet, bool sameAsSelectedLanguage)" method
-> Will return the name of the asked language, usefull for language selection in game options for the player
-> The Second parameter ask if the language's name should be returned in the native language or current language, set at false by default

=======================
[LocalizationComponent]
The Localization component should be refered as a variable (in SerializeField for example) if you want to use it in an external Script

"GetText(string Key)" method
-> Will return the text corresponding to the given Key in parameter, in the given .tsv file of the componenent.
-> The language of the text will be in the current language in the localization manager,
   If the text in said language doesn't exist or language isn't referenced, it will take the one in default language,
   If the text in default language doesn't exist, it will return an error
-> A second parameter exist but is not intended for this kind of usage, pls keep it at false to avoid useless errors

==========================================================================================
If you need more informations, feel free to explore the Sample Scene in the packages files