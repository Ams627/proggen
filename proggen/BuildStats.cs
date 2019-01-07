// Copyright (c) Adrian Sims 2018
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System;
using System.IO;
using System.Reflection;

namespace Proggen
{
    static class BuildStats
    {
        /// <summary>
        /// The number of bytes to read from the assembly header
        /// </summary>
        private const int HeaderSize = 2048;

        /// <summary>
        /// Extension to return the build date and time for an assembly. Credits to Jeff Attwood for this.
        /// (https://blog.codinghorror.com/determining-build-date-the-hard-way/)
        /// 
        /// </summary>
        /// <param name="assembly">A System.Reflection.Assembly</param>
        /// <param name="targetTimeZone">null for UTC</param>
        /// <returns></returns>
        public static DateTime GetBuildDate(this Assembly assembly, TimeZoneInfo targetTimeZone = null)
        {
            var localtime = default(DateTime);
            try
            {
                var filePath = assembly.Location;
                const int peheaderOffset = 60;
                const int linkerTimestampOffset = 8;

                var buffer = new byte[HeaderSize];

                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    stream.Read(buffer, 0, HeaderSize);
                }

                var offset = BitConverter.ToInt32(buffer, peheaderOffset);
                var secondsSince1970 = BitConverter.ToInt32(buffer, offset + linkerTimestampOffset);
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

                var linkTimeUtc = epoch.AddSeconds(secondsSince1970);

                var tz = targetTimeZone ?? TimeZoneInfo.Local;
                localtime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);
            }
            catch (Exception ex)
            {
                // An exception here should be exceedingly rare and there is really nothing we can do here, so just write to the
                // output window. In general not knowing the build date will be non-fatal
                System.Diagnostics.Debug.WriteLine("GetBuildDate failed - exception is" + ex.ToString());
            }

            return localtime;
        }
    }
}