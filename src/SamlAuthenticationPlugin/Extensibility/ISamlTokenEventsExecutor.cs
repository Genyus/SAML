using Telligent.Evolution.Extensibility.Version1;
using Telligent.Services.SamlAuthenticationPlugin.Components;

namespace Telligent.Services.SamlAuthenticationPlugin.Extensibility
{
    public interface ISamlTokenEventsExecutor: ISingletonPlugin
    {
        event EventAfterCreateTokenHandler AfterCreate;
        event EventAfterUpdateTokenHandler AfterUpdate;
        void OnAfterUpdate(SamlTokenData token);
        void OnAfterCreate(SamlTokenData token);
    }
}
