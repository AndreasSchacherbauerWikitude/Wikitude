## Xamarin Wikitude Bindings

### Android
__ Not yet implemented__

### iOS
#### Setup
To include the Wikitude SDK into a existing project, simply add the Wikitude.iOS project to your solution and add a `using Wikitude`. The Wikitude SDK is already included in the repository, but If you need to update the Wikitude SDK, simply follow these simple steps:
	
Download the iOS SDK, and inside the `WikitudeSDK.framework` you'll find `WikitudeSDK`. Copy this to your project, then rename it to `WikitudeSDK.a`.

#### Usage
In general there are three steps to do to get the Wikitude SDK running.

1) Create a Wikitude SDK view
2) Load a Architect World
3) Start the Wikitude SDK rendering

There is a sample included with the Wikitude Binding which describes the basic usage by loading an Architect World from the application bundle. It also includes interaction between the Xamarin application and the Architect World.
