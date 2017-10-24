namespace OneDayWorkshop01.Messages
{
    public class GithubStatusChangedMessage
    {
        public bool IsAuthenticated { get; set; }

        public string DefaultRepository { get; set; }

        public bool AllowNavigation
        {
            get
            {
                if (!IsAuthenticated)
                    return false;
                else if (string.IsNullOrEmpty(DefaultRepository))
                    return false;
                else
                    return true;
            }
        }
    }
}
