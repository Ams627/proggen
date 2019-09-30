// Guids.cs
// MUST match guids.h
using System;

namespace AMS.AMSExtensions
{
    static class GuidList
    {
        public const string guidAMSExPkgString = "3c6063ca-5f33-4889-aaae-387e9d5a0368";
        public const string guidAMSExCmdSetString = "39c54ce6-2aa2-49b9-a912-00a4eee74ede";

        public static readonly Guid guidAMSExCmdSet = new Guid(guidAMSExCmdSetString);
    };
}