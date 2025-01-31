
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UtilsModule;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace OpenCVForUnity.PhotoModule
{

    // C++: class TonemapDrago
    /**
     @brief Adaptive logarithmic mapping is a fast global tonemapping algorithm that scales the image in
     logarithmic domain.
     
     Since it's a global operator the same function is applied to all the pixels, it is controlled by the
     bias parameter.
     
     Optional saturation enhancement is possible as described in @cite FL02 .
     
     For more information see @cite DM03 .
     */

    public class TonemapDrago : Tonemap
    {

        protected override void Dispose(bool disposing)
        {

            try
            {
                if (disposing)
                {
                }
                if (IsEnabledDispose)
                {
                    if (nativeObj != IntPtr.Zero)
                        photo_TonemapDrago_delete(nativeObj);
                    nativeObj = IntPtr.Zero;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }

        }

        protected internal TonemapDrago(IntPtr addr) : base(addr) { }

        // internal usage only
        public static new TonemapDrago __fromPtr__(IntPtr addr) { return new TonemapDrago(addr); }

        //
        // C++:  float cv::TonemapDrago::getSaturation()
        //

        public float getSaturation()
        {
            ThrowIfDisposed();

            return photo_TonemapDrago_getSaturation_10(nativeObj);


        }


        //
        // C++:  void cv::TonemapDrago::setSaturation(float saturation)
        //

        public void setSaturation(float saturation)
        {
            ThrowIfDisposed();

            photo_TonemapDrago_setSaturation_10(nativeObj, saturation);


        }


        //
        // C++:  float cv::TonemapDrago::getBias()
        //

        public float getBias()
        {
            ThrowIfDisposed();

            return photo_TonemapDrago_getBias_10(nativeObj);


        }


        //
        // C++:  void cv::TonemapDrago::setBias(float bias)
        //

        public void setBias(float bias)
        {
            ThrowIfDisposed();

            photo_TonemapDrago_setBias_10(nativeObj, bias);


        }


#if (UNITY_IOS || UNITY_WEBGL) && !UNITY_EDITOR
        const string LIBNAME = "__Internal";
#else
        const string LIBNAME = "opencvforunity";
#endif



        // C++:  float cv::TonemapDrago::getSaturation()
        [DllImport(LIBNAME)]
        private static extern float photo_TonemapDrago_getSaturation_10(IntPtr nativeObj);

        // C++:  void cv::TonemapDrago::setSaturation(float saturation)
        [DllImport(LIBNAME)]
        private static extern void photo_TonemapDrago_setSaturation_10(IntPtr nativeObj, float saturation);

        // C++:  float cv::TonemapDrago::getBias()
        [DllImport(LIBNAME)]
        private static extern float photo_TonemapDrago_getBias_10(IntPtr nativeObj);

        // C++:  void cv::TonemapDrago::setBias(float bias)
        [DllImport(LIBNAME)]
        private static extern void photo_TonemapDrago_setBias_10(IntPtr nativeObj, float bias);

        // native support for java finalize()
        [DllImport(LIBNAME)]
        private static extern void photo_TonemapDrago_delete(IntPtr nativeObj);

    }
}
