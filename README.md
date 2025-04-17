# asteroid-unity
The classic asteroid game created in Unity game engine showcasing the unity features like addressable and NUnit tests

Unity Version – 2022.3.51f1

Note: After importing the project if you come across any error, navigate to Window -> Asset Management -> Addressables and the Addressable Groups window will open. Select Default Build Script for the addressable to make the bundle.
  
 
The game plays best in Landscape mode.
        Controls – Left, Right arrows for rotation, Up arrow for Thrust, Z for shooting.

## Updates to the legacy code:
The literature mentioned after this section was written 2 years ago and the idealogies still exist in this project.
After reviewing the code currently, I noticed the following issues/loopholes and applied the following solutions to make this project legible and easier to understand the general flow of the program.

- **The entry point** was not clearly defined. The game started when the assets get loaded from the addressable address and from there a signal was triggered to notify the observers which again triggered another signal to start the game.
  <br> Furthermore, the usage of signal manager for GameStateUpdateSignal and GameConfigLoadedSignal does not clearly define if the execution of these signals are the entry point in the program.
  <br> This lead to spaghetti code and a continuous loop where the start point was not in control to start the game.
  <br> This approach further led to a pitfall when we need to add other components in the game which might load assets, initialise shop, load inventory from server etc. **This was not scalable**
  <br> <br> **Solution** : Startup.cs is created, and it clearly defines the entry point of the game. It enters `GameLoadState` and this state loads all the addressables needed to run the game effectively.
  _<br>  It also loads all the UI elements needed in the game._
  <br> <br>
- **Inconsistent** folder structure
  <br> Folder structure was not module specific. It should have been spilt into specific modules. 
  <br> **For instance** : Folder name ‘Scripts’ is a generic folder, and it should be updated to something more meaningful if we need to make the game modular and scalable. 
  <br> <br>
- **Game states were only utilized for UI toggling** and an independent system was running and taking care of game systems. 
  <br> **Solution :** Game States will directly communicate with respective managers for doing respective operations based on their naming and functioning.
  <br> The _usage of enums_ are dropped because we can manage game states from respective classes. <br>
- **UI Panels were rendered in a single canvas** MenuManager was singleton class created lazily, and it was using static methods instead of Dependency Injection
  <br> **Solution :** Separate UI Screen into its own independent canvases. These UI prefab screens have their own addressable address defined and the addressable label 'ui' where the asset manager instantiates the prefabs inside the UI manager. 

# More improvements? 
- We can get rid of SignalManager and eliminate extra noise in the project. It will pose issues when we want to move the game to multiplayer mode because relying on signals for state changes would pose a threat leading to irregular and unexpected behaviours in the game.

# Ideologies used while creating this solution:
1.	There should be only one update loop throughout the game instance and all the entities should register to Game Loop class after getting instantiated. This class should be responsible for running Update and Fixed Update loops for the entities.
2.	Classes should have minimal dependencies on each other and should exist as their own individual entity.
3.	Usage of a dependency injection framework - Zenject
4.	SOLID Principles
5.	Communication between different class objects should happen via signal classes.
6.	Maintain code consistency across the entire project.

# Design Patterns implemented:
1.	Abstract Factory 
2.	Observer
3.	State Pattern
4.	Factory Method
5.	Façade

# Unity features used:
1.	Dependency Injection Framework - Zenject
2.	Scriptable objects - for storing prefab references and game specific values.
3.	Addressable with Embedded packages (as there is no remote server setup) 
4.	N unit / Test Runner - for performing play mode unit tests.

# Decision-making Scenarios:
* Wraparound of game objects in screen (to detect if object goes out from camera’s view)
  -	Using colliders could lead to messy situations because had to micromanage if an object gets spawned outside the view, then first collision had to be skipped because it is entering the view.  
  -	On Invisible callback in MonoBehaviour : Reliable approach but not testable in editor because it does not work properly due to the object been rendered from Scene view. The only way to test it is to maximize the Game View.
  -	Points defined with screen width & height : Reliable and consistent because it is dependent on the screen co-ordinates converted to world positions – Went with this approach.
* Simulate the thrust movement of spaceship as close to Asteroids game.
  -	Simulate via change in positions and by gradually reducing the speed by time. 
  <br> **Result**: A tighter turning radius felt blocky, it was nowhere close to the original game.
  - Rigidbody2d - Add Force for trust movement & transform.Rotation for rotation.
  <br> **Result** - Initially the ship was overshooting in terms of movement, after tweaking the force values and linear drag, the result was closer to the original game.
* Core game engine – Custom Update for moving objects that gets triggered from one unity update method, core behavior for objects in the scene, managing destruction and remove them from entities list.
* Creating unit tests - Editor mode or Play Mode Testing - As I wanted to do gameplay related test cases, I shifted my focus to Play Mode testing.
* Play Mode test cases - when life is deducted after player ship collides with asteroid, saucer and saucer bullet, game over state when remaining lives == 0.

# Challenges faced

**Problem** 1 - Setting up the Unit Tests framework:
Got errors for TextMeshPro, AddressableAssets, ResourceManager classes after creating a gameplay assembly for getting references for the classes used in gameplay.
Solution - Add the assemblies inside 'Assembly Definition References' for that assembly.
 

Note: After importing the Zenject unity package, there were compiler errors related to zenject containers. Solution was to add the remaining Zenject packages (Zenject and Zenject-TestFramework)

**Problem** 2 – The tests have a [UnityTests] attributes meaning the test which run with coroutines, and they do not work synchronously when running all of them one after the another. 
Workaround - They pass when tested individually but fail when tested as Run All, it is currently a limitation because all the tests in the project revolve around only one object and action i.e., Ship collision and life deduction. Added a prefix “UT_<TestNameForSpecificCondition>” to identify which of these needs to test individually and not as a batch.

**Problem** 3 – All the tests written before importing Zenject package started failing because it was written in conjunction without the injection part because the dependencies were added manually. 
Solution – The tests had to be written keeping in mind the dependencies that each component has with the DiContainer.

 

