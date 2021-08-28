using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace Audio_Player
{
    public static class PickFile
    {
        public static async Task<FileResult> singleFile(PickOptions options)
        {
            try
            {
                var result = await FilePicker.PickAsync(options);

                return result;
            }

            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
