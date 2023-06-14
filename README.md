# asteroid-unity
The classic asteroid game created in Unity game engine showcasing the unity features like addressable and NUnit tests

Unity Version – 2020.3.37f1

Note: After importing the project if you come across any error, navigate to Window -> Asset Management -> Addressables and the Addressable Groups window will open. Select Default Build Script for the addressable to make the bundle.
  
 
The game plays best in Landscape mode.
        Controls – Left, Right arrows for rotation, Up arrow for Thrust, Z for shooting 

Ideologies used while creating this game test’s solution:
1.	There should be only one update loop throughout the game instance and all the entities should register to Game Loop class after getting instantiated. This class should be responsible for running Update and Fixed Update loops for the entities.
2.	Classes should have minimal dependencies on each other and should exist as their own individual entity.
3.	Usage of a dependency injection framework - Zenject
4.	SOLID Principles
5.	Communication between different class objects should happen via signal classes.
6.	Maintain code consistency across the entire project.

Design Patterns implemented:
1.	Abstract Factory 
2.	Observer
3.	State Pattern
4.	Factory Method
5.	Façade

Unity features used:
1.	Dependency Injection Framework - Zenject
2.	Scriptable objects - for storing prefab references and game specific values.
3.	Addressable with Embedded packages (as there is no remote server setup) 
4.	N unit / Test Runner - for performing play mode unit tests.

Decision making Scenarios:
1.	Wraparound of game objects in screen (to detect if object goes out from camera’s view)
a.	Using colliders – Would lead to messy situations because had to micromanage if an object gets spawned outside the view, then first collision had to be skipped because it is entering the view.  
b.	On Invisible callback in MonoBehaviour – Reliable approach but not testable in editor because it does not work properly due to the object been rendered from Scene view. The only way to test it is to maximize the Game View.
c.	Points defined with screen width & height - Reliable and consistent because it is dependent on the screen co-ordinates converted to world positions – Went with this approach.

2.	Simulate the thrust movement of spaceship as close to Asteroids game.
a.	Simulate via change in positions and by gradually reducing the speed by time. 
Result: A tighter turning radius felt blocky, it was nowhere close to the original game.
b.	Rigidbody2d - Add Force for trust movement & transform.Rotation for rotation.
Result - Initially the ship was overshooting in terms of movement, after tweaking the force values and linear drag, the result was closer to the original game.
3.	Core game engine – Custom Update for moving objects that gets triggered from one unity update method, core behavior for objects in the scene, managing destruction and remove them from entities list.
4.	Creating unit tests - Editor mode or Play Mode Testing - As I wanted to do gameplay related test cases, I shifted my focus to Play Mode testing.
5.	Play Mode test cases - when life is deducted after player ship collides with asteroid, saucer and saucer bullet, game over state when remaining lives == 0.

 
Challenges faced-

Problem 1 - Setting up the Unit Tests framework:
Got errors for TextMeshPro, AddressableAssets, ResourceManager classes after creating a gameplay assembly for getting references for the classes used in gameplay.
Solution - Add the assemblies inside 'Assembly Definition References' for that assembly.
 

Note: After importing the Zenject unitypackage, there were compiler errors related to zenject containers. Solution was to add the remaining Zenject packages (Zenject and Zenject-TestFramework)

Problem 2 – The tests have a [UnityTests] attributes meaning the test which run with coroutines, and they do not work synchronously when running all of them one after the another. 
Workaround - They pass when tested individually but fail when tested as Run All, it is currently a limitation because all the tests in the project revolve around only one object and action i.e., Ship collision and life deduction. Added a prefix “UT_<TestNameForSpecificCondition>” to identify which of these needs to test individually and not as a batch.

Problem 3 – All the tests written before importing Zenject package started failing because it was written in conjunction without the injection part because the dependencies were added manually. 
Solution – The tests had to be written keeping in mind the dependencies that each component has with the DiContainer.

 

