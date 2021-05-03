<!-- ABOUT THE PROJECT -->
## Observer And Observable For Unity
This is a way to create links between game objects where an observer needs to listen in on state changes of another object. 
Such as position change, enabled, disabled and destroyed.

### Built With
Unity 2020.1.4f1 

### Language
C#

<!-- GETTING STARTED -->
## Getting Started
1) Open up example scene to see how things are set up.
2) A consumer script (or any name you chose) is placed on the game object you wish to be listening in on state changes of other objects.
3) A ObservableTransform script is added to gameobject who can be listened in on.
4) Add the object with the observable script to the consumer script 
(in the example scene this can be done by adding the observable to the consumer list and then right clicking on the consumer script and selecting Add observables)

5)In the consumer script define all the functions you wish to be used as action methods like so:
 [observer.AddEventAction(Observer.EventTypes.OnDisabled, OnTargetDisabled);]
 
 See [Consumer](https://github.com/Genso-0/ObserverAndObservableForUnity/blob/master/Assets/Observables/Scripts/Consumer.cs) script for clearer example.
 
 <!-- LICENSE -->
## License

Distributed under the MIT License. See [LICENSE](https://github.com/Genso-0/ObserverAndObservableForUnity/blob/master/LICENSE) for more information.

<!-- CONTACT -->
## Contact

[@genso_0](https://twitter.com/genso_0)

Project Link: [ObserverAndObservable](https://github.com/Genso-0/ObserverAndObservableForUnity)
