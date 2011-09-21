namespace LibHipChat
{
    public class WebClientFactory
    {
        public WebClient CreateWebClient (ActionKey action)
        {
            //TODO: Don't use switch. Implement a strategy
            switch (action)
            {
                
            }
            //TODO: Just for build
            return new WebClient();
        }
    }
}