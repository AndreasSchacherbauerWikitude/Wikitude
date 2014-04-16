using System;

using MonoTouch.Foundation;
using MonoTouch.CoreLocation;
using MonoTouch.CoreMotion;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

namespace Wikitude {

    [Model, BaseType (typeof (NSObject))]
    public partial interface WTArchitectViewDelegate {

		[Export ("architectView:didFinishLoad:")]
		void DidFinishLoad (WTArchitectView architectView, NSUrl url);

		[Export ("architectView:didFailLoadWithError:")]
		void DidFailLoadWithError (WTArchitectView architectView, NSError error);

        [Export ("architectView:invokedURL:")]
        void InvokedURL (WTArchitectView architectView, NSUrl url);

        [Export ("architectView:didCaptureScreenWithContext:")]
        void DidCaptureScreenWithContext (WTArchitectView architectView, NSDictionary context);

        [Export ("architectView:didFailCaptureScreenWithError:")]
        void DidFailCaptureScreenWithError (WTArchitectView architectView, NSError error);

		[Export ("presentingViewControllerForViewControllerPresentationInArchitectView:")]
		UIViewController PresentingViewController (WTArchitectView architectView);

		[Export ("architectView:willPresentViewController:onViewController:")]
		void WillPresentViewController (WTArchitectView architectView, UIViewController presentedViewController, UIViewController presentingViewController);
    }

    [BaseType (typeof (UIView))]
    public partial interface WTArchitectView {

		[Static, Export ("isDeviceSupportedForAugmentedRealityMode:")]
		bool IsDeviceSupportedForAugmentedRealityMode (WTAugmentedRealityMode supportedARMode);

		[Static, Export ("versionNumber")]
		string VersionNumber { get; }

		[Export ("initWithFrame:motionManager:augmentedRealityMode:")]
		IntPtr Constructor (System.Drawing.RectangleF frame, CMMotionManager motionManagerOrNil, WTAugmentedRealityMode augmentedRealityMode);

		[Export ("initializeWithKey:motionManager:")]
		void InitializeWithKey (string key, CMMotionManager motionManager);

        [Export ("delegate", ArgumentSemantic.Assign)]
        WTArchitectViewDelegate Delegate { get; set; }

		[Export ("desiredLocationAccuracy")]
		Double DesiredLocationAccuracy { get; set; }

		[Export ("desiredDistanceFilter")]
		Double DesiredDistanceFilter { get; set; }

		[Export ("shouldWebViewRotate")]
		bool ShouldWebViewRotate { get; set; }

		[Export ("setLicenseKey:")]
		void SetLicenseKey (string licenseKey);

		[Export ("loadArchitectWorldFromUrl:")]
		void LoadArchitectWorldFromUrl (NSUrl architectWorldUrl);

		[Export ("stop")]
		void Stop ();

		[Export ("start")]
		void Start ();

        [Export ("isRunning")]
        bool IsRunning { get; }

        [Export ("callJavaScript:")]
        void CallJavaScript (string javaScript);

		[Export ("captureScreenWithMode:usingSaveMode:saveOptions:context:")]
		void CaptureScreen (WTScreenshotCaptureMode captureMode, WTScreenshotSaveMode saveMode, WTScreenshotSaveOptions options, NSDictionary context);

        [Export ("injectLocationWithLatitude:longitude:altitude:accuracy:")]
        void InjectLocation (Double latitude, Double longitude, Double altitude, Double accuracy);

        [Export ("injectLocationWithLatitude:longitude:accuracy:")]
		void InjectLocation (Double latitude, Double longitude, Double accuracy);

        [Export ("useInjectedLocation")]
        bool UseInjectedLocation { set; }

        [Export ("isUsingInjectedLocation")]
        bool IsUsingInjectedLocation { get; }

        [Export ("cullingDistance")]
        float CullingDistance { get; set; }

        [Export ("clearCache")]
        void ClearCache ();

        [Export ("setShouldRotate:toInterfaceOrientation:")]
        void SetShouldRotate (bool shouldAutoRotate, UIInterfaceOrientation interfaceOrientation);

        [Export ("isRotatingToInterfaceOrientation")]
        bool IsRotatingToInterfaceOrientation { get; }
		       
        [Export ("motionManager")]
        CMMotionManager MotionManager { get; }



		[Field ("kWTScreenshotBundleDirectoryKey", "__Internal")]
		NSString WTScreenshotBundleDirectoryKey { get; }

		[Field ("kWTScreenshotSaveModeKey", "__Internal")]
		NSString WTScreenshotSaveModeKey { get; }

		[Field ("kWTScreenshotCaptureModeKey", "__Internal")]
		NSString WTScreenshotCaptureModeKey { get; }

		[Field ("kWTScreenshotImageKey", "__Internal")]
		NSString WTScreenshotImageKey { get; }
    }
}
