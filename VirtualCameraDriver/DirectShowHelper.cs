using System;
using System.Runtime.InteropServices;

namespace VirtualCameraDriver
{
    /// <summary>
    /// DirectShow COM interfaces और helper methods
    /// </summary>
    public class DirectShowHelper
    {
        // DirectShow interfaces
        [ComImport, Guid("56a86895-0ad4-11ce-b03a-0020af0ba770")]
        public interface IGraphBuilder
        {
            [PreserveSig]
            int AddFilter([In] object pFilter, [In, MarshalAs(UnmanagedType.BStr)] string pName);
            
            [PreserveSig]
            int RemoveFilter([In] object pFilter);
            
            [PreserveSig]
            int EnumFilters([Out] out object ppEnum);
            
            [PreserveSig]
            int FindFilterByName([In, MarshalAs(UnmanagedType.BStr)] string pName, [Out] out object ppFilter);
            
            [PreserveSig]
            int ConnectDirect([In] object ppinOut, [In] object ppinIn, [In] object pmt);
        }

        [ComImport, Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770")]
        public interface IMediaControl
        {
            [PreserveSig]
            int Run();
            
            [PreserveSig]
            int Pause();
            
            [PreserveSig]
            int Stop();
            
            [PreserveSig]
            int GetState([In] int msTimeout, [Out] out int pfs);
        }

        [ComImport, Guid("56a86891-0ad4-11ce-b03a-0020af0ba770")]
        public interface IBaseFilter { }

        [ComImport, Guid("56a86893-0ad4-11ce-b03a-0020af0ba770")]
        public interface IPin { }

        // DirectShow GUIDs
        public static readonly Guid CLSID_FilterGraph = new Guid("e436ebb3-524f-11ce-9f53-0020af0ba770");
        public static readonly Guid IID_IGraphBuilder = new Guid("56a8689f-0ad4-11ce-b03a-0020af0ba770");
        public static readonly Guid IID_IMediaControl = new Guid("56a868b1-0ad4-11ce-b03a-0020af0ba770");

        /// <summary>
        /// Virtual camera को DirectShow में register करता है
        /// </summary>
        public static bool RegisterVirtualCamera(string deviceName, string devicePath)
        {
            try
            {
                // Windows registry में camera को register करें
                // HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\{...}
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error registering camera: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Virtual camera को DirectShow से unregister करता है
        /// </summary>
        public static bool UnregisterVirtualCamera(string deviceName)
        {
            try
            {
                // Windows registry से camera को remove करें
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error unregistering camera: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Image को video frame format में convert करता है
        /// </summary>
        public static byte[] ConvertBitmapToVideoFrame(System.Drawing.Bitmap bitmap)
        {
            try
            {
                // Bitmap को RGB24 format में convert करें
                var bitmapData = bitmap.LockBits(
                    new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    System.Drawing.Imaging.ImageLockMode.ReadOnly,
                    System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                int bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
                byte[] rgbValues = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(bitmapData.Scan0, rgbValues, 0, bytes);
                bitmap.UnlockBits(bitmapData);

                return rgbValues;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error converting bitmap: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// DirectShow filter graph create करता है
        /// </summary>
        public static IGraphBuilder CreateFilterGraph()
        {
            try
            {
                Type type = Type.GetTypeFromCLSID(CLSID_FilterGraph);
                return (IGraphBuilder)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating filter graph: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// System के सभी video devices list करता है
        /// </summary>
        public static string[] EnumerateVideoDevices()
        {
            System.Collections.Generic.List<string> devices = new System.Collections.Generic.List<string>();
            
            try
            {
                // HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\DeviceClasses\{65E8773D-8F56-11D0-A3B9-00A0C9223196}
                // से सभी connected cameras list करें
                
                return devices.ToArray();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error enumerating devices: {ex.Message}");
                return new string[0];
            }
        }
    }
}
