using System;

namespace Wikitude
{
	public enum WTAugmentedRealityMode {
		Geo = 1,
		IR,
		Both
	}

    public enum WTScreenshotCaptureMode {
        Cam,
        CamAndWebView
    }

    public enum WTScreenshotSaveMode {
        PhotoLibrary = 1,
        BundleDirectory = 2,
        Delegate = 3
    }

    public enum WTScreenshotSaveOptions  {
        CallDelegateOnSuccess = 1 << 0,
        SavingWithoutOverwriting = 1 << 1,
        None = 0
    }
}
