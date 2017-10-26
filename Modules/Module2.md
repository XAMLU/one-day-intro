## Register App with GitHub

1. Open the following url: [https://github.com/settings/applications/new](https://github.com/settings/applications/new)
    1. Naming convention XU_Oredev2017_YourName
1. Get your Application Callback URL:
    ```csharp
    var callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
    ```
1. Home Page URL: [http://xamlu.com/XU_Oredev2017_\<YourName\>]([http://xamlu.com/XU_Oredev2017_\<YourName\>)
1. Set app callback URL
1. In Visual Studio, add a folder `Services` at the root level of your project.
1. Add `SettingsService.cs` to the `Services` folder.
1. Add `GitHubSevice.cs` to the `Services` folder.
1. Add the following XAML to `HomePage.xaml`:
```xml
    <Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}" Padding="32">

        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition />
        </Grid.RowDefinitions>

        <TextBlock Text="Home Page" Style="{StaticResource HeaderTextBlockStyle}" />

        <StackPanel Grid.Row="1" x:Name="WaitUI" Visibility="Visible">
            <ProgressBar IsIndeterminate="True" />
            <TextBlock Style="{StaticResource TitleTextBlockStyle}" Text="Please wait" />
        </StackPanel>

        <StackPanel Grid.Row="1" x:Name="LoggedOutUI" Visibility="Collapsed">
            <TextBlock Style="{StaticResource TitleTextBlockStyle}" Text="Authentication is required" />
            <Button Content="Login now" Click="Login_Clicked" />
        </StackPanel>

        <StackPanel Grid.Row="1" x:Name="LoggedInUI" Visibility="Collapsed">
            <TextBlock Style="{StaticResource TitleTextBlockStyle}" x:Name="UserNameTextBlock" Text="Welcome, {0}" />
        </StackPanel>

    </Grid>
 ```
1. Add code to handle the login on the `HomePage`
```csharp
    private async Task LoginAsync()
    {
        var callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
        var startUri = new Uri($"https://github.com/login/oauth/authorize?" +
                                $"client_id={ClientId}&redirect_uri={Uri.EscapeUriString(callbackUri.ToString())}" +
                                $"&scope=read_stream repo&display=popup&response_type=token");
        var webAuthResult = await WebAuthenticationBroker.AuthenticateAsync(
            WebAuthenticationOptions.None,
            startUri);

        switch (webAuthResult.ResponseStatus)
        {
            case WebAuthenticationStatus.Success:
                var resultAsUri = new Uri(webAuthResult.ResponseData);
                var names = new WwwFormUrlDecoder(resultAsUri.Query);
                var code = names.Single(x => x.Name == "code").Value;
                // var names = System.Web.HttpUtility.ParseQueryString(resultAsUri.Query);
                // var code = names["code"];

                // I now have my single use code. Now I need to get my token!

                var client = new GitHubClient();
                await client.GetTokenFromCode(code);
                //var auths = client.GetUserAuthorizations();
                var user = client.GetUserDetails();
                var searchResults = client.SearchRespositories("template10");
                var repository = client.GetRepository(searchResults.items[0].owner, "template10");
                var issues = client.GetRepositoryIssues(repository);

                var issue = new GitHubCreateIssue
                {
                    title = "A sample issue from UWP test app",
                    body = "# Test issue" + Environment.NewLine +
                            "1. First Line." + Environment.NewLine +
                            "1. Second Line",
                    assignee = user.login,
                    labels = new[] {"Bug"}
                };

                //var createdIssue = client.PostRepositoryIssue(repository, issue);

                Result.Text = "Complete";

                break;

            case WebAuthenticationStatus.UserCancel:
                break;
            case WebAuthenticationStatus.ErrorHttp:
                break;
        }
    }
```