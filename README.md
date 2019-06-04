# IT-Project
Android Augmented Reality Project from 4 funny students c:

## Setting up Unity
1. Download Unity Hub from https://unity3d.com/get-unity/download
2. Download Unity from Unity Hub (by clicking the blue "Add" button)
3. Select Version Unity 2019.1.3f1
4. Select following modules:
    - Android Build Support 
    - Android SDK & NDK Tools
    - Vuforia Augmented Reality Support
5. Download "DB Browser for SQLite - Standard installer for 64-bit Windows" from https://sqlitebrowser.org/dl/
    
## Setting up Git
1. Download the Git GUI Program of your choice (https://desktop.github.com/)
2. Clone this repository

## Open Project and Setup Unity IDE
1. In UnityHub go to Projects -> Add -> Select the checked out repository on yout computer and add it to UnityHub
2. Open project by clicking on it

### Check if the following options are filled out/selected correctly
3. Edit -> Project Settings -> Editor -> Version Control Mode: "Visible Meta Files"
4. Edit -> Project Settings -> Editor -> Asset Serialization Mode: "Force Text"
5. ARCamera -> Open Vuforia Configuration (on right panel) -> App Licence Key should not be empty
-> if the App Licence Key is empty: 

    Go to https://developer.vuforia.com/vui/auth/login and log in with:
    
    username: selina.feitl@yahoo.de    
    password: Mara7.11.
    
    -> Develop -> Licence Manager -> ITProject -> Licence Key (copy and paste into Unity)
    
### Update Vuforia Version
1. in Vuforia Configuration you should see a message offering you to update to Vuforia Engine 8.1.11
2. Choose the option and unzip downloaded file and start the installation (Unity needs to be closed for that)
3. As Unity Version folder select ...Programme\Unity\Hub\Editor\2019.1.3f1 
4. Finish installation and check in Unity if Vuforia Engine is updated
    
## Build Configuration
1. Open File -> Build Settings and select "Android" and then "Switch platform" (if not already selected)
2. From the Build Settings window open "Player Settings" to check if following configurations are correct:
3. Enable "Vuforia Augmented Reality" in XR Settings
4. Check if Package Name is assigned in Other Settings -> Package Name
5. Android TV compatibility and Android Game should be unchecked
6. Check if Company Name and Product Name (on the top) are non-default Values 

## Connect device and run application
1. Connect your device and enable USB Debugging
2. Select your device in Build Settings -> Build System
3. Select "Build and Run" -> Enter application name for .apk file and the application should launch on your device

## How to commit
1. Add a sophisticated commit message from http://whatthecommit.com/
2. Before committing, please show your local changes to your assigned IT Administrators Philipp or Selina for validation
