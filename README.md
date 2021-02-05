# Risul_ArmyClash

## Key Features:<br>
<ls>
<li> Dependency Injection Framework (Zenject) to resolve dependencies.<br>
<li> Factory pattern: Zenject Factory for dynamic object creation. <br>
<li> Mode-View-Controller pattern with reactive extension (UniRX) for data binding.  <br>
<li> C# Job System and Burst Compiler based battle simulation - Targeting and Movement of Units. <br>
<li> Custom Editor for game designer for tuning and extending the project. <br>
<li> Zenject Signal based communication between different decoupled system. <br>
<li> Unit tests to ensure accuracy of hp, attack, movement speed, and attack speed calculation of Unit Model, data objects available in the resources folder <br>
</ls>

## Design Choice:

<li> SOID Principle
<li> Event Driven based on Domain of classes
<li> Less MonoBehaviours (only 4) without any update method called
<li> Testable Architecture

## Project Entry Points:

<li> <b>Project Context Installer:</b> To load data from JSON and scriptable object to make it available across the scene.
<li> <b>MenuSceneInstaller:</b> Menu Related dependency container
<li> <b>Game Scene Installer:</b> Hosts signals, factory for game scene.
<br>
![Alt text](Picture1.png?raw=true "Installers")

## Tools for Designers:
### Game Settings

<li> Directory: Assets/Resources/Data/Settings/GameSettings
<li> To edit Unit Placement add and edit value for Hp, Atk, Size of Guilds etc
<br>
![Alt text](Picture2.png?raw=true "Installers")
  
### Unit Configuration Data
<li> Directory: Assets/Resources/Data/UnitConfigs/UnitConfigurationData
<li> The Data Container for ShapeModel, ShapeModel and Size Model
<li> Editing can be done easily from Scriptable Object
<li> To add new Color, Shape, and Size, we need to add new Enums to specific model type
<br>
![Alt text](Picture3.png?raw=true "Installers")
  
### ColorToShapeMap
Directory: Assets/Resources/Data/UnitConfigs/ColorToShapeMap 
The tool with the custom editor to assign values for Specific Color and Specific Shape.
<li> <b>Clear Table:</b> Generates a new table with Color and Shape Combinations
<li> <b>Expand Table:</b> When a new enum is added for color, shape, and size, Expand table is used to add more rows keeping the old data.
<li> <b>Save:</b> Saves the data in JSON file (ColorMap.json) in the same folder in resources. 
<br>
![Alt text](Picture4.png?raw=true "Installers")
  
## Unit Tests

<li> Checks instance of a unit of specific color, size and shape to calculate attack, hp, movement speed and attack speed
<li> Checks the availability of the settings files in data folder
![Alt text](Picture5.png?raw=true "Installers")


