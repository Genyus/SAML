using System.Collections.Generic;
using Telligent.Evolution.Extensibility.Events.Version1;
using Telligent.Services.SamlAuthenticationPlugin.Extensibility;

namespace Telligent.Services.SamlAuthenticationPlugin.Components
{
    public class ApiTokenArgs
    {
        public string AvatarUrl { get; set; }
        public string NameId { get; set; }
        public string CommonName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public List<SamlAttribute> Attributes { get; set; }
    }

    public delegate void EventAfterCreateTokenHandler(ApiTokenArgs e);
    public delegate void EventAfterUpdateTokenHandler(ApiTokenArgs e);

    public class SamlTokenEventsExecutor : EventsBase, ISamlTokenEventsExecutor
    {
        private readonly object _afterTokenCreate = new object();
        private readonly object _afterTokenUpdate = new object();

        public string Name => "SAML token events";

        public string Description => "Defines an event API for SAML token processing";

        public void Initialize() { }

        public event EventAfterCreateTokenHandler AfterCreate
        {
            add { Add(_afterTokenCreate, value); }
            remove { Remove(_afterTokenUpdate, value); }
        }

        public event EventAfterUpdateTokenHandler AfterUpdate
        {
            add { Add(_afterTokenUpdate, value); }
            remove { Remove(_afterTokenUpdate, value); }
        }

        public void OnAfterCreate(SamlTokenData token)
        {
            Get<EventAfterCreateTokenHandler>(_afterTokenCreate)?.Invoke(new ApiTokenArgs()
            {
                AvatarUrl = token.AvatarUrl,
                NameId = token.NameId,
                CommonName = token.CommonName,
                Email = token.Email,
                UserName = token.UserName,
                UserId = token.UserId,
                Attributes = token.Attributes
            });
        }

        public void OnAfterUpdate(SamlTokenData token)
        {
            Get<EventAfterUpdateTokenHandler>(_afterTokenUpdate)?.Invoke(new ApiTokenArgs()
            {
                AvatarUrl = token.AvatarUrl,
                NameId = token.NameId,
                CommonName = token.CommonName,
                Email = token.Email,
                UserName = token.UserName,
                UserId = token.UserId,
                Attributes = token.Attributes
            });
        }
    }
}
