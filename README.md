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

