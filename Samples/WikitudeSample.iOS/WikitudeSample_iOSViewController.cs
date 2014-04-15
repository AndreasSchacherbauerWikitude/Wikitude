using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Wikitude;

namespace WikitudeSample.iOS
{
	public partial class WikitudeSample_iOSViewController : UIViewController
    {
		WTArchitectView 		_architectView;

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

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.

			if (WTArchitectView.IsDeviceSupportedForAugmentedRealityMode(WTAugmentedRealityMode.Both)) {

				_delegate = new WikitudeSample_ArchitectViewDelegate ();

				_architectView = new WTArchitectView ();
				_architectView.Delegate = _delegate;
				_architectView.Frame = this.View.Frame;
				this.View.AddSubview (_architectView);
			} 
			else
			{
				Console.WriteLine ("The device is not supported by the Wikitude SDK");
			}
			Console.WriteLine ("View is loaded");
        }

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		
			_architectView.Start ();

			string architectWorldFileName = "Circles.html";
			string architectWorldURL = System.IO.Path.Combine (NSBundle.MainBundle.BundlePath, architectWorldFileName);

			_architectView.LoadArchitectWorldFromUrl (new NSUrl(architectWorldURL, false));
			Console.WriteLine ("View Will Appear " + architectWorldURL);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);

			_architectView.Stop ();
			Console.WriteLine ("View Will Disappear");
		}


		class WikitudeSample_ArchitectViewDelegate : WTArchitectViewDelegate
		{
			public override void InvokedURL(WTArchitectView architectView, NSUrl url)
			{
				Console.WriteLine ("invoked URL");
				architectView.CallJavaScript ("createCircle(new AR.RelativeLocation(null, -10, 0), '#97FF18');");
			}
		}
    }
}
