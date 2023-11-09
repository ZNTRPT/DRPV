This README will explain all the controls for the project and provide some useful help with the code.

CONTROLS:
WASD / Arrowkeys to move


CODE HELP:

Unity basic functions
Awake(): Is called only once in lifetime, no matter whether the script is enabled or not.
OnEnable(): Is called when game object's status changes from "disable" to "enable".
Start(): Called once in lifetime after Awake() but before Update(), and the script needs to be enabled.
Update(): Will run every frame
FixedUpdate(): Will run once every physics update.

[SerializeField]
Allows for values to show up and be changed in the inspector allowing for quick changes on the fly. This can also be done with Public variables 
Example: 
	[SerializeField] private float speed = 3.0f;
This will create a private variable called speed that can be assigned a float value. 
It will have a default value of 3, and will show up in the inspector of the engine to allow for you to change its value without going back into the code


[Range()]
Using a Range attribute on a serialized field will make it show up with a slider in the inspector.
Example:
	 [SerializeField, Range(5,10)] private float spawnRange = 5;
This will show a slider that will have the values from 5 to 10 on it and will start at 5. 


#region --- #endregion
Regions are sections of code that are collapeable without the changing the scope of access.
Example:
	bool boolean = false;
	
	#region
	private void Func()
	{
		boolean = true;
	}
	#endregion
In this example there is a private variable called boolean and a region with a function inside of it. 
This region can be collaped inside of the IDE to make the rest of the code more readable. 
The function will still have access to the variable dispite it not being in the region.


Inheritance 
Inheritance is when one class is based off another one that it shares the majority of its functionallity with.
Example:
	ClassB : ClassA
	{
		OnDeath()
		{
			Respawn();
		}
	}
In this example ClassB will do the same as ClassA, however when ClassB dies it will not follow the same rules as ClassA, instead it will respawn.


Increment and decrement operators
An increment or decrement operator is used to change the value of something up or down by 1. This is a quick short hand version of writing that "x = x - 1;" or "x = x + 1".
Example:
	int X = 5;
	int Y = 5;
	X++;
	Y--;

Output: X is now equal to 6, Y is now equal to 4.


This
This is a keyword that refers to the instance of the script that is running on a game object. 
Example: 
	this.transform.position;
In this example "this" will get the instance of the script running on an object with a transform comoponent and read the position of the object.


IEnumerators/Coroutines
Coroutines are functions that will run along side everything else. As a result of this, you can pause them from running without pausing the game using the yield statement. 
When a yield statement is used, the coroutine pauses execution and automatically resumes at the next frame. See the Coroutines documentation for more details.
Example: 
	IEnumerator Timer()
    {
        yield return new WaitForSeconds(7);
        Destroy(This.gameobject);
    }
In this example when the coroutine is started it will wait for 7 seconds before resuming then destroy the object. During this 7 seconds the game will continue to run.
