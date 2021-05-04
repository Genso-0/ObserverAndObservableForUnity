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
3) A ObservableTransform script is added to a gameobject who can be listened in on.
4) Add the observable gameobject to the consumer gameobject. 
(In the example scene this can be done by adding the observable to the consumer's list toBeAdded and then right clicking on the consumer script and selecting Add observables)

5)The API looks something like this.
   Observer observer = new Observer(); 
   //To Add an action.
   observer.AddEventAction(ObservableEventTypes.OnDisabled, OnTargetDisabled);
   
   //To Remove an action\n
  observer.RemoveEventAction(ObservableEventTypes.OnDisabled, OnTargetDisabled);
 
 //To add an observable
 observer.AddObservable(observableReference);
 
  //To remove an observable
 observer.RemoveObservable(observableReference);
 
 See [Consumer](https://github.com/Genso-0/ObserverAndObservableForUnity/blob/master/Assets/Observables/Scripts/Consumer.cs) script for clearer example.
 
 <!-- LICENSE -->
## License

Distributed under the MIT License. See [LICENSE](https://github.com/Genso-0/ObserverAndObservableForUnity/blob/master/LICENSE) for more information.

<!-- CONTACT -->
## Contact

[@genso_0](https://twitter.com/genso_0)

Project Link: [ObserverAndObservable](https://github.com/Genso-0/ObserverAndObservableForUnity)
