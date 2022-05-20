using System.Windows;

namespace IronMacbeth.Client
{
    internal static class Extensions
    {
        public static Visibility ToVisibility(this bool boolean) => boolean ? Visibility.Visible : Visibility.Collapsed;
    }
}
