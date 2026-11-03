using Grasshopper.Kernel;

using System;
using System.Drawing;

namespace AdaptiveNetwork.Products.Splat
{
    public sealed class PluginInfo : GH_AssemblyInfo
    {
        public override string Name => "AdaptiveNetwork.Products.Splat";

        //Return a 24x24 pixel bitmap to represent this GHA library.
        public override Bitmap Icon => Properties.Resources.splat_16;

        //Return a short string describing the purpose of this GHA library.
        public override string Description => "Squish Grasshopper";

        public override Guid Id => new Guid("896ce287-84f9-4be3-93fe-010613c841be");

        //Return a string identifying you or your company.
        public override string AuthorName => "Jack Walker";

        //Return a string representing your preferred contact details.
        public override string AuthorContact => "development@theadaptivenetwork.org";

        //Return a string representing the version.  This returns the same version as the assembly.
        public override string AssemblyVersion => GetType().Assembly.GetName().Version.ToString();

    }
}