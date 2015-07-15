namespace OmniXaml.AppServices.NetCore
{
    using System;
    using System.IO;
    using System.Reflection;

    public class NetCoreResourceProvider : IResourceProvider
    {
        public Stream GetStream(Uri uri)
        {
            var absoluteUri = new Uri(Assembly.GetExecutingAssembly().Location, UriKind.Absolute);
            var finalUri = new Uri(absoluteUri, uri);
            return new FileStream(finalUri.LocalPath, FileMode.Open);
        }
    }
}