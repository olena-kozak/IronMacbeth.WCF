namespace IronMacbeth.Client
{
    public interface IPageViewModel
    {
        string PageViewName { get; }

        void Update();
    }
}