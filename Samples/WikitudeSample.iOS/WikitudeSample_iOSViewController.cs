using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Wikitude;

namespace WikitudeSample.iOS
{
	public partial class WikitudeSample_iOSViewController : UIViewController
    {

		WTArchitectView 						_architectView;
		WikitudeSample_ArchitectViewDelegate	_delegate;

        public WikitudeSample_iOSViewController () : base ("WikitudeSample_iOSViewController", null)
        {
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

		#region View Controller Lifecycle
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.


			// Before creating a new Wikitude SDK view, check if the current device is supported or not. If the device is not supported and a view created, it will only be black
			// Depending on the content of the ARchitect World, different hardware requirements exist. To refine those requirements, you can specify the kind of augmented reality experience you want to load afterwards.
			if (WTArchitectView.IsDeviceSupportedForAugmentedRealityMode(WTAugmentedRealityMode.Both)) {

				// create a new delegate object which is used to get status updates from the Wikitude SDK. Those status updates can concern the loading status of the ARchitect World, additional view controller presentation or capturing the screen
				_delegate = new WikitudeSample_ArchitectViewDelegate ();


				// To create a new Wikitude SDK view, simply call the constructor.
				// You can specify the origin and size of the view, as well as pass in a CMMotionManager instance (optional) and the AugmentedRealityMode again.
				// Passing in WTAugmentedRealityMode.IR for example wouldn't start the location services and the thus the location access alert wouldn't appear on first app launch.
				_architectView = new WTArchitectView (this.View.Frame, null, WTAugmentedRealityMode.Both);
				_architectView.Delegate = _delegate;

				// The Wikitude SDK view can be added to the view hierarchy just like any other UIView subclass.
				this.View.AddSubview (_architectView);
			} 
			else
			{
				Console.WriteLine ("This device is not supported by the Wikitude SDK");
			}
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		
			// Start the Wikitude SDK rendering when the view will appear
			this.startAR ();


			// To load an ARchitect World simply pass in a app bundle URL or a URL pointing to a remote resource. Both URLs need to point to a .html file.
			// In this case a ARchitect World from the application bundle is loaded.
			string architectWorldFileName = "Circles.html";
			string architectWorldURL = System.IO.Path.Combine (NSBundle.MainBundle.BundlePath, architectWorldFileName);

			_architectView.LoadArchitectWorldFromUrl (new NSUrl(architectWorldURL, false));
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			// Stop the Wikitude SDK rendering when the view will disappear
			this.stopAR ();
		}
		#endregion

		#region Wikitude SDK Lifecycle
		// Those methods are called from the ViewWill* methods as well as from the AppDelegate.
		public void startAR ()
		{
			if ( !_architectView.IsRunning ) 
			{
				_architectView.Start ();
			}
		}

		public void stopAR ()
		{
			if ( _architectView.IsRunning )
			{
				_architectView.Stop ();
			}
		}
		#endregion


		#region iOS Rotation Handling
		public override bool ShouldAutorotate ()
		{
			return true;
		}
			
		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.AllButUpsideDown;
		}

		public override UIInterfaceOrientation PreferredInterfaceOrientationForPresentation ()
		{
			return UIInterfaceOrientation.Portrait;
		}

		public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);

			// When to view controller is about to initiate the view rotation, call the Wikitude SDK to rotate as well
			_architectView.SetShouldRotate (true, toInterfaceOrientation);
		}
		#endregion


		#region WTArchitectViewDelegate
		class WikitudeSample_ArchitectViewDelegate : WTArchitectViewDelegate
		{
			// Here you can implement the WTArchitectViewDelegate methods to get a method call if something happend in the Wikitude SDK

			// This method is called everytime a 'document.location = 'architectsdk://' redirect is done inside the ARchitect World;
			public override void InvokedURL(WTArchitectView architectView, NSUrl url)
			{
				// Right here we're calling a JavaScript function which creates a new drawable in AR.
				architectView.CallJavaScript ("createCircle(new AR.RelativeLocation(null, -10, 0), '#97FF18');");
			}
		}
		#endregion
    }
}
